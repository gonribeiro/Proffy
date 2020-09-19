import axios from 'axios'

const usersapi = axios.create({
    baseURL: 'https://localhost:44368/api/'
})

export default usersapi