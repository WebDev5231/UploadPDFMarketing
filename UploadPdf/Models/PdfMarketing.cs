using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadPdf.Models
{
    [Table("PdfMarketing")]
    public class PdfMarketing
    {
        [Key]
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataUpload { get; set; }

        [NotMapped]
        public string DataUploadFormatada => DataUpload.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
