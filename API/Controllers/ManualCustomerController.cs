using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/ManualCustomer")]
    public class ManualCustomerController : ApiController
    {

        [Route("SaveNotes")]
        public ResultData Save(ManualCustomerNoteDataBindingModel model)
        {
            ManualCustomers manualCustomer = new ManualCustomers();
            return manualCustomer.SaveNotes(model.Notes,model.CustomerType,model.CustomerID);
        }
        [Route("GetAllNotes")]
        public ManualCustomerNotesWrapper GetAllNotes(long? customerID,HelperEnums.CustomerType customerType, int pageIndex, int pageSize)
        {
            ManualCustomers manualCustomer = new ManualCustomers();
            return manualCustomer.GetAllNotes(customerID,customerType, pageIndex, pageSize);
        }
        [Route("DeleteNote")]
        public ResultData DeleteNote(long? ID)
        {
            ManualCustomers manualCustomer = new ManualCustomers();
            return manualCustomer.DeleteNote(ID);
        }


        [Route("Save")]
        public ResultData Save(ManualCustomerBindingModel model)
        {
            ManualCustomers manualCustomer= new ManualCustomers();
           return manualCustomer.Save(model.FirstName,model.LastName,model.CountryCode,model.Phone,model.Email,model.StreetAddress,model.Apartment,model.LocationID,model.UserID);
        }
        [Route("GetAll")]
        public ManualCustomerWrapper GetAll(long? userID, int pageIndex, int pageSize)
        {
            ManualCustomers manualCustomer = new ManualCustomers();
            return manualCustomer.GetAll(userID, pageIndex, pageSize);
        }



    }
}
