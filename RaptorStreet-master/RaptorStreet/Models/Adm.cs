using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.Xml;

namespace RaptorStreet.Models
{
    public class Adm
    {
        public int IdAdm { get; set; }
        public string NomeAdm { get; set; }
        public string EmailAdm { get; set; }
        public string SenhaAdm { get; set; }
    }
}
