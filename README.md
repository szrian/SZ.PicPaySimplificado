# API PicPay Simplificada

Esta é uma API simplificada do PicPay desenvolvida com **.NET Core**. Ela permite a criação de usuários (comuns ou lojistas), e realiza transações entre eles. Os lojistas podem receber dinheiro, mas não realizar transações. A API segue os princípios de **Domain Driven Design (DDD)** e é integrada com APIs externas para validação e notificação de transações. Utiliza **SQL Server** como banco de dados para armazenar os dados.

## Funcionalidades

- **Criação de Usuários**: É possível criar usuários comuns e lojistas.
- **Saldo de Usuários**: Cada usuário (comum ou lojista) possui um saldo.
- **Transações**: Usuários comuns podem transferir dinheiro entre si, mas lojistas podem apenas receber pagamentos.
- **Integração Externa**: Validação de transações e notificações de status.

## Tecnologias Utilizadas

- **.NET 8**
- **SQL Server** para persistência de dados
- **Domain Driven Design (DDD)** como arquitetura
- **Integração com APIs externas** para validação de transações e notificações

---

## Endpoints da API

### 1. **Usuário Controller**

A primeira controller gerencia os usuários, permitindo criar, atualizar e consultar informações sobre eles.

#### a. **Adicionar Usuário**

- **Método**: `POST`
- **Rota**: `/api/usuario/novo-usuario`
- **Descrição**: Cria um novo usuário (comum ou lojista).
- **Corpo da requisição**:

```json
{
  "nome": "string",
  "documento": "string",
  "email": "string",
  "tipoUsuario": 0,
  "senha": "string",
  "saldo": 100.00
} 
```
#### b. **Atualizar Usuário**

- **Método**: `PUT`
- **Rota**: `/api/usuario/atualizar-usuario`
- **Descrição**: Atualiza as informações de um usuário.
- **Corpo da requisição**:

```json
{
  "nome": "string",
  "documento": "string",
  "email": "string",
  "tipoUsuario": 0,
  "senha": "string",
  "saldo": 100.00
}
```
#### c. **Obter Usuário por Id**

- **Método**: `GET`
- **Rota**: `/api/usuario/obter-por-id/{id}`
- **Descrição**: Obtém as informações de um usuário com base no Id.
- **Reposta de Sucesso**:

```json
{
  "id": "Guid"
  "nome": "string",
  "documento": "string",
  "email": "string",
  "tipoUsuario": 0,
  "senha": "string",
  "saldo": 100.00
}
```
#### d. **Obter Todos Usuários**

- **Método**: `GET`
- **Rota**: `/api/usuario/obter-todos`
- **Descrição**: Obtém uma lista de todos os usuários registrados.
- **Reposta de Sucesso**:

```json
[
  {
    "id": "Guid",
    "nome": "string",
    "documento": "string",
    "email": "string",
    "tipoUsuario": 0,
    "senha": "string",
    "saldo": 100.00
  },
  {
    "id": "Guid",
    "nome": "string",
    "documento": "string",
    "email": "string",
    "tipoUsuario": 0,
    "senha": "string",
    "saldo": 250.00
  }
]
```
- **tipoUsuário** é um Enum com 2 valores:
  - **Comum**: 0,
  - **Lojista**: 1

### 2. **Transação Controller**

A segunda controller gerencia as transações entre os usuários.

#### a. **Transferir Dinheiro**

- **Método**: `POST`
- **Rota**: `/api/transacao/transferir`
- **Descrição**: Permite a transferência de dinheiro de um usuário comum para outro usuário. Lojistas não podem realizar transferências.
- **Corpo da requisição**:

```json
{
  "valor": 0.00,
  "recebedorId": "Guid",
  "pagadorId": "Guid"
} 
```

- **Validações**:
  - Se o **pagadorId** for um lojista, a transação será negada.
  - O saldo do usuário deve ser suficiente para realizar a transação.
  - Se a API externa não autorizar a transação, ela será negada.
 
- **Integração com APIs Externas**:
  - A API realiza a **validação da transação** e envia uma **notificação** para os sistemas externos sobre o status da transação.
 
## Estrutura do Projeto

A aplicação segue os princípios de **Domain Driven Design (DDD)**, com as seguintes pastas e responsabilidades:

- **Controllers**: Define os endpoints da API (Usuário e Transação).
- **Domínio**: Contém as entidades e regras de negócio.
- **Aplicação**: Implementa a lógica de negócio, incluindo as integrações com APIs externas.
- **Data**: Contém o contexto de banco de dados e repositórios, usando **Entity Framework Core** para acessar o **SQL Server**.
- **DTOs**: Objetos de Transferência de Dados, usados para enviar dados entre camadas.

## Banco de Dados

A aplicação utiliza **SQL Server** como banco de dados para persistência das informações. Abaixo estão as tabelas principais:

- **Usuarios**:
  - **Id**: Identificador único do usuário.
  - **Nome**: Nome do usuário.
  - **Documento**: Documento do usuário.
  - **Email**: Email do usuário.
  - **TipoUsuario**: Tipo do usuário (comum ou lojista).
  - **Senha**: Senha do usuário.
  - **Saldo**: Saldo atual do usuário.

- **Transacoes**:
  - **Id**: Identificador único da transação.
  - **Valor**: Quantia transferida.
  - **PagadorId**: Id do usuário que está transferindo o dinheiro.
  - **RecebedorId**: Id do usuário que está recebendo o dinheiro.
  - **DataTransacao**: Data da transação.

## Como executar

1. Clone o repositório:
   
    ```
    git clone https://github.com/seuusuario/picpay-api.git
    cd picpay-api
    ```
2. Restaure as dependências:

   ```
    dotnet restore
   ```
3. Atualize a string de conexão do SQL Server no arquivo **appsettings.json**.
4. Crie o banco de dados com as migrações do Entity Framework:

   ```
   dotnet ef database update
   ```
5. Execute a API:

    ```
    dotnet run
    ```
A API estará disponível em **http://localhost:44316**.

## Testando a API

Você pode testar os endpoints da API utilizando ferramentas como **Postman** ou **Insomnia**. Os exemplos de requisição estão descritos nos endpoints acima.
