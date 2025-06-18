using CashFlow.Application.DTOs;
using CashFlow.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Transaction.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionAppService _transactionAppService;

    public TransactionsController(ITransactionAppService  transactionAppService)
    {
        _transactionAppService = transactionAppService;
    }
    
    [HttpPost]
    [MapToApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequestDTO transaction)
    { 
        var result = await _transactionAppService.CreateTransactionAsync(transaction);

        return Ok(result);

    }
}