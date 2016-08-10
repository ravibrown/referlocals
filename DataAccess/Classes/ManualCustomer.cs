using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class ManualCustomers : dbContext
    {
        public long? ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string Apartment { get; set; }
        public long? LocationID { get; set; }
        public long? UserID { get; set; }
        public string CountryCode { get; set; }


        public ManualCustomerIDDataContract Save(string firstname, string lastname, string CountryCode, string phone, string email, string streetAddress, string apartment, long? locationID, long? userID)
        {
            tbl_ManualCustomers manualCustomer = new tbl_ManualCustomers();
            manualCustomer.FirstName = firstname;
            manualCustomer.CountryCode = CountryCode;
            manualCustomer.LastName = lastname;
            manualCustomer.Phone = phone;
            manualCustomer.Email = email;
            manualCustomer.StreetAddress = streetAddress;
            manualCustomer.Apartment = apartment;
            manualCustomer.LocationID = locationID;
            manualCustomer.UserID = userID;
            manualCustomer.addedon = DateTime.UtcNow;
            db.tbl_ManualCustomers.Add(manualCustomer);
            db.SaveChanges();

            return new ManualCustomerIDDataContract {ID=manualCustomer.ID, ResultDescription = "Added Successfully", ResultStatus = 1 };
        }
        public ManualCustomerWrapper GetAll(long? userID, int pageIndex, int pageSize)
        {



            var query = (from p in db.tbl_ManualCustomers
                         where p.UserID == userID
                         select new ManualCustomersDataContract
                         {
                             Apartment = p.Apartment,
                             Email = p.Email,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             ID = p.ID,
                             Location = p.tbl_State != null ? p.tbl_State.City + " " + p.tbl_State.State : "",
                             Phone = p.Phone,
                             StreetAddress = p.StreetAddress,
                             UserID = p.UserID,
                             CountryCode = p.CountryCode,
                         });
            var countCustomers = query.Count();
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countCustomers) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            ManualCustomerWrapper wrapper = new ManualCustomerWrapper();
            wrapper.CustomerCount = countCustomers;
            if (totalpages <= (pageIndex + 1))
            {
                wrapper.HideShowMore = true;
            }
            else
            {
                wrapper.HideShowMore = false;
            }

            wrapper.Customers = data;
            return wrapper;

        }

        public ResultData DeleteNote(long? ID)
        {
          var data=  db.tbl_ManualCustomerNotes.FirstOrDefault(p => p.ID == ID);
            if (data != null)
            {
                db.tbl_ManualCustomerNotes.Remove(data);
                db.SaveChanges();
            }
            return new ResultData { ResultDescription = "Added Successfully", ResultStatus = 1 };
        }
        public ResultData SaveNotes(string notes,HelperEnums.CustomerType customerType,long? customerID)
        {
            tbl_ManualCustomerNotes manualCustomerNotes =new tbl_ManualCustomerNotes();
            manualCustomerNotes.Notes= notes;
            manualCustomerNotes.CustomerType= (int)customerType;
            manualCustomerNotes.ManualCustomerID= customerID;
            manualCustomerNotes.AddedOn = DateTime.UtcNow;
            db.tbl_ManualCustomerNotes.Add(manualCustomerNotes);
            db.SaveChanges();

            return new ResultData { ResultDescription = "Added Successfully", ResultStatus = 1 };
        }
        public ManualCustomerNotesWrapper GetAllNotes(long? customerID,HelperEnums.CustomerType customerType, int pageIndex, int pageSize)
        {
            
            var query = (from p in db.tbl_ManualCustomerNotes
                         where p.ManualCustomerID== customerID&&p.CustomerType==(int)customerType
                         select new ManualCustomerNotesDataContract
                         {
                             ID= p.ID,
                             CustomerType=(HelperEnums.CustomerType)p.CustomerType,
                             ManualCustomerID=p.ManualCustomerID,
                             Notes=p.Notes
                         });
            var countCustomerNotes = query.Count();
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countCustomerNotes) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            ManualCustomerNotesWrapper wrapper = new ManualCustomerNotesWrapper();
            wrapper.NotesCount = countCustomerNotes;
            if (totalpages <= (pageIndex + 1))
            {
                wrapper.HideShowMore = true;
            }
            else
            {
                wrapper.HideShowMore = false;
            }

            wrapper.CustomerNotes = data;
            return wrapper;

        }

    }
}
