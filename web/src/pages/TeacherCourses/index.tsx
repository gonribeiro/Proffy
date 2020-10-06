import React, { useState, FormEvent, Fragment, useEffect } from 'react'

import proffyapi from '../../services/Proffy'
import { isLogged } from '../../services/Auth'

import CreatableSelect from 'react-select'

import PageHeader from '../../components/PageHeader'
import TeacherMenu from '../../components/Menu'
import Input from '../../components/Input'

import enumSchedule from '../../storage/enumSchedule'

import warningIcon from '../../assets/images/icons/warning.svg'

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
        height: '55px',
        borderColor: '#E6E6F0',
        boxShadow: '#E6E6F0',
        '&:hover': {
            border: '#E6E6F0'
        },
    })
}

const TeacherCourses: React.FC = () => {
    const [courses, setCourses] = useState([])
    const [weekDays, setWeekdays] = useState([])

    const [teacherCourseId, setTeacherCourseId] = useState('')
    const [courseId, setCourseId] = useState('')
    const [cost, setCost] = useState('')

    const [scheduleId, setScheduleId] = useState('')
    const [weekDay, setWeekDay] = useState(-1)
    const [from, setFrom] = useState(-1)
    const [to, setTo] = useState(-1)
    
    const userId = localStorage.getItem('proffyId')
    const proffyToken = localStorage.getItem('proffyToken')
    
    useEffect(() => {
        isLogged() // Check if you are logged in

        // Take all courses
        proffyapi.get('v1/courses/', {
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

        proffyapi.get('v1/enum/weekday/', {
            headers: {
                Authorization: 'Bearer ' + proffyToken
            }
        }).then((response) => {
            let option = response.data.map((data:any) => {
                return {'value': data['value'], 'label': data['label']}
            })
            setWeekdays(option)
        }).catch(() => {
            alert('Erro ao os dias da semana.')
        })

        // Takes the teacher's courses and schedules
        proffyapi.get('v1/users/'+userId, {
            headers: {
                Authorization: 'Bearer ' + proffyToken
            }
        }).then((response) => {
            setTeacherCourseId(response.data.teacherCourses[0].id)
            setCourseId(response.data.teacherCourses[0].course.id)
            setCost(response.data.teacherCourses[0].cost)
            setScheduleId(response.data.teacherCourses[0].schedules[0].id)
            setWeekDay(response.data.teacherCourses[0].schedules[0].weekDay)
            setFrom(response.data.teacherCourses[0].schedules[0].from)
            setTo(response.data.teacherCourses[0].schedules[0].to)
        }).catch(() => {            
            console.log('Professor ainda não possui matéria cadastrada')
        })
    }, [])

    function handleTeacherClass(e: FormEvent) {
        e.preventDefault()

        if (teacherCourseId === '') {
            proffyapi.post('v1/teachercourses/', {
                userId,
                courseId,
                cost
            },{
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {
                setTeacherCourseId(response.data.id)
                
                alert(response.data.message)
            }).catch((error) => {
                console.log(error)
                alert(error.response)
            })
        } else {
            proffyapi.put('v1/teachercourses/'+teacherCourseId, {
                id: teacherCourseId,
                userId,
                courseId,
                cost
            },{
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {
                alert(response.data.message)
            }).catch((error) => {
                alert(error.response.data.message)
            })
        }
    }

    function handleTeacherClassSchedule(e: FormEvent) {
        e.preventDefault()

        if (teacherCourseId === '') {
            alert('Primeiro você deve cadastrar uma matéria')
        } else if (scheduleId === '') {
            proffyapi.post('v1/Schedules/', {
                teacherCourseId,
                weekDay,
                from,
                to
            },{
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {
                console.log(response)
                setScheduleId(response.data.id)
                
                alert(response.data.message)
            }).catch((error) => {
                alert(error.response.data.message)
            })
        } else {
            proffyapi.put('v1/Schedules/'+scheduleId, {
                id: scheduleId,
                teacherCourseId,
                weekDay,
                from,
                to
            },{
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {                
                alert(response.data.message)
            }).catch((error) => {
                alert(error.response.data.message)
            })
        }
    }

    return (
        <div id="page-teacher-form" className="container">
            <PageHeader 
                title="Obaaaaa! Vamos ensinar!"
                description="Atualize sua disciplina e horários"
            >
                <TeacherMenu 
                    menuLeft="Seus dados"
                    urlLeft="teacher-edit"
                    menuRight="Suas aulas"
                    urlRight="teacher-courses"
                />
            </PageHeader>

            <Fragment>
                <main>
                    <form onSubmit={handleTeacherClass}>
                        <fieldset>
                            <legend>Sobre a aula</legend>
                            <div className="select-block">
                                <label>Matéria</label>
                                <CreatableSelect 
                                    name="courseId" 
                                    label="Matéria"
                                    value={{value: courseId, label: courses.map((obj:any) => {
                                        return obj['value'] === courseId ? obj['label'] : null
                                    })}}
                                    noOptionsMessage={() => 'Nenhuma opção encontrada'}
                                    styles={customStyles}
                                    onChange={(e: any) => {setCourseId(e.value) }}
                                    options={courses}
                                />
                            </div>
                            
                            <Input 
                                name="cost" 
                                label="Custo da sua hora por aula"
                                value={cost}
                                required
                                onChange={(e) => {setCost(e.target.value) }}
                            />
                        </fieldset>

                        <footer>
                            <p>
                                <img src={warningIcon} alt="Aviso importante" />
                                Importante! <br />
                                Preencha todos os dados
                            </p>
                            <button type="submit">
                                {courseId ? 'Salvar aula' : 'Salvar e avançar' }
                            </button>
                        </footer>
                    </form>
                </main>

                <br />

                <main>
                    <form onSubmit={handleTeacherClassSchedule}>
                        <fieldset>
                            <legend>
                                Horário disponível
                            </legend>

                            <div className="schedule-item">
                                <div className="select-block">
                                    <label>Dia da semana</label>
                                    <CreatableSelect 
                                        name="weekDay"
                                        label="Matéria"
                                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                                        styles={customStyles}
                                        value={{value: weekDay, label: weekDays.map((obj:any) => {
                                            return obj['value'] === weekDay ? obj['label'] : null
                                        })}}
                                        onChange={(e: any) => {setWeekDay(e.value) }}
                                        options={weekDays}
                                    />
                                </div>
                                <div className="select-block">
                                    <label>De</label>
                                    <CreatableSelect 
                                        name="from"
                                        label="De"
                                        value={enumSchedule.find(schedule => schedule.value === from )}
                                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                                        placeholder=""
                                        styles={customStyles}
                                        onChange={(e: any) => {setFrom(e.value) }}
                                        options={enumSchedule}
                                    />
                                </div>
                                <div className="select-block">
                                    <label>Até</label>
                                    <CreatableSelect 
                                        name="to"
                                        label="Até"
                                        value={enumSchedule.find(schedule => schedule.value === to )} 
                                        noOptionsMessage={() => 'Nenhuma opção encontrada'}
                                        placeholder=""
                                        styles={customStyles}
                                        onChange={(e: any) => {setTo(e.value) }}
                                        options={enumSchedule}
                                    />
                                </div>
                            </div>
                        </fieldset>

                        <footer>
                            <p>
                                <img src={warningIcon} alt="Aviso importante" />
                                Importante! <br />
                                Preencha todos os dados
                            </p>
                            <button type="submit">
                                Salvar horário
                            </button>
                        </footer>
                    </form>
                </main>
            </Fragment>
        </div>
    )
}

export default TeacherCourses;