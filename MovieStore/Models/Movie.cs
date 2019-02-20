using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Director")]
        [Required]
        public int Director_Id { get; set; }
        [Required]
        public Director Director { get; set; }
        [DisplayName("ReleseDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        //public DateTime StartDate { get; set; }
        public DateTime ReleseDate {get; set;}
        public DateTime Created_Date { get; set; }
        [ForeignKey("IdentityUser")]
        public string Created_By { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public DateTime? Updated_Date { get; set; }
    }
}
