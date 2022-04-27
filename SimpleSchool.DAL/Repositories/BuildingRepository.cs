using SimpleSchool.Core;
using SimpleSchool.Core.Entities;
using SimpleSchool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSchool.DAL.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        public Result Add(Building building)
        {
            var result = new Result();
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Building.Add(building);
                    context.SaveChanges();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public Result Delete(int buildingId)
        {
            var result = new Result();
            
            try
            {
                using (var context = new AppDbContext())
                {
                    var building = context.Building.Find(buildingId);

                    if(building == null)
                    {
                        result.Messages.Add($"Could not find building with id {buildingId}");
                    }
                    else
                    {
                        context.Building.Remove(building);
                        context.SaveChanges();
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public Result Edit(Building building)
        {
            var result = new Result();

            try
            {
                using (var context = new AppDbContext())
                {
                    context.Building.Update(building);
                    context.SaveChanges();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public Result<List<Building>> GetAll()
        {
            var result = new Result<List<Building>>();

            try
            {
                using (var context = new AppDbContext())
                {
                    result.Data = context.Building.ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public Result<Building> Get(int buildingId)
        {
            var result = new Result<Building>();

            try
            {
                using (var context = new AppDbContext())
                {
                    result.Data = context.Building.Find(buildingId);

                    if(result.Data == null)
                    {
                        result.Messages.Add($"Could not find building with id {buildingId}");
                    }
                    else
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }
    }
}
