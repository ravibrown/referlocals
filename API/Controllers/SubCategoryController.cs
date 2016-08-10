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
    [RoutePrefix("api/subcategory")]
    public class SubCategoryController : ApiController
    {   

        [Route("All")]
        [HttpGet]
        public List<SubCategoryBasicDataContract> CategorySubcategoryMap()
        {
            SubCategory obj = new SubCategory();
            List<SubCategoryBasicDataContract> subcats = obj.GetAllSubcategories();
           
            return subcats;
        }


    }
}
