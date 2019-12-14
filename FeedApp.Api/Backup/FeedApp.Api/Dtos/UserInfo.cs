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
        [Column(name: "ID")]
        public long UserId { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        public Nullable<Gender> Gender { get; set; }

        [Required]
        public int Height { get; set; }
        public Nullable<ExerciseLevel> ExerciseLevel { get; set; }
        public int CaloriesForMaitenance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Dictionary<DateTime, double> Weight { get; } = new Dictionary<DateTime, double>();

        public double this[DateTime date]
        {
            get { return Weight[date]; }
            set { Weight[date] = value; }
        }

       // public User User { get; set; }
    }
}
