using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {

        [Route("CategorySubcategoryMap")]
        [HttpGet]
        public CategorySubcategoryDataContract CategorySubcategoryMap()
        {
            Category obj = new Category();
            List<PropCategory> lst_cat = obj.GetAllCategories();
            CategorySubcategoryDataContract data = new CategorySubcategoryDataContract();
            
            data.Categories = Mapper.Map<List<PropCategory>,List<CategoryDataContract>>(lst_cat);

            return data;
        }


    }
}
