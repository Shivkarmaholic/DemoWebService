using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
/// <summary>
/// Summary description for WebCall
/// </summary>
public class WebCall
{
	public WebCall()
	{
		//
		// TODO: Add constructor logic here
		//


	}
    string connectionStr = "server=DESKTOP-SQKJ3UE\\SQLEXPRESS;database=demodb;Integrated Security=true;";

    internal DataSet getDetails(int id,string name, int price,char ch)
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(connectionStr))
        {

            using (SqlCommand cmd = new SqlCommand("abc_insertupdatdelete", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ch", ch);
                cmd.Parameters.AddWithValue("pid", id);
                cmd.Parameters.AddWithValue("pname",name);
                cmd.Parameters.AddWithValue("pprice",price);


                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
            }
        }
        return ds;        
    }

    internal DataSet Sp_Web_GetBilling_DPU_1_0(long UnitId, int PageNo, long OrgId)
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(connectionStr))
        {

            using (SqlCommand cmd = new SqlCommand("Sp_Web_GetBilling_DPU_1_0", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("a_unitid", UnitId);
                cmd.Parameters.AddWithValue("nopage", PageNo);
                cmd.Parameters.AddWithValue("a_OrgId", OrgId);


                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
            }
        }
        return ds;
    }
}