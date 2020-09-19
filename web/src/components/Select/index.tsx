import React, { SelectHTMLAttributes } from 'react'
import CreatableSelect from 'react-select'

import './styles.css'

interface SelectProps extends SelectHTMLAttributes<HTMLSelectElement> {
    name: string;
    label: string;
    options: Array<{}>;
}

const customStyles = {
    control: (styles: any) => ({ 
        ...styles, 
        borderRadius: 7,
        backgroundColor: '#F8F8FC',
        height: '55px',
        borderColor: '#E6E6F0',
        boxShadow: '#E6E6F0',
        '&:hover': {
            border: '#E6E6F0'
        },
    })
}

const Select: React.FC<SelectProps> = ({label, name, options }) => {
    return (
        <div className="select-block">
            <label htmlFor={name}>{label}</label>
            <CreatableSelect
                placeholder="Pesquise"
                styles={customStyles}
                options={options}
            />
        </div>
    )
}

export default Select