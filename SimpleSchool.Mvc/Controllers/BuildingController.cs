using Microsoft.AspNetCore.Mvc;
using SimpleSchool.Core.Entities;
using SimpleSchool.Core.Interfaces;
using SimpleSchool.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSchool.Mvc.Controllers
{
    public class BuildingController : Controller
    {
        private IBuildingRepository _buildingRepository;
        private IRoomRepository _roomRepository;

        public BuildingController(IBuildingRepository buildingRepository, IRoomRepository roomRepository)
        {
            _buildingRepository = buildingRepository;
            _roomRepository = roomRepository;
        }

        [Route("buildings")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _buildingRepository.GetAll();

            if(result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _buildingRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/add")]
        [HttpGet]
        public IActionResult Add()
        {
            // we want to bind @model, so we will send an empty object
            var model = new Building();
            return View(model);
        }

        [Route("buildings/add")]
        [HttpPost]
        public IActionResult Add(Building model)
        {
            var result = _buildingRepository.Add(model);

            if(result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _buildingRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(Building model)
        {
            var result = _buildingRepository.Edit(model);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/remove/{id}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _buildingRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/remove/{id}")]
        [HttpPost]
        public IActionResult Remove(Building model)
        {
            var result = _buildingRepository.Delete(model.BuildingID);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [Route("buildings/{id}/rooms")]
        [HttpGet]
        public IActionResult GetRooms(int id)
        {
            var model = new BuildingRoomViewModel();

            var bResult = _buildingRepository.Get(id);
            var rResult = _roomRepository.GetByBuilding(id);

            if (bResult.Success && rResult.Success)
            {
                model.Building = bResult.Data;
                model.Rooms = rResult.Data;

                return View(model);
            }
            else
            {
                throw new Exception($"Failed to load rooms for building {id}");
            }
        }
    }
}
