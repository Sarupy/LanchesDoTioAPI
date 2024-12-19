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
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            var meals = await _mealService.GetAll();

            return Ok(meals);
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDTO>> GetMealById(int id)
        {
            var meals = await _mealService.GetById(id);

            return Ok(meals);
        }

        // PUT: api/Meals/5]
        [HttpPut("Rename/{id}")]
        public async Task<ActionResult<MealDTO>> UpdateMeal(int id, [FromBody] MealDTO mealDTO)
        {
            var meal = await _mealService.Update(id, newName);

            return Ok(meal);
        }

        // PUT: api/Meals/rename/5?newName={newName}
        [HttpPut("Rename/{id}")]
        public async Task<ActionResult<MealDTO>> RenameMeal(int id, [FromQuery] string newName)
        {
            var meal = await _mealService.Rename(id, newName);

            return Ok(meal);
        }

        // PUT: api/Meals/updatePrice/5?newPrice={newPrice}
        [HttpPut("UpdatePrice/{id}")]
        public async Task<ActionResult<MealDTO>> UpdateMealPrice(int id, [FromQuery] decimal newPrice)
        {
            var meal = await _mealService.UpdatePrice(id, newPrice);

            return Ok(meal);
        }

        // POST: api/Meals
        [HttpPost]
        public async Task<ActionResult<Meal>> CreateMeal(MealDTO mealDTO)
        {
            var meal = await _mealService.Create(mealDTO);

            return CreatedAtAction(nameof(GetMealById), new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mealService.Delete(id);

            return NoContent();
        }
    }
}
