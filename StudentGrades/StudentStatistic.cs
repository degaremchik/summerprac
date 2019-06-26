using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades
{
    class StudentStatistic
    {
        public StudentStatistic ( )
        {

        }
        public StudentStatistic(
            int id,
            string lastname,
            string name,
            string groupNumber,
            float rate5,
            float rate4,
            float rate3,
            float rate2 )
        {
            Id = id;
            Lastname = lastname;
            Name = name;
            GroupNumber = groupNumber;
            Rate5 = rate5;
            Rate4 = rate4;
            Rate3 = rate3;
            Rate2 = rate2;
        }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string GroupNumber { get; set; }
        public float Rate5 { get; set; }
        public float Rate4 { get; set; }
        public float Rate3 { get; set; }
        public float Rate2 { get; set; }
    }
}
