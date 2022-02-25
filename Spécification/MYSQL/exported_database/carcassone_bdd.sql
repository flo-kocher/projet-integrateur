-- phpMyAdmin SQL Dump
-- version 4.9.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Feb 25, 2022 at 10:05 PM
-- Server version: 5.7.24
-- PHP Version: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `carcassone_bdd`
--

-- --------------------------------------------------------

--
-- Table structure for table `composer`
--

CREATE TABLE `composer` (
  `ID_PARTIE` int(11) NOT NULL,
  `ID_HISTORIQUE` int(11) NOT NULL,
  `SCORE` int(11) DEFAULT NULL,
  `CLASSEMENT` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `etre_ami`
--

CREATE TABLE `etre_ami` (
  `JOU_ID_JOUEUR` int(11) NOT NULL,
  `ID_JOUEUR` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `historique`
--

CREATE TABLE `historique` (
  `ID_HISTORIQUE` int(11) NOT NULL,
  `ID_JOUEUR` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `jouer`
--

CREATE TABLE `jouer` (
  `ID_JOUEUR` int(11) NOT NULL,
  `ID_PARTIE` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `composer`
--
ALTER TABLE `composer`
  ADD PRIMARY KEY (`ID_PARTIE`,`ID_HISTORIQUE`);

--
-- Indexes for table `etre_ami`
--
ALTER TABLE `etre_ami`
  ADD PRIMARY KEY (`JOU_ID_JOUEUR`,`ID_JOUEUR`);

--
-- Indexes for table `historique`
--
ALTER TABLE `historique`
  ADD PRIMARY KEY (`ID_HISTORIQUE`);

--
-- Indexes for table `jouer`
--
ALTER TABLE `jouer`
  ADD PRIMARY KEY (`ID_JOUEUR`,`ID_PARTIE`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
