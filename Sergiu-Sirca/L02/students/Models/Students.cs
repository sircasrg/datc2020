using System;

namespace students
{
    public class Students
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public int YearOfStudy { get; set; }

        public Students() { }
        // public Student(int id, string fn, string ln, string fac, int yof) {
        //     this.Id = id;
        //     this.FirstName = fn;
        //     this.LastName = ln;
        //     this.Faculty = fac;
        //     this.YearOfStudy = yof;
        // }
    }

}
