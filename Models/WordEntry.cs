using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace aspnetcore_api.Models {
    public class WordEntry {
        [Required]
        public int Id {get; set; }
        [Required, MaxLength(255)]
        public string Word {get; set;}
        public virtual List<DefinitionEntry> Definitions {get; set;}
    }
}