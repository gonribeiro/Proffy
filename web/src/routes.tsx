import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'

import Landing from './pages/Landing'
import Register from './pages/Register'
import TeacherEdit from './pages/TeacherEdit'
import TeacherCourses from './pages/TeacherCourses'
import TeacherList from './pages/TeacherList'
import Login from './pages/Login'

function Routes() {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Landing} />
            <Route path="/register" exact component={Register} />
            <Route path="/login" exact component={Login} />
            <Route path="/teacher-edit" exact component={TeacherEdit} />
            <Route path="/teacher-courses" exact component={TeacherCourses} />
            <Route path="/study" exact component={TeacherList} />
        </BrowserRouter>
    )
}

export default Routes;