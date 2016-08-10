using AutoMapper;
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


                 cfg.CreateMap<Category, CategorySubcategoryDataContract>().ForMember(d => d.Categories, c => c.Ignore()).ConvertUsing(s => new CategorySubcategoryDataContract());
                 cfg.CreateMap<SubCategory, SubCategoryDataContract>().ForMember(d => d.CategoryId, c => c.Ignore()).ConvertUsing(s => new SubCategoryDataContract());

                 cfg.CreateMap<DataAccess.Classes.User, tbl_User>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new tbl_User());
                 cfg.CreateMap<tbl_User, DataAccess.Classes.User>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new DataAccess.Classes.User());
                 cfg.CreateMap<UserDataContract, tbl_User>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new tbl_User());
                 cfg.CreateMap<tbl_User, UserDataContract>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new UserDataContract());

                 cfg.CreateMap<ProfessionalDataContract, DataAccess.Classes.User>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new DataAccess.Classes.User());
                 cfg.CreateMap<DataAccess.Classes.User, ProfessionalDataContract>().ForMember(d => d.FirstName, c => c.Ignore()).ConvertUsing(s => new ProfessionalDataContract());

                 //cfg.CreateMap<ProfessionalDataContract, tbl_User>();
                 //cfg.CreateMap<tbl_User, ProfessionalDataContract>();

                 cfg.CreateMap<ProfessionalDataContract, tbl_User>().ForMember(d => d.RoleId, c => c.Ignore()).ConvertUsing(s => new tbl_User());
                 cfg.CreateMap<tbl_User, ProfessionalDataContract>().ForMember(d => d.RoleId, c => c.Ignore()).ConvertUsing(s => new ProfessionalDataContract());


                 cfg.CreateMap<Category, tbl_Category>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new tbl_Category());
                 cfg.CreateMap<tbl_Category, Category>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new Category());

                 cfg.CreateMap<Country, CountryCode>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new CountryCode());
                 cfg.CreateMap<CountryCode, Country>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new Country());

                 cfg.CreateMap<HomeCards, tbl_HomeCards>().ForMember(d => d.Image, c => c.Ignore()).ConvertUsing(s => new tbl_HomeCards());
                 cfg.CreateMap<tbl_HomeCards, HomeCards>().ForMember(d => d.Image, c => c.Ignore()).ConvertUsing(s => new HomeCards());

                 cfg.CreateMap<Jobs, tbl_Jobs>().ForMember(d => d.Image, c => c.Ignore()).ConvertUsing(s => new tbl_Jobs());
                 cfg.CreateMap<tbl_Jobs, Jobs>().ForMember(d => d.Image, c => c.Ignore()).ConvertUsing(s => new Jobs());

                 cfg.CreateMap<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>().ForMember(d => d.JobId, c => c.Ignore()).ConvertUsing(s => new tbl_Job_SubCategory_Mapping());
                 cfg.CreateMap<tbl_Job_SubCategory_Mapping, JobSubCategoryMapping>().ForMember(d => d.JobId, c => c.Ignore()).ConvertUsing(s => new JobSubCategoryMapping());

                 cfg.CreateMap<Referal, tbl_Referal>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new tbl_Referal());
                 cfg.CreateMap<tbl_Referal, Referal>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new Referal());

                 cfg.CreateMap<States, tbl_State>().ForMember(d => d.State, c => c.Ignore()).ConvertUsing(s => new tbl_State());
                 cfg.CreateMap<tbl_State, States>().ForMember(d => d.State, c => c.Ignore()).ConvertUsing(s => new States());

                 cfg.CreateMap<SubCategory, tbl_SubCategory>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new tbl_SubCategory());
                 cfg.CreateMap<tbl_SubCategory, SubCategory>().ForMember(d => d.Name , c => c.Ignore()).ConvertUsing(s => new SubCategory());

                 cfg.CreateMap<Testimonial, tbl_Testimonial>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new tbl_Testimonial());
                 cfg.CreateMap<tbl_Testimonial, Testimonial>().ForMember(d => d.Name, c => c.Ignore()).ConvertUsing(s => new Testimonial());

                 cfg.CreateMap<UserCityMapping, tbl_User_City_Mapping>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new tbl_User_City_Mapping());
                 cfg.CreateMap<tbl_User_City_Mapping, UserCityMapping>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new UserCityMapping());

                 cfg.CreateMap<UserImages, tbl_UserImages>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new tbl_UserImages());
                 cfg.CreateMap<tbl_UserImages, UserImages>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new UserImages());

                 cfg.CreateMap<UserSubCategoryMapping, tbl_User_SubCategory_Mapping>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new tbl_User_SubCategory_Mapping());
                 cfg.CreateMap<tbl_User_SubCategory_Mapping, UserSubCategoryMapping>().ForMember(d => d.UserId, c => c.Ignore()).ConvertUsing(s => new UserSubCategoryMapping());



             });

            //var mapper = config.CreateMapper();
            //return mapper;

        }


    }

}
