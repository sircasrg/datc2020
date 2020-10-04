using System.Collections.Generic;

namespace students
{
    public static class StudentRepo
    {
        private static List<Students> students = new List<Students>();

        public static void insert(Students data)
        {
            data.Id = students.Count;
            students.Add(data);
        }

        public static Students getById(int id)
        {
            return students.Find(student => student.Id == id);
        }

        public static List<Students> getAll()
        {
            return students;
        }

        public static void deleteById(int id)
        {
            students.RemoveAll(s => s.Id == id);
        }

        public static Students update(int id, Students student)
        {
            int index = students.FindIndex(s => s.Id == id);
            students[index].FirstName = student.FirstName;
            students[index].LastName = student.LastName;
            students[index].Faculty = student.Faculty;
            students[index].YearOfStudy = student.YearOfStudy;
            return students[index];
        }
    }
}