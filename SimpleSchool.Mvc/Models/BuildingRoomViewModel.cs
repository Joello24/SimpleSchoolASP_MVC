using SimpleSchool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSchool.Mvc.Models
{
    public class BuildingRoomViewModel
    {
        public Building Building { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
