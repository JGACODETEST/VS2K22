# VS2K22
VS2K22

# VERSIONES
2024/05/06 v.1.0.0 
- INICIO

v.1.0.1

# INSTALACION

1. INSTALA DOCKER ENGINE 1.8+

2. DESCARGAR IMAGEN DOCKER

	SQL SERVER
	
		SQL 2022: docker pull mcr.microsoft.com/mssql/server:2022-latest
		SQL 2019: docker pull mcr.microsoft.com/mssql/server:2019-latest	
		SQL 2017: docker pull mcr.microsoft.com/mssql/server:2017-latest
	
	MYSQL
		LATEST
			docker pull mysql	
			docker pull mysql:latest			
		8.0: docker pull mysql:8.0			
		5.7: docker pull mysql:5.7
			
3. CREAR VOLUMES DOCKER
		docker volume create sql2K19-vol

4. LEVANTAR CONTAINER CON IMAGEN DOCKER

	SQL SERVER (CAMBIAR <YourPassword> POR EL PASSWORD A UTILIZAR (sql2K19@))
		[OPTIONAL] CONECTA INSTANCIA : sqlcmd -S localhost,1433 -U SA -P "<YourPassword>"
		
		SQL 2022
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K22@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 --name sql2K22 --hostname sql2K22 -d mcr.microsoft.com/mssql/server:2022-latest 
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K22@" -p 1433:1433 --name sql2K22 --hostname sql2K22 -d mcr.microsoft.com/mssql/server:2022-latest 	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 --name sql2K22 --hostname sql2K22 -v E:/DOCKER/DockerDir/SQL2K22/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K22/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K22/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2022-latest 
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K22@" -p 1433:1433 --name sql2K22 --hostname sql2K22 -v E:/DOCKER/DockerDir/SQL2K22/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K22/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K22/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2022-latest 
			
			
			DETENER CONTAINER: docker stop sql2K22
			
			
		SQL 2019
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K19@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 --name sql2K19 --hostname sql2K19 -d mcr.microsoft.com/mssql/server:2019-latest 
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K19@" -p 1433:1433 --name sql2K19 --hostname sql2K19 -d mcr.microsoft.com/mssql/server:2019-latest 	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K19@" -p 1433:1433 --name sql2K19 --hostname sql2K19 -v E:/DOCKER/DockerDir/SQL2K19/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K19/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K19/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest 	
			(*) docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K19@" -p 1433:1433 --name sql2K19 --hostname sql2K19 -v E:/DOCKER/DockerDir/SQL2K19/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K19/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K19/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest 	


			INICIAR CONTAINER: docker start sql2K19
			DETENER CONTAINER: docker stop sql2K19
		
		
		SQL 2017

			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K17@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourPassword>" -p 1433:1433 --name sql2K17 --hostname sql2K17 -d mcr.microsoft.com/mssql/server:2017-latest 
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K17@" -p 1433:1433 --name sql2K17 --hostname sql2K17 -d mcr.microsoft.com/mssql/server:2017-latest 	
			
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K17@" -p 1433:1433 --name sql2K17 --hostname sql2K17 -v E:/DOCKER/DockerDir/SQL2K17/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K17/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K17/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest 	
			docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql2K17@" -p 1433:1433 --name sql2K17 --hostname sql2K17 -v E:/DOCKER/DockerDir/SQL2K17/data:/var/opt/mssql/data -v E:/DOCKER/DockerDir/SQL2K17/log:/var/opt/mssql/log -v E:/DOCKER/DockerDir/SQL2K17/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest 	



			DETENER CONTAINER: docker stop sql2K17
			
			
	MYSQL
	
		docker pull container-registry.oracle.com/mysql/community-server:tag


		LATEST
			docker run -p 3306:3306 --name mySqlLatest -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:latest
			
			docker run -p 3306:3306 --name=mySqlLatest -v E:\DOCKER\DockerDir\MYSQLLatest:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:latest
			
			
			DETENER CONTAINER: docker stop mySqlLatest
			
		8.0			
			docker run -p 3306:3306 --name mySql80 -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:8.0
			
			docker run -p 3306:3306 --name=mySql80 -v E:\DOCKER\DockerDir\MYSQL80:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:8.0
			
			
			DETENER CONTAINER: docker stop mySql80
			
		5.7			
			docker run -p 3306:3306 --name mySql57 -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:5.7
			
			(*) docker run -p 3306:3306 --name=mySql57 -v E:\DOCKER\DockerDir\MYSQL57:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=root@2K24 -d mysql:5.7
	
	
			INICIAR CONTAINER: docker start mySql57
			DETENER CONTAINER: docker stop mySql57


	