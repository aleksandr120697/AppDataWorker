using AppDataWorker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AppDataWorker.Data
{
    public class DataWorker
    {
        #region Аптеки
        public void RenewApteks()
        {
            using ApplicationDbContext db = new ApplicationDbContext(); //Объявляем контекст
            db.Database.EnsureCreated(); //Создаём бд. Если создана уже, она не пересоздастcя
            string json = File.ReadAllText("apteki.json");
            var resultJson = JsonSerializer.Deserialize<Apteka[]>(json);
            foreach (var dataJson in resultJson)
            {
                var aptekaDb = db.Apteks.Include(x => x.operating_mode).FirstOrDefault(x => x.id_apt == dataJson.id_apt); //Возвращаем аптеку из бд если она там есть. Include - https://metanit.com/sharp/entityframeworkcore/3.3.php
                if (aptekaDb == null)
                {
                    CreateApteka(dataJson, aptekaDb, db); // Если apteka равна null создаём новую аптеку
                    Console.WriteLine("Добавлено --- "+ dataJson.name);
                }
                else
                {
                    EditApteka(dataJson, aptekaDb, db);
                    Console.WriteLine("Обновлено --- " + dataJson.name);
                }
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Добавляем новую аптеку в бд
        /// </summary>
        /// <param name="res"></param>
        /// <param name="aptekaDb"></param>
        /// <param name="db"></param>
        private void CreateApteka(Apteka? dataJson, Apteka? aptekaDb, ApplicationDbContext db)
        {
            List<Operating_mode> operating_Mode = dataJson.operating_modeJson
                                                   .SelectMany(dict => dict.Select(kv => new Operating_mode { Day = kv.Key, Time = kv.Value }))
                                                   .ToList(); //Заполняем данными operating_Mode из массива словарей operating_modeJson.
            aptekaDb = new Apteka()
            {
                id_apt = dataJson.id_apt,
                is_active = dataJson.is_active,
                is_point_issue = dataJson.is_point_issue,
                is_shipment = dataJson.is_shipment,
                name = dataJson.name,
                address = dataJson.address,
                phone = dataJson.phone,
                longitude = dataJson.longitude,
                latitude = dataJson.latitude,
                schedule = dataJson.schedule,
                metro = dataJson.metro,
                hub = dataJson.hub,
                region = dataJson.region,
                operating_mode = operating_Mode // Указываем связь  ParseModel с operating_mode
            };
            db.Add(aptekaDb); //Создаем таблицу ParseModel 
            db.operating_Modes.AddRange(operating_Mode); //Добавляем operating_mode. Начинает отслеживание заданных сущностей и любых других доступных сущностей, которые еще не отслеживаются, в Added состоянии таким образом, что они будут вставлены в базу данных при SaveChanges() вызове .
        }

        /// <summary>
        /// Если аптека существует, редактируем её.
        /// </summary>
        /// <param name="dataJson"></param>
        /// <param name="aptekaDb"></param>
        /// <param name="db"></param>
        private void EditApteka(Apteka? dataJson, Apteka? aptekaDb, ApplicationDbContext db)
        {
            aptekaDb.is_active = dataJson.is_active;
            aptekaDb.is_point_issue = dataJson.is_point_issue;
            aptekaDb.is_shipment = dataJson.is_shipment;
            aptekaDb.name = dataJson.name;
            aptekaDb.address = dataJson.address;
            aptekaDb.phone = dataJson.phone;
            aptekaDb.longitude = dataJson.longitude;
            aptekaDb.latitude = dataJson.latitude;
            aptekaDb.schedule = dataJson.schedule;
            aptekaDb.metro = dataJson.metro;
            aptekaDb.hub = dataJson.hub;
            aptekaDb.region = dataJson.region;

            foreach (Dictionary<string, string> dictArray in dataJson.operating_modeJson) // Перебираем массив словарей
            {
                foreach (var dict in dictArray) //Перебираем словари
                {
                    var operating_mode = aptekaDb.operating_mode?.FirstOrDefault(x => x.Day == dict.Key);
                    if (operating_mode != null)
                    {
                        operating_mode.Day = dict.Key;
                        operating_mode.Time = dict.Value;
                    }
                }
            }
        }

        #endregion

        #region Продукты

        public static Product GetProductById(int id)
        {
            Product product = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                product = db.Products.FirstOrDefault(x => x.Id == id);
            }
            return product;
        }

        public void RenewProducts()
        {
            //Десериализуем json
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            string folderPath = Path.GetDirectoryName(assembly.Location);
            string filePath = Path.Combine(folderPath, "TransportData\\EvryDayRenew\\products.json");
            var json = File.ReadAllText(filePath);

            var resultJson = JsonSerializer.Deserialize<Product[]>(json);

            //Сохраняем в БД
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var dataJson in resultJson)
                {
                    var productDb = db.Products.Include(x => x.analog).FirstOrDefault(x => x.prodId == dataJson.prodId); //Возвращаем аптеку из бд если она там есть. Include - https://metanit.com/sharp/entityframeworkcore/3.3.php
                    if (productDb == null)
                    {
                        CreateProduct(dataJson, productDb, db);
                        Console.WriteLine("Добавлено --- " + dataJson.name);
                    }
                    else
                    {
                        EditProduct(dataJson, productDb, db);
                        Console.WriteLine("Обновлено --- " + dataJson.name);
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Создание товара
        /// </summary>
        /// <param name="dataJson"></param>
        /// <param name="productDb"></param>
        /// <param name="db"></param>
        private void CreateProduct(Product? dataJson, Product? productDb, ApplicationDbContext db)
        {
            List<Analog>? analogs = new List<Analog>();
            if (dataJson.analog_Json != null)
            {
                for (int i = 0; i < dataJson.analog_Json.Length; i++)
                {
                    Analog analog = new Analog()
                    {
                        Product = GetProductById(dataJson.analog_Json[i]),
                        AnalogProd = GetProductById(dataJson.prodId)
                    };
                    analogs.Add(analog);
                }
            }

            Product product = new Product
            {
                prodId = dataJson.prodId,
                name = dataJson.name,
                code = dataJson.code,
                is_active = dataJson.is_active,
                description = dataJson.description,
                group_id = dataJson.group_id,
                image = dataJson.image,
                country = dataJson.country,
                brand = dataJson.brand,
                mnn = dataJson.mnn,
                release_form = dataJson.release_form,
                barcode = dataJson.barcode,
                recept = dataJson.recept,
                analog = analogs
            };
            db.Products.Add(product);
            db.Analogs.AddRange(analogs);
            db.SaveChanges();
        }
        /// <summary>
        /// Редактирует товар
        /// </summary>
        /// <param Товар из JSON="dataJson"></param>
        /// <param Товар из БД="productDb"></param>
        /// <param Соединение="db"></param>
        private void EditProduct(Product? dataJson, Product? productDb, ApplicationDbContext db)
        {
            productDb.prodId = dataJson.prodId;
            productDb.name = dataJson.name;
            productDb.code = dataJson.code;
            productDb.is_active = dataJson.is_active;
            productDb.description = dataJson.description;
            productDb.group_id = dataJson.group_id;
            productDb.image = dataJson.image;
            productDb.country = dataJson.country;
            productDb.brand = dataJson.brand;
            productDb.mnn = dataJson.mnn;
            productDb.release_form = dataJson.release_form;
            productDb.barcode = dataJson.barcode;
            productDb.recept = dataJson.recept;
            


            if(dataJson.analog_Json != null)
            {
                foreach (int item in dataJson.analog_Json) // Перебираем массив словарей
                {
                    var analog = productDb.analog?.FirstOrDefault(x => x.ProductId == dataJson.prodId && x.AnalogProductId == item);
                    if (analog != null)
                    {
                        analog.AnalogProd = GetProductById(item);
                    }
                }
            }
        }
                
        #endregion
            
    }
}
