USE [Cupones]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[Id_Articulo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Articulo] [varchar](100) NOT NULL,
	[Descripcion_Articulo] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Precio] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[Id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones](
	[Id_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[PorcentajeDto] [numeric](18, 2) NULL,
	[ImportePromo] [numeric](18, 2) NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[Id_Tipo_Cupon] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cupones_1] PRIMARY KEY CLUSTERED 
(
	[Id_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Clientes]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Clientes](
	[Id_Cupon] [int] NOT NULL,
	[NroCupon] [varchar](100) NOT NULL,
	[FechaAsignado] [datetime] NOT NULL,
	[Id_Usuario] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Detalle]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Detalle](
	[Id_Cupon] [int] NOT NULL,
	[id_Articulo] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Cupones_Detalle] PRIMARY KEY CLUSTERED 
(
	[Id_Cupon] ASC,
	[id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Historial]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Historial](
	[NroCupon] [varchar](100) NOT NULL,
	[FechaUso] [date] NOT NULL,
	[Id_Usuario] [varchar](50) NOT NULL,
	[Id_Cupon] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_Rol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Cupon]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Cupon](
	[Id_Tipo_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo_Cupon] PRIMARY KEY CLUSTERED 
(
	[Id_Tipo_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 24/06/2025 10:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Dni] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Estado] [bit] NOT NULL,
	[Id_Rol] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cupones] ADD  CONSTRAINT [DF__Cupones__Activo__73BA3083]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Cupones_Historial] ADD  DEFAULT ((0)) FOR [Id_Cupon]
GO
ALTER TABLE [dbo].[Cupones]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Tipo_Cupon] FOREIGN KEY([Id_Tipo_Cupon])
REFERENCES [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon])
GO
ALTER TABLE [dbo].[Cupones] CHECK CONSTRAINT [FK_Cupones_Tipo_Cupon]
GO
ALTER TABLE [dbo].[Cupones_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Clientes_Cupones1] FOREIGN KEY([Id_Cupon])
REFERENCES [dbo].[Cupones] ([Id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Clientes] CHECK CONSTRAINT [FK_Cupones_Clientes_Cupones1]
GO
ALTER TABLE [dbo].[Cupones_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Clientes_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Cupones_Clientes] CHECK CONSTRAINT [FK_Cupones_Clientes_Usuarios]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Articulos] FOREIGN KEY([id_Articulo])
REFERENCES [dbo].[Articulos] ([Id_Articulo])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Articulos]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Cupones1] FOREIGN KEY([Id_Cupon])
REFERENCES [dbo].[Cupones] ([Id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Cupones1]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([Id_Rol])
REFERENCES [dbo].[Roles] ([Id_Rol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
