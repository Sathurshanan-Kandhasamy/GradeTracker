using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace DataManagement
{
    public class Adapter
    {
        /// <summary>
        /// Returns all student records from the SQL Database.
        /// </summary>
        /// <returns>A list of student records stored in Student class objects.</returns>
        public List<Student> GetAllStudents()
        {
            // String containing the query to be executed.
            string sql = "SELECT * FROM Students";
            // Using statement that manages and disposes of our sql connection.
            using (var connection = Helper.GetConnection())
            {
                // The command being sent to the database to be executed.
                return connection.Query<Student>(sql).ToList();
            }
        }

        /// <summary>
        /// Returns single student record from the SQL Database.
        /// </summary>
        /// <returns>An student record stored in Student class object.</returns>
        public Student GetStudent(int id) 
        {
            // String containing the query to be executed.
            string sql = $"SELECT * FROM Students WHERE Students.StudentID = {id}";
            // Using statement that manages and disposes of our sql connection.
            using (var connection = Helper.GetConnection())
            {
                    // The command being sent to the database to be executed.
                    return connection.QuerySingle<Student>(sql); 
            }
        }

        /// <summary>
        /// Updates the details of the provided record in the database.
        /// </summary>
        /// <param name="studentDetails">The details of the record to be updated.</param>
        public void UpdateStudent(Student updatedStudent)
        {
            // @ means parameters from DAPPER.
            string sql = $"UPDATE Students SET LastName = @LastName, FirstName = @FirstName, Email = @Email WHERE Students.StudentId = {updatedStudent.StudentId}";
            // Execute a command using SQL connection.
            using(var connection = Helper.GetConnection())
            {
                connection.Execute(sql, updatedStudent);
            }

        }

        /// <summary>
        /// Takes a provided student model and sends it to the database to be added as a new record.
        /// </summary>
        /// <param name="newStudent">The student model holding the details to be added.</param>
        public void AddNewStudent(Student newStudent)
        {
            // String containing the query to be executed.
            string sql = "INSERT INTO Students(FirstName, LastName, Email) " +
                         "VALUES(@FirstName, @LastName, @Email)";
            // Using statement that manages and disposes of our sql connection.
            using (var connection = Helper.GetConnection())
            {
                // The command being sent to the database to be executed.
                connection.Execute(sql, newStudent);
            }
        }

        /// <summary>
        /// Deletes the student record from the database that matches the provided primary key.
        /// </summary>
        /// <param name="id">The primary key(Id) of the record to be deleted.</param>
        public void DeleteStudent(int id)
        {
            // String containing the query to be executed.
            string sql = $"DELETE FROM Students WHERE StudentId = {id}";
            // Using statement that manages and disposes of our sql connection.
            using (var connection = Helper.GetConnection())
            {
                // The command being sent to the database to be executed.
                connection.Execute(sql);
            }
        }

        public List<ResultView> GetAllForResultView()
        {
            string sql = "SELECT Grades.GradeId, Students.LastName, Students.FirstName, Subjects.SubjectName, Grades.Percentage FROM Grades INNER JOIN Students ON Grades.StudentId = Students.StudentId INNER JOIN Subjects ON Grades.SubjectId = Subjects.SubjectId\r\n";
            using (var connection = Helper.GetConnection())
            {
                return connection.Query<ResultView>(sql).ToList();
            }
        }
    }
}
