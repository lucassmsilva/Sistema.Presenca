﻿using Sistema.Core.Dominio.Models;


namespace Sistema.Core.Dominio.DTO.Pessoa
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }        
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static PessoaDTO FromEntity(PessoaModel pessoa)
        {
            return new()
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Cpf = pessoa.Cpf,
                DataNascimento = pessoa.DataNascimento,
                Telefone = pessoa.Telefone,
                Email = pessoa.Email,
            };
        }
    }
}