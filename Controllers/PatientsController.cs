using APBD_tutorial11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial11.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _service;

    public PatientsController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("{idClient}")]
    public async Task<IActionResult> GetPatientData(int idClient)
    {
        try
        {
            var patientData = await _service.GetPatientData(idClient);
            return Ok(patientData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    
}