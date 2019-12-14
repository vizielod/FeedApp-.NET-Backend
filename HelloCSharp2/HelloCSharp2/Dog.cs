using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCSharp2
{
    public class Dog
    {
        public string Name { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime? DateOfBirth { get; set; }
        private int? AgeInDays => (int?)(-DateOfBirth?.Subtract(DateTime.Now))?.Days; 
        public int? Age => AgeInDays / 365;
        public int? AgeInDogYears => AgeInDays * 7 / 365;
        public override string ToString() => $"{Name} ({Age} | {AgeInDogYears}) [ID: {Id}]";

        public Dictionary<string, object> Metadata { get; } = new Dictionary<string, object>();

        public object this[string key]
        {
            get { return Metadata[key]; }
            set { Metadata[key] = value; }
        }
    }
}
