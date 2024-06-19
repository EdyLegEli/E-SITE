using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de DataAcceso
/// </summary>
public class DataAcceso
{
    public DataAcceso()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static DataTable GlobalClassV001(long codUsuario, string FuncionName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6)
    {
        WCOMP.Compatible ws = new WCOMP.Compatible();
        DataTable dt = new DataTable();
        dt = ws.GlobalClassV001(codUsuario, FuncionName, Param1, Param2, Param3, Param4, Param5, Param6);

        return dt;
    }

    public static DataSet GlobalClassV002(long codUsuario, string FuncionName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6)
    {
        WCOMP.Compatible WS = new WCOMP.Compatible();
        DataSet ds = new DataSet();
        ds = WS.GlobalClassV002(codUsuario, FuncionName, Param1, Param2, Param3, Param4, Param5, Param6);

        return ds;
    }

    public static string GlobalClassV003(long codUsuario, string FuncionName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6)
    {
        try
        {
            WCOMP.Compatible ws = new WCOMP.Compatible();
            string res;
            res = ws.GlobalClassV003(codUsuario, FuncionName, Param1, Param2, Param3, Param4, Param5, Param6);

            return res;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static bool GlobalClassV004(long codUsuario, string FuncionName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6)
    {
        WCOMP.Compatible ws = new WCOMP.Compatible();
        bool res;
        res = ws.GlobalClassV004(codUsuario, FuncionName, Param1, Param2, Param3, Param4, Param5, Param6);

        return res;
    }

    public static DataTable GetDataFromSQL(long codUsuario, string SQLStaintment)
    {
        // Esto para lo que sirve es para obtener un datatable desde un SQL
        DataTable dt = new DataTable();
        string FLTR = Convert.ToBase64String(Encoding.UTF8.GetBytes(SQLStaintment));

        dt = GlobalClassV001(codUsuario, "GetFullData", FLTR, "LE", "BODESKTOP", "", "", "");

        return dt;
    }

    public static DataRow GetDataRowFromSQL(long codUsuario, string filtro)
    {
        try
        {
            DataTable dt = GetDataFromSQL(codUsuario, filtro);

            if (dt == null)
            {
                return null;
            }

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return dt.Rows[0];
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static bool SaberSiExisteRow(long codUsuario, string sqlStaintment)
    {
        DataTable dt = new DataTable();

        string FLTR = Convert.ToBase64String(Encoding.UTF8.GetBytes(sqlStaintment));

        dt = GlobalClassV001(codUsuario, "GetFullData", FLTR, "LE", "BODESKTOP", "", "", "");

        if (dt == null)
        {
            return false;
        }

        if (dt.Rows.Count == 0)
        {
            return false;
        }

        return true;
    }

    public static bool ExecuteSql(long codUsuario, string SqlStaintment)
    {
        string FLTR = Convert.ToBase64String(Encoding.UTF8.GetBytes(SqlStaintment));

        return GlobalClassV004(codUsuario, "ExcecFullData", FLTR, "LE", "BODESKTOP", "", "", "");
    }

    public static string Leer_Global(long codUsuario, string SECCION, string CAMPO, string VDEFAULT)
    {
        return GlobalClassV003(codUsuario, "Leer_Configuracion_Global", SECCION, CAMPO, VDEFAULT, "", "", "");
    }

    public static string Grabar_Global(long codUsuario, string SECCION, string CAMPO, string VALOR)
    {
        return GlobalClassV003(codUsuario, "Grabar_Configuracion_Global", SECCION, CAMPO, VALOR, "", "", "");
    }



}