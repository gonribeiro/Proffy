import React, { useState, FormEvent, useEffect } from 'react'
import { useHistory } from 'react-router-dom'

import usersApi from '../../services/User'
import { logout, isLogged } from '../../services/Auth'

import Input from '../../components/Input'
import Textarea from '../../components/TextArea'

import warningIcon from '../../assets/images/icons/warning.svg'

interface TeacherInfoProps {
    id: string,
    email: string,
    password: string
    name: string,
    photo: string,
    whatsapp: string,
    facebook: string
    bio: string
}

const TeacherInfo: React.FC<TeacherInfoProps> = () => {
    const history = useHistory()
    const proffyToken = localStorage.getItem('proffyToken')
    const id = localStorage.getItem('proffyId') ? localStorage.getItem('proffyId') : ''

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [name, setName] = useState('')
    const [photo, setPhoto] = useState('')
    const [whatsapp, setWhatsapp] = useState('')
    const [facebook, setFacebook] = useState('')
    const [bio, setBio] = useState('')
  
    useEffect(() => {        
        if (id !== '') {
            isLogged() // Check if user are logged in

            usersApi.get('v1/users/'+id, {
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {
                let data = response.data

                setEmail(data.email)
                setName(data.name)
                setPhoto(data.photo)
                setWhatsapp(data.whatsapp)
                setFacebook(data.facebook)
                setBio(data.bio)
            }).catch((error) => {
                alert(error.response.data)

                logout()
            })
        }
    }, []);

    function handleUser(e: FormEvent) {
        e.preventDefault()

        if (id === '') {
            usersApi.post('v1/users', {
                email, 
                password,
                name,
                photo,
                whatsapp,
                facebook,
                bio
            }).then((response) => {
                alert(response.data.message)

                history.push('/login')
            }).catch((error) => {
                alert(error.response.data)
            })
        } else {
            usersApi.put('v1/users/'+id, {
                id,
                password,
                photo,
                whatsapp,
                facebook,
                bio
            },{
                headers: {
                    Authorization: 'Bearer ' + proffyToken
            }}).then((response) => {
                alert(response.data.message)

                setPassword('')
            }).catch((error) => {
                alert(error.response.data)
                console.log(error.response)
                
                logout()
            })
        }
    }

    return (
        <main>
            <form onSubmit={handleUser}>
                <fieldset>
                    <legend>Seus dados</legend>

                    {id === '' ? <div><b> Atenção! <br /> Após o cadastro, você não poderá alterar o Email e Nome. <br /> Portanto, certifique-se de estar cadastrando corretamente. </b ><br /><br /></div> : <div><b> *Email e Nome não podem ser alterados </b><br /><br /> </div> }                    

                    <input type="hidden" name="id" value={id!} />
                    <Input 
                        name="email" 
                        label="Email" 
                        value={email} 
                        required
                        type="email"
                        disabled={id === '' ? false : true}
                        onChange={(e) => {setEmail(e.target.value)}} 
                        maxLength={40}
                    />
                    <Input 
                        name="password" 
                        label="Senha" 
                        value={password} 
                        placeholder={id === '' ? '' : "Preencha se desejar alterar a sua senha"}
                        required={id === '' ? true : false}
                        type="password"
                        maxLength={40}
                        onChange={(e) => {setPassword(e.target.value)}} 
                    />
                    <Input 
                        name="name" 
                        label="Nome Completo" 
                        value={name} 
                        placeholder="Nome sem abreviações"
                        required
                        disabled={id === '' ? false : true}
                        maxLength={120}
                        onChange={(e) => {setName(e.target.value)}} 
                    />
                    <Input 
                        name="photo" 
                        label="Foto"
                        placeholder="Uma foto sem óculos escuro ou chapéu"
                        value={photo} 
                        required
                        maxLength={255}
                        onChange={(e) => {setPhoto(e.target.value)}} 
                    />
                    <Input 
                        name="whatsapp" 
                        label="WhatsApp"
                        placeholder="Por onde o aluno entrará em contato"
                        value={whatsapp} 
                        required
                        maxLength={13}
                        onChange={(e) => {setWhatsapp(e.target.value)}} 
                    />
                    <Input 
                        name="facebook" 
                        label="Facebook"
                        placeholder="Para o aluno te conhecer melhor"
                        value={facebook}
                        required 
                        maxLength={255}
                        onChange={(e) => {setFacebook(e.target.value)}} 
                    />
                    <Textarea 
                        name="bio"
                        label="Biografia" 
                        placeholder="Fale um pouco sobre sua experiência profissional"
                        value={bio} 
                        required
                        onChange={(e) => {setBio(e.target.value)}} 
                    />                    
                </fieldset>

                <footer>
                    <p>
                        <img src={warningIcon} alt="Aviso importante" />
                        Importante! <br />
                        Preencha todos os dados
                    </p>
                    <button type="submit">
                        Salvar cadastro
                    </button>
                </footer>
            </form>
        </main>
    )
}

export default TeacherInfo;