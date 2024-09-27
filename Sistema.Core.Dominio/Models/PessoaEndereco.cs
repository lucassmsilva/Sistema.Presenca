using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class PessoaEndereco : BaseEntity
    {
        public string Logradouro { get; set; } // Renomeado de Rua para Logradouro
        public int Numero { get; set; } // Mudado de long para int
        public string Complemento { get; set; }
        public string Quadra { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; } // Adicionado campo Estado
        public string Cep { get; set; }

        // Construtor
        public PessoaEndereco(string logradouro, int numero, string complemento, string quadra, string bairro, string cidade, string estado, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Quadra = quadra;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }

        // Método ToString para formatação padrão de endereço no Brasil
        public override string ToString()
        {
            var enderecoFormatado = $"{Logradouro}, {Numero}";

            if (!string.IsNullOrWhiteSpace(Complemento))
                enderecoFormatado += $" - {Complemento}";

            if (!string.IsNullOrWhiteSpace(Quadra))
                enderecoFormatado += $" - Quadra {Quadra}";

            enderecoFormatado += $"\n{Bairro}\n{Cidade} - {Estado}\nCEP: {FormatarCep(Cep)}";

            return enderecoFormatado;
        }

        // Método auxiliar para formatar o CEP
        private string FormatarCep(string cep)
        {
            return cep.Length == 8 ? $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}" : cep;
        }
    }
}
