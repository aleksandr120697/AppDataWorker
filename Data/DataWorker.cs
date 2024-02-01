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

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode
            };

            Apteka[]? resultJson = JsonSerializer.Deserialize<Apteka[]>(json,options);
            //Сохраняем в БД
            List<Operating_mode>? operating_Mode;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var res in resultJson)
                {
                    Apteka parseModel;
                    //Конвертируем координаты
                    double convert_longitudes;
                    if(!double.TryParse(Convert.ToString(res.latitude_json as JsonNode), out convert_longitudes))
                        convert_longitudes = 0;
                    double convert_latitude;
                    if (!double.TryParse(Convert.ToString(res.latitude_json as JsonNode), out convert_latitude))
                        convert_latitude = 0;

                    //проверяем operating_modeJson на null 
                    if (res.operating_modeJson != null)
                    {
                        operating_Mode = res.operating_modeJson
                                                            .SelectMany(dict => dict.Select(kv => new Operating_mode { Day = kv.Key, Time = kv.Value }))
                                                            .ToList(); //Заполняем данными operating_Mode из массива словарей operating_modeJson.

                        parseModel = new Apteka()
                        {
                            is_active = res.is_active,
                            is_point_issue = res.is_point_issue,
                            is_shipment = res.is_shipment,
                            name = res.name,
                            address = res.address,
                            phone = res.phone,
                            longitude = convert_longitudes,
                            latitude = convert_latitude,
                            schedule = res.schedule,
                            metro = res.metro,
                            hub = res.hub,
                            region = res.region,
                            operating_mode = operating_Mode // Указываем связь  Apteks с operating_mode
                        };
                        db.Add(parseModel); //Создаем таблицу Apteka 
                        db.operating_Modes.AddRange(operating_Mode);
                    }
                    else
                    {
                        parseModel = new Apteka()
                        {
                            is_active = res.is_active,
                            is_point_issue = res.is_point_issue,
                            is_shipment = res.is_shipment,
                            name = res.name,
                            address = res.address,
                            phone = res.phone,
                            longitude = convert_longitudes,
                            latitude = convert_latitude,
                            schedule = res.schedule,
                            metro = res.metro,
                            hub = res.hub,
                            region = res.region,// Указываем связь  Apteks с operating_mode
                        };
                        db.Add(parseModel); //Создаем таблицу Apteka 
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
