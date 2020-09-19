import React from 'react'
import { Link } from 'react-router-dom';

import './styles.css'

interface MenuProps {
    menuLeft: string,
    urlLeft: string,
    menuRight: string,
    urlRight: string
}

const Menu: React.FC<MenuProps> = (props) => {
    return (
        <div className="buttons-menu-container">                    
            <Link to={props.urlLeft}>
                <button>{ props.menuLeft }</button>
            </Link>

            <Link to={props.urlRight}>
                <button>{ props.menuRight }</button>
            </Link>
        </div>
    )
}

export default Menu;