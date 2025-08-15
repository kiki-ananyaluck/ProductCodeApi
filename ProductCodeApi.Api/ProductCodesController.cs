using Microsoft.AspNetCore.Mvc;
using ProductCodeApi.Application.Interfaces;
using ProductCodeApi.Application.Validators;
using QRCoder;

namespace ProductCodeApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCodesController : ControllerBase
{
    private readonly IProductCodeService _service;

    public ProductCodesController(IProductCodeService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCodeRequest request)
    {
        var validator = new CreateCodeValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        try
        {
            var entity = await _service.CreateAsync(request.Code);

            return CreatedAtAction(nameof(GetAll),
                new { id = entity.Id },
                new
                {
                    id = entity.Id,
                    codeDisplay = entity.Code
                });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _service.GetAllAsync();

            if (list == null)
            {
                return NotFound(new { message = "No product codes found" });
            }

            var hashids = new HashidsNet.Hashids("my-secret-salt", 8);

            var result = list.Select(x => new
            {
                id = x.Id,
                codeDisplay = x.Code,
                createdAt = x.CreatedAt
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR in GetAll: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpGet("{id}/qrcode")]
    public async Task<IActionResult> GetQrCode(int id)
    {
        try
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound(new { message = $"Product code with id {id} not found" });
            }

            var codeDisplay = entity.Code ?? "";
            if (string.IsNullOrWhiteSpace(codeDisplay))
            {
                return BadRequest(new { message = "Code value is empty or invalid" });
            }

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(codeDisplay, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = qrCode.GetGraphic(20);

            return File(qrBytes, "image/png");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR in GetQrCode: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(new { message = $"Product code with id {id} not found" });
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR in Delete: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new { message = ex.Message });
        }
    }
}