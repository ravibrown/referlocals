using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Category : dbContext
    {
        //IMapper mapper;
        //public Category()
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

        //lst_subcategory
        private List<PropSubCategory> _lst_subcategory = null;
        public List<PropSubCategory> lst_subcategory
        {
            get { return _lst_subcategory; }
            set { _lst_subcategory = value; }
        }

        #endregion

        #region "Add Delete Update"
        public bool Add(Category obj)
        {
            try
            {
                tbl_Category checkobj = db.tbl_Category.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<Category, tbl_Category>();
                    tbl_Category newobj = Mapper.Map<Category, tbl_Category>(obj);
                    db.tbl_Category.Add(newobj);
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

        public bool Edit(Category obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<Category, tbl_Category>();
                tbl_Category checkobj = db.tbl_Category.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<Category, tbl_Category>(obj, checkobj);
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
                tbl_Category checkobj = db.tbl_Category.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_Category.Remove(checkobj);
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

        public List<Category> GetAll()
        {
            List<Category> lst = new List<Category>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Category
                       {
                           Id = r.Id,
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
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Category
                       {
                           Id = r.Id,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Category
                       {
                           Id = r.Id,
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
            lst = lst.Select((r, index) => new Category
            {
                Id = r.Id,
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

        public List<PropCategory> GetAllForAjax()
        {
            List<PropCategory> lst = new List<PropCategory>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropCategory
                       {
                           Id = r.Id,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           SubCategoryCount=r.tbl_SubCategory.Count(p=>p.IsDeleted==false&&p.IsApprovedByAdmin==true),
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropCategory
                       {
                           Id = r.Id,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           SubCategoryCount = r.tbl_SubCategory.Count(p => p.IsDeleted == false && p.IsApprovedByAdmin == true),
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_Category
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new PropCategory
                       {
                           Id = r.Id,
                           Name = r.Name,
                           Description = r.Description,
                           Image = r.Image,
                           SubCategoryCount = r.tbl_SubCategory.Count(p => p.IsDeleted == false && p.IsApprovedByAdmin == true),
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new PropCategory
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Image = r.Image,
                SubCategoryCount = r.SubCategoryCount,
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
            tbl_Category obj = db.tbl_Category.FirstOrDefault(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                   (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            if (obj != null)
            {
                Id = obj.Id;
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

        #endregion

        #region "TotalRecords"
        public Int64 GetTotalRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.tbl_Category.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            }
            catch(Exception ex)
            {

            }
            return Records;
        }
        #endregion

        #region Api Methods
        public List<PropCategory> GetAllCategories()
        {
            var data = db.tbl_Category.Where(r => r.IsApprovedByAdmin == true && r.IsDeleted == false).Select(r => new PropCategory
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Image = r.Image,
                lst_subcategory = db.tbl_SubCategory.Where(p => p.CategoryId == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true)
                .Select(p => new PropSubCategory
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    //CategoryName = p.tbl_Category.Name,
                    Name = p.Name,
                    Description = p.Description,
                    Image = Common.SubCategoryImagesPath+p.Image,

                }).ToList()
            });
            return data.ToList(); 
        }
        #endregion
    }
}
