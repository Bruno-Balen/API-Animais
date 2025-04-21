using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace API_csv.database.Models
{
    public class Animal
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Reproduction { get; set; }
        public string Feeding { get; set; }
    }
}
