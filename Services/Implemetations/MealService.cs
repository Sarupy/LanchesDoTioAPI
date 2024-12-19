﻿using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Services.Implemetations
{
    public class MealService : IMealService
    {
        private readonly LanchesContext _context;
        public MealService(LanchesContext context)
        {
            _context = context;
        }    

        public async Task<MealDTO> GetById(int mealId)
        {
            var meal = await EnsureMealExists(mealId);
            return ModelToDto(meal);
        }
        public async Task<IEnumerable<MealDTO>> GetAll()
        {
            var allMealsQuery = _context.Meal.Include(x => x.PriceHistoryList).Select(x => ModelToDto(x));
            return await allMealsQuery.AsNoTracking().ToListAsync();
        }

        public async Task<MealDTO> Create(MealDTO mealDTO)
        {
            var meal = DtoToModel(mealDTO);
            _context.Meal.Add(meal);
            var mealId = await _context.SaveChangesAsync();
            mealDTO.Id = mealId;
            return mealDTO;
        }

        public async Task<MealDTO> Rename(int mealId, string newName)
        {
            var meal = await EnsureMealExists(mealId);

            if (string.IsNullOrWhiteSpace(newName))
                throw new BadHttpRequestException("New name cannot be empty.");

            if (newName != meal.Name)
            {
                meal.Name = newName;
                _context.Entry(meal).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
                
            return ModelToDto(meal);
        }

        public async Task<MealDTO> UpdatePrice(int mealId, decimal newPrice)
        {
            var meal = await EnsureMealExists(mealId);

            if (newPrice == 0)
                throw new BadHttpRequestException("Price cannot be empty or zero.");

            if (newPrice != meal.CurrentPrice)
            {
                meal.UpdatePrice(newPrice);
                _context.Entry(meal).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return ModelToDto(meal); ;
        }

        public async Task Delete(int mealId)
        {
            var meal = await EnsureMealExists(mealId);

            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();
        }


        public static MealDTO ModelToDto(Meal meal)
        {
            return new MealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.CurrentPrice
            };
        }
        private static Meal DtoToModel(MealDTO mealDTO)
        {
            return new Meal
            {
                Id = mealDTO.Id,
                Name = mealDTO.Name,
                PriceHistoryList = [new PriceHistory(mealDTO.Price)]
            };
        }
        private async Task<Meal> EnsureMealExists(int mealId)
        {
            var meal = await _context.Meal.Include(x => x.PriceHistoryList).FirstOrDefaultAsync(x=> x.Id == mealId);

            if (meal == null)
                throw new KeyNotFoundException($"Meal with ID {mealId} was not found.");

            return meal;
        }
    }
}