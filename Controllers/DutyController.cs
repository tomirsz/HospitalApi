using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HospitalApi.Models;


[ApiController]
public class DutyController : ControllerBase
{
    private readonly UserService userService = UserService.Instance();
    private readonly DutyService dutyService = DutyService.Instance();

    [HttpPost]
    [Route("/duty/add")]
    [Authorize(Policy = Policies.ADMIN)]
    public IActionResult addDuty([FromBody] DutyDTO dto) {
        IActionResult response;
        try
        {
            Employee user = (Employee)userService.findUser(dto.Username);
            Duty duty = new Duty(DateTime.Parse(dto.Date), (Specialization)Enum.Parse(typeof(Specialization), dto.Specialization));
            dutyService.addDuty(user, duty);
            response = Ok(new
            {
                date = duty.Date,
                specialization = duty.Specialization.ToString()
            }); ;
            return response;
        }
        catch (Exception e) {
            return response = BadRequest(new
            {
                e.Message
            });
        }
    }
}
