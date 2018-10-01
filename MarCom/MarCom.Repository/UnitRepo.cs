﻿using MarCom.DataModel;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.Repository
{
    public class UnitRepo
    {
        public static List<UnitViewModel> Get()
        {
            List<UnitViewModel> result = new List<UnitViewModel>();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_Unit
                          //where u.Is_Delete == false
                          select new UnitViewModel
                          {
                              Id = u.Id,
                              Code = u.Code,
                              Name = u.Name,

                              Create_By = u.Create_By,
                              Create_Date = DateTime.Now
                          }).ToList();
            }
            return result;
        }

        public static bool Update(UnitViewModel entity)
        {
            bool result = true;
            try
            {
                using (var db = new MarComContext())
                {
                    if (entity.Id == 0)
                    {
                        M_Unit unit = new M_Unit();
                        unit.Code = entity.Code;
                        unit.Name = entity.Name;
                        unit.Description = entity.Description;
                        unit.Is_Delete = entity.Is_Delete;
                        unit.Create_By = "Admin";
                        
                        unit.Create_Date = DateTime.Now;

                        db.M_Unit.Add(unit);
                        db.SaveChanges();

                    }
                    else
                    {
                        M_Unit unit = db.M_Unit.Where(u => u.Id == entity.Id).FirstOrDefault();
                        if (unit != null)
                        {
                            unit.Code = entity.Code;
                            unit.Name = entity.Name;
                            unit.Description = entity.Description;

                            unit.Update_By = "Admin";
                            unit.Update_Date = DateTime.Now;
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

        public static UnitViewModel GetById(int id)
        {
            UnitViewModel result = new UnitViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_Unit
                          where u.Id == id
                          select new UnitViewModel
                          {
                              Id = u.Id,
                              Code = u.Code,
                              Name = u.Name,
                              Description = u.Description,
                              Is_Delete = u.Is_Delete
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
                    M_Unit unit = db.M_Unit.Where(u => u.Id == id).FirstOrDefault();
                    if (unit != null)
                    {
                        unit.Is_Delete = true;
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
