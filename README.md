
# 💰 Fluxo de Caixa - Desafio Técnico

Este projeto tem como objetivo controlar lançamentos financeiros (débitos e créditos) de um comerciante e consolidar os saldos diários, garantindo escalabilidade, resiliência e segurança.

---

## 🧱 Arquitetura

O sistema foi construído com base nos princípios de **Clean Architecture**, **DDD (Domain-Driven Design)** e separação de responsabilidades em múltiplos microsserviços com comunicação assíncrona via mensageria.

### 🔄 Microsserviços

- **Lancamentos.API**  
  Exposição de uma API REST para registrar lançamentos financeiros.
  
- **Consolidado.Worker**  
  Serviço em background que escuta eventos de lançamentos e processa o saldo diário consolidado.

---

## 📂 Estrutura de Projetos

```
src/
│
├── Core.Domain/             # Entidades, Objetos de Valor, contratos de repositórios
├── Core.Application/        # UseCases, DTOs, interfaces para gateways
├── Core.Infrastructure/     # EF Core, repositórios, RabbitMQ, config
│
├── Lancamentos.API/         # Web API (.NET 9)
├── Consolidado.Worker/      # Worker service que consome fila RabbitMQ
│
└── Tests/
    ├── UnitTests/
    └── IntegrationTests/
```

---

## 🧠 Camadas

```
Presentation
├── API / Worker (input/output)
Application
├── UseCases, DTOs, interfaces de gateways
Domain
├── Entidades, Value Objects, interfaces
Infrastructure
├── EF Core, RabbitMQ, repositórios concretos
```

---

## 🚀 Tecnologias Utilizadas

- .NET 9
- C#
- ASP.NET Core Web API
- Worker Services
- PostgreSQL
- RabbitMQ
- Entity Framework Core
- Docker e Docker Compose
- xUnit
- Clean Architecture + DDD

---

## ▶️ Como Executar Localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/fluxo-caixa.git
cd fluxo-caixa

# 2. Suba os containers necessários (RabbitMQ, PostgreSQL)
docker-compose up -d

# 3. Rode os serviços
dotnet build

# Em terminais diferentes:
dotnet run --project src/Lancamentos.API
dotnet run --project src/Consolidado.Worker
```

---

## 🧪 Rodar Testes

```bash
# Testes unitários
dotnet test src/Tests/UnitTests

# Testes de integração (se configurado)
dotnet test src/Tests/IntegrationTests
```

---

## ⚙️ Requisitos Não Funcionais Atendidos

- ✅ Microsserviços desacoplados por RabbitMQ
- ✅ Continuidade de operação do serviço de lançamentos mesmo com falha no consolidador
- ✅ Persistência segura com PostgreSQL
- ✅ Separação de camadas e domínio limpo
- ✅ Docker para infraestrutura

---

## 🔐 Segurança

- Separação clara de responsabilidades (evita acesso indevido entre camadas)
- Autenticação/autorização pode ser facilmente adicionada com OAuth2 (não implementada no MVP)

---

## 📈 Escalabilidade e Resiliência

- Escalabilidade horizontal dos microsserviços via Docker/Kubernetes
- RabbitMQ permite filas distribuídas e entrega assíncrona
- Serviços independentes com retry/resiliência no Worker
- API continua funcional mesmo que o Worker falhe (comunicação desacoplada)

---

## 📚 Documentação Adicional

Você encontrará na pasta `/docs`:

- Diagramas de arquitetura
- Modelos de dados
- Fluxo de mensagens
- Sugestões de evolução futura

---

## 🛠️ Melhorias Futuras

- Adição de autenticação e RBAC
- Observabilidade (Prometheus, Grafana)
- Retry com Polly nos workers
- Exposição de métricas de saúde
- Deploy automatizado via GitHub Actions

---

## 👤 Autor

**Ícaro Milet**  
[LinkedIn](https://www.linkedin.com/in/icaro-milet/) • [GitHub](https://github.com/icaro-milet)

---

## ✅ Status

🚧 Projeto em desenvolvimento para avaliação técnica.  
Última atualização: **junho de 2025**.
