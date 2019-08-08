# Robbiblubber in nutshell

bitbucket repositories - https://bitbucket.org/%7Beccaa04e-8f94-40b3-a78a-7305e37074a6%7D/

**rule** - the project must not running into folder with space (ex. c:\this is a test\test.csproj)

**rule** - java.exe must be in PATH variable in System Variables (aka c:\jre86\bin)

**good to know** - Robbiblubber.dlls compiled under frm472, but you can use it also under frm4.5 (omg?)

at the time of writing, the Robbiblubber.libraries last updated 2018/10. With a quick read, he is using a **thing** called **server** where execute commands (what developer has wrote to .net project) via :
```csharp
protected void _LaunchHost(object param)
{
	Thread.Sleep(80); //booooooo!

	_Host = new Process();

	_Host.StartInfo.RedirectStandardOutput = true;
	_Host.StartInfo.UseShellExecute = false;
	_Host.StartInfo.CreateNoWindow = true;

	_Host.StartInfo.FileName = "java.exe";
	_Host.StartInfo.Arguments = "-jar " + ((string[]) param)[0] + " " + ((string[]) param)[1];
	_Host.Start();
}
```

some times you have to use JDBCConnection when you working with other vendors and needed to exchange data on secure environment (here just LOL, these strings passing to Process stay at system memory)..

--

Including a **basic working example** to create & connect to sqlite.

For other dbase systems, download the required JAR & replace the connection string :

  - SQL Server - [Download JAR](https://docs.microsoft.com/en-us/sql/connect/jdbc/using-the-jdbc-driver?view=sql-server-2017) (tested with mssql-jdbc-7.4.1.jre8.jar)  [Building the Connection URL](https://docs.microsoft.com/en-us/sql/connect/jdbc/building-the-connection-url?view=sql-server-2017)
```sql
JdbcConnection cn = new JdbcConnection("jdbc:sqlserver://xx.xx.xx.xx:13021;databaseName=POPAY", "com.microsoft.sqlserver.jdbc.SQLServerDriver","user","password");
```

  - Oracle - [Download Oracle Database 12c Release 2 (12.2.0.1) drivers](https://www.oracle.com/technetwork/database/application-development/jdbc/downloads/index.html) (tested with ojdbc8.jar) 
```sql
JdbcConnection cn = new JdbcConnection("jdbc:oracle:thin:@HOSTNAME:1521:SERVICENAME", "oracle.jdbc.driver.OracleDriver", "user", "pass");

--TNSNAMES style - src - https://stackoverflow.com/a/6047081
JdbcConnection cn = jdbc:oracle:thin:@(description=(address=(host=HOSTNAME)(protocol=tcp)(port=PORT))(connect_data=(service_name=SERVICENAME)(server=SHARED)))
--TNSNAMES style - src - https://stackoverflow.com/a/4832205
JdbcConnection cn = jdbc:oracle:thin:@(DESCRIPTION =(ADDRESS_LIST =(ADDRESS =(PROTOCOL=TCP)(HOST=blah.example.com)(PORT=1521)))(CONNECT_DATA=(SID=BLAHSID)(GLOBAL_NAME=BLAHSID.WORLD)(SERVER=DEDICATED)))
```
