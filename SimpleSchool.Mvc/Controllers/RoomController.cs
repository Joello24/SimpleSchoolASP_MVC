using System;
using Microsoft.AspNetCore.Mvc;
using SimpleSchool.Core.Entities;
using SimpleSchool.Core.Interfaces;

namespace SimpleSchool.Mvc.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [Route("/rooms/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var room = _roomRepository.Get(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }
        
        [Route("rooms/add")]
        [HttpGet]
        public IActionResult Add(int buildingId)
        {
            var model = new Room()
            {
                BuildingID = buildingId
            };

            return View(model);
        }
        
        [Route("rooms/add")]
        [HttpPost]
        public IActionResult Add(Room room)
        {
            var result = _roomRepository.Add(room);
            if (result.Success)
            {
                return RedirectToAction("GetRooms", "Building", new {id = room.BuildingID});
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Messages[0].ToString());
            }
        }
        
        [Route("rooms/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = _roomRepository.Get(id);
            if (!room.Success)
            {
                return NotFound();
            }

            return View(room.Data);
        }
        
        [Route("rooms/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(Room model)
        {
            var result = _roomRepository.Edit(model);
            if (result.Success)
            {
                return RedirectToAction("GetRooms", "Building", new {id = model.BuildingID});
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }
        [Route("rooms/remove/{id}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var room = _roomRepository.Get(id);
            if (!room.Success)
            {
                return NotFound();
            }

            return View(room.Data);
        }
        [Route("rooms/remove/{id}")]
        [HttpPost]
        public IActionResult Remove(Room model)
        {
            var result = _roomRepository.Delete(model.RoomID);
            if (result.Success)
            {
                return RedirectToAction("GetRooms", "Building", new {id = model.BuildingID});
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }
    }
}