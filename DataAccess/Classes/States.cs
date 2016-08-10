using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class States : dbContext
    {
        //IMapper mapper;
        //public States()
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

        //State
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        //State
        private string _Keyword = string.Empty;
        public string Keyword
        {
            get { return _Keyword; }
            set { _Keyword = value; }
        }

        //City
        private string _City = string.Empty;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        //Zip
        private string _Zip = string.Empty;
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        //IsApproved
        private Int64 _IsApproved = (int)HelperEnums.BooleanValues.Both;
        public Int64 IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        //Take
        private Int64 _Take = 0;
        public Int64 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Index
        private Int64 _Index = 0;
        public Int64 Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        //CreatedDate
        private DateTime? _CreatedDate = DateTime.UtcNow;
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //UpdatedDate
        private DateTime? _UpdatedDate = DateTime.UtcNow;
        public DateTime? UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        //CreatedBy
        private Int64? _CreatedBy = 0;
        public Int64? CreatedBy
        {
            get { return _CreatedBy ?? 0; }
            set { _CreatedBy = value; }
        }

        //IsDeleted
        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //IsApprovedByAdmin
        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
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
        public bool Add(States obj)
        {
            try
            {
                tbl_State checkobj = db.tbl_State.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<States, tbl_State>();
                    tbl_State newobj = Mapper.Map<States, tbl_State>(obj);
                    db.tbl_State.Add(newobj);
                    db.SaveChanges();
                    Id = newobj.Id;
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Edit(States obj)
        {
            try
            {
                //  AutoMapper.Mapper.CreateMap<States, tbl_State>();
                tbl_State checkobj = db.tbl_State.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<States, tbl_State>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(States obj)
        {
            try
            {
                tbl_State checkobj = db.tbl_State.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_State.Remove(checkobj);
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

        public List<States> GetAllByState()
        {
            List<States> lst = new List<States>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.State })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                  (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.State })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.State })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                  (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new States
            {
                Id = r.Id,
                State = r.State,
                City = r.City,
                Zip = r.Zip,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1
            }).OrderBy(a => a.State).ToList();

            return lst;
        }

        public List<States> GetAllByCity()
        {
            List<States> lst = new List<States>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.City })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                  (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.City })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                  (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_State.GroupBy(g => new { g.City })
                         .Select(g => g.FirstOrDefault())
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                   (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new States
            {
                Id = r.Id,
                State = r.State,
                City = r.City,
                Zip = r.Zip,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1
            }).OrderBy(a => a.City).ToList();

            return lst;
        }

        public List<long> GetLocationIDsNearBy(long locationID)
        {
            var data = db.tbl_State.FirstOrDefault(p => p.Id == locationID);
            if (data != null)
            {
                var nearbyCities = db.tbl_State.Where(p => p.City == data.City && p.State == data.State).Select(p => p.Id).ToList();
                //var nearbyCities = (from p in db.tbl_State
                //                    where p.City == data.City && p.State == data.State
                //                    select new 
                //                    {
                //                        p.Id
                //                    }).ToList();
                return nearbyCities;
            }
            return null;
        }


        public List<PropStates> GetCityZipByKeyword(string keyword)
        {
            try
            {
                //List<PropStates> lst = new List<PropStates>();
                int pageSize = Convert.ToInt32(Take);
                keyword = keyword.Trim().ToLower();
                var lst = (from r in db.tbl_State
                           where (r.zip.Trim().ToLower().Contains(keyword) || r.City.Trim().ToLower().Contains(keyword))
                       //((keyword.Contains(r.zip.Trim().ToLower())) || (keyword.Contains(r.City.Trim().ToLower()))) &&
                       && (r.IsDeleted == false && r.IsApprovedByAdmin == true)
                           select new PropStates
                           {
                               Id = r.Id,
                               State = r.State,
                               City = r.City,
                               Zip = r.zip,

                           }).GroupBy(g => new { g.City, g.State }).Select(g => g.FirstOrDefault());

                int outKeyword = 0;
                if (int.TryParse(keyword, out outKeyword))
                {
                    //lst=lst.Where(p=>p.)
                    lst = lst.OrderBy(a => SqlFunctions.PatIndex("%" + keyword + "%", a.Zip));
                }
                else
                {
                    //  lst = lst.OrderBy(a => a.City);
                    lst = lst.OrderBy(a => SqlFunctions.PatIndex("%" + keyword + "%", a.City));
                }

                List<PropStates> data = new List<PropStates>();
                data = lst.Take(pageSize).ToList();
                //data = lst.Take(pageSize).GroupBy(g => new { g.City, g.State })
                //                   .Select(g => g.FirstOrDefault()).ToList();
                //int index=0;
                
                
                //data = (from r in data
                //        select new PropStates
                //        {
                //            Id = r.Id,
                //            State = r.State,
                //            City = r.City,
                //            Zip = r.Zip,

                //            //SNO = index + 1

                //        }).ToList();

                //data = data.Select((r, index) => new PropStates
                //      {
                //          Id = r.Id,
                //          State = r.State,
                //          City = r.City,
                //          Zip = r.Zip,

                //          SNO = index + 1
                //      });

                //if (int.TryParse(keyword, out outKeyword))
                //{
                //    lst = lst.OrderBy(a => a.Zip).GroupBy(g => new { g.City, g.State })
                //                 .Select(g => g.FirstOrDefault());
                //}
                //else {
                //    lst = lst.OrderBy(a => a.City).GroupBy(g => new { g.City, g.State })
                //                 .Select(g => g.FirstOrDefault());

                //}


                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PropStates> GetAllByCityForAjax()
        {
            List<PropStates> lst = new List<PropStates>();
            try
            {
                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_State
                           where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                       (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                       (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                       (!string.IsNullOrEmpty(Keyword) ? Keyword.Trim().ToLower().Contains(r.zip.Trim().ToLower()) || Keyword.Trim().ToLower().Contains(r.City.Trim().ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                           (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                           select new PropStates
                           {
                               Id = r.Id,
                               State = r.State,
                               City = r.City,
                               Zip = r.zip,

                           }).Take(Convert.ToInt32(Take)).ToList();
                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_State
                           where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                              (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                       (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                       (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                       (!string.IsNullOrEmpty(Keyword) ? Keyword.Trim().ToLower().Contains(r.zip.Trim().ToLower()) || Keyword.Trim().ToLower().Contains(r.City.Trim().ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                           (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                           select new PropStates
                           {
                               Id = r.Id,
                               State = r.State,
                               City = r.City,
                               Zip = r.zip,

                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_State
                           where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                              (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                       (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                       (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                       (!string.IsNullOrEmpty(Keyword) ? Keyword.Trim().ToLower().Contains(r.zip.Trim().ToLower()) || Keyword.Trim().ToLower().Contains(r.City.Trim().ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                           (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                           select new PropStates
                           {
                               Id = r.Id,
                               State = r.State,
                               City = r.City,
                               Zip = r.zip,

                           }).ToList();
                }
                lst = lst.Select((r, index) => new PropStates
                {
                    Id = r.Id,
                    State = r.State,
                    City = r.City,
                    Zip = r.Zip,

                    //SNO = index + 1
                }).OrderBy(a => a.City).ToList();//.GroupBy(g => new { g.City, g.State,g.Zip }).Select(g => g.FirstOrDefault()).ToList();

            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<States> GetAll()
        {
            List<States> lst = new List<States>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_State
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                                          (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                   (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_State
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                     (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                 (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_State
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                   (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? r.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                  (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(r.zip.ToLower()) || Keyword.ToLower().Contains(r.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new States
                       {
                           Id = r.Id,
                           State = r.State,
                           City = r.City,
                           Zip = r.zip,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new States
            {
                Id = r.Id,
                State = r.State,
                City = r.City,
                Zip = r.Zip,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1
            }).ToList();

            return lst;
        }
        public bool GetRecord()
        {
            tbl_State obj = db.tbl_State.FirstOrDefault(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (!string.IsNullOrEmpty(State) ? a.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)) &&
                   (!string.IsNullOrEmpty(City) ? a.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)) &&
                   (!string.IsNullOrEmpty(Zip) ? a.zip.ToLower() == Zip.ToLower() : string.IsNullOrEmpty(Zip)) &&
                   (!string.IsNullOrEmpty(Keyword) ? Keyword.ToLower().Contains(a.zip.ToLower()) || Keyword.ToLower().Contains(a.City.ToLower()) : string.IsNullOrEmpty(Keyword)) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both));
            if (obj != null)
            {
                Id = obj.Id;
                State = obj.State;
                City = obj.City;
                Zip = obj.zip;
                CreatedBy = obj.CreatedBy;
                CreatedDate = obj.CreatedDate;
                UpdatedDate = obj.UpdatedDate;
                IsDeleted = Convert.ToBoolean(obj.IsDeleted);
                IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
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
                Records = db.tbl_State.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both));
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        #endregion

        #region "DropDowns"
        public List<DropDowns> GetDropDownAllByState()
        {
            List<DropDowns> lst = new List<DropDowns>();
            lst = (from r in db.tbl_State
                   where ((Id != 0 ? r.Id == Id : Id == 0) &&
                   (!string.IsNullOrEmpty(State) ? r.State.ToLower() == State.ToLower() : string.IsNullOrEmpty(State)))
                   select new DropDowns
                   {
                       Id = r.Id,
                       Name = r.City
                   }).GroupBy(g => new { g.Name })
                         .Select(g => g.FirstOrDefault()).ToList();
            return lst;
        }

        public List<DropDowns> GetDropDownAllByCity()
        {
            List<DropDowns> lst = new List<DropDowns>();
            lst = (from r in db.tbl_State
                   where ((Id != 0 ? r.Id == Id : Id == 0) &&
                   (!string.IsNullOrEmpty(City) ? r.City.ToLower() == City.ToLower() : string.IsNullOrEmpty(City)))
                   select new DropDowns
                   {
                       Id = r.Id,
                       Name = r.zip
                   }).GroupBy(g => new { g.Name })
                         .Select(g => g.FirstOrDefault()).ToList();
            return lst;
        }
        #endregion
    }
}
