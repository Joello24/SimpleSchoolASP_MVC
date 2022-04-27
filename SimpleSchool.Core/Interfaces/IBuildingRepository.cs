using SimpleSchool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSchool.Core.Interfaces
{
    public interface IBuildingRepository
    {
        Result<Building> Get(int buildingId);
        Result<List<Building>> GetAll();
        Result Add(Building building);
        Result Edit(Building building);
        Result Delete(int buildingId);
    }
}
