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
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingsController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet]
        public IActionResult GetBuildings()
        {
            var result = _buildingRepository.GetAll();

            if(result.Success)
            {
                return Ok(
                    result.Data.Select(
                        b => new BuildingModel() 
                            { 
                                BuildingID = b.BuildingID, 
                                BuildingName = b.BuildingName 
                            }
                        )
                    );
            }
            else
            {
                throw new Exception(result.Messages[0]);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/{id}", Name="GetBuilding")]
        public IActionResult GetBuilding(int id)
        {
            var result = _buildingRepository.Get(id);

            if(result.Success)
            {
                return Ok(new BuildingModel()
                {
                    BuildingID = result.Data.BuildingID,
                    BuildingName = result.Data.BuildingName
                });
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPost]
        public IActionResult AddBuilding(string buildingName)
        {
            Building b = new Building();
            b.BuildingName = buildingName;

            var result = _buildingRepository.Add(b);

            if(result.Success)
            {
                return CreatedAtRoute(nameof(GetBuilding), new { id = b.BuildingID }, b);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPut]
        public IActionResult EditBuilding(Building building)
        {
            if(!_buildingRepository.Get(building.BuildingID).Success)
            {
                return NotFound($"Building {building.BuildingID} not found");
            }

            var result = _buildingRepository.Edit(building);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBuilding(int id)
        {
            if (!_buildingRepository.Get(id).Success)
            {
                return NotFound($"Building {id} not found");
            }

            var result = _buildingRepository.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
    }
}
