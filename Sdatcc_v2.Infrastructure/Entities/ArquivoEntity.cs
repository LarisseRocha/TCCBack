using System.ComponentModel.DataAnnotations;

namespace Sdatcc_v2.Infrastructure.Entities
{
    public class ArquivoEntity
    {
        [Key]
        public int Id { get; set; }
        public string CaminhoArquivo { get; set; }
        public string NomeOriginal { get; set; }
        public string GuidArquivo { get; set; }
    }
}
