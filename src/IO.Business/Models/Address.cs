using System;

namespace IO.Business.Models
{
    public class Address : Entity
    {
        public Guid ProviderId { get; set; }

        public string PublicPlace { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string ZipCode { get; set; }

        public string Neighborhoodty { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        /* Entity Framework Relations */
        public Provider Provider { get; set; }
    }
}
