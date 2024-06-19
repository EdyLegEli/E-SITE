using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de Ejemplo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class Ejemplo : System.Web.Services.WebService
{

    public Ejemplo()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(string Nombre)
    {

        //System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        //correo.From = new System.Net.Mail.MailAddress("edison@legionelite.com");
        //correo.To.Add("edison@legionelite.com");
        //correo.Subject = "Mail de prueba";
        //correo.Body = "Hola!";
        ////correo.IsBodyHtml = true;
        //correo.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
        ////correo.BodyEncoding = System.Text.Encoding.UTF8;
        ////correo.Priority = System.Net.Mail.MailPriority.Normal;
        ////correo.Headers.Add("MadeWith", "LEGIONELITE");


        //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //smtp.Host = "smtp.serviciodecorreo.es";
        //smtp.Port = 587; // 465;
        //smtp.EnableSsl = true;

        ////smtp.Credentials = new System.Net.NetworkCredential("cesar@legionelite.com", "");
        //smtp.Credentials = new System.Net.NetworkCredential("edison@legionelite.com", "L3g10nEl1t32024#."); //3g10nEl1t32024#.

        //smtp.Send(correo);
        //Utilidades.GrabarLogEnDisco("Teste", "Fichero");

        return("Hola, la hora es: " + DateTime.Now.ToString("HH:mm:ss"));
        //return "Hola a todos, soy " + Nombre + " y he leido: " + Config.GetItem("TZone");


    }

}
