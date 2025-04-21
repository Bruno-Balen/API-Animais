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

        [HttpGet]
        public ActionResult<List<Animal>> GetAll(){

            return Ok(_dBContext.Animais);
        }

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

        [HttpPost("InserirAnimal")]
        public ActionResult<Animal> PostAnimal([FromBody] Animal animal) 
        {
            if (animal == null)
            {
                return BadRequest();
            }

            foreach (var property in typeof(Animal).GetProperties())
            {
                var value = property.GetValue(animal);

                if (value == null)
                {
                    return BadRequest("Informações necessárias não foram informadas.");
                }

            
            }

            _dBContext.Animais.Add(animal);

            return Ok(animal);
        }
    }
}
