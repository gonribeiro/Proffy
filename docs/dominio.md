## Domínio

https://yuml.me/diagram/scruffy/class/draw

```
[User]-[TeacherCourse]
[TeacherCoursesSchedule]-[TeacherCourse]
[Connection]-[User]
[TeacherCourse]-[Course]
[TeacherCoursesSchedule]-[EnumWeekDay]

[User; Id; Email; Password; Name; Photo; Whatsapp; Facebook; Bio; CreatedAt; Actived ]

[Connection; Id; UserId; CreatedAt ]

[TeacherCourse; Id; UserId; Cost; Description; Actived ]

[TeacherCoursesSchedule; Id; ClassId; WeekDay; From; To ]

[Course; Id; Name; Actived ]

[EnumWeekDay; Segunda; Terça; Quarta; Quinta; Sexta; Sábado]

[ TeacherCourseSchedule; CreateSchedule(TeacherCourseSchedule); GetSchedule(Guid TeacherCourseId); UpdateSchedule(TeacherCourseSchedule); ]

[ TeacherCourse; CreateTeacherCourse(TeacherCourse); GetTeacherCourse(Guid UserId); Update TeacherCourse(TeacherCourse);  SearchCourse(CourseId, weekday, from)]

[UserService; CreateUser(User); GetUser(Guid UserId); UpdateUser(User); Login (Email e Password); SearchUser(List Guid Id)] 

[WeekDayEnum; GetAll()] 

[Courses; GetAll()]  
```