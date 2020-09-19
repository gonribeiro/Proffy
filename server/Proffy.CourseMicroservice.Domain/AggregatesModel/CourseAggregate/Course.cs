using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate
{
    public class Course
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public bool Actived { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
