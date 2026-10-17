# StudyFlowAPI

Web API criada para armazenar e gerenciar as tarefas do aplicativo StudyFlow.

## Tecnologias

- ASP.NET Core 8;
- Entity Framework Core;
- SQL Server;
- Swagger.

## Funcionalidades

- Cadastrar uma tarefa;
- Listar todas as tarefas;
- Consultar uma tarefa pelo ID;
- Editar uma tarefa ou alterar seu status;
- Excluir uma tarefa.

## Endpoints

| Método | Endpoint | Operação |
|---|---|---|
| GET | `/api/tarefas` | Listar tarefas |
| GET | `/api/tarefas/{id}` | Consultar por ID |
| POST | `/api/tarefas` | Cadastrar |
| PUT | `/api/tarefas/{id}` | Editar ou alterar status |
| DELETE | `/api/tarefas/{id}` | Excluir |

## Configurar o banco

A conexão padrão está no arquivo `appsettings.json`:

```text
Server=.;Database=StudyFlowDB;Trusted_Connection=True;TrustServerCertificate=True
```

Se o SQL Server usar a instância `SQLEXPRESS`, troque o início da conexão por:

```text
Server=.\SQLEXPRESS
```

## Executar o projeto

Abra o CMD dentro da pasta do projeto e execute:

```cmd
dotnet restore
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update
dotnet run --launch-profile http
```

O `dotnet-ef` só precisa ser instalado uma vez. Se ele já estiver instalado, prossiga normalmente.

Depois, acesse:

- Swagger: <http://localhost:5288/swagger>
- Tarefas: <http://localhost:5288/api/tarefas>

## Aplicativo mobile

O projeto React Native que consome esta API está em:

<https://github.com/Restartmygame/StudyFlow>
