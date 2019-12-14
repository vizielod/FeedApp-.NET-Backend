using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedApp.Bll.Context
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                /*context.Foods.Add(new Food() { FoodName = "Chicken Breast", Calories = 120, Fat = 2.6, Carbohydrate = 0, Sugar = 0, Fiber = 0, Protein = 22.5, Salt = 0.1125 });
                context.Foods.Add(new Food() { FoodName = "Basmati Rice", Calories = 360, Fat = 3.0, Carbohydrate = 80, Sugar = 0, Fiber = 8, Protein = 10, Salt = 0 });
                context.Foods.Add(new Food() { FoodName = "Oats", Calories = 389, Fat = 6.9, Carbohydrate = 55.6, Sugar = 0, Fiber = 10.6, Protein = 16.8, Salt = 0.005 });
                context.Foods.Add(new Food() {
                    FoodName = "Chicken",
                    Calories = 120,
                    Fat = 2.6,
                    Carbohydrate = 0,
                    Protein = 22.5,
                    FoodsForEatings = { new FoodsForEating() { Eating = new Eating() { Meal = Meal.Dinner, Quantity = 2 } } }
                });

                context.Eatings.Add(new Eating() {//Oats
                    Meal = Meal.Breakfast,
                    Quantity = 1
                });
                context.Eatings.Add(new Eating() {//Chicken
                    Meal = Meal.Lunch,
                    Quantity = 5
                });
                context.Eatings.Add(new Eating(){//Rice
                    Meal = Meal.Lunch,
                    Quantity = 2
                });*/

                context.Users.Add(new User()
                {
                    FirstName = "Elod",
                    LastName = "Vizi",
                    Eatings = { new Eating()
                        {
                            Meal = Meal.Lunch,
                            Quantity = 2
                        }
                    }
                });

                context.Users.Add(new User()
                {
                    FirstName = "Lucie",
                    LastName = "Ventas",
                    Eatings = { new Eating()
                        {
                            Meal = Meal.Other,
                            Quantity = 3
                        }
                    }
                });

                UserInfo ui = new UserInfo()
                {
                    UserID = 1,
                    Age = 20,
                    Gender = Gender.Male,
                    Height = 180,
                    ExerciseLevel = ExerciseLevel.ExtraActive,
                };

                UserInfo ui2 = new UserInfo()
                {
                    UserID = 2,
                    Age = 30,
                    Gender = Gender.Female,
                    Height = 160,
                    ExerciseLevel = ExerciseLevel.LightlyActive
                };

                context.UserInfos.Add(ui);
                context.UserInfos.Add(ui2);

                

                context.SaveChanges();
            }
        }
    }
}
