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
            if (!context.Foods.Any())
            {
                Eating e1 = new Eating()
                {//Oats
                    Meal = Meal.Breakfast,
                    Quantity = 1
                };

                Eating e2 = new Eating()
                {//Chicken
                    Meal = Meal.Lunch,
                    Quantity = 5
                };

                Eating e3 = new Eating()
                {//Rice
                    Meal = Meal.Lunch,
                    Quantity = 2
                };

                context.Eatings.Add(e1);
                context.Eatings.Add(e2);
                context.Eatings.Add(e3);

                context.Foods.Add(new Food() { FoodName = "Chicken Breast", Calories = 120, Fat = 2.6, Carbohydrate = 0, Sugar = 0, Fiber = 0, Protein = 22.5, Salt = 0.1125 });
                context.Foods.Add(new Food() { FoodName = "Basmati Rice", Calories = 360, Fat = 3.0, Carbohydrate = 80, Sugar = 0, Fiber = 8, Protein = 10, Salt = 0 });
                context.Foods.Add(new Food() {
                    FoodName = "Oats",
                    Calories = 389,
                    Fat = 6.9,
                    Carbohydrate = 55.6,
                    Sugar = 0,
                    Fiber = 10.6,
                    Protein = 16.8,
                    Salt = 0.005,
                    
                });
                context.Foods.Add(new Food() {
                    FoodName = "Chicken",
                    Calories = 120,
                    Fat = 2.6,
                    Carbohydrate = 0,
                    Protein = 22.5,
                    FoodsForEatings = { new FoodsForEating() { Eating = new Eating() { Meal = Meal.Dinner, Quantity = 2 } } }
                });


                FoodsForEating ffe1 = new FoodsForEating()
                {
                    FoodId = 3,
                    EatingId = 1
                };
                FoodsForEating ffe2 = new FoodsForEating()
                {
                    FoodId = 3,
                    EatingId = 2
                };
                FoodsForEating ffe3 = new FoodsForEating()
                {
                    FoodId = 3,
                    EatingId = 3
                };

                context.FoodsForEatings.Add(ffe1);
                context.FoodsForEatings.Add(ffe2);
                context.FoodsForEatings.Add(ffe3);

                context.Users.Add(new ApplicationUser()
                {
                    FirstName = "Elod",
                    LastName = "Vizi",
                    Email = "elod.vizi@yahoo.com",
                    EmailConfirmed = true,
                    Eatings = { new Eating()
                        {
                            Meal = Meal.Lunch,
                            Quantity = 2
                        }
                    }
                });

                context.Users.Add(new ApplicationUser()
                {
                    FirstName = "Lucie",
                    LastName = "Ventas",
                    Email = "lucie.ventas@yahoo.com",
                    EmailConfirmed = true,
                    Eatings = { new Eating()
                        {
                            Meal = Meal.Other,
                            Quantity = 3
                        }
                    }
                });


                DailyWeightInfo dwi1 = new DailyWeightInfo()
                {
                    DateTime = new DateTime(2016,05,23),
                    Weight = 100
                };

                DailyWeightInfo dwi2 = new DailyWeightInfo()
                {
                    DateTime = new DateTime(2017,05,23),
                    Weight = 90
                };
                context.DailyWeightInfos.Add(dwi1);
                context.DailyWeightInfos.Add(dwi2);


                UserInfo ui = new UserInfo()
                {
                    ApplicationUserID = 1,
                    Age = 20,
                    Gender = Gender.Male,
                    Height = 180,
                    ExerciseLevel = ExerciseLevel.ExtraActive,
                    WeightByDate = { dwi1, dwi2 }
                };

                UserInfo ui2 = new UserInfo()
                {
                    ApplicationUserID = 2,
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
