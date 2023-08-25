using Application.Features.Properties.Commands;
using Application.Features.Properties.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly ISender _sender;
        public PropertyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool res = await _sender.Send(new CreatePropertyRequest(newPropertyRequest));

            if (res)
            {
                return Ok("Done!");

            }
            return BadRequest("Problem Occured");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            bool res = await _sender.Send(new UpdatePropertyRequest(updateProperty));

            if (res)
            {
                return Ok("Done!");

            }
            return BadRequest("Problem Occured");
        }
        [HttpGet]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var res = await _sender.Send(new GetPropertyByIdRequest(id));

            if (res != null)
            {
                return Ok(res);

            }
            return NotFound("Problem Occured");
        }
        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var res = await _sender.Send(new GetAllPropertiesRequest());

            if (res != null)
            {
                return Ok(res);

            }
            return NotFound("Problem Occured");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var res = await _sender.Send(new DeletePropertyRequest(id));
            if (res)
            {
                return Ok();
            }
            return BadRequest("Problem Occured");
             
        }
    }
}
