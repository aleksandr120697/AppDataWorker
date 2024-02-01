using AppDataWorker.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AppDataWorker.Data
{
    public class DataWorker
    {
        #region Аптеки

        public static void UpdateApteks()
        {
            //Десериализуем json
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            string folderPath = Path.GetDirectoryName(assembly.Location);
            string filePath = Path.Combine(folderPath, "TransportData\\EvryDayRenew\\apteki.json");
            var json = File.ReadAllText(filePath);

            //JsonSerializerOptions options = new JsonSerializerOptions()
            //{
            //    UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode
            //};

            Apteka[]? resultJson = JsonSerializer.Deserialize<Apteka[]>(json);
            //Сохраняем в БД
            List<Operating_mode>? operating_Mode;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var res in resultJson)
                {
                    Apteka parseModel;
                    //Конвертируем координаты
                    //var con1 = Convert.ToDouble(Convert.ToString((res.latitude_json as JsonNode).GetValue<double>().ToString()));
                    //double convert_longitudes;
                    //if(!double.TryParse(Convert.ToString(Convert.ToDouble((res.latitude_json as JsonNode).GetValue<object>().ToString())), out convert_longitudes))
                    //    convert_longitudes = 0;
                    //double convert_latitude;
                    //if (!double.TryParse(Convert.ToString((res.longitude_json as JsonNode).GetValue<object>().ToString()), out convert_latitude))
                    //    convert_latitude = 0;

                    //проверяем operating_modeJson на null 
                    if (res.operating_modeJson != null)
                    {
                        operating_Mode = res.operating_modeJson
                                                            .SelectMany(dict => dict.Select(kv => new Operating_mode { Day = kv.Key, Time = kv.Value }))
                                                            .ToList(); //Заполняем данными operating_Mode из массива словарей operating_modeJson.

                        parseModel = new Apteka()
                        {
                            id_apt = res.id_apt,
                            is_active = res.is_active,
                            is_point_issue = res.is_point_issue,
                            is_shipment = res.is_shipment,
                            name = res.name,
                            address = res.address,
                            phone = res.phone,
                            longitude = res.longitude,
                            latitude = res.latitude,
                            schedule = res.schedule,
                            metro = res.metro,
                            hub = res.hub,
                            region = res.region,
                            operating_mode = operating_Mode // Указываем связь  Apteks с operating_mode
                        };
                        Apteka oldApteka = db.Apteks.Where(apteka => apteka.id_apt == parseModel.id_apt).FirstOrDefault();
                        if (!oldApteka.Equals(parseModel))
                        {
                            db.Add(parseModel); //Создаем таблицу Apteka 
                            db.operating_Modes.AddRange(operating_Mode);
                            Console.WriteLine("Добавлена аптека " + res.name + ".");
                        }
                    }
                    else
                    {
                        parseModel = new Apteka()
                        {
                            id_apt = res.id_apt,
                            is_active = res.is_active,
                            is_point_issue = res.is_point_issue,
                            is_shipment = res.is_shipment,
                            name = res.name,
                            address = res.address,
                            phone = res.phone,
                            longitude = res.longitude,
                            latitude = res.latitude,
                            schedule = res.schedule,
                            metro = res.metro,
                            hub = res.hub,
                            region = res.region,// Указываем связь  Apteks с operating_mode
                        };
                        Apteka oldApteka = db.Apteks.Where(apteka => apteka.id_apt == parseModel.id_apt).FirstOrDefault();
                        if(!oldApteka.Equals(parseModel))
                        {
                            db.Add(parseModel); //Создаем таблицу Apteka 
                            Console.WriteLine("Добавлена аптека " + res.name + " без режима работы");
                        }
                        
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine("Аптеки обновлены!");
        }


        #endregion

        #region Продукты

        public static void UpdateProducts()
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
                foreach (var res in resultJson)
                {
                    List<Analog> analogs = res.analog_Json
                                                        .SelectMany(dict => dict.Select(kv => new Analog { Id = kv.Key, productId = kv.Value }))
                                                        .ToList(); //Заполняем данными operating_Mode из массива словарей analog_Json.


                    //var pruducts = JsonConvert.DeserializeObject<List<Product>>(json);


                    //Сохраняем в БД
                    //using (ApplicationDbContext db = new ApplicationDbContext())
                    //{
                    //    bool checkIsExist = db.Products.Any(el => el.id == id);
                    //    try
                    //    {
                    //        foreach (var apteka in apteki)
                    //        {
                    //            if (!checkIsExist)
                    //            {

                    //            }
                    //            else
                    //            {

                    //            }

                    //            db.Apteks.Add(apteka);
                    //            db.SaveChanges();
                    //        }
                    //    }
                    //    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    //}
                    Console.WriteLine("Аптеки обновлены!");
                }


                #endregion
            }
        }
    }
}
