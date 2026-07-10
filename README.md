Sistema de Estacionamento

Tecnologia : Asp NET CORE MVC 10 com C#

IDE que Utilizo pra Desenvolver e Rodar: Visual Studio 2026 ou Visual Studio 2022. (IDEs compativeis com AspNet Core 10)

Gerenciador que Utilizo para o Banco de Dados: Microsoft Sql Server 2022 com Microsoft Sql Server Management Studio 21(ou a versão mais recente, fique a vontade)

1- Configuração do Banco de dados no arquivo appsettings.Development.json , por gentileza preencher de acordo com suas informações.

    "ConnectionStrings": {
      "SistemaEstacionamentoContext": "Data Source=NomeInstânciaDB;Initial Catalog=SistemaEstacionamento;User ID=UsuarioBanco;Password=SenhaBanco;Encrypt=False"
    },
    
  
    "EmailConfiguration": {
      "NomeRemetente": "seu nome",
      "EmailRemetente": "seu email",
      "Senha": "senha do email",
      "EnderecoServidorEmail": "o smtp do seu email",
      "PortaServidorEmail": 587(se não for essa porta coloca outra correspondente a seu smtp),
      "UsarSsl": true
    }

  

2-Por gentileza , pegar o backup ou script do banco de dados que está no projeto SistemaEstacionamento.Main em:

     a)..\SistemaEstacionamento.Main\Data\BackupDatabase\SistemaEstacionamento.bak
     
     ou
     
     b)..\SistemaEstacionamento\SistemaEstacionamento.Main\Data\ScriptDatabase\SistemaEstacionamento.sql
    
     c)e restaurar o backup ou rodar o script no seu gerenciador do banco de dados


3- Em seguida rodar a solução no seu Visual Studio pressionando F5

4- Ou Pode rodar também na IDE Visual Code com os seguintes comandos na seguinte ordem no terminal:
    1-dotnet build
    2-dotnet run --project .\SistemaEstacionamento.Main\SistemaEstacionamento.Main.csproj
