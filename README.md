# Toro Bank - Backend

# Tecnologias
O projeto de backend foi desenvolvido utilizando a plataforma .NET Core 6.0.
A arquitetura definida para o projeto foi o Clean Architecture, com o fim de obter a separação das responsabilidades e aplicar os princípios do SOLID.
Para isso, foi adotado utilizar os seguintes padrões:

  **Padrão Mediator**
  
  **Padrão CQRS - Command Query Responsability Segregation**

  Outras tecnologias usadas:
  Swagger
  Serilog
  Entity Framework Core 
  FluentValidation


# Como instalar as dependências do projeto
Antes de executa o projeto é necessário baixar suas dependências. Para realizar este procedimento, execute o comando abaixo:
```
dotnet restore
```

# Como executar a aplicação

Para executar a aplicação, execute o comando 
```
dotnet run
```
Ou no Visual Studio, pressione a tecla **F5** para executar em modo de debug

![image](https://user-images.githubusercontent.com/79017725/169442742-89fde6de-0ac6-4d18-9df7-48e76514a331.png)

Caso execute localmente com sucesso, aparecerá no navegador a seguinte página:

![image](https://user-images.githubusercontent.com/79017725/169444464-982e08ee-d3ec-4350-ae2b-db5021f6ca76.png)


# Como executar os testes
Para executar os testes unitários, pode-se realizar da seguinte maneira:
Execute o comando abaixo no console:
```
dotnet test
```
Ou com o Visual Studio aberto, selecione o menu Tests e execute os testes manualmente, conforme a imagem abaixo:

![image](https://user-images.githubusercontent.com/79017725/169443667-3e6f986d-8a89-4230-baa0-01b4ec4582fd.png)

Uma região mostrará os testes a serem realizados, basta clicar no botão de teste 

![image](https://user-images.githubusercontent.com/79017725/169443437-4c454668-d302-418d-9278-84ba7f7779ab.png)


