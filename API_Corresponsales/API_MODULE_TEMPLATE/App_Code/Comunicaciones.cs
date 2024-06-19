using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;

/// <summary>
/// Descripción breve de Comunicaciones
/// </summary>
public class Comunicaciones
{
    public Comunicaciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string EnviarMailV1(string Cuenta, string MFrom, string MTo, string MCc, string MHid, string MAsunto, string MCuerpo, string File)
    {
        if (string.IsNullOrWhiteSpace(MTo.Trim()))
        {
            return "No ha especificado el destinatario";
        }

        if (string.IsNullOrWhiteSpace(MCuerpo.Trim()))
        {
            return "No ha especificado texto del mensaje";
        }

        if (string.IsNullOrWhiteSpace(MAsunto.Trim()))
        {
            MAsunto = "Mensaje desde sin asunto";
        }

        DataTable dtc = new DataTable();

        try
        {
            dtc = DataAcceso.GlobalClassV001(-1, "Comm_CuentaListar", Cuenta, "", "", "", "", "");

            if (dtc == null)
            {
                return "No se pudo obtener el registro desde la base de datos (NOTHING)";
            }

            if (dtc.Rows.Count == 0) 
            {
                return "No se pudo obtener el registro desde la base de datos (COUNT=0)";
            }
        }
        catch(Exception ex)
        {
            return ("No se pudo obtener la configuracion: " + ex.Message);
        }

        if (!(bool)dtc.Rows[0]["optm_Activo"]) 
        {
            return "La cuenta no está activa";
        }

        string DHost = (string)dtc.Rows[0]["optm_ServidorOut"];
        string DUser = (string)dtc.Rows[0]["optm_UsuarioOut"];
        string DPass = (string)dtc.Rows[0]["optm_PassOut"];

        MFrom = (string)dtc.Rows[0]["optm_Direccion"];

        if (string.IsNullOrEmpty(DHost))
        {
            return "No esta configurada las opcion HOST, debe configurarlos en las opciones globales de comunicacion";
        }

        MailMessage correo = new MailMessage();
        correo.From = new MailAddress(MFrom);


        // Destinatario normal
        if(MTo.Contains(";"))
        {
            string[] DRC = MTo.Split(';');

            foreach (string recipient in DRC)
            {
                if (!string.IsNullOrWhiteSpace(recipient))
                {
                    correo.To.Add(recipient.Trim());
                }
            }
        }
        else
        {
            correo.To.Add(MTo);
        }

        // Destinatario CC
        if (!string.IsNullOrWhiteSpace(MCc))
        {
            if (MCc.Contains(";"))
            {
                string[] DRC = MCc.Split(';');
                foreach (string recipient in DRC)
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                    {
                        correo.CC.Add(recipient.Trim());
                    }
                }
            }
            else
            {
                correo.CC.Add(MCc);
            }
        }

        // Destinatarios Hidden
        if (!string.IsNullOrWhiteSpace(MHid))
        {
            if (MHid.Contains(";"))
            {
                string[] DRC = MHid.Split(';');
                foreach (string recipient in DRC)
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                    {
                        correo.Bcc.Add(recipient.Trim());
                    }
                }
            }
            else
            {
                correo.Bcc.Add(MHid);
            }
        }

        if (!string.IsNullOrEmpty(File))
        {
            Attachment Atta = new Attachment(File);
            correo.Attachments.Add(Atta);
        }

        correo.Subject = MAsunto;
        correo.Body = MCuerpo;
        correo.IsBodyHtml = true;
        correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        correo.BodyEncoding = Encoding.UTF8;
        correo.Priority = MailPriority.Normal;
        correo.Headers.Add("MadeWith", "LEGIONELITE");

        SmtpClient smtp = new SmtpClient();
        smtp.Host = DHost;

        // Por si usa SSL
        if ((bool)(dtc.Rows[0]["optm_AlowSSLOut"]))
        {
            smtp.EnableSsl = true;
        }

        // Por si usa puerto distinto
        string EPORT = dtc.Rows[0]["optm_PuertoOut"].ToString();

        if(EPORT != "")
        {
            smtp.Port = int.Parse(EPORT);
        }

        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(DUser, DPass);

        try {
            smtp.Send(correo);
            // correo.Save("f:\Paso\tmp.eml")
            return "0";
        }
        catch (Exception ex) {
            // Utilidades.GrabarLogEnDisco("ERROR en E-mail: " & ex.Message, "ErrorEnvioMail")
            return "ERROR en E-mail: " + ex.Message;
        }
    }
}