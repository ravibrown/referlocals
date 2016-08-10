using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Location")]
    public class LocatoinController : ApiController
    {

        [Route("Search")]
        [HttpGet]
        public LocationDataContract Search(string keyword)
        {

            States city = new States();
            city.Take = 40;
            List<PropStates> locations = city.GetCityZipByKeyword(keyword);
            LocationDataContract lst = new LocationDataContract();
            lst.Locations = locations;
            return lst;


        }

        [Route("GetLocationIDByZip")]
        public PropStates GetLocationIDByZip(string zip)
        {

            States state = new States();
            state.Zip = zip;
            state.GetRecord();
            PropStates locationData = new PropStates();
            if (state.DataRecieved)
            {
                locationData.Id = state.Id;
                locationData.City = state.City;
                locationData.State = state.State;
                return locationData;
            }
            else
            {
                locationData.Id = 0;
                return locationData;
            }


        }

        [Route("SaveCityIServe")]
        public ResultData SaveCityIServe(LocationBindingModel model)
        {
            UserCityMapping cityMapping = new UserCityMapping();
            var cities = cityMapping.CheckCityIServe(model.UserID.GetValueOrDefault(), model.LocationID.GetValueOrDefault());
            if (cities > 0)
            {
                return new ResultData { ResultDescription = "City already added", ResultStatus = 0 };                
            }
            else
            {
                cityMapping.AddCityIServe(model.UserID.GetValueOrDefault(), model.LocationID.GetValueOrDefault());
                return new ResultData { ResultDescription = "City added successfully", ResultStatus = 1 };

            }

        }

        [Route("DeleteCityIServe")]
        [HttpPost]
        public ResultData DeleteCityIServe(LocationBindingModel model)
        {
            UserCityMapping cityMapping = new UserCityMapping();
            cityMapping.DeleteCityMappingByUserIdAndCityID(model.UserID.GetValueOrDefault(), model.LocationID.GetValueOrDefault());
            return new ResultData { ResultDescription = "City deleted successfully", ResultStatus = 1 };

        }
    }
}
