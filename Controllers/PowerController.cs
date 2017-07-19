using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerMeterApi.Models;

namespace PowerMeterApi.Controllers
{
    [Route("power")]
    public class PowerController : Controller
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;

        public PowerController(TodoContext context, ILogger<PowerController> logger)
        {
            _context = context;
            _logger = logger;

            if (_context.TodoItems.Count() == 0)
            {
                _logger.LogDebug("Looking at todoitems before add: {0}", _context.TodoItems.Count());

                _context.TodoItems.Add(new TodoItem {Name = "Item1"});
                _context.SaveChanges();

                _logger.LogDebug("Looking at todoitems after add: {0}", _context.TodoItems.Count());
            }
        }

        // GET: api/todo
        [HttpGet("todo")]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        // GET api/todo/id
        [HttpGet("todo/{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }


        [HttpPost("todo")]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new {id = item.Id}, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
