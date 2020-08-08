``` 
ReactJs
create-react-app web --template typescript
yarn add react-router-dom
yarn add @types/react-router-dom -D
yarn add axios

NodeJs
yarn init -y 
yarn add typescript -D 
yarn tsc --init
tsconfig.json -> "target": "es5" para "target": "es2017", 

yarn add ts-node-dev -D
package.json -> 
"scripts": {
    "start": "tsnd --transpile-only --ignore-watch node_modules --respawn src/server.ts"
},

yarn add express
yarn add @types/express -D
yarn add knex sqlite3

package.json -> 
"scripts": {
    "start": "tsnd --transpile-only --ignore-watch node_modules --respawn src/server.ts",
    "knex:migrate": "knex --knexfile knexfile.ts migrate:latest",
    "knex:migrate:rollback": "knex --knexfile knexfile.ts migrate:rollback"
},

Exemplo de post:
{
    "name": "Fulano",
    "avatar": "https://avatars1.githubusercontent.com/u/32773851?s=460&u=f4ad109717d170dc5bc8c4e0327681a348d41682&v=4",
    "whatsapp": "18451848",
    "bio": "Bio",
    "subject": "Informatica",
    "cost": 80,
    "schedule": [
        { "week_day": 1, "from": "8:00", "to": "14:00" },
        { "week_day": 2, "from": "8:00", "to": "14:00" },
        { "week_day": 5, "from": "8:00", "to": "14:00" }
    ]
}

yarn add cors
yarn add @types/cors -D
```