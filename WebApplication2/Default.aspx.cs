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
using System.IO;

namespace WebApplication2
{

    public partial class Default : System.Web.UI.Page
    {
        WebClient client = new WebClient();
        string apiUrl, id, json;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //PopulateGridView();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SchoolEntities dupa = new SchoolEntities();
                var xd = dupa.Database.Exists();
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
            else
            {
                DataTable dataTable = new DataTable();
                dataTable.Rows.Add(dataTable.NewRow());
                gvWychowawca.DataSource = dataTable;
                gvWychowawca.DataBind();
                gvWychowawca.Rows[0].Cells.Clear();
                gvWychowawca.Rows[0].Cells.Add(new TableCell());
                gvWychowawca.Rows[0].Cells[0].ColumnSpan = dataTable.Columns.Count;
                gvWychowawca.Rows[0].Cells[0].Text = "No data!!";
                gvWychowawca.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }


            gvWychowawca.Visible = true;
            gvKlasa.Visible = false;
            gvUczniowie.Visible = false;

            
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
            Button button = gvKlasa.FooterRow.FindControl("Add") as Button;
            button.Enabled = false;
        }


        protected void gvUczniowie_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSuccessMessage.Text = "";
            lblErrorMessage.Text = "";
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
                            await httpClient.PostAsync(apiUrl, contentString);

                        


                        if ((gvWychowawca.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() != string.Empty && (gvWychowawca.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim()!=string.Empty)
                        { 
                            lblSuccessMessage.Text = "New Record Added";
                            lblErrorMessage.Text = "";

                           apiUrl = "http://localhost:57771/api/Klasa";

                          client.Headers["Content-type"] = "application/json";
                          client.Encoding = Encoding.UTF8;
                          json = client.DownloadString(apiUrl);

                          var obj = JsonConvert.DeserializeObject<List<Klasa>>(json);

                       
                            gvKlasa.DataSource = obj;
                            gvKlasa.DataBind();
                        
                        


                           gvWychowawca.Visible = false;
                          gvKlasa.Visible = true;
                          gvUczniowie.Visible = false;
                            gvKlasa.Columns[0].Visible = false;
                        }
                    }
                    
                    }
                    catch (Exception ex)
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
        protected async void gvWychowawca_RowUpdating(object sender, GridViewUpdateEventArgs e)

       

        {
            
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    apiUrl = "http://localhost:57771/api/Wychowawca/" + (gvWychowawca.Rows[e.RowIndex].FindControl("txtId") as Label).Text;
                    string data = "{'Imie': '" + (gvWychowawca.Rows[e.RowIndex].FindControl("txtImie") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvWychowawca.Rows[e.RowIndex].FindControl("txtNazwisko") as TextBox).Text.Trim() + "'}";
                    var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiUrl, contentString);
                    lblSuccessMessage.Text = " Record Updated";
                    lblErrorMessage.Text = "";
                    gvWychowawca.EditIndex = -1;
                    PopulateGridView();
                }

            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }


        protected void gvWychowawca_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWychowawca.EditIndex = -1;
            PopulateGridView();
        }

        protected async void gvKlasa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddK")
            {
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        apiUrl = "http://localhost:57771/api/Klasa";
                        string data = "{'Nazwa': '" + (gvKlasa.FooterRow.FindControl("txtNazwaFooter") as TextBox).Text.Trim() + "'}";
                        var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, contentString);
                        if ((gvKlasa.FooterRow.FindControl("txtNazwaFooter") as TextBox).Text.Trim() != string.Empty)
                        {
                           
                            lblSuccessMessage.Text = "New Record Added";
                            lblErrorMessage.Text = "";

                            apiUrl = "http://localhost:57771/api/Uczniowie";

                            client.Headers["Content-type"] = "application/json";
                            client.Encoding = Encoding.UTF8;
                            json = client.DownloadString(apiUrl);

                            var obj = JsonConvert.DeserializeObject<List<Uczniowie>>(json);


                            gvUczniowie.DataSource = obj;
                            gvUczniowie.DataBind();




                            gvWychowawca.Visible = false;
                            gvKlasa.Visible = false;
                            gvUczniowie.Visible = true;
                            gvUczniowie.Columns[0].Visible = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblSuccessMessage.Text = "";
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected async void gvUczniowie_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddU")
            {
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        apiUrl = "http://localhost:57771/api/Uczniowie";
                        string data = "{'Imie': '" + (gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() + "'}";
                        var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, contentString);
                        if ((gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() != string.Empty&& (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() != string.Empty)
                        {
                            lblSuccessMessage.Text = "New Record Added";
                            lblErrorMessage.Text = "";

                            apiUrl = "http://localhost:57771/api/Uczniowie";

                            client.Headers["Content-type"] = "application/json";
                            client.Encoding = Encoding.UTF8;
                            json = client.DownloadString(apiUrl);

                            var obj = JsonConvert.DeserializeObject<List<Uczniowie>>(json);


                            gvUczniowie.DataSource = obj;
                            gvUczniowie.DataBind();




                            gvWychowawca.Visible = false;
                            gvKlasa.Visible = false;
                            gvUczniowie.Visible = true;
                            gvUczniowie.Columns[0].Visible = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblSuccessMessage.Text = "";
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }

        protected async void gvWychowawca_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    apiUrl = "http://localhost:57771/api/Wychowawca/" + (gvWychowawca.Rows[e.RowIndex].FindControl("IdW") as Label).Text;
                   
                    var response = await httpClient.DeleteAsync(apiUrl);
                    lblSuccessMessage.Text = " Record Deleted";
                    lblErrorMessage.Text = "";
                    PopulateGridView();
                }

            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }


        protected void gvKlasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                     id = (gvKlasa.SelectedRow.FindControl("IdK") as Label).Text;
                    apiUrl = "http://localhost:57771/api/Uczniowie";

                    json = client.DownloadString(apiUrl + "/" + id);

                    var obj = JsonConvert.DeserializeObject<List<Uczniowie>>(json);
                    gvUczniowie.DataSource = obj;
                    gvUczniowie.DataBind();
                    gvWychowawca.Visible = false;
                    gvKlasa.Visible = false;
                    gvUczniowie.Visible = true;

            Button button = gvUczniowie.FooterRow.FindControl("Add") as Button;
            button.Enabled = false;
        }
    }
}