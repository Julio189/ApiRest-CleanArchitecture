# 🏗️ Projeto .NET 8 com Arquitetura Limpa e Clean Code

Este repositório contém uma aplicação **ASP.NET Core** desenvolvida com foco em **Clean Code** e **Arquitetura Limpa**, incluindo **Testes de Domínio** e autenticação com **ASP.NET Identity**. O projeto visa fornecer uma base robusta e escalável para o desenvolvimento de APIs e aplicações web modernas.

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Plataforma para construção de aplicações modernas de alto desempenho.
- **ASP.NET Core** - Framework web para desenvolvimento de APIs RESTful e aplicações web.
- **Entity Framework Core** - ORM para acesso a dados com suporte a bancos relacionais.
- **ASP.NET Identity** - Sistema de autenticação e autorização com suporte a JWT e roles.
- **xUnit** - Framework de testes para criação de testes unitários e de integração.
- **FluentValidation** - Biblioteca para validação de objetos de forma fluente.
- **AutoMapper** - Para mapeamento de objetos entre diferentes camadas.
- **Clean Architecture** - Organização do código em camadas independentes, promovendo alta manutenibilidade.

## 🏗️ Estrutura do Projeto

Este projeto segue a **Arquitetura Limpa** (Clean Architecture), dividindo a aplicação em camadas com responsabilidades bem definidas:

```bash
├── Domain
│   ├── Entities
│   ├── Interfaces
│   └── Domain Services
├── Application
│   ├── Use Cases
│   ├── DTOs
│   └── Validations
├── Infrastructure
│   ├── Data
│   ├── Identity
│   ├── Repositories
│   └── External Services
└── WebAPI
    ├── Controllers
    ├── Middlewares
    └── Dependency Injection Configurations
```
## 🧩 Camada **Domain**
- **Entidades**: Objetos centrais que representam os conceitos do negócio.
- **Serviços de Domínio**: Implementação da lógica central de negócio.
- **Testes de Domínio**: Garantem que as regras do domínio estão corretas.

## 🧩 Camada **Application**
- **Casos de Uso**: Implementação das operações que coordenam a lógica de negócio.
- **Validações**: Uso do **FluentValidation** para garantir dados consistentes.

## 🧩 Camada **Infrastructure**
- **Repositórios**: Manipulação de dados e acesso ao banco de dados.
- **Identity**: Autenticação e autorização usando **ASP.NET Identity**.
- **Serviços Externos**: Integração com APIs e serviços de terceiros.

## 🧩 Camada **WebAPI**
- **Controllers**: Exposição de endpoints da API.
- **Autenticação e Autorização**: Implementadas com **JWT Tokens** e políticas de autorização baseadas em **roles** e **claims**.

## 🔐 Autenticação e Autorização
A autenticação no projeto é baseada no **ASP.NET Identity**, com suporte a **JWT Tokens** para autenticação nas APIs e **roles** para controle de acesso.

- **Registro e Login**: Implementação de registro de usuários e geração de JWT.
- **Roles e Claims**: Controle de acesso baseado em políticas definidas por **roles** (Admin, User, etc.).
- **Refresh Tokens**: Suporte para renovação de tokens de autenticação.

