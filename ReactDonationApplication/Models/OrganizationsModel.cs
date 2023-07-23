using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReactDonationApplication.Models
{
    public class OrganizationsModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Organization Name *")]
        public string OrganizationName { get; set; }
        [Required]
        [DisplayName("Organization Type *")]
        public string OrganizationType { get; set; }
        [Required]
        [DisplayName("Organization Description *")]
        public string OrganizationDescription { get; set; }

        [Required]
        [DisplayName("Organization Country *")]
        public string OrganizationCountryName { get; set; }
        [Required]
        [DisplayName("Organization Country Code *")]
        public string OrganizationCountryCode { get; set; }
        [Required]
        [DisplayName("Organization Email *")]
        public string OrganizationEmail { get; set; }
        [Required]
        [DisplayName("Organization Phone *")]
        public string OrganizationPhone { get; set; }

        [Required]
        [DisplayName("Organization Code *")]
        public string OrganizationCode { get; set; }
    }
}
