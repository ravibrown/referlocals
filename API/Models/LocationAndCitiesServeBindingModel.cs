using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LocationAndCitiesServeBindingModel
    {
        public long loggedInUserID { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public long locationID { get; set; }
        public List<long?> citiesServe { get; set; }
        
    }
    
    
}