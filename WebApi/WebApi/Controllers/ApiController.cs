using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class ApiController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public ApiController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    

    [HttpGet("clients/{id:int}/orders")]
    public async Task<IActionResult> GetRequest(int id)
    {
        var account = await _databaseService.GetClientOrdersAsync(id);
            
        if (account == null)
        {
            return NotFound($"Client o id {id} nie został znaleziony");
        }
        
        return Ok(account);
    }
}