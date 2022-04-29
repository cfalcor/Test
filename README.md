# Test

Proyecto ASP.NET Core Web App MVC en .NET 5 con Enity Framework 5.0. Se ha usado IDE VS22.

Dada la facilidad de uso del ejecutbale de .NET Core se ignora la ejecuión del IIS express y se usa como stand alone.
Al ejecutar la aplicación pedirá la instalación de los certificados. Aceptamos.

El proyecto necesita de una conexión a una BD SQL Express "Model". Esta BD necesita la tabla "Products".
Se adjunta al proyecto una copia de la BD.
Cadena de conexión para la BD: "Server=localhost\SQLEXPRESS;Database=model;Trusted_Connection=True;"
No se usa autentificación ya que no es necesaria para este test.

Script para la tabla "Products", por si no se quiere restaurar la copia de BD:

''''
	CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [varchar](max) NULL,
	[price] [decimal](18, 2) NOT NULL,
	[FamilyProduct] [varchar](255) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
''''

SWAGGER
Este proyecto contiene Swagger. Para viusalizarlo acceder a la url "https://localhost:5001/swagger".

UNIT TEST
Este proyecto contiene Unit Test. Se ha usado NUnit para la ejeccución de los test. Estos se encuentran en otro proyecto (UnitTest) dentro de la misma solución. 