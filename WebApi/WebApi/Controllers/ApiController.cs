using Microsoft.AspNetCore.Mvc;
using WebApi.RequestModels;
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
    
    
    [HttpPost("clients/{id:int}/orders")]
    public async Task<IActionResult> PostRequest(int id, PostClientOrderModel request)
    {
        try
        {
            var orderId = await _databaseService.PostClientOrder(id, request);
            return Created();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
}