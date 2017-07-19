using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Models;
using WebApp.Infraestructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        private readonly IDbConnection _connection;
        private readonly ITodoItemRepository _repository;
        private readonly ITodoService _service;
        private readonly IQueryHandler<GetAllTodoQuery,IEnumerable<TodoItem>> _queryHandler;

        public DemoController(IDbConnection connection, ITodoItemRepository repository, ITodoService service, IQueryHandler<GetAllTodoQuery, IEnumerable<TodoItem>> queryHandler)
        {
            _connection = connection;
            _repository = repository;
            _service = service;
            _queryHandler = queryHandler;
        }

        // GET: api/values
        [HttpGet("directo")]
        public IEnumerable<TodoItem> GetDirecto()
        {
            var todos = new List<TodoItem>();
            _connection.Open();
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "Select * from todos";
                using (var reader = cmd.ExecuteReader())
                {
                    //leemos el datareader y agregamos el todo.
                }
            }
            _connection.Close();

            return todos;
        }

        [HttpGet("contexto")]
        public IEnumerable<string> GetContexto()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("repository")]
        public IEnumerable<TodoItem> GetRepository()
        {
            return _repository.GetAll();
        }

        [HttpGet("reposervicio")]
        public IEnumerable<TodoItem> GetRepositoryServicio()
        {
            return _service.GetAll();
        }

        [HttpGet("query")]
        public async Task<IEnumerable<TodoItem>> GetQueryAsync([FromQuery] GetAllTodoQuery query)
        {
            return await _queryHandler.Handle(query);
        }
    }
}
