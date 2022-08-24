using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    

        [System.Web.Services.WebMethod]
    public string getDetails(int id,string name, int price,char ch)
    {
        string response = "", response1 = "";
        DataSet ds = new DataSet();
        try
        {

            WebCall wc = new WebCall();
            ds = wc.getDetails(id, name,price,ch);

            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    response1 = JsonConverterClass.GetJson(ds.Tables[0], "data");
                   

                    response = "{" + response1 + "}";
                    return response;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
                
    }


    [System.Web.Services.WebMethod]
    public string GetBilling_DPU_1_0(long UnitId, int PageNo)
    {
        string response = "", response1 = "", response2 = "";

        long OrgId = HttpContext.Current.Session["OrgId"] == null ? 0 : long.Parse(HttpContext.Current.Session["OrgId"].ToString());
        OrgId = 1;
        if (OrgId == 0)
        {
            response1 = "\"data\":\"" + "sessionTimeOut" + "\"";
            response = "{" + response1 + "}";
            return response;
        }
        else
        {
            DataSet ds = new DataSet();
            try
            {

                WebCall wc = new WebCall();
                ds = wc.Sp_Web_GetBilling_DPU_1_0(UnitId, PageNo, OrgId);

                DataTable dt = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response1 = JsonConverterClass.GetJsonWithBlank(ds.Tables[0], "data");
                        response2 = JsonConverterClass.GetJsonWithBlank(ds.Tables[1], "data1");

                        response = "{" + response1 + "," + response2 + "}";
                        return response;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }


}