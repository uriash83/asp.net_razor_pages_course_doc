using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {

        //The DatabaseGenerated attribute allows the app to specify the primary key rather than having the database generate it.
        // takie podejscie jest najlepsze. ale czasem aplikacja wymaga manualnego podania PK, jak w tym przypadku
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }
        // oglnie EF Core z autoamtu przypisze do Department ukryty FK. Ale lepiej jest zrobic to jawnie poprzez DepartmentID
        // gdyby nie bylo jawnego FK to trzeba by bylo przy kazdym create/update course pobierac osobno Department
        public int DepartmentID { get; set; }
        // tego ponizej nie ma w Database
        // bo to jest nazywa Navigation Property
        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}
