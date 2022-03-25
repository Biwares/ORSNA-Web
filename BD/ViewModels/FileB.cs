using Microsoft.AspNetCore.Http;

namespace BD.ViewModels
{
    public class FileB
    {
        public string id { get; set; }
        public IFormFile archivo { get; set; }
        public int idEntidad { get; set; }
        public string tipoAnexo { get; set; }
    }
}
