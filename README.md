# 🚀 DevFreela API

O **DevFreela** é uma aplicação desenvolvida em **ASP.NET Core** durante a formação da **Mentoria .Start**, com o objetivo de gerenciar projetos entre **clientes** e **freelancers**.  
O projeto foi criado do zero, seguindo boas práticas de arquitetura, autenticação e persistência de dados.

---

## 🧩 Tecnologias Utilizadas

- **.NET 8 / ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **FluentValidation**
- **JWT Authentication**
- **SendGrid (recuperação de senha por e-mail)**
- **Moq, NSubstitute e Bogus (testes unitários)**
- **xUnit**

---

## ⚙️ Funcionalidades

✅ Cadastro e autenticação de usuários (JWT)  
✅ Criação e gerenciamento de projetos entre clientes e freelancers  
✅ Validação de entrada de dados com **FluentValidation**  
✅ Envio de e-mail de recuperação de senha via **SendGrid**  
✅ Testes unitários com **Moq**, **NSubstitute** e **Bogus**  
✅ Persistência de dados com **Entity Framework Core (Code First)**  
✅ Configuração de relacionamentos com **Fluent API**

---

## 🧠 Conceitos Aplicados

- **Padrão REST** na construção dos endpoints  
- Separação de responsabilidades com **Controllers**, **Services**, **Repositories** e **DTOs**  
- **Injeção de Dependência (DI)** nativa do ASP.NET Core  
- **Validação de modelos** desacoplada do domínio  
- **Migrations** e **Code First** para versionamento do banco de dados  
- **Clean Architecture (inspirada)** para manter o código escalável e de fácil manutenção

---

## 🧪 Testes Unitários

O projeto conta com testes unitários implementados para garantir a qualidade e a confiabilidade das principais funcionalidades.

Bibliotecas utilizadas:
- **xUnit** – Estrutura de testes
- **Moq / NSubstitute** – Criação de mocks e simulação de dependências
- **Bogus** – Geração de dados fake para os testes

---

## 🔐 Autenticação JWT

A autenticação é feita por meio de **JSON Web Tokens (JWT)**:
- Login retorna um token JWT válido com tempo de expiração.  
- Endpoints protegidos exigem o envio do token no cabeçalho `Authorization: Bearer <token>`.

---

## 📧 Recuperação de Senha

A API integra o **SendGrid** para envio de e-mails de recuperação de senha.  
Após solicitar a redefinição, o usuário recebe um link de recuperação em seu e-mail.

---

## 🗂️ Estrutura do Projeto

DevFreela/
│
├── DevFreela.API/ # Camada de apresentação (Controllers)
├── DevFreela.Application/ # Casos de uso e DTOs
├── DevFreela.Core/ # Entidades e interfaces
├── DevFreela.Infrastructure/ # Persistência, repositórios e contexto EF Core
└── DevFreela.Tests/ # Testes unitários
---

## 🧰 Como Executar o Projeto

1️⃣ Clone o repositório  
```bash
git clone https://github.com/seuusuario/DevFreela.git

2️⃣ Acesse o diretório da API

cd DevFreela/DevFreela.API

3️⃣ Configure o arquivo appsettings.json com sua connection string e chave do SendGrid

4️⃣ Execute as migrations

dotnet ef database update


5️⃣ Rode o projeto

dotnet run


A aplicação estará disponível em:
👉 https://localhost:5001 ou http://localhost:5000

🧑‍💻 Autor
João Pedro Moreira Gomes
