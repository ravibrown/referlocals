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
    [RoutePrefix("api/Favorite")]
    public class FavoriteController : ApiController
    {
       
        [Route("Save")]
        public ResultData Save(FavoriteBindingModel model)
        {
            Favorite favorite = new Favorite();
           return favorite.Save(model.UserID, model.FavoriteTypeID, model.FavoriteType,model.Status);
        }

        [Route("GetFavoriteJobs")]
        public FavoriteJobListWrapper GetFavoriteJobs(long userID,int pageIndex,int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteJobs(userID, pageIndex, pageSize);
        }

        [Route("GetFavoriteProfessionals")]
        public FavoriteProfessionalListWrapper GetFavoriteProfessionals(long userID, int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteProfessionals(userID, pageIndex, pageSize);
        }

        [Route("GetFavoriteUsers")]
        public FavoriteUserListWrapper GetFavoriteUsers(long userID, int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteUsers(userID, pageIndex, pageSize);
        }

        [Route("GetMyUserFollowers")]
        public FavoriteUserListWrapper GetMyUserFollowers(long userID, int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.GetMyUserFollowers(userID, pageIndex, pageSize);
        }

        [Route("GetMyProfessionalFollowers")]
        public FavoriteProfessionalListWrapper GetMyProfessionalFollowers(long userID, int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.GetMyProfessionalFollowers(userID, pageIndex, pageSize);
        }
    }
}
