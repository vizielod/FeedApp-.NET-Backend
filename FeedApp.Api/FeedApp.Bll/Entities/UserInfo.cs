using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public enum ExerciseLevel
    {
        BasalMetabolicRate,
        Sedentary, //little or no excercise
        LightlyActive, //1-3 times/week
        ModeratelyActive, //3-5 times/week
        VeryActive, //6-7 times/week
        ExtraActive //twice/day

    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class UserInfo
    {
        public long ID { get; set; }

        public long ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //[Required]
        public int Age { get; set; }

        public Nullable<Gender> Gender { get; set; }

        //[Required]
        public double Height { get; set; }
        public Nullable<ExerciseLevel> ExerciseLevel { get; set; }
        public int CaloriesForMaitenance { get; set; }

        public ICollection<DailyWeightInfo> WeightByDate { get; } = new List<DailyWeightInfo>();

        
    }
}
