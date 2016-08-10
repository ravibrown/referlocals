using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class SubCategory:dbContext
    {
        //IMapper mapper;
        //public SubCategory()
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

        //CategoryId
        private Int64? _CategoryId = 0;
        public Int64? CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        //CategoryName
        private string _CategoryName = string.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
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
        public bool Add(SubCategory obj)
        {
            try
            {
                tbl_SubCategory checkobj = db.tbl_SubCategory.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<SubCategory, tbl_SubCategory>();
                    tbl_SubCategory newobj = Mapper.Map<SubCategory, tbl_SubCategory>(obj);
                    db.tbl_SubCategory.Add(newobj);
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

        public bool Edit(SubCategory obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<SubCategory, tbl_SubCategory>();
                tbl_SubCategory checkobj = db.tbl_SubCategory.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<SubCategory, tbl_SubCategory>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(Category obj)
        {
            try
            {
                tbl_SubCategory checkobj = db.tbl_SubCategory.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_SubCategory.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }
        public bool DisableEnableSubcategoryByCategoryID(long categoryID, bool status)
        {
            var subCatData = db.tbl_SubCategory.Where(p => p.CategoryId == categoryID);
            foreach (var item in subCatData)
            {
                item.IsApprovedByAdmin = status;
            }
            db.SaveChanges();
            return true;
        }


        #endregion

        #region "Get Method"

        public string GetSubcategoryNamesStringByIDs(List<long> subcategoryIDs)
        {
            var data = (from p in db.tbl_SubCategory
                        where subcategoryIDs.Contains(p.Id)
                        select new
                        {
                            p.Name
                        }).ToList();

            string subcategoryNames =  string.Empty;
            foreach (var item in data)
            {
                subcategoryNames += item.Name + ",";
            }
           return subcategoryNames=subcategoryNames.Remove(subcategoryNames.Length - 1);
        }
        public List<SubCategory> GetAll()
        {
            List<SubCategory> lst = new List<SubCategory>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (CategoryId!=0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new SubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName=r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                        (CategoryId != 0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new SubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName = r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                        (CategoryId != 0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new SubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName = r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new SubCategory
            {
                Id = r.Id,
                CategoryId = r.CategoryId,
                CategoryName = r.CategoryName,
                Name = r.Name,
                Description = r.Description,
                Image = r.Image,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1
            }).ToList();

            return lst;
        }

        public List<PropSubCategory> GetAllForAjax()
        {
            List<PropSubCategory> lst = new List<PropSubCategory>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (CategoryId != 0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropSubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName = r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                        (CategoryId != 0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropSubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName = r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a=>a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_SubCategory
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                        (CategoryId != 0 ? r.CategoryId == CategoryId : CategoryId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropSubCategory
                       {
                           Id = r.Id,
                           CategoryId = r.CategoryId,
                           CategoryName = r.tbl_Category.Name,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new PropSubCategory
            {
                Id = r.Id,
                CategoryId = r.CategoryId,
                CategoryName = r.CategoryName,
                Name = r.Name,
                Description = r.Description,
                Image = r.Image,
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
            tbl_SubCategory obj = db.tbl_SubCategory.FirstOrDefault(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (CategoryId != 0 ? a.CategoryId == CategoryId : CategoryId == 0) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                   (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            if (obj != null)
            {
                Id = obj.Id;
                CategoryId = obj.CategoryId;
                CategoryName = obj.tbl_Category.Name;
                Name = obj.Name;
                Description = obj.Description;
                Image = obj.Image;
                CreatedBy = obj.CreatedBy;
                CreatedDate = obj.CreatedDate;
                UpdatedDate = obj.UpdatedDate;
                IsDeleted = Convert.ToBoolean(obj.IsDeleted);
                IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
                DataRecieved = true;
            }
            return DataRecieved;
        }

        public long GetSubCategoryIDByName(string name)
        {
            var subCatData= db.tbl_SubCategory.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            if (subCatData != null)
                return subCatData.Id;
            return 0;
        }
        #endregion

        #region "TotalRecords"
        public Int64 GetTotalRecords()
        {
            Int64 Records = 0;
            Records = db.tbl_SubCategory.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                   (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            return Records;
        }
        #endregion

        #region API Methods
        public List<SubCategoryBasicDataContract> GetAllSubcategories()
        {
           var data= db.tbl_SubCategory.Where(p=>p.IsDeleted == false && p.IsApprovedByAdmin == true)
                .Select(p=>new SubCategoryBasicDataContract {Id=p.Id,Name=p.Name }).ToList();
            return data;
        }
        #endregion

    }
}
