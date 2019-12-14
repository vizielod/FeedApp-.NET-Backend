using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeedApp.Api.Dtos
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

        public long ApplicationUserID { get; set; }
        public User User { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public int Height { get; set; }
        public ExerciseLevel? ExerciseLevel { get; set; }
        public int CaloriesForMaitenance { get; set; }

        public List<DailyWeightInfo> WeightByDayList { get; set; }

    }
}
