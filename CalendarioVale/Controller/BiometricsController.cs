using CalendarioVale.Data.Model;
using CalendarioVale.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarioVale;

[Route("api/biometrics")]
[AllowAnonymous]
public class BiometricsController : Controller
{
    private IDailyStatusService _dailyStatusService;
    public BiometricsController(IDailyStatusService dailyStatusService)
    {
        _dailyStatusService = dailyStatusService;
    }

    [HttpPost]
    public async Task<ActionResult> AddBiometrics([FromBody] BiometricsDto bio)
    {
        if (bio == null)
        {
            return BadRequest();
        }        
        await _dailyStatusService.AddBiometrics(bio.ToBiometrics(), DateTime.Today);
        return Ok();
    }
}
