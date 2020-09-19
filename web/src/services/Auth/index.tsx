export const logout = () => {
    localStorage.clear()

    window.location.href = '/'

    alert("Desconectado. Até logo!")
}

export const sessionExpired = () => {
    localStorage.clear()

    window.location.href = '/'

    alert("Sua sessão expirou.")
}

export const isLogged = () => {
    if (!localStorage.getItem('proffyToken')) {
        localStorage.clear()

        window.location.href = '/'

        alert("Você precisa estar conectado para acessar essa página")
    }
}