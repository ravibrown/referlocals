using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.Classes;
using DataAccess;
using System.Web;
using API.Models;
using System.Collections.Generic;

//[assembly: PreApplicationStartMethod(typeof(API.AutoMapperWebConfiguration), "Start")]
namespace API
{
    public class AutoMapperWebConfiguration
    {


        public static void Start()
        {
            ConfigureMapping();

        }

        public static void ConfigureMapping()
        {
            

            Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<Category, CategorySubcategoryDataContract>();
                cfg.CreateMap<SubCategory, SubCategoryDataContract>();

                cfg.CreateMap<ProfessionalDataContract, DataAccess.Classes.User>();
                cfg.CreateMap<DataAccess.Classes.User, ProfessionalDataContract>();

                cfg.CreateMap<ProfessionalDataContract, tbl_User>();
                cfg.CreateMap<tbl_User, ProfessionalDataContract>();

                cfg.CreateMap<DataAccess.Classes.User, tbl_User>();
                cfg.CreateMap<tbl_User, DataAccess.Classes.User>();


                cfg.CreateMap<ProfessionalProfileBindingModel, User>();

                cfg.CreateMap<User, ProfessionalProfileBindingModel>();

                cfg.CreateMap<PropCategory, CategoryDataContract>();
                cfg.CreateMap<CategoryDataContract, PropCategory>();

                cfg.CreateMap<PropSubCategory, SubCategoryDataContract>();
                cfg.CreateMap<SubCategoryDataContract, PropSubCategory>();

                cfg.CreateMap<ProfileBindingModel, User>();
                cfg.CreateMap<User, ProfileBindingModel>();

                cfg.CreateMap<UserDataContract, User>();
                cfg.CreateMap<User, UserDataContract>();

                
                cfg.CreateMap<UserDataContract, tbl_User>();
                cfg.CreateMap<tbl_User, UserDataContract>();

                cfg.CreateMap<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>();
                cfg.CreateMap<tbl_Job_SubCategory_Mapping, JobSubCategoryMapping>();

            });


            //var mapper = config.CreateMapper();
            //return mapper;


        }

        // ... etc
    }
}