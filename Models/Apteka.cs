﻿using Microsoft.EntityFrameworkCore;
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
        public string id_apt { get; set; }
        public bool is_active { get; set; }
        public bool is_point_issue { get; set; }
        public bool is_shipment { get; set; }
        public string name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        [NotMapped,JsonPropertyName("longitude")]
        public object? longitude_json { get; set; }
        [JsonIgnore]
        public double? longitude { get; set; }
        [NotMapped,JsonPropertyName("latitude")]
        public object? latitude_json { get; set; }
        [JsonIgnore]
        public double? latitude { get; set; }
        public string? schedule { get; set; }
        public string? metro { get; set; }
        public bool hub { get; set; }
        public string? region { get; set; }
        [NotMapped]
        [JsonPropertyName("operating_mode")]
        public Dictionary<string, string>[]? operating_modeJson { get; set; }
        [JsonIgnore]
        public List<Operating_mode>? operating_mode { get; set; } = new();

    }
    public class Operating_mode
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public string? Time { get; set; }
    }
    

}
