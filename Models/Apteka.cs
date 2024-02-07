using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Buffers.Text;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppDataWorker.Models
{
    
    public class Apteka
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public string? id_apt { get; set; }
        public bool is_active { get; set; }
        public bool is_point_issue { get; set; }
        public bool is_shipment { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        [JsonConverter(typeof(DoubleConverter))]
        public double? longitude { get; set; }
        [JsonConverter(typeof(DoubleConverter))]
        public double? latitude { get; set; }
        public string? schedule { get; set; }
        public string? metro { get; set; }
        public bool hub { get; set; }
        public string? region { get; set; }
        [NotMapped, JsonPropertyName("operating_mode")]
        public Dictionary<string, string>[]? operating_modeJson { get; set; }
        [JsonIgnore]
        public List<Operating_mode>? operating_mode { get; set; }

    }
    public class Operating_mode
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public string? Time { get; set; }
        public long AptekaId { get; set; }
        public Apteka Apteka { get; set; }
    }
    public class DoubleConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Если это числовое значение, то пробуем десериализовать как double
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out double result))
                return result;

            // В противном случае возвращаем значение по null
            return null;
        }
        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }


}
