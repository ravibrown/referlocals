using AutoMapper;
using DataAccess;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(DataAccess.AutoMapperWebConfiguration), "Configure")]
namespace DataAccess
{

    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            ConfigureMapping();

        }

        public static void ConfigureMapping()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<QuoteDataContract, tbl_Quotes>();
                cfg.CreateMap<tbl_Quotes, QuoteDataContract>();



                cfg.CreateMap<Category, CategorySubcategoryDataContract>();
                cfg.CreateMap<SubCategory, SubCategoryDataContract>();

                cfg.CreateMap<ProfessionalDataContract, DataAccess.Classes.User>();
                cfg.CreateMap<DataAccess.Classes.User, ProfessionalDataContract>();

                cfg.CreateMap<ProfessionalDataContract, tbl_User>();
                cfg.CreateMap<tbl_User, ProfessionalDataContract>();

                cfg.CreateMap<DataAccess.Classes.User, tbl_User>();
                cfg.CreateMap<tbl_User, DataAccess.Classes.User>();

                cfg.CreateMap<UserDataContract, tbl_User>();
                cfg.CreateMap<tbl_User, UserDataContract>();


                cfg.CreateMap<Category, tbl_Category>();
                cfg.CreateMap<tbl_Category, Category>();

                cfg.CreateMap<SubCategory, tbl_SubCategory>();
                cfg.CreateMap<tbl_SubCategory, SubCategory>();

                cfg.CreateMap<Country, CountryCode>();
                cfg.CreateMap<CountryCode, Country>();

                cfg.CreateMap<HomeCards, tbl_HomeCards>();
                cfg.CreateMap<tbl_HomeCards, HomeCards>();

                cfg.CreateMap<Jobs, tbl_Jobs>();
                cfg.CreateMap<tbl_Jobs, Jobs>();

                cfg.CreateMap<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>();
                cfg.CreateMap<tbl_Job_SubCategory_Mapping, JobSubCategoryMapping>();
                

                cfg.CreateMap<Referal, tbl_Referal>();
                cfg.CreateMap<tbl_Referal, Referal>();

                cfg.CreateMap<States, tbl_State>();
                cfg.CreateMap<tbl_State, States>();

                

                cfg.CreateMap<Testimonial, tbl_Testimonial>();
                cfg.CreateMap<tbl_Testimonial, Testimonial>();

                cfg.CreateMap<UserCityMapping, tbl_User_City_Mapping>();
                cfg.CreateMap<tbl_User_City_Mapping, UserCityMapping>();

                cfg.CreateMap<UserImages, tbl_UserImages>();
                cfg.CreateMap<tbl_UserImages, UserImages>();

                cfg.CreateMap<UserSubCategoryMapping, tbl_User_SubCategory_Mapping>();
                cfg.CreateMap<tbl_User_SubCategory_Mapping, UserSubCategoryMapping>();



            });

            //var mapper = config.CreateMapper();
            //return mapper;

        }


    }

}
