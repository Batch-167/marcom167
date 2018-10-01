using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class MenuAccessRepo
    {
        public static List<MenuAccessViewModel> Get()
        {
            List<MenuAccessViewModel> result = new List<MenuAccessViewModel>();
            using (var db = new MarComContext())
            {
                result = (from ma in db.M_Menu_Access
                          join r in db.M_Role on ma.M_Role_Id equals r.Id
                          select new MenuAccessViewModel
                          {
                              Id=ma.Id,
                              M_Role_Id=ma.M_Role_Id,
                              RoleName=r.Name,
                              Is_Delete=ma.Is_Delete,
                              
                              Create_By=ma.Create_By,
                              Create_Date=ma.Create_Date


                          }).ToList();
            }
            return result;
        }
    }
}
