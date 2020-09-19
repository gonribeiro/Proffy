using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate
{
    public class TeacherCourse
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        [StringLength(7)]
        public string Cost { get; set; }
        [Required]
        public bool Actived { get; set; }
        public Course Course { get; set; }
        public ICollection<TeacherCourseSchedule> TeacherCourseSchedules { get; set; }
    }
}
