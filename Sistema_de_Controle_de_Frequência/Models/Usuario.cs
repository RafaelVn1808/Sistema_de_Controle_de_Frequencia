﻿using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFrequencia.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [Required]
        public string Perfil { get; set; }
    }
}
