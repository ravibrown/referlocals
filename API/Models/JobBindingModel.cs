using DataAccess.HelperClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class JobBindingModel
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public long Location { get; set; }
        public string Address { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public long UserId { get; set; }

        public long JobID { get; set; }

        public List<long> SubCategoryIDs { get; set; }
    }

    public class JobSearchBindingModel
    {

        [Required]
        public long loggedInUserID { get; set; }
        [Required]
        public long LocationId { get; set; }
        [Required]
        public List<long?> SubCategoryIDs { get; set; }
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
    }

    public class JobStatusChangeBindingModel
    {
        [Required]
        public HelperEnums.JobStatus JobStatus { get; set; }
        [Required]
        public long? JobID{ get; set; }
        
    }
}