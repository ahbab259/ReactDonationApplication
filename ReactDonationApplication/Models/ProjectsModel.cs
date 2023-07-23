using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReactDonationApplication.Models
{
    public class ProjectsModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("PROJECT NAME *")]
        public string PROJECT_NAME { get; set; }
        [Required]
        [DisplayName("PROJECT DESCRIPTION *")]
        public string PROJECT_DESCRIPTION { get; set; }
        [Required]
        [DisplayName("PROJECT CODE *")]
        public string PROJECT_CODE { get; set; }
        [Required]
        [DisplayName("PROJECT ORGANIZATION CODE *")]
        public string PROJECT_ORGANIZATION_CODE { get; set; }
        [Required]
        [DisplayName("PROJECT CURRENT FUND")]
        public decimal PROJECT_FUND { get; set; }
        [Required]
        [DisplayName("PROJECT TARGET FUND")]
        public decimal PROJECT_TARGET_FUND { get; set; }
    }
}
