using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppDataWorker.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public int prodId { get; set; }
        public string name { get; set; }
        public string? code { get; set; }
        public bool is_active { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public int group_id { get; set; }
        public string? brand { get; set; }
        public string? country { get; set; }
        public string? mnn { get; set; }
        public string? release_form { get; set; }
        public string? barcode { get; set; }
        public int recept { get; set; }
        [NotMapped]
        [JsonPropertyName("analog")]
        public int[]?  analog_Json { get; set; }
        [JsonIgnore]
        public List<Analog>? analog { get; set; }

    }
    public class Analog
    {
        [JsonIgnore]
        public int Id { get; set; }
        public long ProductId { get; set; }
        public long AnalogProductId { get; set; }
        public Product? Product { get; set; }
        public Product? AnalogProd { get; set; }
    }
    
}


