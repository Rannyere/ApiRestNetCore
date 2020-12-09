using System;

namespace IO.Business.Models
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Value { get; set; }

        public DateTime DateRegister { get; set; }

        public bool Activ { get; set; }

        /* Entity Framework Relations */
        public Provider Provider { get; set; }
    }
}
