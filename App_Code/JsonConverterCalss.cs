using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using System.Reflection;

/// <summary>
/// Summary description for JsonConverterCalss
/// </summary>

public static class JsonConverterClass
{
    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }
    public static string GetJson(DataTable dt, string method)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }
        string res = serializer.Serialize(rows);
        res = "{\"" + method + "\": " + res + " }";

        return res;
    }

    public static string GetJsonWithBlank(DataTable dt, string method)
    {

        System.Web.Script.Serialization.JavaScriptSerializer

    serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = 2147483644;
        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        // row.Add("{res:", "");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }

        string res = serializer.Serialize(rows);

        if (res == "[]")
            return res = "\"" + method + "\": \"\" ";

        else
            return res = "\"" + method + "\": " + res + " ";


    }

    public static string GetJsonWithBlankIndianCurrency(DataTable dt, string method)
    {

        System.Web.Script.Serialization.JavaScriptSerializer

    serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        // row.Add("{res:", "");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                if (dr[col].GetType() == typeof(System.Int16))
                    row.Add(col.ColumnName.Trim(), getIndianCurrency(dr[col].ToString()));
                else if (dr[col].GetType() == typeof(System.Int32))
                    row.Add(col.ColumnName.Trim(), getIndianCurrency(dr[col].ToString()));
                else if (dr[col].GetType() == typeof(System.Int64))
                    row.Add(col.ColumnName.Trim(), getIndianCurrency(dr[col].ToString()));
                else if (dr[col].GetType() == typeof(System.Decimal))
                    row.Add(col.ColumnName.Trim(), getIndianCurrency(dr[col].ToString()));
                else
                    row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }

        string res = serializer.Serialize(rows);

        if (res == "[]")
            return res = "\"" + method + "\": \"\" ";

        else
            return res = "\"" + method + "\": " + res + " ";


    }

    public static string getIndianCurrency(string num)
    {
        decimal parsed = decimal.Parse(num, CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("hi-IN");
        string text = string.Format(hindi, "{0:c}", parsed);
        return text.Substring(0, text.Length - 3);
    }

    public static string GetJson1(DataTable dt, string method)
    {

        System.Web.Script.Serialization.JavaScriptSerializer

    serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        // row.Add("{res:", "");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }
        //string res="";
        //res = String.Join("res:",rows.ToArray());
        string res = serializer.Serialize(rows);

        res = "\"" + method + "\": " + res;
        return res;
    }

    public static string GetJsonWithBlankTable(DataSet dt, string method)
    {

        System.Web.Script.Serialization.JavaScriptSerializer

    serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        // row.Add("{res:", "");
        //foreach (DataRow dr in dt.Tables[0].Rows)
        //{
        //    row = new Dictionary<string, object>();

        //    foreach (DataColumn col in dt.Tables[0].Columns)
        //    {
        //        row.Add(col.ColumnName.Trim(), dr[col]);
        //    }
        //    foreach (DataColumn col in dt.Tables[1].Columns)
        //    {
        //        row.Add(col.ColumnName.Trim(), dr[col]);
        //    }
        //    rows.Add(row);
        //}
        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        {
            row = new Dictionary<string, object>();

            //foreach (DataColumn col in dt.Tables[0].Columns)
            for (int x = 0; x < dt.Tables.Count; x++)
            {
                for (int j = 0; j < dt.Tables[x].Columns.Count; j++)
                {
                    row.Add(dt.Tables[x].Columns[j].ColumnName.Trim(), dt.Tables[x].Rows[i][0]);
                }
                //for (int j = 0; j < dt.Tables[1].Columns.Count; j++)
                //{
                //    row.Add(dt.Tables[1].Columns[j].ColumnName.Trim(), dt.Tables[1].Rows[i]);
                //}
            }
            rows.Add(row);
        }

        string res = serializer.Serialize(rows);

        if (res == "[]")
            return res = "\"" + method + "\": \"\" ";

        else
            return res = "\"" + method + "\": " + res + " ";


    }

    public static string GetJsonWithPhoto(DataTable dt, string method)
    {

        System.Web.Script.Serialization.JavaScriptSerializer

    serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        // row.Add("{res:", "");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.Trim() == "photo")
                    row.Add(col.ColumnName.Trim(), Convert.ToBase64String((byte[])dr[col]));
                else
                    row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }

        string res = serializer.Serialize(rows);

        if (res == "[]")
            return res = "\"" + method + "\": \"\" ";

        else
            return res = "\"" + method + "\": " + res + " ";


    }

    public static string ConvertStringToJson(string msg, string method)
    {
        string jsonString = "";
        try
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            jsonString = javaScriptSerializer.Serialize(msg);
            jsonString = "\"" + "Msg" + "\": " + jsonString;
            jsonString = "\"" + method + "\":  [{ " + jsonString + "}]";
            return jsonString;
        }
        catch
        {
            //throw;
        }
        return jsonString;
    }



}
