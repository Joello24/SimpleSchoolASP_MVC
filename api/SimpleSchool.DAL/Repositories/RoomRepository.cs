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
    public class RoomRepository : IRoomRepository
    {
        public Result Add(Room room)
        {
            var result = new Result();
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Room.Add(room);
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

        public Result Delete(int roomId)
        {
            var result = new Result();

            try
            {
                using (var context = new AppDbContext())
                {
                    var room = context.Room.Find(roomId);

                    if (room == null)
                    {
                        result.Messages.Add($"Could not find room with id {roomId}");
                    }
                    else
                    {
                        context.Room.Remove(room);
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

        public Result Edit(Room room)
        {
            var result = new Result();

            try
            {
                using (var context = new AppDbContext())
                {
                    context.Room.Update(room);
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

        public Result<Room> Get(int roomId)
        {
            var result = new Result<Room>();

            try
            {
                using (var context = new AppDbContext())
                {
                    result.Data = context.Room.Find(roomId);

                    if (result.Data == null)
                    {
                        result.Messages.Add($"Could not find room with id {roomId}");
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

        public Result<List<Room>> GetByBuilding(int buildingId)
        {
            var result = new Result<List<Room>>();

            try
            {
                using (var context = new AppDbContext())
                {
                    result.Data = context.Room.Where(r => r.BuildingID == buildingId).ToList();
                    result.Success = true;
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
