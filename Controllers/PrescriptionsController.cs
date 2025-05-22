using APBD_tutorial11.DTOs;
using APBD_tutorial11.Models;
using APBD_tutorial11.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace APBD_tutorial11.Controllers;


[Route("/api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPrescription(PrescriptionDto prescriptionDto)
    {
        try
        {
            await _dbService.IssueNewPrescription(prescriptionDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }
    
    
}