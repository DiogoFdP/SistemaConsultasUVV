# Sistema de Gestão de Consultas UVV

Sistema web desenvolvido em **C# com ASP.NET Core MVC** para gerenciamento de usuários e consultas.

## 👥 Integrantes

* **Diogo Fernandes de Paula**

---

## 📋 Sobre o projeto

O sistema permite que usuários realizem seu cadastro, efetuem login e gerenciem suas consultas.

Após realizar o login, cada usuário pode:

* Cadastrar consultas;
* Visualizar suas próprias consultas;
* Editar consultas;
* Excluir consultas;
* Encerrar sua sessão.

As consultas são associadas ao usuário que as cadastrou, garantindo que cada usuário tenha acesso somente às suas próprias consultas.

---

## 🛠️ Tecnologias utilizadas

* **C#**
* **ASP.NET Core MVC**
* **.NET 10**
* **Entity Framework Core**
* **SQL Server**
* **HTML / CSS**
* **Razor Views**

---

## 🏗️ Arquitetura

O projeto utiliza o padrão **MVC (Model-View-Controller)**.

### Model

Responsável pela representação dos dados da aplicação.

Principais entidades:

* `Usuario`
* `Consulta`

### View

Responsável pela interface apresentada ao usuário, utilizando **Razor Views**.

### Controller

Responsável pelo processamento das requisições e pela comunicação entre as Views e o banco de dados.

Principais controllers:

* `ContaController`
* `ConsultaController`

---

## 🗄️ Banco de dados

O sistema utiliza **SQL Server** e **Entity Framework Core** com a abordagem **Code First**.

O relacionamento principal do sistema é:

```text
Usuario 1 -------- N Consulta
```

Um usuário pode possuir várias consultas, enquanto cada consulta pertence a um único usuário.

---

## ⚙️ Configuração do banco de dados

A conexão com o banco de dados está configurada no arquivo:

```text
appsettings.json
```

Exemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SistemaConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Caso seja utilizado outro servidor SQL Server, a string de conexão deverá ser ajustada conforme o ambiente.

---

## 🚀 Como executar o projeto

### 1. Pré-requisitos

É necessário possuir instalado:

* .NET SDK 10
* SQL Server ou SQL Server LocalDB
* Visual Studio 2022 ou superior

### 2. Restaurar os pacotes

Abra o terminal na pasta do projeto e execute:

```bash
dotnet restore
```

### 3. Aplicar as migrations

Execute:

```bash
dotnet ef database update
```

Esse comando cria/atualiza o banco de dados utilizando as migrations do Entity Framework Core.

### 4. Executar o projeto

Execute:

```bash
dotnet run
```

Ou abra o projeto no Visual Studio e execute utilizando o botão **Executar**.

---

## 🔐 Autenticação e autorização

O sistema utiliza autenticação baseada em **Cookies**.

As áreas de gerenciamento de consultas são protegidas por autenticação, utilizando:

```csharp
[Authorize]
```

Dessa forma, somente usuários autenticados podem acessar o gerenciamento de consultas.

Além disso, as consultas são filtradas pelo ID do usuário autenticado, impedindo que um usuário visualize ou altere consultas pertencentes a outro usuário.

---

## ✅ Funcionalidades

### Usuários

* [x] Cadastro de usuário
* [x] Validação dos dados
* [x] Login
* [x] Logout
* [x] Verificação de e-mail já cadastrado

### Consultas

* [x] Criar consulta
* [x] Listar consultas
* [x] Editar consulta
* [x] Excluir consulta
* [x] Associar consulta ao usuário autenticado

---

## 📌 Validações

O sistema utiliza Data Annotations para validação dos dados, incluindo:

```csharp
[Required]
[EmailAddress]
[StringLength]
```

Essas validações são utilizadas nos modelos de usuário e consulta.

---

## 🔗 Repositório

**GitHub:**
https://github.com/DiogoFdP/SistemaConsultasUVV

---

## 🎥 Vídeo de demonstração

**Vídeo:**
https://www.youtube.com/watch?v=-UE0lKuKMSs

O vídeo demonstra as principais funcionalidades do sistema, incluindo cadastro, login e gerenciamento de consultas.
