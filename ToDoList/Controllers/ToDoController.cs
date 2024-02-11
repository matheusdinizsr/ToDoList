using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Storage;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        ToDoStorage _todostorage = new ToDoStorage();
        
        public ToDoController() { }

        
        [HttpGet]
        public ToDoItem[] GetToDos()
        {
            var retornavel = _todostorage.GetTodo();

            return retornavel;
        }

        [HttpPost("add")]
        public void PostToDos([FromQuery] string input)
        {
            _todostorage.AddTodo(input);
        }
        
        [HttpPost]
        public void PostToDos2([FromBody] Payload payload)
        {
            _todostorage.AddTodo(payload.Input);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteToDos([FromQuery] DateTime input)
        {
            
            if (_todostorage.RemoveTodo(input))
            {
                return Ok();
            } 
            else
            {
                return NotFound("Não encontrado!");
            
            }

        }



    }
    public class Payload
    {
        public string Input { get; set; }
    }
}
