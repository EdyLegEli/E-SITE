using System;
using System.Activities.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml;

//using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;

/// <summary>
/// Descripción breve de Utilidades
/// </summary>
public class Utilidades
{
    public Utilidades()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static void GrabarLogEnDisco(string TextoSAVE, string NombreFile)
    {
        try
        {
            StreamWriter sw;
            string dd = HttpContext.Current.Server.MapPath("~");
            if (dd.EndsWith(@"\") == false) { dd += @"\"; }
            dd += @"Conf\LOG\" + DateTime.Now.ToString("yyyy-MM");

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dd);
            if (dir.Exists == false)
                dir.Create();
            string fileName;
            fileName = dd + @"\" + NombreFile + ".resources";
            if (File.Exists(fileName))
                // sobreescribir el texto (no añadir al final)
                sw = new StreamWriter(fileName, true);
            else
                // crear un archivo vacío
                sw = File.CreateText(fileName);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +  "\t" + TextoSAVE);
            sw.Close();
        }
        catch (Exception ex)
        {
        }
    }
    public static DataTable DevuelveErrorTable(long erg_Id, string erg_DetalleError)
    {
        DataTable dt = new DataTable("hd_Errores");
        {
            var withBlock = dt.Columns;
            withBlock.Add("erg_Id", typeof(string));
            withBlock.Add("erg_DetalleError", typeof(string));
        }
        DataRow nRow = dt.NewRow();
        nRow["erg_Id"] = erg_Id;
        nRow["erg_DetalleError"] = erg_DetalleError;
        dt.Rows.Add(nRow);
        return dt;
    }

}



[System.Diagnostics.DebuggerStepThrough()]
public class Config
{
    // Gets the resource you want. If you are in DEBUG mode and the key isn't there, this will throw an exception
    [System.Diagnostics.DebuggerStepThrough()]
    public static string GetItem(string key)
    {

        Hashtable messages = GetResource();

        if (messages[key] == null)
            messages[key] = string.Empty;

        string msgRet = Convert.ToString(messages[key]);

        return msgRet;
    }

    [System.Diagnostics.DebuggerStepThrough()]
    private static Hashtable GetResource()
    {
        string currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        string cacheKey = "SystemConfig";

        if (HttpRuntime.Cache.Get(cacheKey) == null)
        {
            Hashtable resource = new Hashtable();
            LoadResource(resource, cacheKey);
        }

        return (Hashtable)HttpRuntime.Cache.Get(cacheKey);

    }

    [System.Diagnostics.DebuggerStepThrough()]
    private static void LoadResource(Hashtable resource, string cacheKey)
    {

        if (HttpContext.Current == null)
            return;

        string file = HttpContext.Current.Server.MapPath("~/Conf/config.xml.resources");
        XmlDocument xml = new XmlDocument();
        xml.Load(file);

        foreach (XmlNode node in xml.SelectSingleNode("config"))
        {
            if (node.NodeType != XmlNodeType.Comment)
                resource[node.Attributes["name"].Value] = node.InnerText;
        }

        HttpRuntime.Cache.Insert(cacheKey, resource, new System.Web.Caching.CacheDependency(file), DateTime.MaxValue, TimeSpan.Zero);
    }

    public static bool SetItem(string key, string value)
    {
        try
        {
            string file = HttpContext.Current.Server.MapPath("~/Conf/config.xml.resources");

            // Hacer copia de seguridad
            if (File.Exists(file) == false)
                return false;

            string RBK = file + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhMMss");
            File.Copy(file, RBK, true);
            // Fin de hacer copia de seguridad

            XmlDocument xml = new XmlDocument();
            xml.Load(file);
            foreach (XmlNode n in xml.SelectSingleNode("config"))
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    if (n.Attributes["name"].Value == key)
                    {
                        n.InnerText = value;
                        xml.Save(file);
                        return true;
                    }
                }
            }
            XmlNode nnode = xml.CreateElement("item");
            XmlAttribute anode = xml.CreateAttribute("name");
            anode.Value = key;
            nnode.Attributes.Append(anode);
            nnode.InnerText = value;
            xml.SelectSingleNode("config").AppendChild(nnode);
            xml.Save(file);

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }
}
