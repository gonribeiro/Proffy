import React from 'react'

import PageHeader from '../../components/PageHeader';
import TeacherInfo from '../../components/TeacherInfo';
import TeacherMenu from '../../components/Menu';

function Register() {
    return (
        <div id="page-teacher-form" className="container">
            <PageHeader 
                title="Que incrível que você quer dar aulas"
                description="O primeiro passo é preencher esse formulário de inscrição"
            >
                <TeacherMenu 
                    menuLeft="Login"
                    urlLeft="login"
                    menuRight="Cadastrar"
                    urlRight="register"
                />
            </PageHeader>

            <TeacherInfo 
                id=""
                email="" 
                password=""
                name=""
                photo=""
                whatsapp=""
                facebook=""
                bio=""
            />
        </div>
    )
}

export default Register;