using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using DBAccess;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;

namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        WebClient client = new WebClient();
        string apiUrl, id, json;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridView();
            }

        }
        public void PopulateGridView()
        {
             apiUrl = "http://localhost:57771/api/Wychowawca";

            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            json = client.DownloadString(apiUrl);

            var obj = JsonConvert.DeserializeObject<List<Wychowawca>>(json);
            
            if (obj.Count > 0)
            {
                gvWychowawca.DataSource = obj;
                gvWychowawca.DataBind();

            }
            gvWychowawca.Visible = true;
            gvKlasa.Visible = false;
            gvUczniowie.Visible = false;
            
        }

        protected void gvWychowawca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvWychowawca, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
            
        }

        protected void gvWychowawca_SelectedIndexChanged(object sender, EventArgs e)
        {


            id = (gvWychowawca.SelectedRow.FindControl("IdW") as Label).Text;


                    apiUrl = "http://localhost:57771/api/Klasa";

                    json = client.DownloadString(apiUrl + "/" + id);



                    var obj = JsonConvert.DeserializeObject<List<Klasa>>(json);
                    gvKlasa.DataSource = obj;
                    gvKlasa.DataBind();
                    gvWychowawca.Visible = false;
                    gvKlasa.Visible = true;
                    gvUczniowie.Visible = false;
                
            
            
                
                //if(row.RowIndex > gvWychowawca.SelectedIndex)
                //{
                //    lblErrorMessage.Text = "Haven't equals record";
                //    lblSuccessMessage.Text = "";
                //}
            
            

            
        }

        protected void gvUczniowie_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUczniowie, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void gvUczniowie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGridView();
        }

        protected async void gvWychowawca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddW")
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        apiUrl = "http://localhost:57771/api/Wychowawca";
                        string data = "{'Imie': '" + (gvWychowawca.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvWychowawca.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() + "'}";
                        var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, contentString);
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                        PopulateGridView();

                    }

                }
                catch(Exception ex)
                {
                    lblSuccessMessage.Text = "";
                    lblErrorMessage.Text = ex.Message;
                }
             }
              }

        protected void gvWychowawca_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWychowawca.EditIndex = e.NewEditIndex;
            
            PopulateGridView();
            
        }

        protected void gvWychowawca_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string imie = (gvWychowawca.SelectedRow.FindControl("txtId") as TextBox).Text;
        }

        protected void gvKlasa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvKlasa, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void gvKlasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(GridViewRow row in gvKlasa.Rows)
            {
                if (row.RowIndex == gvKlasa.SelectedIndex)
                {
                     id = ((Label)row.FindControl("IdK")).Text;
                    apiUrl = "http://localhost:57771/api/Uczniowie";

                    json = client.DownloadString(apiUrl + "/" + id);

                    var obj = JsonConvert.DeserializeObject<List<Uczniowie>>(json);
                    gvUczniowie.DataSource = obj;
                    gvUczniowie.DataBind();
                    gvWychowawca.Visible = false;
                    gvKlasa.Visible = false;
                    gvUczniowie.Visible = true;
                }
            }
        }
    }
}