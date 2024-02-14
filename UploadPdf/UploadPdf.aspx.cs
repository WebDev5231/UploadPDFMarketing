using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using System.Linq;
using System.Web.UI.WebControls;
using UploadPdf.Models;

namespace UploadPdf
{
    public partial class UploadPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    if (Path.GetExtension(fileUpload.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        string originalFileName = fileUpload.FileName;
                        string cleanedFileName = LimparNomeArquivo(originalFileName);

                        PdfMarketing pdf = new PdfMarketing { NomeArquivo = cleanedFileName, DataUpload = DateTime.Now };

                        InserirDadosNoBanco(pdf);

                        string uploadPath = Server.MapPath("~/../../uploads/PDF/") + cleanedFileName;
                        fileUpload.SaveAs(uploadPath);

                        lblFileName.Text = $"Arquivo Enviado com sucesso: {originalFileName}";
                        BindGridView();
                    }
                    else
                    {
                        lblFileName.Text = "Por favor, selecione um arquivo PDF.";
                    }
                }
                catch (Exception ex)
                {
                    lblFileName.Text = $"Erro ao fazer upload do arquivo. {ex.Message}";
                }
            }
        }

        protected void GridViewFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    string fileName = e.CommandArgument.ToString();
                    ExcluirArquivoDoBanco(fileName);
                }
                catch (Exception ex)
                {
                    lblFileName.Text = $"Erro no GridViewFiles_RowCommand: {ex.Message}";
                }

                BindGridView();
            }
        }

        //FINALIZAR O METODO DELETE
        protected void GridViewFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string fileName = e.Values["NomeArquivo"].ToString();
                ExcluirArquivoDoBanco(fileName);
            }
            catch (Exception ex)
            {
                lblFileName.Text = $"Error in GridViewFiles_RowDeleting: {ex.Message}";
            }

            BindGridView();
        }

        private void BindGridView()
        {
            List<PdfMarketing> files = ObterArquivosDoBanco();
            GridViewFiles.DataSource = files;
            GridViewFiles.DataBind();
        }

        private List<PdfMarketing> ObterArquivosDoBanco()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Anfir"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT NomeArquivo, DataUpload FROM PdfMarketing";
                return connection.Query<PdfMarketing>(query).ToList();
            }
        }

        private void InserirDadosNoBanco(PdfMarketing pdf)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Anfir"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO PdfMarketing (NomeArquivo, DataUpload) VALUES (@NomeArquivo, @DataUpload)";
                connection.Execute(query, pdf);
            }
        }

        private void ExcluirArquivoDoBanco(string fileName)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Anfir"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM PdfMarketing WHERE NomeArquivo = @FileName";
                    int rowsAffected = connection.Execute(query, new { FileName = fileName });

                    if (rowsAffected > 0)
                    {
                        string filePath = Server.MapPath("~/../../uploads/PDF/") + fileName;
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        else
                        {
                            Console.Write("Arquivo não existe");
                        }

                        BindGridView();
                    }
                    else
                    {
                        Console.Write("Arquivo não foi deletado");
                    }
                }
            }
            catch (Exception ex)
            {
                lblFileName.Text = $"Erro ao excluir o arquivo. {ex.Message}";
            }
        }

        private string LimparNomeArquivo(string originalFileName)
        {
            return originalFileName.Replace(" ", "-");
        }
    }
}
