Hackathon Fiap Pós Tech

Vídeo de Apresentação: https://www.youtube.com/watch?v=Gk9sA1WTWuc

Documento de Requisitos


Projeto: Sistema de Agendamentos da HelthMed
			
			
			

1. Introdução

Este documento apresenta os requisitos de usuário da ferramenta Sistema de Agendamentos da HelthMed e está organizado da seguinte forma: a seção 2 contém uma descrição do propósito do sistema; a seção 3 contém uma descrição do cenário atual apresentando o problema; e a seção 4 apresenta a lista de requisitos de usuário levantados junto ao cliente.

2. Descrição do Propósito do Sistema

Atender a necessidade da HelthMed permitindo o cadastro de médicos e pacientes, cadastro de horários disponíveis na agenda dos médicos e agendamento de consultas para os pacientes em datas e horários disponíveis na agenda do médico escolhido.
3. Descrição do Cenário Atual
A Health&Med, é uma Operadora de Saúde que tem como objetivo digitalizar seus processos e operação. O principal gargalo da empresa é o Agendamento de Consultas Médicas, que atualmente ocorre exclusivamente através de ligações para a central de atendimento da empresa. 
4. Requisitos de Usuário
Tomando por base o contexto do sistema, foram identificados os seguintes requisitos de usuário: 
Requisitos Funcionais
Identificador	Descrição	Prioridade	Depende de
RF001	Cadastro do Usuário (Médico). O médico deverá poder se cadastrar preenchendo os campos obrigatórios: Nome, CPF, Número CRM, E-mail e Senha.	1	
RF002	Autenticação do Usuário (Médico). O sistema deve permitir que o médico faça login usando o e-mail e uma senha.	2	RF001
RF003	Cadastro/Edição de Horários Disponíveis (Médico). O sistema deve permitir que o médico faça o cadastro e a edição de seus dias e horários disponíveis para agendamento de consultas.	3	 RF001, RF002
RF004	Cadastro do Usuário (Paciente). O paciente poderá se cadastrar preenchendo os campos Nome, CPF, E- mail e Senha.	4	
RF005	Autenticação do Usuário (Paciente). O sistema deve permitir que o paciente faça login usando E-mail e Senha	5	RF004
RF006	Busca por Médicos (Paciente). O sistema deve permitir que o paciente visualize a listagem dos médicos disponíveis.	6	RF001, RF003, RF004, RF005
RF007	Agendamento de Consultas (Paciente). Após selecionar o médico, o paciente deve visualizar os dias e horários disponíveis do médico. O paciente poderá selecionar o horário de preferência e realizar o agendamento.	7	RF001, RF003, RF004, RF005
RF008	Notificação de consulta marcada (Médico). Após o agendamento realizado pelo usuário Paciente, o médico deverá receber um e-mail contendo:
Título do e-mail: ˮHealth&Med - Nova consulta agendadaˮ 
Corpo do e-mail: ˮOlá, Dr. {nome_do_médico}! 
Você tem uma nova consulta marcada! Paciente: {nome_do_paciente}. 
Data e horário: {data} às {horário_agendado}.ˮ	8	RF007

Regras de Negócio

Identificador	Descrição	Prioridade	Depende de
RN001	Apenas o usuário administrador pode adicionar saldo as carteiras		RF003
RN002	Apenas o usuário administrador pode cadastrar ativos no sistema (ações)		RF003
RN003	O Sistema deve ter controle de autorização nas funcionalidades de compra e venda de ações		RF003
RN004	Apenas o próprio usuário pode movimentar sua carteira (compra e venda de ações)		RF003
			

Requisitos Não Funcionais

Identificador	Descrição	Categoria	Escopo	Prioridade	Depende de
RNF001	Concorrência de Agendamentos • O sistema deverá ser capaz de suportar múltiplos acessos simultâneos e garantir que apenas uma marcação de consulta seja permitida para um determinado horário.				
RNF002	Validação de Conflito de Horários • O sistema deverá validar a disponibilidade do horário selecionado em tempo real, assegurando que não haja sobreposição de horários para consultas agendadas.				
RNF003	Uso de banco de dados relacional em nuvem				
RNF004	Uso API’s				


 
Detalhamento dos Requisitos Funcionais
![image](https://github.com/user-attachments/assets/b554d038-2606-4865-9482-ecc7e69c45a4)
![image](https://github.com/user-attachments/assets/cda37883-a304-48e5-9c6d-cc89a29fbfa4)
![image](https://github.com/user-attachments/assets/4e93da4e-752a-4a24-bb5a-d4e09af447d5)
![image](https://github.com/user-attachments/assets/579e2a41-98d9-4a69-835f-7f238a691f85)

RF001
Ao receber uma solicitação de auto cadastro através da rota /Cadastro/Medico, o sistema deve validar os campos obrigatórios Nome, CPF, Número CRM, Uf CRM, E-mail e Senha, então deve cadastrar o usuário médico no banco de dados.
![image](https://github.com/user-attachments/assets/6d13f41a-e1ec-4650-bfa4-6d8b0bc134e6)
Campos obrigatórios: 
•	nome: login do usuário a ser cadastrado.
•	senha: senha do usuário a ser cadastrado.
•	Nome: Nome do médico.![image](https://github.com/user-attachments/assets/dd97cdf0-68f7-4f7c-af29-1a8d4331fd68)

•	CPF: Número da pessoa no Cadastro de Pessoas Físicas da Receita Federal.
•	Número CRM: Número do médico no Cadastro Regional de Medicina.
•	Uf CRM: Unidade Federativa (estado) do CRM do médico.
•	E-mail: e-mail do médico (que será utilizado para o login).
•	Senha: senha de acesso do médico.
Campos opcionais: não possui.


RF002 Autenticação do Usuário (Médico). O sistema deve permitir que o médico faça login usando o e-mail e uma senha.
RF005 Autenticação do Usuário (Paciente). O sistema deve permitir que o paciente faça login usando E-mail e Senha.

Ao realizar uma solicitação de login através da rota /Login, o sistema deve verificar no banco de dados, validar se o usuário (e-mail) e senha informados existem e são válidos, então deve retornar um token de autenticação/autorização que deverá ser utilizado para as próximas iterações com o sistema.
![image](https://github.com/user-attachments/assets/0efdb0d7-d8b5-478a-bebf-67823d50d295)
Campos obrigatórios: 
•	email: login do usuário.
•	senha: senha do usuário.
Campos opcionais: não possui.


RF003
Ao realizar uma solicitação de cadastro de horários através da rota /Medico/CadastrarHorarios, o sistema deve gerar slots de 20 em 20 minutos dentro do período de horário inicial e final informado e disponibilizar estes horários (de 20 e 20 minutos) para agendamento de consultas pelos pacientes.
	Cadastro/Edição de Horários Disponíveis (Médico). O sistema deve permitir que o médico faça o cadastro e a edição de seus dias e horários disponíveis para agendamento de consultas.
![image](https://github.com/user-attachments/assets/d8577fb0-93bf-4446-88ff-7f27e0921505)
Campos obrigatórios: 
•	idMedico: identificador do usuário médico que disponibilizará o horário na sua agenda.
•	diaSemana: identificação do dia da semana que será disponibilizado.
•	horarioInicio: horário de início de disponibilização da agenda (geração dos slots de 20 minutos para as consultas).
•	horarioFim: horário final de disponibilização da agenda (geração dos slots de 20 minutos para as consultas).
•	Data: data que será disponibilizado horários (slots de 20 minutos) para as consultas.
Campos opcionais: não possui.


RF004
Ao receber uma solicitação de auto cadastro através da rota /Cadastro/Paciente, o sistema deve validar os campos obrigatórios Nome, CPF, E-mail e Senha, então deve cadastrar o usuário paciente no banco de dados.
![image](https://github.com/user-attachments/assets/727bf4a6-ede7-4e66-9d61-abe9275ca6ec)
Campos obrigatórios: 
•	nome: login do usuário a ser cadastrado.
•	senha: senha do usuário a ser cadastrado.
•	Nome: Nome do paciente.
•	CPF: Número da pessoa no Cadastro de Pessoas Físicas da Receita Federal.
•	E-mail: e-mail do paciente (que será utilizado para o login).
•	Senha: senha de acesso do paciente.
Campos opcionais: não possui.

RF006
	Busca por Médicos (Paciente). O sistema deve permitir que o paciente visualize a listagem dos médicos disponíveis.
![image](https://github.com/user-attachments/assets/5177a910-367b-4ae0-896f-456381b64080)
Campos obrigatórios: não possui.
Campos opcionais: não possui.

RF007
	Agendamento de Consultas (Paciente). Após selecionar o médico, o paciente deve visualizar os dias e horários disponíveis do médico. O paciente poderá selecionar o horário de preferência e realizar o agendamento.
![image](https://github.com/user-attachments/assets/706cadfb-5afd-4716-93d1-7802f8e6e80f)
Campos obrigatórios: 
•	pacienteId: identificador do usuário paciente que está agendando a consulta.
•	medicoId: identificação médico que possui a agenda onde o horário será marcado.
•	horarioId: identificação do horário da agenda que será marcada a consulta).
•	dataConsulta: data de agendamento da consulta.
Campos opcionais: não possui.

RF008
	Notificação de consulta marcada (Médico). Após o agendamento realizado pelo usuário Paciente, o médico deverá receber um e-mail contendo:
Título do e-mail: ˮHealth&Med - Nova consulta agendadaˮ 
Corpo do e-mail: ˮOlá, Dr. {nome_do_médico}! 
Você tem uma nova consulta marcada! Paciente: {nome_do_paciente}. 
Data e horário: {data} às {horário_agendado}.ˮ
![image](https://github.com/user-attachments/assets/0468ed67-cf66-4052-8616-7e49a212bc59)
![image](https://github.com/user-attachments/assets/75482b87-57be-48f1-a126-ec8050e9b16f)



Na raiz do repositorio possui o documento de requisitos com os detalhes do nosso projeto
Para rodar o projeto bastar clonar o projeto e executar em uma maquina que possua .net 7.0
os métodos de cadastro de médico e paciente são abertos para serem usados e após o cadastro deve-se gerar o token no método de login para autorizar e autenticar o uso nas rotas diversas
Caso haja erros no metodos é possivel que haja um problema de conexao com o banco devido as configurações de segurança do banco no azure, pois estamos utilizando um sqlserver na nuvem do azure e acontecem alguns problemas de bloqueio de ip ao tentar acessar, caso ocorra é possivel pedir para um dos integrantes do grupo liberar o ip


 



