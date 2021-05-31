using System.Collections.Generic;

namespace DeveloperTest.Database.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public virtual CustomerType Type { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
