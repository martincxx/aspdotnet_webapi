using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
  [Route("api/Todo")]
  [ApiController]
  public class TodoController : ControllerBase
  {
    private readonly TodoContext _context;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public TodoController(TodoContext context)
    {
      _context = context;

      if (_context.TodoItems.Count() == 0)
      {
        _context.TodoItems.Add(new TodoItem { Name = "Item1" });
        _context.SaveChanges();
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll()
    {
      return _context.TodoItems.ToList();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetTodo")]
    public ActionResult<TodoItem> GetById(long id)
    {
      var item = _context.TodoItems.Find(id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(TodoItem item)
    {
      _context.TodoItems.Add(item);
      _context.SaveChanges();

      return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(long id, TodoItem item)
    {
      var todo = _context.TodoItems.Find(id);
      if (todo == null)
      {
        return NotFound();
      }

      todo.IsComplete = item.IsComplete;
      todo.Name = item.Name;

      _context.TodoItems.Update(todo);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var todo = _context.TodoItems.Find(id);
      if (todo == null)
      {
        return NotFound();
      }

      _context.TodoItems.Remove(todo);
      _context.SaveChanges();
      return NoContent();
    }
  }

}