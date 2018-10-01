﻿using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class UserRepo
    {

        public static List<UserViewModel> Get()
        {
            List<UserViewModel> result = new List<UserViewModel>();
            using (var db = new MarComContext())
            {
                result = (from us in db.M_User
                          join em in db.M_Employee on
                          us.M_Employee_Id equals em.Id
                          join r in db.M_Role on
                          us.M_Role_Id equals r.Id
                              //where u.Is_Delete == false
                          select new UserViewModel
                          {
                              Id = us.Id,
                              M_Employee_Id = us.M_Employee_Id,
                              Fullname = em.First_Name + " " + em.Last_Name,
                              M_Role_Id = us.M_Role_Id,
                              Role = r.Name,
                             // Company = em,
                              Username = us.Username,
                              Password = us.Password,
                              Is_Delete = us.Is_Delete,

                              Create_By = us.Create_By,
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static bool Update(UserViewModel entity)
        {
            bool result = true;
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_User user = new M_User();
                        user.Username = entity.Username;
                        user.Password = entity.Password;
                        user.M_Role_Id = entity.M_Role_Id;
                        user.M_Employee_Id = entity.M_Employee_Id;
                        user.Is_Delete = entity.Is_Delete;

                        user.Create_By = "Admin";
                        user.Create_Date = DateTime.Now;

                        db.M_User.Add(user);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_User user = db.M_User.Where(us => us.Id == entity.Id).FirstOrDefault();
                        if (user != null)
                        {
                            user.Username = entity.Username;
                            user.Password = entity.Password;
                            user.M_Role_Id = entity.M_Role_Id;
                            user.M_Employee_Id = entity.M_Employee_Id;
                            user.Is_Delete = entity.Is_Delete;

                            user.Update_By = "Admin";
                            user.Update_Date = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }

        public static UserViewModel GetById(int id)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from us in db.M_User
                          where us.Id == id
                          select new UserViewModel
                          {
                              Id = us.Id,
                              Username = us.Username,
                              Password = us.Password,
                              M_Role_Id = us.M_Role_Id,
                              M_Employee_Id = us.M_Employee_Id,

                              Is_Delete = us.Is_Delete
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
                    M_User user = db.M_User.Where(us => us.Id == id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Is_Delete = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
