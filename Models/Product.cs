using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string FamilyProduct { get; set; }
 
        public (bool, string) IsValid()
        {
            List<string> messages = new();

            if (string.IsNullOrEmpty(Name))
                messages.Add("Name cannot be empty.");

            if (Price < 0m)
                messages.Add("Price cannot be negative.");

            return (messages.Count == 0, string.Join("\r\n", messages));
        }
    }
}
