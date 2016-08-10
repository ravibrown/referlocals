using DataAccess.Classes;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class Favorites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static FavoriteJobListWrapper GetMyFavoriteJobs(int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteJobs(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
        [WebMethod]
        public static FavoriteUserListWrapper GetMyFavoriteUsers(int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteUsers(SessionService.Pull(SessionKeys.UserId),pageIndex,pageSize);
        }

        [WebMethod]
        public static FavoriteProfessionalListWrapper GetMyFavoriteProfessionals(int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.FavoriteProfessionals(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
    }
}