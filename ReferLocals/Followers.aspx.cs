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
    public partial class Followers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        [WebMethod]
        public static FavoriteUserListWrapper GetMyFollowerUsers(int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
           return favorite.GetMyUserFollowers(SessionService.Pull(SessionKeys.UserId),pageIndex,pageSize);
        }

        [WebMethod]
        public static FavoriteProfessionalListWrapper GetMyFollowerProfessionals(int pageIndex, int pageSize)
        {
            Favorite favorite = new Favorite();
            return favorite.GetMyProfessionalFollowers(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
    }
}