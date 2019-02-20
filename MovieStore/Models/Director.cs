﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }        
        public DateTime Created_Date { get; set; }
        [ForeignKey("IdentityUser")]
        public string Created_By { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public DateTime? Updated_Date { get; set; }
        public List<Movie> Movie { get; set; }
    }
}
