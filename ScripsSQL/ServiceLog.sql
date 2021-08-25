Create Table ServiceLog
(
	Id int identity primary key,
	Process varchar(50),
	DataLog DateTime default(GetDate()),
	LogMessage varchar(Max)
)
go
create index Ix_ServiceLog_001 on ServiceLog (Process,DataLog) include (LogMessage)