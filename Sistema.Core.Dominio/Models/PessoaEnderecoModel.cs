﻿namespace Sistema.Core.Dominio.Models
{
    public class PessoaEnderecoModel : BaseEntity
    {
        public string Logradouro { get; set; } // Renomeado de Rua para Logradouro
        public int Numero { get; set; } // Mudado de long para int
        public string Complemento { get; set; } = string.Empty;
        public string Quadra { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;// Adicionado campo Estado
        public string Cep { get; set; } = string.Empty;

        // Construtor
        public PessoaEnderecoModel(string logradouro, int numero, string complemento, string quadra, string bairro, string cidade, string estado, string cep)
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
        public string ToEnderecotring()
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
