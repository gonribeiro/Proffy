import React from 'react'

import whatsappIcon from '../../assets/images/icons/whatsapp.svg'

import './styles.css'
import proffyapi from '../../services/Proffy'

export interface Teacher {
    teacherCourses: any
    id: number
    photo: string
    bio: string
    cost: number
    name: string
    subject: string
    whatsapp: string
    from: string
    to: string
}

interface TeacherItemProps {
    teacher: Teacher
}

const TeacherItem: React.FC<TeacherItemProps> = ({ teacher }) => {
    function createNewConnection() {
        proffyapi.post('connections', {
            user_id: teacher.id,
        })
    }
    
    return (
        <article className="teacher-item">
            <header>
                <img src={teacher.photo} alt={teacher.photo} />
                <div>
                    <strong>{teacher.name}</strong>
                </div>
            </header>

            <p>
                <strong>Biografia:</strong> 
                <br />{teacher.bio}
            </p>

            <footer>
                <p>
                    Hora/aula:
                    <strong>R$ {teacher.teacherCourses[0].cost}</strong>
                    <br /> Disponibilidade: <strong>De {teacher.from} até {teacher.to}</strong>
                </p>
                <a 
                    onClick={createNewConnection} 
                    href={`https://wa.me/${teacher.whatsapp}`} 
                    rel="noopener noreferrer"
                >
                    <img src={whatsappIcon} alt="Whatsapp"/>
                    Entrar em contato
                </a>
            </footer>
        </article>
    )
}

export default TeacherItem