using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HospitalApi.Models;


[ApiController]
public class DutyController : ControllerBase
{
    private readonly DutyService dutyService = DutyService.Instance();
    private readonly UserRepositoryImpl userRepository = UserRepositoryImpl.Instance();

    [HttpGet]
    [Route("/duty/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult GetDuty(int id)
    {
        IActionResult response;
        Duty duty = dutyService.FindById(id);
        response = Ok(new
        {
            date = duty.Date,
            specialization = duty.Specialization.ToString()
        });
        return response;
    }

    [HttpPost]
    [Route("/duty/add")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult AddDuty([FromBody] DutyDTO dto)
    {
        IActionResult response;
        try
        {
            User user = userRepository.FindByUsername(dto.Username);

            Duty duty = null;
            if (dto.Specialization != null)
            {
                duty = new Duty(DateTime.Parse(dto.Date), (Specialization)Enum.Parse(typeof(Specialization), dto.Specialization));
            }
            else
            {
                duty = new Duty(DateTime.Parse(dto.Date));
            }
            dutyService.AddDuty((Nurse)user, duty);
            response = Ok(new
            {
                date = duty.Date,
                specialization = duty.Specialization.ToString()
            }); ;
            return response;
        }
        catch (Exception e)
        {
            return response = BadRequest(new
            {
                e.Message
            });
        }
    }


    [HttpPut]
    [Route("/duty/{username}/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult EditDuty([FromBody]EditDutyDto dto, string username, int id)
    {
        IActionResult response;
        try
        {
            dutyService.EditDuty(dto, username, id);
            return Ok();
        }
        catch (Exception e) {
            return response = BadRequest(new
            {
                e.Message
            });
        }
    }

    [HttpDelete]
    [Route("/duty/{username}/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult DeleteDuty(string username, int id)
    {
        IActionResult response;
        try
        {
            dutyService.DeleteDuty(username, id);
            return Ok();
        }
        catch (Exception e)
        {
            return response = BadRequest(new
            {
                e.Message
            });
        }
    }
}
