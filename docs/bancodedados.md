## Banco de Dados

https://yuml.me/diagram/scruffy/class/draw

```
[Users]1-1..*[TeacherCourses]
[Schedules]1..*-1[TeacherCourses]
[Connections]1..*-1[Users]
[TeacherCourses]1-1[Courses]

[Users; Id: Guid; Email: Varchar(40); Password: Varchar(40); Name: Varchar(120); Photo: Varchar(255); CreatedAt: DateTime; Actived: Bool; Whatsapp: Varchar(13); Facebook: Varchar(255); Bio: Text; ]

[Connections; Id: Guid; UserId: Guid; CreatedAt: DateTime ]

[TeacherCourses; Id: Guid; UserId: Guid; Cost: Varchar(7); Description: Varchar(255); Actived: Bool ]

[Schedules; Id: Guid; ClassId: Guid; WeekDay: Int; From: Int; To: Int ]

[Courses; Id: Guid; Name: Varchar(50); Actived: Bool ]
```