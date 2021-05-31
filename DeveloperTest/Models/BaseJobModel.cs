using System;
using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Models
{
    public class BaseJobModel
    {
        public string Engineer { get; set; }

        public DateTime When { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
