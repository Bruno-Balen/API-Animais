using API_csv.database;
using API_csv.database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_csv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisControllers : ControllerBase
    {
        private readonly DBContext _dBContext;

        public AnimaisControllers(DBContext dBContext) {

            _dBContext = dBContext;
        }

        //MÉTODO GET RETORNANDO TODOS OS ANIMAIS DA LISTA
        [HttpGet]
        public ActionResult<List<Animal>> GetAll(){

            return Ok(_dBContext.Animais);
        }


        //MÉTODO GET BUSCANDO POR ID
        [HttpGet("{id}")]
        public ActionResult<Animal> GetById(int id)
        {
            Animal animal = _dBContext.Animais.Find(a => a.Id == id);
            if (animal == null)
            {
                return NotFound($"Animal com ID {id} não encontrado.");
            }

            return Ok(animal);
        }


        //MÉTODO DELETE
        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id) 
        {
            Animal animal = _dBContext.Animais.Find(a => a.Id == id);

           if (animal == null)
            {
                return NotFound();
            }

            _dBContext.Animais.Remove(animal);

            return NoContent();
        }

        
        //MÉTODO PATCH
        [HttpPatch("AlterarNome")]
        public ActionResult<Animal> UpdateName([FromBody] Animal boddy)
        {
            Animal animal = _dBContext.Animais.Find(a => a.Id == boddy.Id);

            if (animal == null || string.IsNullOrEmpty(boddy.Name))
            {
                return NotFound();
            }

            animal.Name = boddy.Name;

            return Ok(animal);
        }


        //MÉTODO POST
        [HttpPost("InserirAnimal")]
        public ActionResult<Animal> PostAnimal([FromBody] Animal animal)
        {
            if (animal == null)
            {
                return BadRequest();
            }

            // Validação dos campos obrigatórios, ignorando o ID
            foreach (var property in typeof(Animal).GetProperties())
            {
                if (property.Name == "Id") continue;

                var value = property.GetValue(animal);
                if (value == null)
                {
                    return BadRequest("Informações necessárias não foram informadas.");
                }
            }

            // Gerar ID automaticamente
            animal.Id = _dBContext.Animais.Any() ? _dBContext.Animais.Max(a => a.Id) + 1 : 1;

            _dBContext.Animais.Add(animal);

            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }


        //MÉTODO PUT
        [HttpPut("{id}")]
        public ActionResult<Animal> PutAnimal(int id, [FromBody] Animal animal)
        {
            if (animal == null)
                return BadRequest("Animal não informado.");

            // Verifica se algum campo obrigatório está vazio
            if (string.IsNullOrEmpty(animal.Name) || string.IsNullOrEmpty(animal.Description) ||
                string.IsNullOrEmpty(animal.Origin) || string.IsNullOrEmpty(animal.Reproduction) ||
                string.IsNullOrEmpty(animal.Feeding))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            // Localiza o animal na lista
            var animalExistente = _dBContext.Animais.FirstOrDefault(a => a.Id == id);
            if (animalExistente == null)
                return NotFound();

            // Atualiza os dados
            animalExistente.Name = animal.Name;
            animalExistente.Description = animal.Description;
            animalExistente.Origin = animal.Origin;
            animalExistente.Reproduction = animal.Reproduction;
            animalExistente.Feeding = animal.Feeding;

            return Ok(animalExistente);
        }
    }
}
