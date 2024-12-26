using LanchesDoTioAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Debt { get; set; }
    }
}
