using System.IO.Pipelines;
using CalendarioVale.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarioVale;

[Route("api/biometrics")]
[AllowAnonymous]
public class BiometricsController : Controller
{
    private IDailyStatusService _dailyStatusService;
    private IPersonService _personService;

    public BiometricsController(IDailyStatusService dailyStatusService, IPersonService personService)
    {
        _dailyStatusService = dailyStatusService;
        _personService = personService;
    }

    [HttpPost]
    public async Task<ActionResult> AddBiometrics([FromBody] BiometricsDto bio, [FromBody] PersonDto person)
    {
        if (bio == null)
        {
            return BadRequest();
        }
        var personDb = await _personService.GetById(person.Id).ConfigureAwait(true);
        await _dailyStatusService.AddBiometrics(bio.ToBiometrics(), personDb);
        return Ok();
    }
}