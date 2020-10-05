using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameRentalApp.Models
{
    public class PCGame
    {
        [Key,Required]
        public int PCID { get; set; }
        [Display(Name = "Number of available copies")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")] 
        public int AvailableCopies { get; set; }
        [Display(Name = "Game Name")]
        public string GameName { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Image")]
        public string ImgUrl { get; set; }
        [Display(Name = "Price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] 
        public int Price { get; set; }
        public string Description { get; set; }

    }
}