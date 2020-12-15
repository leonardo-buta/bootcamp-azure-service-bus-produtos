<h1 align="center">
  Programa aceleração global dev #3 - Azure Service Bus
  <br>
</h1>

<p align="center">
  <a href="#descrição">Descrição</a> •
  <a href="#tecnologias-utilizadas">Tecnologias utilizadas</a> •
  <a href="#como-inicializar-o-projeto">Como inicializar o projeto</a> •
  <a href="#estrutura-do-projeto">Estrutura do Projeto</a> •
  <a href="#créditos">Créditos</a>
</p>

## Descrição
Esse projeto foi construído para o bootcamp aceleração global dev #3, que tem como objetivo construir um sistema que faça o uso do Azure Service Bus em um cenário de e-commerce utilizando a publicação e o consumo de mensagens entre diferentes banco de dados.

## Tecnologias utilizadas

* .NET (Core) 5
* Web API com Swagger
* MS SQL Local DB
* Azure Service Bus

## Como inicializar o projeto

<p>Realize o clone do projeto, navegue até a pasta e execute o comando dotnet run.
Alternativamente você pode abrir a solution principal (EVendas.sln) e usa-lo com o Visual Studio ou Visual Studio Code.
Em seguida, crie uma estrura no Azure Service Bus conforme abaixo:

No Service Bus, crie três tópicos:
* produtocriado
* produtoeditado
* produtovendido

Para cada tópico, crie as subscriptions de acordo com cada tópico acima, respectivamente:
* ProdutoCriadoServicoVendas
* ProdutoEditadoServicoVendas
* ProdutoVendidoServicoEstoque

Uma imagem abaixo para facilitar:

![screenshot](/img/estrutura-azure.png)

</p>

Por último, coloque a connection string do seu Service Bus no app.settings.json das duas API(EVendas.API.ModuloEstoque) e (Evendas.API.ModuloVendas).

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

**1 - API**
<p>
  Contém duas WEB Api, com swagger instalado, para facilitar a manipulação de dados.
  
  Projetos:
  
  - EVendas.API.ModuloEstoque
  
  
  - Evendas.API.ModuloVendas
</p>

**2 - Application**
<p>
  Contém as classes de serviço e a lógica de negócio, bem como as classes necessárias para fazer a comunicação com o ServiceBus.
  Projeto: Evendas.Application
</p>

**3 - Domain**
<p>
  Contém as models e interfaces do projeto.
  
  Projeto: Evendas.Domain
  
</p>  

**4 - Data**
<p>
  Contém a camada de comunicação com o banco, o entity framework e os repositórios, bem como a camada de IoC.
  
  Projetos:
  - Evendas.Data
  
  - Evendas.IoC
  
</p>

![screenshot](/img/estrutura-projeto.png)

## Créditos

- [EquinoxProject](https://github.com/EduardoPires/EquinoxProject) - Estrutura do projeto em camadas
- [AspNetCoreServiceBus](https://github.com/damienbod/AspNetCoreServiceBus) - Estrutura de Sender e Consumer do Azure Service Bus
---
