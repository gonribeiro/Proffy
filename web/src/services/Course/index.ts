import axios from 'axios'

const coursesapi = axios.create({
    baseURL: 'https://localhost:44320/api/'
})

export default coursesapi