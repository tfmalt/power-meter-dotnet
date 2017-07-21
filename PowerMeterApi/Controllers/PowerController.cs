using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerMeterApi.Models;
using StackExchange.Redis;

namespace PowerMeterApi.Controllers
{
    [Route("power")]
    public class PowerController : Controller
    {
        // private readonly TodoContext _context;
        private readonly ILogger _logger;
        private ConnectionMultiplexer _redis;

        public PowerController(ILogger<PowerController> logger)
        {
            _redis = CreateAndReturnRedisConnection();

            // _context = context;
            _logger = logger;
 
            //if (_context.TodoItems.Count() == 0)
            //{
            //    _logger.LogDebug("Looking at todoitems before add: {0}", _context.TodoItems.Count());

            //    _context.TodoItems.Add(new TodoItem {Name = "Item1"});
            //    _context.SaveChanges();

            //    _logger.LogDebug("Looking at todoitems after add: {0}", _context.TodoItems.Count());
            //}
        }

        private ConnectionMultiplexer CreateAndReturnRedisConnection() {
            var redis = ConnectionMultiplexer.Connect(
                "duro.malt.no:6379,password=g3yHBIdiGPBsoGgPdb6rkEJa"
            );

            return redis;
        }

        [HttpGet("meter/total")]
        public IActionResult GetMeterTotal()
        {
            var db = _redis.GetDatabase();

            var result = db.StringGet("meterTotal");
            var data = new MeterTotal {
                Value = (Double)result,
                Description = "Current Meter Total",
                Timestamp = DateTime.Now
            };

            return new ObjectResult(data); 
        }

    //    [HttpPut("todo/{id}")]
        //public IActionResult Update(long id, [FromBody] TodoItem item)
    //    {
    //        if (item == null || item.Id != id)
    //        {
    //            return BadRequest();
    //        }

    //        var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
    //        if (todo == null)
    //        {
    //            return NotFound();
    //        }

    //        todo.IsComplete = item.IsComplete;
    //        todo.Name = item.Name;

    //        _context.TodoItems.Update(todo);
    //        _context.SaveChanges();

    //        return new NoContentResult();
    //    }

        // GET api/todo/id
    //    [HttpGet("todo/{id}", Name = "GetTodo")]
    //    public IActionResult GetById(int id)
    //    {
    //        var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
    //        if (item == null)
    //        {
    //            return NotFound();
    //        }

            
    //        var result = new ObjectResult(item);
    //        return result;
    //    }


    //    [HttpPost("todo")]
    //    public IActionResult Create([FromBody] TodoItem item)
    //    {
    //        if (item == null)
    //        {
    //            return BadRequest();
    //        }

    //        _context.TodoItems.Add(item);
    //        _context.SaveChanges();

    //        return CreatedAtRoute("GetTodo", new {id = item.Id}, item);
    //    }

    //    // DELETE api/values/5
    //    [HttpDelete("todo/{id}")]
    //    public IActionResult Delete(long id)
    //    {
    //        var todo = _context.TodoItems.First(t => t.Id == id);
    //        if (todo == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.TodoItems.Remove(todo);
    //        _context.SaveChanges();
    //        return new NoContentResult();
    //    }
    }
    
}
