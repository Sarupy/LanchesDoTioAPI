using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Services.Interfaces;

namespace LanchesDoTioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly LanchesContext _context;
        private readonly IMealService _mealService;

        public MealsController(LanchesContext context, IMealService mealService)
        {
            _context = context;
            _mealService = mealService;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetAll()
        {
            return await _context.Meal.Include(x => x.PriceHistoryList).Select(x => _mealService.ModelToDto(x)).ToListAsync();
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetById(int id)
        {
            var meal = await _context.Meal.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        // PUT: api/Meals/rename/5?newName={newName}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Rename/{id}")]
        public async Task<IActionResult> Rename(int id, [FromQuery] string newName)
        {
            await _mealService.Rename(id, newName);
            return Ok("Meal renamed successfully.");
        }

        // PUT: api/Meals/updatePrice/5?newPrice={newPrice}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePrice/{id}")]
        public async Task<IActionResult> UpdatePrice(int id, [FromQuery] decimal newPrice)
        {
            await _mealService.UpdatePrice(id, newPrice);
            return Ok("Meal price updated successfully.");
        }

        // POST: api/Meals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> Create(MealDTO mealDTO)
        {
            var meal = _mealService.DtoToModel(mealDTO);
            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
