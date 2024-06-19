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
        //LoadInvoiceAgn_V090 es un m�todo POST que espera recibir un objeto
        public ActionResult<string> LoadInvoiceAgn_V090([FromBody] LoadInvoiceRequest request)
        {
            try
            {
                // Diccionario para mapear c�digos de pa�s a mensajes de salida
                Dictionary<string, string> countryMessages = new Dictionary<string, string>
                {
                    { "ECU", "000 - Ha salido OK! para Ecuador" },
                    { "OTRO_CODIGO", "000 - Ha salido OK! para otro c�digo" },
                    // Agrega m�s c�digos de pa�s y mensajes seg�n sea necesario
                };

                // Verificar si el c�digo de pa�s est� en el diccionario
                if (countryMessages.ContainsKey(request.PaisCliente))
                {
                    return countryMessages[request.PaisCliente];
                }
                else
                {
                    return "1217 - C�digo de pa�s no v�lido para la transmisi�n";
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
