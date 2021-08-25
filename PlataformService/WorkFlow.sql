CREATE TABLE [dbo].[WorkFlow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataInclusao] [datetime] NULL,
	[EmailDestino] [varchar](8000) NOT NULL,
	[DataEnvio] [datetime] NULL,
	[EnvioOK] [bit] NULL,
	[Assunto] [varchar](517) NULL,
	[CorpoEmail] [varchar](8000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WorkFlow] ADD  DEFAULT (getdate()) FOR [DataInclusao]
GO

ALTER TABLE [dbo].[WorkFlow] ADD  DEFAULT ((0)) FOR [EnvioOK]
GO



/*
** Autor: Daniel Pitthan Silveira
** Obtem os anexos pelo Id do Workflow
** 
** Exec GetWorkflows 
*/

Create or alter  procedure GetWorkflows
As
select 
	w.Id,w.DataInclusao,
	w.EmailDestino,w.DataEnvio
	,w.EnvioOK,w.Assunto,w.CorpoEmail
from [WorkFlow] as w with(nolock)
where EnvioOK=0

go



/*
** Autor: Daniel Pitthan Silveira
** Obtem os anexos pelo Id do Workflow
** 
** Exec GetAttachments 1
*/
create or alter  procedure GetAttachments
	( @WorkFlowId int)
as 
select  
		Id
		,Nome
		,Descricao
		,ImgBase64
		,ProdutoId
		,DataInclusao
		,Ativo
		,NomeOriginal
		,UsuarioId
		,PathServer
		,Extensao
		,Tamanho
		,RelativePath
		,IsCapa
		,ZC0_NUM
		,WorkflowId

from ImagensTickets
where WorkflowId= @WorkFlowId
and Ativo=1