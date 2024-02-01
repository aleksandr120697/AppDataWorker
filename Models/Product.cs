using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppDataWorker.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool is_active { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int group_id { get; set; }
        public string brand { get; set; }
        public string country { get; set; }
        public string mnn { get; set; }
        public string release_form { get; set; }
        public string barcode { get; set; }
        public int recept { get; set; }
        [NotMapped]
        [JsonPropertyName("analog")]
        public Dictionary<int, int>[] analog_Json { get; set; }
        [JsonIgnore]
        public List<Analog> analog { get; set; } = new();

    }
    public class Analog
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int productId { get; set; }
    }
}


