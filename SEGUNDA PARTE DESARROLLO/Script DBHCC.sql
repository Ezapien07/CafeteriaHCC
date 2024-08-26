CREATE DATABASE HCC;
GO

USE HCC;
GO

CREATE TABLE Tb_HccAlmacen(
alm_id INT IDENTITY(1,1) PRIMARY KEY,
alm_cantidad INT NOT NULL,
alm_fecha_actualizacion DATE NOT NULL,
alm_estatus TINYINT NOT NULL
);

CREATE TABLE Tb_HccMesas(
mes_id INT IDENTITY(1,1) PRIMARY KEY,
mes_lugares SMALLINT NOT NULL,
mes_disponible TINYINT NOT NULL,
mes_estatus TINYINT NOT NULL
);

CREATE TABLE Tb_HccCatEstatusOrden(
catord_id INT IDENTITY(1,1) PRIMARY KEY,
catord_nombre VARCHAR(50) NOT NULL,
catord_estatus TINYINT NOT NULL);

CREATE TABLE Tb_HccProductos(
pro_id INT IDENTITY(1,1) PRIMARY KEY,
alm_id INT NOT NULL,
pro_nombre VARCHAR(50) NOT NULL,
pro_descripcion VARCHAR(120) NOT NULL,
pro_precio DECIMAL (10,4) NOT NULL,
pro_estatus TINYINT NOT NULL,
CONSTRAINT FK_Tb_HccAlmacen FOREIGN KEY (alm_id) REFERENCES Tb_HccAlmacen(alm_id)
);

CREATE TABLE Tb_HccOrdenes(
ord_id INT IDENTITY(1,1) PRIMARY KEY,
mes_id INT NOT NULL,
catord_id INT NOT NULL,
ord_fecha_inicio DATE NOT NULL,
ord_estatus TINYINT NOT NULL
CONSTRAINT FK_Tb_HccMesas FOREIGN KEY (mes_id) REFERENCES Tb_HccMesas(mes_id),
CONSTRAINT FK_Tb_HccCatEstatusOrden FOREIGN KEY (catord_id) REFERENCES Tb_HccCatEstatusOrden(catord_id)
);

CREATE TABLE Tb_HccOrdenesDetalle(
orddet_id INT IDENTITY(1,1) PRIMARY KEY,
ord_id INT NOT NULL,
pro_id INT NOT NULL,
orddet_cantidad SMALLINT NOT NULL,
orddet_estatus TINYINT NOT NULL,
CONSTRAINT FK_Tb_HccOrdeness FOREIGN KEY (ord_id) REFERENCES Tb_HccOrdenes(ord_id),
CONSTRAINT FK_Tb_HccProductos FOREIGN KEY (pro_id) REFERENCES Tb_HccProductos(pro_id)
);


INSERT INTO Tb_HccAlmacen (alm_cantidad, alm_fecha_actualizacion, alm_estatus)
VALUES 
(100,'2024-08-01',1),
(200,'2024-08-02',1),
(150,'2024-08-03',1),
(120,'2024-08-04',1),
(250,'2024-08-08',1);

INSERT INTO Tb_HccMesas (mes_lugares, mes_disponible, mes_estatus)
VALUES 
(2,1,1),
(4,1,0),
(6,1,1),
(6,1,0),
(8,1,1),
(8,1,0),
(6,1,1),
(4,1,1),
(2,1,0);

INSERT INTO Tb_HccProductos (alm_id, pro_nombre, pro_descripcion, pro_precio, pro_estatus)
VALUES 
(1,'Café Americano','Café negro sin azúcar',2.5000,1),
(2,'Capuccino','Café con leche y espuma',3.5000,1),
(2,'Latte','Café con leche suave',3.2000,1),
(2,'Mocaccino','Café con chocolate y crema',3.8000,1),
(3,'Té Verde','Té verde caliente',2.0000,1),
(3,'Té Negro','Té negro fuerte',2.1000,1),
(4,'Croissant','Croissant de mantequilla',1.5000,1),
(5,'Panini','Panini de jamón y queso',4.0000,1);

INSERT INTO Tb_HccCatEstatusOrden (catord_nombre, catord_estatus)
VALUES 
('Carlos Pérez',1),
('María López',2),
('José García',3),
('Ana Martínez',4),
('Luis Hernández',5),
('Sofía González',1),
('Pedro Sánchez',2),
('Lucía Ramírez',3);

INSERT INTO Tb_HccOrdenes (mes_id, catord_id, ord_fecha_inicio, ord_estatus)
VALUES 
(1,1,'2024-08-22',1),
(2,2,'2024-08-22',1),
(3,3,'2024-08-22',1),
(4,4,'2024-08-22',1),
(5,5,'2024-08-22',1),
(6,6,'2024-08-22',1),
(7,7,'2024-08-22',1),
(8,8,'2024-08-22',1);

INSERT INTO Tb_HccOrdenesDetalle (ord_id, pro_id, orddet_cantidad, orddet_estatus)
VALUES 
(1,1,2,1),
(2,2,1,0),
(3,3,1,1),
(4,4,2,1),
(5,5,1,1),
(6,6,2,1),
(7,7,3,0),
(8,8,1,1);
