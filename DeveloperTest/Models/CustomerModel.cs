using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        public int CustomerTypeId { get; set; }

        public string CustomerTypeName { get; set; }
    }
}
