using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>
        {
            new TaskModel { Id = 1, Title = "Jurasik Park", Description = "Dinosorous Movie" },
            new TaskModel { Id = 2, Title = "Titanic", Description = "Ocean Movie" }
        };

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            var existingTask = tasks.Find(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;

            return NoContent();
        }

        // Other CRUD operations like DELETE can be added similarly
    }

    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}