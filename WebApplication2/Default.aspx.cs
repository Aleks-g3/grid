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
        int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //PopulateGridView();
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
            

                apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Wychowawca";
            i = 0;
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            json = client.DownloadString(apiUrl);

            var obj = JsonConvert.DeserializeObject<List<Wychowawca>>(json);

            
                gvWychowawca.DataSource = obj;
                gvWychowawca.DataBind();
           


            gvWychowawca.Visible = true;
            gvKlasa.Visible = false;
            gvUczniowie.Visible = false;
            gvKlasa.Columns[0].Visible = true;
            Button button = gvWychowawca.Rows[0].FindControl("DeleteW") as Button;
            if (obj.Count == 1)
            {
                button.Enabled = false;
            }
            else
            {
                button.Enabled = true;
            }
            
        }

       

        protected void gvWychowawca_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            id = (gvWychowawca.SelectedRow.FindControl("IdW") as Label).Text;


                    apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Klasa";

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
        
        protected void gvWychowawca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "AddW")
                {
                
                    try
                    {
                    
                 
                            HiddenW.Value = "{'Imie': '" + (gvWychowawca.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvWychowawca.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() + "'}";
                            

                        


                        if ((gvWychowawca.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() != string.Empty && (gvWychowawca.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim()!=string.Empty)
                        { 
                            lblSuccessMessage.Text = "";
                            lblErrorMessage.Text = "";

                           apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Klasa";

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
                    apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Wychowawca/" + (gvWychowawca.Rows[e.RowIndex].FindControl("txtId") as Label).Text;
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
        
        protected void gvKlasa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddK")
            {
                try
                {

                   
                        HiddenK.Value = "{'Nazwa': '" + (gvKlasa.FooterRow.FindControl("txtNazwaFooter") as TextBox).Text.Trim() + "'}";
                        
                        if ((gvKlasa.FooterRow.FindControl("txtNazwaFooter") as TextBox).Text.Trim() != string.Empty)
                        {
                           
                            lblSuccessMessage.Text = "";
                            lblErrorMessage.Text = "";

                            apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";

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
                    if (i == 0)
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {

                            

                            HttpClient httpClient1 = new HttpClient();

                            string apiUrlW = "https://webappzaliczenie.azurewebsites.net/api/Wychowawca";
                            StringContent contentStringW = new StringContent(HiddenW.Value, UnicodeEncoding.UTF8, "application/json");
                            await httpClient1.PostAsync(apiUrlW, contentStringW);



                            HttpClient httpClient2 = new HttpClient();

                            string apiUrlK = "https://webappzaliczenie.azurewebsites.net/api/Klasa";
                            StringContent contentStringK = new StringContent(HiddenK.Value, UnicodeEncoding.UTF8, "application/json");
                            await httpClient2.PostAsync(apiUrlK, contentStringK);


                            apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";
                            string data = "{'Imie': '" + (gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() + "'}";
                            var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                            await httpClient.PostAsync(apiUrl, contentString);
                            if ((gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() != string.Empty && (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() != string.Empty)
                            {
                                lblSuccessMessage.Text = "New Record Added";
                                lblErrorMessage.Text = "";

                                apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";

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

                            i = 1;
                        }
                    }
                    if (i == 1)
                    {
                        using (HttpClient httpClient = new HttpClient())
                       {
                            apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";
                             string data = "{'Imie': '" + (gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() + "','Nazwisko': '" + (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() + "'}";
                             var contentString = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                             await httpClient.PostAsync(apiUrl, contentString);
                            if ((gvUczniowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim() != string.Empty && (gvUczniowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim() != string.Empty)
                            {
                                lblSuccessMessage.Text = "New Record Added";
                                lblErrorMessage.Text = "";

                                apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";

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
                  
                    apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Wychowawca/" + (gvWychowawca.Rows[e.RowIndex].FindControl("IdW") as Label).Text;
                   
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
            apiUrl = "https://webappzaliczenie.azurewebsites.net/api/Uczniowie";

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