using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class SouvenirRepo
    {
        public static List<SouvenirViewModel> Get()
        {
            return Get(true);
        }
        public static List<SouvenirViewModel> Get(bool all)
        {
            List<SouvenirViewModel> result = new List<SouvenirViewModel>();
            using (var db = new MarComContext())
            {
                result = (from s in db.M_Souvenir
                          join u in db.M_Unit on s.M_Unit_Id equals u.Id
                          select new SouvenirViewModel
                          {
                              Id = s.Id,
                              Code = s.Code,
                              Name = s.Name,
                              Description = s.Description,
                              Quantity = s.Quantity,
                              M_Unit_Id = s.Id,
                              Unit = u.Name,                              
                              Is_Delete = s.Is_Delete,

                              Create_By = "Admin",
                              Create_Date = DateTime.Now

                          })
                          .Where(s => s.Is_Delete == all ? s.Is_Delete : true)
                          .ToList();
            }
            return result;
        }

        //List markom selesai

        public static ResultResponse Update(SouvenirViewModel entity)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Souvenir souvenir = new M_Souvenir();
                        //souvenir.Code = entity.Code;
                        souvenir.Code = entity.Code;
                        souvenir.Name = entity.Name;
                        souvenir.Description = entity.Description;
                        souvenir.Quantity = entity.Quantity;
                        souvenir.M_Unit_Id = entity.M_Unit_Id;
                        souvenir.Is_Delete = entity.Is_Delete;

                        souvenir.Create_By = "Admin";
                        souvenir.Create_Date = DateTime.Now;

                        db.M_Souvenir.Add(souvenir);
                        db.SaveChanges();
                    }
                    else
                    {
                        M_Souvenir souvenir = db.M_Souvenir.Where(s => s.Id == entity.Id).FirstOrDefault();
                        if (souvenir != null)
                        {
                            souvenir.Code = entity.Code;
                            souvenir.Name = entity.Name;
                            souvenir.Description = entity.Description;
                            souvenir.Quantity = entity.Quantity;
                            souvenir.M_Unit_Id = entity.M_Unit_Id;
                            souvenir.Is_Delete = entity.Is_Delete;

                            souvenir.Update_By = "Admin";
                            souvenir.Update_Date = DateTime.Now;

                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        // update marcom wis

        public static SouvenirViewModel GetById(int id)
        {
            SouvenirViewModel result = new SouvenirViewModel();
            using (var db = new MarComContext())
            {
                result = (from s in db.M_Souvenir
                          where s.Id == id
                          select new SouvenirViewModel
                          {
                              Id = s.Id,
                              Code = s.Code,
                              Name = s.Name,
                              Description = s.Description,
                              Quantity = s.Quantity,
                              M_Unit_Id = s.M_Unit_Id,              
                              Is_Delete = s.Is_Delete,
                          }).FirstOrDefault();
            }
            return result;
        }

        public static bool Delete(int id)
        {
            bool result = true;
            try
            {
                using (var db = new MarComContext())
                {
                    M_Souvenir souvenir = db.M_Souvenir.Where(s => s.Id == id).FirstOrDefault();
                    if (souvenir != null)
                    {
                        souvenir.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
    }
}
