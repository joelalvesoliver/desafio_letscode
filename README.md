Antes de executar o projeto é necessario realizar as configurações de login e senha e também inserir a secret que será usada para gerar o token JWT.
Para isso basta atualizar o arquivo appsettings.Development.json na raiz do projeto adicionando as informações conforme ilustrado nos campos abaixo.

 {
  "Login": "colocar o login aqui",
  "Password": "colocar a senha aqui",
  "SecretJWT": "adicionar secret para o JWT Aqui"
 }

A aplicação disponibiliza uma rota de login e rotas para inserir, atualizar e deletar cards, alem de uma rota para recuperar todos os cards criados.
Para consultar cada rota basta realizar as requisições a seguir.

## Login

curl --location --request POST 'https://localhost:5000/login' \
--header 'Content-Type: application/json' \
--data-raw '{
    "login": login,
    "password":senha
}'

## Criar novo card

curl --location --request POST 'https://localhost:5000/card' \
--header 'Authorization: Bearer 'token'' \
--header 'Content-Type: application/json' \
--data-raw '{
    "list": "lista de tarefaas do dia",
    "title": "tarefas do dia",
    "content": "tarefas do dia"
}'

## buscar todos os cards
curl --location --request GET 'https://localhost:5000/card' \
--header 'Authorization: Bearer 'token''

## atualizar um card

curl --location --request PUT 'https://localhost:5000/card/{id}' \
--header 'Authorization: Bearer 'token'' \
--header 'Content-Type: application/json' \
--data-raw '{
    "title": "tarefas do mes",
    "content": "teste",
    "list": "lista de tarefaas"
}'

## Remover um card
curl --location --request DELETE 'https://localhost:5000/card/{id}' \
--header 'Authorization: Bearer 'token''