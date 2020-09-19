import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

import { logout, sessionExpired } from '../../services/Auth'

import backIcon from '../../assets/images/icons/back.svg'

import './styles.css'

interface PageHeaderProps {
    title: string,
    description?: string
}

const PageHeader: React.FC<PageHeaderProps> = (props) => {
    const [ minute, setMinute ] = useState(14)
    const [ second, setSecond ] = useState(59)
    
    useEffect(() => {        
        if (localStorage.getItem('proffyToken')) {            
            setTimeout(() => {
                setSecond(second - 1)

                if (second === 0) {
                    setSecond(59)

                    setMinute(minute - 1)
                }

                if (minute === -1) {
                    sessionExpired()
                }
            }, 1000);
        }
    });

    return (
        <header className="page-header">
            <div className="top-bar-container">
                <Link to="/">
                    <img src={backIcon} alt="Voltar"/>
                </Link>
                
                {
                    localStorage.getItem('proffyName') ?
                    <div className="top-bar-user">
                        Olá <Link to="/teacher-edit">{localStorage.getItem('proffyName')?.split(' ')[0]}</Link>!
                        | <Link onClick={logout} to="#">Sair</Link> <br/> { minute }:{ second }
                    </div>
                    : <Link to="/register">Seja um Proffy</Link>
                }
            </div>
            
            <div className="header-content">
                <strong>{props.title}</strong>
                {props.description && <p> {props.description} </p>}
                {props.children}
            </div>
        </header>
    )
}

export default PageHeader