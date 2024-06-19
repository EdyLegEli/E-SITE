using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace API_Corresponsales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AG_ESP_ELITEController : ControllerBase
    {
        [HttpPost("LoadInvoiceAgn_V090")]
        //LoadInvoiceAgn_V090 es un método POST que espera recibir un objeto
        public ActionResult<string> LoadInvoiceAgn_V090([FromBody] LoadInvoiceRequest request)
        {
            try
            {
                // Diccionario para mapear códigos de país a mensajes de salida
                Dictionary<string, string> countryMessages = new Dictionary<string, string>
                {
                    { "ECU", "000 - Ha salido OK! para Ecuador" },
                    { "OTRO_CODIGO", "000 - Ha salido OK! para otro código" },
                    // Agrega más códigos de país y mensajes según sea necesario
                };

                // Verificar si el código de país está en el diccionario
                if (countryMessages.ContainsKey(request.PaisCliente))
                {
                    return countryMessages[request.PaisCliente];
                }
                else
                {
                    return "1217 - Código de país no válido para la transmisión";
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        public class LoadInvoiceRequest
        {
            public long CodCorresponsal { get; set; }
            public string ClaveAgente { get; set; }
            public long Operacion { get; set; }
            public long ReferenciaGiro { get; set; }
            public int CodUsuario { get; set; }
            public string Accion { get; set; }
            public string PaisCliente { get; set; }
        }
    }
}
