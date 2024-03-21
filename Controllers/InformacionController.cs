using Microsoft.AspNetCore.Mvc;
using PanchitoProyectApi.Models;
using PanchitoProyectApi.Services;

namespace PanchitoProyectApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformacionController : ControllerBase
    {
    private readonly ILogger<InformacionController> _logger;

    private readonly InformacionServices _infoServices;

    public InformacionController(ILogger<InformacionController> logger, InformacionServices infoServices)
    {
        _logger = logger;
        _infoServices = infoServices;
    }

    ///Obtener todos los Sensores
    [HttpGet]
    public async Task<IActionResult> GetInformacion()
    {
        var info = await _infoServices.GetAsync();
        return Ok(info);
    }

    ///Obtener Sensor por Id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetInformacionById(string Id)
    {
        return Ok(await _infoServices.GetInformacionById(Id));
    }

    ///Crear Sensor
    [HttpPost]
    public async Task<IActionResult> CreateInformacion([FromBody] Informacion info)
    {
        if (info == null)
            return BadRequest();

        await _infoServices.InsertInformacion(info);

        return Ok(new { mesage = "Se ha agregado correctamente" });
    }

    ///Actualizar Sensor
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateInformacion([FromBody] Informacion info, string Id)
    {
        if (info == null)
            return BadRequest();

        info.Id = Id;

        await _infoServices.UpdateInformacion(info);
        return Ok(new { mesage = "Se ha actualizado correctamente" });
    }
}

