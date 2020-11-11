using System.Collections.Generic;

namespace Migrations.DataAccess.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}