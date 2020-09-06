using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace aspnetcore_api.Models {
    public class DefinitionEntry {
        [Required]
        public int Id {get; set; }
        [Required, MaxLength(255)]
        public string PartOfSpeech {get; set;}
        [Required]
        public string Definition {get; set;}
        [Required]
        public int WordId {get; set;}
        [JsonIgnore]
        public virtual WordEntry Word {get; set;}
    }
}