# miniqr-api
Esta API é responsável por gerenciar as movimentações de cobranças. Ela oferece recursos para criar e buscar cobranças, bem como realizar o cancelamento de cobranças pagas.

## Funcionalidades
- Criação de cobranças por usuários lojistas.
- Consulta de status de cobranças por usuários lojistas.
- Cancelamento de cobranças pagas por usuários administradores.
- Cadastro de usuário por usuários master.

## Tecnologias Utilizadas
- ASP.NET Core para o desenvolvimento da API.
- Entity Framework Core para a camada de persistência de dados.
- MediatR para manipulação de comandos e consultas.
- AutoMapper para mapear entidades de domínio para modelos de leitura.
- FluentValidation para validação de dados de entrada.
- XUnit e Moq para testes unitários.

## Pré-requisitos
- ASP.NET Core.
- Entity Framework Core.
- SQL Server.
- Docker.

## Instalação
1. Clone este repositório para o seu ambiente local.
2. Certifique-se de ter os pré-requisitos instalados e configurados.
3. Na pasta raiz da solução, execute o comando `docker-compose up --build` para criar e iniciar os contêineres.

## Usuário Master Padrão
Ao iniciar a aplicação, um usuário master padrão é criado. Você pode usar as seguintes credenciais para acessar as funcionalidades de cadastro de usuário master:

- **E-mail:** master_abc@mail.com
- **Senha:** M@ster!1010

**Nota:** Essas credenciais padrão podem ser alteradas no arquivo `appsettings.json` no seguinte trecho:

```json
"ConfiguracoesUsuarioMaster": {
    "EmailMaster": "novo_email@mail.com",
    "SenhaMaster": "NovaSenha123!"
}
```

## Como Usar
A documentação detalhada sobre como usar a API e suas funcionalidades está disponível no endpoint de documentação (`/swagger`). Certifique-se de ler a documentação para obter informações sobre os endpoints disponíveis.
