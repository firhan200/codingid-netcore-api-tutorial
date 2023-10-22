using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;
        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = _todoRepository.GetAll();

            return Ok(todos);
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] AddTodoDto addTodoDto)
        {
            var todo = _todoRepository.Create(new Todo { 
                Title = addTodoDto.Name
            });

            return Ok(todo);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] AddTodoDto addTodoDto)
        {
            var todo = _todoRepository.Update(new Todo
            {
                Id = id,
                Title = addTodoDto.Name
            });

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            return Ok(_todoRepository.Delete(id));
        }
    }
}