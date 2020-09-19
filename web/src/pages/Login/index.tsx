import React, { FormEvent, useState } from 'react'
import { useHistory } from 'react-router-dom';

import usersApi from '../../services/User';

import PageHeader from '../../components/PageHeader'

import Input from '../../components/Input'
import TeacherMenu from '../../components/Menu';

interface LoginProps {
    email: string,
    password: string
}

const Login: React.FC<LoginProps> = () => {
    const history = useHistory()
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    function handleLogin(e: FormEvent) {
        e.preventDefault()

        usersApi.post('/v1/users/login', {
            email,
            password
        }).then((response) => {
            localStorage.setItem('proffyToken', response.data.token);
            localStorage.setItem('proffyId', response.data.id);
            localStorage.setItem('proffyName', response.data.name);

            history.push("/teacher-edit")
        }).catch((error) => {
            alert(error.response.data)
        })
    }
    
    return (
        <div id="page-teacher-form" className="container">
            <PageHeader 
                title="Bom ter você de volta!"
                description="Vamos começar?"
            >
                <TeacherMenu 
                    menuLeft="Login"
                    urlLeft="login"
                    menuRight="Cadastrar"
                    urlRight="register"
                />
            </PageHeader>

            <main>
                <form onSubmit={handleLogin}>
                    <fieldset>
                        <legend>Conectar ao Proffy</legend>

                        <Input 
                            name="Email" 
                            label="Email" 
                            required
                            maxLength={40}
                            onChange={(e) => {setEmail(e.target.value)}} 
                        />
                        <Input 
                            name="Password" 
                            label="Senha"
                            required
                            type="password"
                            maxLength={40}
                            onChange={(e) => {setPassword(e.target.value)}} 
                        />
                    </fieldset>

                    <footer>
                        <button type="submit">
                            Entrar
                        </button>
                    </footer>
                </form>
            </main>
        </div>
    )
}

export default Login