using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TodoApp.Contexts;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        public async Task<IActionResult >Index()
        {
            using (TodoContext context = new TodoContext())
            {
                return View(await context.Todos.ToListAsync());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Todo data)
        {
            using(TodoContext context = new())
            {
                await context.Todos.AddAsync(data);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(int? id)
        {
            if (id is null) return BadRequest();
            using(TodoContext context = new())
            {
                if (await context.Todos.AnyAsync (x => x.Id == id))
                {
                    context.Todos.Remove(new Todo { Id = id.Value });
                    await context.SaveChangesAsync ();
                }
                
            }
            return RedirectToAction (nameof (Index));

        }
        
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            using (TodoContext context = new())
            {
                var data = await context.Todos.FindAsync(id.Value);
                if (data is  null) return NotFound();
                return View(data);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Todo data)
        {
            if (!id.HasValue) return BadRequest();
            using (TodoContext context = new())
            {
                var entity = await context.Todos.FindAsync(id.Value);
                if (entity is null) return NotFound();

                entity.Title = data.Title;
                entity.Description = data.Description;
                entity.DeadLine = data.DeadLine;
                await context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));


            
        }
        public async Task<IActionResult> CompleteTask(int? id)
        {
            if (!id.HasValue) return BadRequest();
            using (TodoContext context = new())
            {
                var entity = await context.Todos.FindAsync(id);
                if (entity is null) return NotFound();
                entity.IsDone = true;
                await context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Show(int? id)            
        { 
            using (TodoContext context = new()) 
            {
                var entity= await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
                if (entity==null)
                {
                    return NotFound();
                    
                }
                return View(entity);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
            
        }
    }
}


