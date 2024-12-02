Projeto em .NET 8 com SQLite, OpenTelemetry e Prometheus

## Requisitos
- Docker Desktop

## How To Run (Fase 1)
- Rode o Docker Desktop
- Clone o projeto
- Abra a pasta do projeto onde o docker-compose.yml se encontra
- Abra um terminal de sua escolha e digite "docker compose up"
- Abra o endereço http://localhost:5400/swagger
- Você também pode consultar outras métricas pelo http://localhost:9090
- Utilize o userId 1 para acessar a controller de Performance

## Fase 2 - Refinamento
### 1. Sobre permissões
- Um usuário "Gerente" poderá ver tarefas criados por outros usuários? Se sim, qual a regra para isto?
- Um gerente poderá reatribuir tarefas?
- Haverá uma funcionalidade para gerenciamento de permissões?
### 2. **Sobre a Arquitetura e Escalabilidade**
- O sistema será um SaaS? Se sim, como escalaremos o banco de dados?
- Qual o nível de preocupação com alta disponibilidade? É imprescindível investir em ferramentas de alta disponibilidade, cache e filas?
### 3. **Sobre Métricas**
- Quais outras métricas são consideráveis pro monitoramento do sistema?

## Fase 3 - Final
### 1. **Observabilidade**
   - **Ferramentas de Observabilidade:** Implementar ferramentas mais robustas para monitoramento e rastreamento, como **Grafana**, **Jaeger** e **Grafana Loki**.

### 2. **Gestão de Logs**
   - **Ferramentas de Logs:** Adotar ferramentas específicas para gestão de logs, como **Elmah.io** ou **Serilog**.

### 3. **Design e Arquitetura**
   - **Padrões de Validação de Domínio:** Implementar padrões **SpecificationPattern** para mais aderência aos princípios SOLID, bem como melhorar a validação de regras de negócios (tornando a manutenção mais fácil).
     - Isso também pode ajudar a tornar os testes mais específicos, limpos e concisos.

### 4. **Processamento Assíncrono e Desempenho**
   - **Uso de Fila de Mensagens:** Integrar o **RabbitMQ** para processar tarefas em segundo plano, como salvar históricos.

### 5. **Auditoria e Rastreamento**
   - **Logs com EF Core:** Automatizar os Logs utilizando o EF Core rastreando as Entries de banco para saber o que foi criado, alterado ou removido.

### 6. **Conversão de Dados**
   - **Uso de Mappers:** Adotar o uso de **AutoMapper** ou bibliotecas similares para facilitar a conversão entre entidades e DTOs.

### 7. **Padrões de Criação de Objetos**
   - **Uso de Factories:** Utilizar **Factories** para a criação de serviços e repositórios, melhorando a manutenção do código.

### 8. **Autenticação e Autorização**
   - **Gerenciamento de Usuário Logado:** Criar estrutura para autenticar e autorizar usuários.

### 9. **Cobertura de Testes**
   - **Aumentar a Cobertura de Testes:** No momento, validamos principalmente as regras de negócio, mas o ideal é extender a cobertura dos testes pra Controllers, AppServices e Repositories.
