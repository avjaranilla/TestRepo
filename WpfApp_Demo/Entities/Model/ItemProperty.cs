using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Model
{
    public class ItemProperty
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public int ListID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [Required]
        [MaxLength(400)]
        public string ItemDesc { get; set; }


        [Required]
        public int ItemStatus { get; set; }
    }
}
