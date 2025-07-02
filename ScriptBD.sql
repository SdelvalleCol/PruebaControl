-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 02, 2025 at 09:45 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `control_prueba`
--

-- --------------------------------------------------------

--
-- Table structure for table `imagenes_productos`
--

CREATE TABLE `imagenes_productos` (
  `identificador_imagenes_productos` char(36) NOT NULL,
  `nombre_imagen` varchar(30) NOT NULL DEFAULT 'Nombre Imagen',
  `url_imagen` varchar(500) NOT NULL,
  `identificador_producto` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `imagenes_productos`
--

INSERT INTO `imagenes_productos` (`identificador_imagenes_productos`, `nombre_imagen`, `url_imagen`, `identificador_producto`) VALUES
('731ad4d1-5765-11f0-abe0-bceca01d159c', 'Shampo pantene 1', 'https://olimpica.vtexassets.com/arquivos/ids/1518509/7506309840024_1.jpg?v=638646907592970000', '2acfa059-5765-11f0-abe0-bceca01d159c'),
('82857b12-5765-11f0-abe0-bceca01d159c', 'Shampo pantene 2', 'https://exitocol.vtexassets.com/arquivos/ids/28943388/Champu-De-Cabello-PANTENE-Repara-y-Protege-510-ml-3569408_d.jpg?v=638860467714570000', '2acfa059-5765-11f0-abe0-bceca01d159c'),
('9e678b9e-5765-11f0-abe0-bceca01d159c', 'Colgate Blanca', 'https://mercaldas.vtexassets.com/arquivos/ids/1342276-800-auto?v=638743859267970000&width=800&height=auto&aspect=true', '47b4ff5e-5765-11f0-abe0-bceca01d159c'),
('b04985c6-5765-11f0-abe0-bceca01d159c', 'Colgate Clasica', 'https://vaquitaexpress.com.co/media/catalog/product/cache/e89ece728e3939ca368b457071d3c0be/7/5/7509546696768_1.jpg', '47b4ff5e-5765-11f0-abe0-bceca01d159c');

-- --------------------------------------------------------

--
-- Table structure for table `productos`
--

CREATE TABLE `productos` (
  `id_producto` char(36) NOT NULL,
  `nombre_producto` varchar(30) NOT NULL DEFAULT 'Nombre Base',
  `descripcion_producto` varchar(100) NOT NULL DEFAULT 'Descripcion Base',
  `precio_producto` double NOT NULL,
  `fecha_creacion_producto` datetime NOT NULL DEFAULT current_timestamp(),
  `estado` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `productos`
--

INSERT INTO `productos` (`id_producto`, `nombre_producto`, `descripcion_producto`, `precio_producto`, `fecha_creacion_producto`, `estado`) VALUES
('2acfa059-5765-11f0-abe0-bceca01d159c', 'Shampoo2', 'Pantene', 10000, '2025-07-02 11:54:07', 1),
('47b4ff5e-5765-11f0-abe0-bceca01d159c', 'Crema Dental23', 'Crema Dental descripcion', 5000, '2025-07-02 11:54:55', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `imagenes_productos`
--
ALTER TABLE `imagenes_productos`
  ADD PRIMARY KEY (`identificador_imagenes_productos`),
  ADD KEY `fk_imagenes_productos` (`identificador_producto`);

--
-- Indexes for table `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`id_producto`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `imagenes_productos`
--
ALTER TABLE `imagenes_productos`
  ADD CONSTRAINT `fk_imagenes_productos` FOREIGN KEY (`identificador_producto`) REFERENCES `productos` (`id_producto`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
