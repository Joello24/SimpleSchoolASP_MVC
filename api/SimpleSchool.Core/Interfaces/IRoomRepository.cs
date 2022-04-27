using SimpleSchool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSchool.Core.Interfaces
{
    public interface IRoomRepository
    {
        Result<Room> Get(int roomId);
        Result<List<Room>> GetByBuilding(int buildingId);
        Result Add(Room room);
        Result Edit(Room room);
        Result Delete(int roomId);
    }
}
