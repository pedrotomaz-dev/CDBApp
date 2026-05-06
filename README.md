# Sistema de Cálculo de CDB
Este projeto implementa um calculador de CDB contendo o Backend (.NET 8 Web API / Clean Architecture / CQRS) e Frontend (Angular CLI).

## Arquitetura e Decisões
- O backend foi construído seguindo os princípios de Domain-Driven Design (DDD) e SOLID utilizando pacote MediatR para implementação de CQRS, mantendo um desacoplamento limpo.
- A Minimal API no ASP.NET Core 8 expõe os endpoints, onde toda a regra de juros compostos e taxas reside nos serviços injetados da camada de Domínio.
- O Frontend (cdb-web) foi construído modularmente no Angular, aplicando HTML5 Semântico e CSS Moderno puro (SCSS, Glassmorphism, Dark Mode) para garantir UX Premium.

## Testes Unitários e Cobertura
- A Web API atende rigorosamente a cobertura > 90% utilizando o pacote xUnit e bibliotecas NSubstitute/FluentAssertions.
- Foram implementados testes em Angular (Stretch Goal) usando Jasmine e Karma mockando os serviços de API.

## Como Executar

### Backend (.NET API)
1. Navegue até o diretório raiz.
2. Inicie a API com o comando, ela utilizará a porta local para iniciar.
   ```bash
   dotnet run --project CDB.Api
   ```
   > [!TIP]
   > Você pode acessar o **Swagger** para testar direto a API na rota `https://localhost:7058/swagger`.

### Frontend (Angular)
1. Navegue até o diretório `cdb-web`.
2. Instale as dependências:
   ```bash
   npm install
   ```
3. Rode a aplicação de desenvolvimento:
   ```bash
   npm run start
   ```

## Como Testar a Aplicação

### API:
Execute os testes automatizados da solução toda:
```bash
dotnet test
```

### Frontend Angular (Karma/Jasmine):
Na pasta `cdb-web`:
```bash
npm run test -- --watch=false
```
