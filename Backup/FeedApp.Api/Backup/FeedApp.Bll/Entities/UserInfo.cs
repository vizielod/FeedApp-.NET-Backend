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
        /*[Key]
        [Column(name: "ID", Order = 0)]
        public long UserId { get; set; }*/
        public long ID { get; set; }

        public long UserID { get; set; }
        public User User { get; set; }
        //[Required]
        public int Age { get; set; }

        public Nullable<Gender> Gender { get; set; }

        //public int CurrentWeight { get; set; }

        //[Required]
        public double Height { get; set; }
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

        //public int UserId { get; set; }
        
    }
}
