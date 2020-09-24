using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.AggregatesModel.CourseAggregate
{
    public class Course
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public bool Actived { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
