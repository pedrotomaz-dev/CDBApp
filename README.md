# Sistema de Cálculo de CDB

Este projeto implementa um calculador de CDB contendo o Backend (.NET 8 Web API / Clean Architecture / CQRS) e Frontend (Angular CLI).

## Arquitetura e Decisões

- O backend foi construído seguindo os princípios de **Domain-Driven Design (DDD)** e **SOLID** utilizando o pacote **MediatR** para implementação de **CQRS**, mantendo um desacoplamento limpo entre as camadas.
- A Minimal API no ASP.NET Core 8 expõe os endpoints, onde toda a regra de juros compostos e taxas reside nos serviços injetados da camada de Domínio.
- O Frontend (`cdb-web`) foi construído modularmente no Angular, aplicando HTML5 Semântico e CSS Moderno puro (SCSS, Glassmorphism, Dark Mode) para garantir UX Premium.

> [!NOTE]
> A adoção de DDD/CQRS para um projeto deste escopo é reconhecidamente um **over-engineering** intencional. A decisão foi tomada para demonstrar familiaridade com os padrões arquiteturais solicitados e com Clean Architecture em .NET, não necessariamente como recomendação para aplicações desta complexidade em contexto produtivo.

## Testes Unitários e Cobertura

- A Web API atinge cobertura **> 90%** utilizando **xUnit**, **NSubstitute** e **FluentAssertions**.
- Foram implementados testes no Frontend Angular (Stretch Goal) usando **Jasmine** e **Karma**, mockando os serviços de API com `HttpClientTestingModule`.

## Configuração da URL da API (Frontend)

A URL base da API está definida diretamente no arquivo `cdb-web/src/app/cdb.service.ts`:

```typescript
private readonly apiUrl = 'https://localhost:7058/api/cdb/calculate';
```

**Se a API subir em uma porta diferente**, altere esse valor antes de rodar o frontend.

### Por que não é possível usar variável de ambiente do sistema operacional diretamente?

O Angular é compilado para **arquivos estáticos** (HTML/JS/CSS) que rodam no browser — diferente de aplicações Node.js/backend, o processo do sistema operacional não está acessível em runtime. As abordagens para externalizar a URL são:

| Abordagem | Quando é substituída | Requer recompilação? |
|---|---|---|
| `environment.ts` (padrão Angular) | Em **build-time** via `fileReplacements` | ✅ Sim |
| `window.__env` via `env.js` servido | Em **runtime** pelo servidor web | ❌ Não |

Para este projeto de demonstração local, a URL está hardcoded para simplicidade. Em um cenário de produção/CI-CD, o ideal seria utilizar o arquivo `environment.ts` do Angular (substituído automaticamente em build) ou uma estratégia de configuração em runtime.

## Como Executar

### Backend (.NET API)

1. Navegue até o diretório raiz.
2. Inicie a API:
   ```bash
   dotnet run --project CDB.Api
   ```
   > [!TIP]
   > Acesse o **Swagger** em `https://localhost:7058/swagger` para testar os endpoints diretamente.

### Frontend (Angular)

1. Navegue até o diretório `cdb-web`.
2. Instale as dependências:
   ```bash
   npm install
   ```
3. Confirme que a URL em `cdb-web/src/app/cdb.service.ts` aponta para a porta correta da API (padrão: `7058`).
4. Inicie a aplicação:
   ```bash
   npm run start
   ```

## Como Testar a Aplicação

### API (.NET — xUnit):

Na pasta raiz do projeto:
```bash
dotnet test
```

### Frontend Angular (Karma/Jasmine):

Na pasta `cdb-web`:
```bash
npm run test -- --watch=false
```
