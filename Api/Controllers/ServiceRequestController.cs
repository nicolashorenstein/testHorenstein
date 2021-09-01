using Core.Commands;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Util;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [EnableCors("testPolicy")]
    [ApiController]

    public class ServiceRequestController : ControllerBase
    {

        private readonly IServiceRepository repository;
        public ServiceRequestController(IServiceRepository rep)
        {
            repository = rep;
        }

        [HttpGet]
        [Route("api/servicerequest")]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            try
            {
                var result = await repository.GetAllServiceRequestAsync();
                if (result == null || result.Count > 0)
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/servicerequest/{id}")]
        public async Task<IActionResult> GetAllServiceRequestById(Guid id)
        {
            try
            {
                var srequest = await repository.GetServiceRequestByIdAsync(id);
                if (srequest != null)
                {
                    return Ok(srequest);
                }

                return NotFound("Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        
        }

        [HttpPost]
        [Route("api/servicerequest")]
        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequestCreateCommand command)
        {
            try
            {
                if (command.description.Equals("") || command.buildingCode.Equals("") || command.createdBy.Equals(""))
                {
                    return BadRequest("Bad request");
                }

                var sRequest = new SRequest
                {
                    buildingCode = command.buildingCode,
                    createdDate = DateTime.Now,
                    createdBy = command.createdBy,
                    currentStatus = (int)CurrentStatusEnum.Created,
                    description = command.description,
                    lastModifiedBy = "",
                    lastModifiedDate = null
                };

                var srequest = await repository.CreateServiceRequestAsync(sRequest);

                return CreatedAtAction(nameof(CreateServiceRequest), new { id = sRequest.id }, sRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("api/servicerequest/{id}")]
        public async Task<IActionResult> UpdateServiceRequest([FromBody] ServiceRequestUpdateCommand command, Guid id)
        {
            try
            {

                if (command.buildingCode.Equals("") || command.description.Equals("")) //complete with more validations if needed
                {
                    return BadRequest("Bad request");
                }

                var request = await repository.GetServiceRequestByIdAsync(id);
                if (request != null)
                {
                    var status = (CurrentStatusEnum)command.currentStatus;
                    request.buildingCode = command.buildingCode;
                    request.description = command.description;
                    request.createdBy = command.createdBy;
                    request.currentStatus = (int)status;
                    request.lastModifiedBy = command.lastModifiedBy;
                    request.lastModifiedDate = DateTime.Now;

                    var result = await repository.UpdateServiceRequestAsync(request);

                    if (status == CurrentStatusEnum.Complete || status == CurrentStatusEnum.Canceled)
                    {
                        NotificationProvider.Notify(status);
                    }

                    return Ok(result);
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete]
        [Route("api/servicerequest/{id}")]
        public async Task<IActionResult> DeleteServiceRequest(Guid id)
        {
            try
            {
                var request = await repository.GetServiceRequestByIdAsync(id);
                if (request != null)
                {
                    var result = await repository.DeleteServiceRequestAsync(request);
                    if (result)
                    {
                        return CreatedAtAction(nameof(DeleteServiceRequest), new { id = id });
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
           
        }
    }
}
