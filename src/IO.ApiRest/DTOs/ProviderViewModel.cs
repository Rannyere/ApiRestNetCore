using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IO.ApiRest.DTOs
{
    public class ProviderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} - Required field")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} e {1} characteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} - Required field")]
        [StringLength(14, ErrorMessage = "The field {0} must be between {2} e {1} characteres", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Type")]
        public int TypeProvider { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Activ?")]
        public bool Activ { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
