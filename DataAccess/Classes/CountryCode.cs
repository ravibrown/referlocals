using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class CountryCode:dbContext
    {
        //IMapper mapper;
        //public CountryCode()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}
        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //SNO
        private Int64 _SNO = 0;
        public Int64 SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //TeleCode
        private Int64? _TeleCode = 0;
        public Int64? TeleCode
        {
            get { return _TeleCode; }
            set { _TeleCode = value; }
        }

        //CodeTelePhone
        private string _CodeTelePhone = string.Empty;
        public string CodeTelePhone
        {
            get { return _CodeTelePhone; }
            set { _CodeTelePhone = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }
        #endregion


        #region "Add Delete Update"
        public bool Add(CountryCode obj)
        {
            try
            {
                Country checkobj = db.Countries.FirstOrDefault(a => a.id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<CountryCode, Country>();
                    Country newobj = Mapper.Map<CountryCode, Country>(obj);
                    db.Countries.Add(newobj);
                    db.SaveChanges();
                    Id = newobj.id;
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Edit(CountryCode obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<CountryCode, Country>();
                Country checkobj = db.Countries.FirstOrDefault(a => a.id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<CountryCode, Country>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(CountryCode obj)
        {
            try
            {
                Country checkobj = db.Countries.FirstOrDefault(a => a.id == obj.Id);
                if (checkobj != null)
                {
                    db.Countries.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        #endregion

        #region "Get Method"

        public List<CountryCode> GetAll()
        {
            List<CountryCode> lst = new List<CountryCode>();
                lst = (from r in db.Countries
                       where ((Id != 0 ? r.id == Id : Id == 0) )

                       select new CountryCode
                       {
                           Id = r.id,
                           Name = r.Name,
                           CodeTelePhone=r.CodeTelePhone
                       }).ToList();

                lst = lst.Select((r, index) => new CountryCode
            {
                Id = r.Id,
                Name = r.Name,
                TeleCode = Convert.ToInt64(r.CodeTelePhone),
                CodeTelePhone=r.CodeTelePhone,
                SNO = index + 1
            }).OrderBy(a => a.TeleCode).GroupBy(g => new { g.TeleCode })
                         .Select(g => g.First())
                         .ToList();

            return lst;
        }
        public bool GetRecord()
        {
            Country obj = db.Countries.FirstOrDefault(a => (Id != 0 ? a.id == Id : Id == 0));
            if (obj != null)
            {
                Id = obj.id;
                Name = obj.Name;
                TeleCode = Convert.ToInt64(obj.CodeTelePhone);
                CodeTelePhone = obj.CodeTelePhone;
                DataRecieved = true;
            }
            return DataRecieved;
        }

        #endregion

        #region "TotalRecords"
        public Int64 GetTotalRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.Countries.Count(a => (Id != 0 ? a.id == Id : Id == 0));
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        #endregion
    }
}
