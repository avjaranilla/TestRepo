using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Model
{
    public class ListProperty
    {
        [Key]
        public int ListID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ListName { get; set; }

        [Required]
        [MaxLength(400)]
        public string ListDesc { get; set; }

    }
}
