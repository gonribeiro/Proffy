/**
 * In C# it is not possible to use enum starting with a number, for example 1h
 * https://stackoverflow.com/questions/3916914/c-sharp-using-numbers-in-an-enum
 */

const schedule = [
    { value: 0, label: '7h' },
    { value: 1, label: '8h' },
    { value: 2, label: '9h' },
    { value: 3, label: '10h' },
    { value: 4, label: '11h' },
    { value: 5, label: '12h' },
    { value: 6, label: '13h' },
    { value: 7, label: '14h' },
    { value: 8, label: '15h' },
    { value: 9, label: '16h' },
    { value: 10, label: '17h' },
    { value: 11, label: '18h' },
    { value: 12, label: '19h' },
    { value: 13, label: '20h' },
    { value: 13, label: '21h' }
]

export default schedule