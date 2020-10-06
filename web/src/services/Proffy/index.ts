import axios from 'axios'

const proffyapi = axios.create({
    baseURL: 'https://localhost:44354/api/'
})

export default proffyapi