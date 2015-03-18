﻿using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}