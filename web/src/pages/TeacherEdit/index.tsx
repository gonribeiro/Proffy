import React from 'react'

import PageHeader from '../../components/PageHeader';
import TeacherInfo from '../../components/TeacherInfo';
import TeacherMenu from '../../components/Menu';

function TeacherEdit() {
    return (
        <div id="page-teacher-form" className="container">
            <PageHeader 
                title="É tão bom ver você de novo!"
                description="Mantenha seus dados sempre atualizados!"
            >
                <TeacherMenu 
                    menuLeft="Seus dados"
                    urlLeft="teacher-edit"
                    menuRight="Suas aulas"
                    urlRight="teacher-courses"
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

export default TeacherEdit;