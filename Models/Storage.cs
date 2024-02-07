using System.ComponentModel.DataAnnotations.Schema;

namespace AppDataWorker.Models
{
    
        public class Class1
        {
            public string id { get; set; }
            public string vendor_code { get; set; }
            public string name { get; set; }
            public Price[] prices { get; set; }
            public int count { get; set; }
            public Storage[] storage { get; set; }
        }

        public class Price
        {
            public string type { get; set; }
            public string price { get; set; }
        }

        public class Storage
        {
            [NotMapped]
            public string id { get; set; }
            public string idApt { get; set; }
            public int count { get; set; }
        }

}
