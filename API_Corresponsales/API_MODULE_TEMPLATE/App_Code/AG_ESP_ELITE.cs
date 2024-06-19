using System;
using System.Activities.Expressions;
using System.Configuration;
using System.Data;
using System.Web.Services;


/// <summary>
/// Descripción breve de AG_ESP_ELITE
/// </summary>
[WebService(Description="Utilice estos metodos para interactuar con los corresponsales.", Namespace = "http://legionelite.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class AG_ESP_ELITE : System.Web.Services.WebService
{

    public AG_ESP_ELITE()
    {



        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }


    [WebMethod(Description = "Compatibiliza el envio a un corresponsal")]
    public string LoadInvoiceAgn_V090(long CodCorresponsal, string ClaveAgente, long Operacion, long ReferenciaGiro, int CodUsuario, string Accion)
    {
        try
        {
            //return "En desarrollo";
            string FILTRO;
            FILTRO = "SELECT TOP 1 Facturas.FraID, Facturas.FraReferenciaCo, Facturas.FraARecibir, Facturas.FraDolares, Facturas.FraTasa, Facturas.FraMonDPago, Facturas.FraMonPagoISO, ";
            FILTRO += "                       Facturas.FraMensaje, Facturas.FraModoPago, Facturas.FraCtaBenef, Facturas.FraAgenciaPago, Facturas.FraClave, Facturas.FraTags, Facturas.FraStatus, OrdenesPago.Op_Fecha, ";
            FILTRO += "                       Clientes.CliNumero, Clientes.CliNombre, Clientes.CliApellidos, Clientes.CliTipoDOC, Clientes.CliDNI, Clientes.CliDomicilio, Clientes.CliPortal, Clientes.CliTelefono, ";
            FILTRO += "                       Clientes.CliTelefono2, hd_Paises_1.Pai_Sufijo3 AS PaisCliente, hd_Paises_1.pai_Sufijo AS PaisClienteS2, Clientes.CliProvincia, Clientes.CliCP, Clientes.CliCiudad, ";
            FILTRO += "                       Clientes.CliPisoPuerta, Clientes.CliFechaNacimiento, Beneficiarios.BnfNombre, Beneficiarios.BnfApellidos, Beneficiarios.BnfDireccion, Beneficiarios.BnfBarrio, ";
            FILTRO += "                       Beneficiarios.BnfTeléfono, Beneficiarios.BnfTelefono2, Beneficiarios.BnfPais, Ciudades.CiuCiudad, hd_Paises.Pai_Sufijo3, hd_Paises.pai_Sufijo AS PaisBenefSufijo2,";
            FILTRO += "                        hd_Paises.pai_MonedaBDE, Corresponsales.CorrNumero, Corresponsales.CorrNombre, Corresponsales.CorrAgente, Corresponsales.CorrDependencia, ";
            FILTRO += "                       Corresponsales.CorrEmailGiros, Corresponsales.CorrRelacionMoneda, Corresponsales.CorrFormatoFichero, Corresponsales.CorrClaveCifrado, ";
            FILTRO += "                       Corresponsales.CorrAdicionalMailText, Corresponsales.CorrFormatoFichero AS Expr1, Corresponsales.CorrActivo, OficCorresp.OFObs, ";
            FILTRO += "                       hd_ConceptosGiros.cgr_Concepto, UnionClientesBeneficiarios.uniRelacion";
            FILTRO += " FROM         Facturas INNER JOIN";
            FILTRO += "                       OrdenesPago ON Facturas.FraOrdenPago = OrdenesPago.Op_Orden INNER JOIN";
            FILTRO += "                       Corresponsales ON OrdenesPago.Op_Corr = Corresponsales.CorrNumero INNER JOIN";
            FILTRO += "                       Clientes ON Facturas.FraCliNumero = Clientes.CliNumero INNER JOIN";
            FILTRO += "                       Beneficiarios ON Facturas.FraBnfNumero = Beneficiarios.BnfNumero INNER JOIN";
            FILTRO += "                       Ciudades ON Beneficiarios.BnfProvincia = Ciudades.CiuNumero INNER JOIN";
            FILTRO += "                       hd_Paises ON Beneficiarios.BnfPais = hd_Paises.pai_CodElite INNER JOIN";
            FILTRO += "                       hd_Paises AS hd_Paises_1 ON Clientes.ClPais = hd_Paises_1.pai_CodElite INNER JOIN";
            FILTRO += "                       hd_ConceptosGiros ON Facturas.FraConcepto = hd_ConceptosGiros.cgr_ID LEFT OUTER JOIN";
            FILTRO += "                       UnionClientesBeneficiarios ON Beneficiarios.BnfNumero = UnionClientesBeneficiarios.uniBeneficiario AND ";
            FILTRO += "                       Clientes.CliNumero = UnionClientesBeneficiarios.uniCliente LEFT OUTER JOIN";
            FILTRO += "                       OficCorresp ON Facturas.FraAgenciaPago = OficCorresp.OFId";
            FILTRO += $" WHERE     (Facturas.FraID = {Operacion}) And (Facturas.FraCorresponsal = {CodCorresponsal}) And (Facturas.FraReferenciaCo = {ReferenciaGiro}) AND (FraStatus in(7,8))";
            DataRow DR = DataAcceso.GetDataRowFromSQL(CodUsuario, FILTRO);
            if ((DR == null))
            {
                return "1201 - No se ha podido abrir la operacion en la base de datos";
            }

            string TAGS = "";
            if (!DR.IsNull("FraTags")) {
                TAGS = (string)DR["FraTags"];
            }
            if (TAGS.Contains("RCOK"))
            {
                return "Recibida";
            }

            //if (!DBNull.Value.Equals(DR["FraStatus"]))
            //    return "Algo";
            //else
            //    return "Otra cosa";

            if ((int)DR["FraStatus"] == 7 || (int)DR["FraStatus"] == 8)
            { }
            else
            { return "1216 - Remesa no está en el status esperado"; }
                

            //Aca aplicar la lógica de la transmision





            return ("000 - Ha salido OK!");
            

        }
        catch (Exception ex)
        {
            Utilidades.GrabarLogEnDisco($"CodCorresponsal: {CodCorresponsal}, ClaveAgente {ClaveAgente}, Operacion: {Operacion}" + Operacion + ", EX: " + ex.Message, "LoadInvoiceAgn_V090");
            return "1167 - Error general de aplicacion, EX: " + ex.Message;
        }
    }

    [WebMethod(Description = "Obtiene una lista conmpatible con LE con la informacion de los giros pagados y con incidencia entre dos referencias")]
    public string GetPayByRef_V090(long CodCorresponsal, string ClaveAgente, long Operacion, long ReferenciaGiro, int CodUsuario)
    // Este metodo es para retornar la informacion de un giro, no necesariamente la informacion del pagado o la información disponible
    {
        try
        {
            
            return "En desarrollo";

            //Ejemplo de pagado
            //return "0;20240605;P;CEDULA:ABC123456";
            //Ejemplo de incidencia
            //return "0;20240605;R;PAGO RETRASADO";

        }
        catch (Exception ex)
        {
            Utilidades.GrabarLogEnDisco($"CodCorresponsal: {CodCorresponsal}, ClaveAgente: {ClaveAgente}, Operacion: {Operacion}, ReferenciaGiro: {ReferenciaGiro}, EX: {ex.Message}", "GetPayByRef_V090");
            return "6 - Error Desconocido : " + ex.Message;
        }
    }

    [WebMethod(Description = "Obtiene una lista de paises disponibles")]
    public DataTable GetCountryList_V090(long CodCorresponsal, string ClaveAgente, string CountryISO3, int CodUsuario)
    {
        try
        {
            // Este debe retornar la lista de los paises de cobertura del corresponsal
            // Debe tener un formato especifico, tambien opcionalmente debe retornar PAIS/CORRESPONSAL

            // Return "1201" 'No se ha podido abrir la operacion en la base de datos

            DataTable DT = new DataTable("Paises");
            {
                var withBlock = DT.Columns;
                withBlock.Add("pai_CodElite", typeof(int));
                withBlock.Add("Pai_Sufijo3", typeof(string));
                withBlock.Add("pai_NombreESP", typeof(string));
                withBlock.Add("CorrNumero", typeof(int));
                withBlock.Add("CorrNombre", typeof (string));
            }
            DataRow nRow = DT.NewRow();
            nRow["pai_CodElite"] = 218;
            nRow["Pai_Sufijo3"] = "ECU";
            nRow["pai_NombreESP"] = "ECUADOR";
            nRow["CorrNumero"] = 0; // Siempre 0
            nRow["CorrNombre"] = "TEST";
            DT.Rows.Add(nRow);

            return DT;

            //return Utilidades.DevuelveErrorTable(1201, "1201 - No se ha podido obtener la lista de paises");
        } 
        catch (Exception ex)
        {
            Utilidades.GrabarLogEnDisco($"CodCorresponsal: {CodCorresponsal}, ClaveAgente: {ClaveAgente}, CountryISO3: {CountryISO3}, EX: {ex.Message}", "GetCountryList_V090");
            return Utilidades.DevuelveErrorTable(1201, "6 - Error Desconocido: " + ex.Message);
        }
    }

    [WebMethod(Description = "Obtiene una lista de puntos de pago disponibles de un pais y opcionalmente de un corresponsal")]
    public DataTable GetPaymentPointList_V090(long CodCorresponsal, string ClaveAgente, string CountryISO3, string Corresponsal, int CodUsuario)
    {
        try
        {
            // Return Utilidades.DevuelveErrorTable(9, "DUMNNI, use only for compatibility")

            int ctarow = 0;
            DataTable RDT = new DataTable("Result");
            {
                var withBlock = RDT.Columns;
                withBlock.Add("Pais", typeof(string));
                withBlock.Add("Ciudad", typeof(string));
                withBlock.Add("Direccion", typeof(string));
                withBlock.Add("MasDatos", typeof(string));
                withBlock.Add("Poblado", typeof(string));
                withBlock.Add("Telefono1", typeof(string));
                withBlock.Add("Telefono2", typeof(string));
                withBlock.Add("Retorno", typeof(string));
                withBlock.Add("HorarioLV", typeof(string));
                withBlock.Add("HorarioS", typeof(string));
                withBlock.Add("HorarioD", typeof(string));
                withBlock.Add("Delegacion", typeof(string));
            }

            DataRow nRow = RDT.NewRow();
            ctarow += 1;
            nRow["Pais"] = "ECU";
            nRow["Ciudad"] = "QUITO";
            nRow["Direccion"] = "Av la Coruña y San Ignacio";
            nRow["MasDatos"] = "Smart Help Desk SAS"; // Mas datos
            nRow["Poblado"] = ""; // Poblado
            nRow["Telefono1"] = "";
            nRow["Telefono2"] = ""; // Telefono2
            nRow["Retorno"] = ""; // & CStr(ctarow).PadLeft(3, "0")  'Retorno
            nRow["HorarioLV"] = ""; // Horario de lunes a viernes
            nRow["HorarioS"] = ""; // Horario de sabado
            nRow["HorarioD"] = ""; // Horario de domingo
            nRow["Delegacion"] = "EXAMPLE"; // Delegacion
            RDT.Rows.Add(nRow);

            return RDT;

        }
        catch(Exception ex)
        {
            Utilidades.GrabarLogEnDisco($"CodCorresponsal: {CodCorresponsal}, ClaveAgente: {ClaveAgente}, CountryISO3: {CountryISO3}, Corresponsal: {Corresponsal}, EX: {ex.Message}", "GetPaymentPointList_V090");
            return Utilidades.DevuelveErrorTable(1201, "6 - Error Desconocido: " + ex.Message);
        }
    }

    [WebMethod(Description = "Solicita la anulacion de una remesa")]
    public string PerformRemittanceAnnulment_V090(long CodCorresponsal, string ClaveAgente, long Operacion, long ReferenciaGiro, int CodUsuario, string Motivo)
    {
        try
        {
            return "1303";
        }
        catch (Exception ex)
        {
            return ("6 - Desconocido : " + ex.Message);
        }

    }

}


