using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Services
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _context;


        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        //DELETE
        public void DeleteFood(int foodId)
        {
            _context.Foods.Remove(new Food { ID = foodId });

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Food not found");
            }
           
        }

        //GET entity
        public Food GetFood(int foodId)
        {
            return _context.Foods.SingleOrDefault(f => f.ID == foodId) ?? throw new EntityNotFoundException("Food not found!");
        }

        public IEnumerable<Food> GetFoods()
        {
            var foods = _context.Foods.ToList();

            return foods;
        }

        public Food InsertFood(Food newFood)
        {
            _context.Foods.Add(newFood);

            _context.SaveChanges();

            return newFood;
        }

        public void UpdateFood(int foodId, Food updatedFood)
        {
            updatedFood.ID = foodId;
            var entry = _context.Attach(updatedFood);
            entry.State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Food not found!");
            }
        }

    }
}
