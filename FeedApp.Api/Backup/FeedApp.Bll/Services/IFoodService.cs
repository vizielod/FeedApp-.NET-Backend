using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IFoodService
    {
        Food GetFood(int foodId);
        IEnumerable<Food> GetFoods();
        Food InsertFood(Food newFood);
        void UpdateFood(int foodId, Food updatedFood);
        void DeleteFood(int foodId);
    }
}
