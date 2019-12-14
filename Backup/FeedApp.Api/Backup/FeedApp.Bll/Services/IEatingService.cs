using FeedApp.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Services
{
    public interface IEatingService
    {
        Eating GetEating(int eatingId);
        IEnumerable<Eating> GetEatings();
        Eating InsertEating(Eating newEating);
        void UpdateEating(int eatingId, Eating updatedEating);
        void DeleteEating(int eatingId);
    }
}
