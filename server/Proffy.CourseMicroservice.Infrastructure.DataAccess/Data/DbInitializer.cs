using Proffy.CourseMicroservice.Domain.AggregatesModel.CourseAggregate;
using Proffy.CourseMicroservice.Infrastructure.DataAccess.Contexts;
using System;
using System.Linq;

namespace Proffy.ClassMicroservice.Infrastructure.DataAccess.Data
{
    public class DbInitializer
    {
        public static void Initialize(CourseContext context)
        {
            // Look for any courses.
            if (context.Courses.Any())
            {
                return; // DB has been seeded
            }

            var courses = new Course[]
            {
                new Course { Id = Guid.NewGuid(), Name = "Matemática", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Química", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Física", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Geografia", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "História", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Biologia", Actived = Convert.ToBoolean(1) },

                new Course { Id = Guid.NewGuid(), Name = "Português", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Inglês", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Espanhol", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Japonês", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Alemão", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Francês", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Mandarim", Actived = Convert.ToBoolean(1) },

                new Course { Id = Guid.NewGuid(), Name = "Programação", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Redes", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Hardware", Actived = Convert.ToBoolean(1) },

                new Course { Id = Guid.NewGuid(), Name = "Desenho", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Música", Actived = Convert.ToBoolean(1) },
                new Course { Id = Guid.NewGuid(), Name = "Teatro", Actived = Convert.ToBoolean(1) }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}
