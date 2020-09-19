## Caso de Uso

https://yuml.me/diagram/scruffy/class/draw

```
[Professor]-(Criar conta)
[Professor]-(Efetuar Login)
[Professor]-(Atualizar dados pessoais)
[Professor]-(Cadastrar Curso)
(Cadastrar Curso)<(Cadastrar horário disponível)
[Professor]-(Atualizar Curso)
[Professor]-(Atualizar horário disponível)

[Aluno]-(Procurar professor/curso)
(Procurar professor/curso)<(Entrar em contato com professor)
```