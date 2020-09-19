import React, { useState, FormEvent, useEffect } from 'react'
import coursesApi from '../../services/Course'
import usersApi from '../../services/User'

import enumSchedule from '../../storage/enumSchedule'

import CreatableSelect from 'react-select'

import PageHeader from '../../components/PageHeader'
import TeacherItem, { Teacher } from '../../components/TeacherItem'

import './styles.css'

/**
 * TODO
 * Transform select into component
 */
// Select Style
const customStyles = {
    control: (styles: any) => ({ 
        ...styles, 
        borderRadius: 7,
        backgroundColor: '#F8F8FC',
        height: '57px',
        width: '100%',
        borderColor: '#E6E6F0',
        boxShadow: '#E6E6F0',
        '&:hover': {
            border: '#E6E6F0'
        },
    })
}

function TeacherList() {    
    const proffyToken = localStorage.getItem('proffyToken')

    const [courses, setCourses] = useState([])
    const [weekDays, setWeekdays] = useState([])

    const [researchedCourse, setResearchedCourse] = useState('')
    const [researchedWeekDay, setResearchedWeekDay] = useState(Number)
    const [researchedHour, setResearchedHour] = useState(Number)

    const [teachersList, setTeachersList] = useState([])

    useEffect(() => {
        coursesApi.get('v1/courses/', {
            headers: {
                Authorization: 'Bearer ' + proffyToken
            }
        }).then((response) => {
            let option = response.data.map((data:any) => {
                return {'value': data['id'], 'label': data['name']}
            })
            setCourses(option)
        }).catch(() => {
            alert('Erro ao carregar as matérias.')
        })

        coursesApi.get('v1/enum/weekday/', {
            headers: {
                Authorization: 'Bearer ' + proffyToken
            }
        }).then((response) => {
            setWeekdays(response.data)
        }).catch(() => {
            alert('Erro ao carregar as matérias.')
        })
    }, [])  

    function searchTeachers(e: FormEvent) {
        e.preventDefault()

        coursesApi.get('v1/teachercourses/search/'+researchedCourse+','+researchedWeekDay+','+researchedHour)
        .then((response) => {
            let courseList = response.data

            let teachersId = response.data.map((data:any) => {
                return data['userId']
            })

            usersApi.get('v1/Users/search/'+teachersId)
            .then((response) => {
                let teachers = response.data.map((teacher:any) => {
                    courseList.map((course: any) => {
                        course.teacherCourseSchedules.map((schedule:any) => {
                            enumSchedule.map((hour: any) => {
                                return schedule.from === hour.value ? teacher.from = hour.label : ''
                            })
                            enumSchedule.map((hour: any) => {
                                return schedule.to === hour.value ? teacher.to = hour.label : ''
                            })
                        })

                        return teacher.id === course.userId ? teacher.cost = course.cost : ''
                    })

                    return teacher
                })
                
                setTeachersList(teachers)
            }).catch(() => {
                alert('Nenhum professor encontrado para esta pesquisa.')
            })
        }).catch(() => {
            alert('Preencha todos os campos')
        })
    }

    return (
        <div id="page-teacher-list" className="container">
            <PageHeader  title="Estes são os proffys disponíveis">
                <form id="search-teachers" onSubmit={searchTeachers}>

                    <CreatableSelect 
                        name="classid"
                        placeholder="Matéria"
                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                        styles={customStyles}
                        onChange={(e: any) => {setResearchedCourse(e.value) }}
                        options={courses}
                    />
                    <CreatableSelect 
                        name="weekDay"
                        placeholder="Dia"
                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                        styles={customStyles}
                        onChange={(e: any) => {setResearchedWeekDay(e.value) }}
                        options={weekDays}
                    />
                    <CreatableSelect 
                        name="from"
                        placeholder="Horário"
                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                        styles={customStyles}
                        onChange={(e: any) => {setResearchedHour(e.value) }}
                        options={enumSchedule}
                    />

                    <button type="submit">
                        Buscar
                    </button>
                </form>
            </PageHeader>

            <main>
                {teachersList.map((teacher: Teacher) => {
                    return <TeacherItem key={teacher.id} teacher={teacher} />
                })}
            </main>
        </div>
    )
}

export default TeacherList;