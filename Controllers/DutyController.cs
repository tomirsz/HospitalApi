using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HospitalApi.Models;


[ApiController]
public class DutyController : ControllerBase
{
    private readonly UserService userService = UserService.Instance();
    private readonly DutyService dutyService = DutyService.Instance();
    private readonly SerializeService serializeService = SerializeService.Instance();
    private readonly UserRepositoryImpl userRepository = UserRepositoryImpl.Instance();


    [HttpPost]
    [Route("/duty/add")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult addDuty([FromBody] DutyDTO dto)
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
            dutyService.addDuty((Nurse)user, duty);
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

    [HttpGet]
    [Route("/edit/duty/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult getDuty(int id)
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


    [HttpPut]
    [Route("/edit/duty/{username}/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult editDuty([FromBody]EditDutyDto dto, string username, int id)
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
    [Route("/edit/duty/{username}/{id}")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult deleteDuty(string username, int id)
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
