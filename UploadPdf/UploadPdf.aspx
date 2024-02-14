<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPdf.aspx.cs" Inherits="UploadPdf.UploadPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload de PDF</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <style>
        .bg-black {
            background-color: black !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container mt-5">
            <div class="row" style="text-align: center;">
                <div class="col-md-6 offset-md-3">
                    <h3 class="text-center mb-4"><b>Upload de PDF</b></h3>
                    <div class="mb-3 d-flex justify-content-between">
                        <asp:FileUpload ID="fileUpload" runat="server" accept=".pdf" CssClass="form-control" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-primary" />
                    </div>
                    <div>
                        <asp:Label ID="lblFileName" runat="server" Text="" CssClass="text-center"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row mt-5" style="text-align: center;">
                <div class="col-md-8 offset-md-2">
                    <h3 class="text-center mb-4"><b><u>Arquivos Enviados</u></b></h3>

                    <asp:GridView ID="GridViewFiles" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="GridViewFiles_RowCommand" OnRowDeleting="GridViewFiles_RowDeleting">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="NomeArquivo" DataNavigateUrlFormatString="~/../../uploads/PDF/{0}" DataTextField="NomeArquivo" HeaderText="Nome do Arquivo" Target="_blank" />
                            <asp:BoundField DataField="DataUploadFormatada" HeaderText="Data do Upload" />
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Deletar" CommandName="Delete" CommandArgument='<%# Eval("NomeArquivo") %>' CssClass="btn btn-danger btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="bg-black text-white" />
                    </asp:GridView>

                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.min.js" integrity="sha384-Atwg2Pkwv9vp0ygtn1JAojH0nYbwNJLPhwyoVbhoPwBhjQPR5VtM2+xf0Uwh9KtT" crossorigin="anonymous"></script>
</body>
</html>
