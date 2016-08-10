using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContractClasses
{
    public class CategorySubcategoryDataContract
    {
        public List<CategoryDataContract> Categories { get; set; }
    }
    public class SubCategoryDataContract {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class SubCategoryBasicDataContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
     
    }
    public class CategoryDataContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubCategoryDataContract> lst_subcategory { get; set; }
    }
}
