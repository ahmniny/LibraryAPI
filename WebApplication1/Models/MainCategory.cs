﻿using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class MainCategory
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual List<Category>? Categories { get; set; }
    }

}
