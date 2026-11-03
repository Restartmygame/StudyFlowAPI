# StudyFlowAPI

Web API REST responsável pelo cadastro e pela persistência das tarefas do aplicativo StudyFlow.

## Tecnologias

- ASP.NET Core 8;
- Entity Framework Core 8;
- SQL Server;
- Swagger/OpenAPI.

## Operações

| Método | Endpoint | Resposta esperada |
|---|---|---|
| GET | `/api/tarefas` | `200` com a lista |
| GET | `/api/tarefas/{id}` | `200` ou `404` |
| POST | `/api/tarefas` | `201` com a tarefa criada |
| PUT | `/api/tarefas/{id}` | `204`, `400` ou `404` |
| DELETE | `/api/tarefas/{id}` | `204` ou `404` |

## Validações

- título obrigatório, com 3 a 100 caracteres;
- disciplina obrigatória, com 2 a 60 caracteres;
- data de entrega obrigatória e válida;
- status aceito: `Pendente` ou `Concluida`.

## Configurar o SQL Server

A conexão padrão está em `appsettings.json`:

```text
Server=.;Database=StudyFlowDB;Trusted_Connection=True;TrustServerCertificate=True
```

Para SQL Server Express, altere para:

```text
Server=.\SQLEXPRESS;Database=StudyFlowDB;Trusted_Connection=True;TrustServerCertificate=True
```

## Criar o banco e executar

Abra o CMD na pasta da API:

```cmd
dotnet restore
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update
dotnet run --launch-profile http
```

O `dotnet-ef` só precisa ser instalado uma vez. Se ele já estiver instalado, prossiga com os próximos comandos.

Endereços locais:

- Swagger: <http://localhost:5288/swagger>
- Tarefas: <http://localhost:5288/api/tarefas>

O arquivo `StudyFlowAPI.http` possui exemplos prontos de todas as requisições.

## Acesso por um celular na mesma rede

Para permitir que um celular acesse a API durante um teste local, execute:

```cmd
dotnet run --urls http://0.0.0.0:5288
```

Depois, configure o aplicativo com o IPv4 do computador, por exemplo:

```env
EXPO_PUBLIC_API_URL=http://192.168.0.10:5288
```

O computador e o celular devem estar na mesma rede. Também pode ser necessário autorizar a porta `5288` no Firewall do Windows.

## Estrutura

```text
Controllers/  endpoints e respostas HTTP
Data/         contexto do Entity Framework
DTO/          dados recebidos e devolvidos pela API
Migrations/   criação da tabela no banco
Models/       entidade e enum de status
Program.cs    serviços, Swagger, CORS e pipeline HTTP
```

## Projeto relacionado

Aplicativo React Native: <https://github.com/Restartmygame/StudyFlow>
