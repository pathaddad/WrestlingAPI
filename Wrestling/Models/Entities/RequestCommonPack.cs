using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wrestling.Models
{
    public class RequestCommonPack
    {
        [MaxLength(2)]
        [Required(ErrorMessage = "You should provide a LanguageCode value.")]
        public string LanguageCode { get; set; }
    }
}