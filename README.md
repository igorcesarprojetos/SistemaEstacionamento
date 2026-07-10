Sistema de Estacionamento

Tecnologia : Asp NET CORE MVC 10

IDE que Utilizo pra Desenvolver: Visual Studio 2026 Community

IDE que Utilizo para o Banco de Dados: Microsoft Sql Server 2022 com Microsoft Sql Server Management Studio 21(ou mais recente)

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

  

  2-Por gentileza , pegar o backup ou script do banco de dados que esá no projeto SistemaEstacionamento.Main em:
   a)..\SistemaEstacionamento.Main\Data\BackupDatabase\SistemaEstacionamento.bak
   
   ou
   
   b)..\SistemaEstacionamento\SistemaEstacionamento.Main\Data\ScriptDatabase\SistemaEstacionamento.sql
