/*
SQLyog Community v11.01 (32 bit)
MySQL - 5.1.37 : Database - dentalsmile
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dentalsmile` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `dentalsmile`;

/*Table structure for table `doctor` */

DROP TABLE IF EXISTS `doctor`;

CREATE TABLE `doctor` (
  `userid` varchar(15) NOT NULL,
  `password` varchar(15) NOT NULL,
  `fname` varchar(45) NOT NULL,
  `lname` varchar(45) DEFAULT NULL,
  `birthdate` date DEFAULT NULL,
  `birthplace` varchar(45) DEFAULT NULL,
  `address1` varchar(45) DEFAULT NULL,
  `address2` varchar(45) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `created` date DEFAULT NULL,
  `createdBy` varchar(45) DEFAULT NULL,
  `modified` timestamp NULL DEFAULT NULL,
  `modifiedBy` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`userid`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `doctor` */

/*Table structure for table `measurement` */

DROP TABLE IF EXISTS `measurement`;

CREATE TABLE `measurement` (
  `measurement_id` int(7) NOT NULL AUTO_INCREMENT,
  `patient_id` char(7) NOT NULL,
  `file_id` int(7) NOT NULL,
  `measurement_status` int(1) DEFAULT NULL,
  `measurement_lastupdate` date DEFAULT NULL,
  `measured_by` char(25) DEFAULT NULL,
  PRIMARY KEY (`measurement_id`,`patient_id`,`file_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `measurement` */

/*Table structure for table `measurement_detail` */

DROP TABLE IF EXISTS `measurement_detail`;

CREATE TABLE `measurement_detail` (
  `measurement_detail_id` int(7) NOT NULL AUTO_INCREMENT,
  `measurement_id` int(7) NOT NULL,
  `tooth_id` int(2) DEFAULT NULL,
  PRIMARY KEY (`measurement_detail_id`,`measurement_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `measurement_detail` */

/*Table structure for table `patient` */

DROP TABLE IF EXISTS `patient`;

CREATE TABLE `patient` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'auto-increment has a limit;\nor generated with format SUN0104130001',
  `fname` varchar(45) NOT NULL,
  `lname` varchar(45) DEFAULT NULL,
  `birthdate` date NOT NULL,
  `birthplace` varchar(50) NOT NULL,
  `gender` char(1) NOT NULL COMMENT 'M;F',
  `address1` varchar(100) NOT NULL,
  `address2` varchar(100) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `phone` varchar(15) NOT NULL,
  `created` date DEFAULT NULL,
  `createdBy` varchar(45) DEFAULT NULL,
  `modified` timestamp NULL DEFAULT NULL,
  `modifiedBy` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `patient` */

/*Table structure for table `pfile` */

DROP TABLE IF EXISTS `pfile`;

CREATE TABLE `pfile` (
  `PATIENT` int(11) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `filename` varchar(35) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `type` tinyint(5) NOT NULL DEFAULT '0',
  `created` date DEFAULT NULL COMMENT 'uploaded',
  `createdBy` varchar(50) DEFAULT NULL,
  `modified` timestamp NULL DEFAULT NULL,
  `modifiedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_PFILE_PATIENT` (`PATIENT`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `pfile` */

/*Table structure for table `phase` */

DROP TABLE IF EXISTS `phase`;

CREATE TABLE `phase` (
  `id` int(1) NOT NULL,
  `name` varchar(25) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `phase` */

/*Table structure for table `tooth` */

DROP TABLE IF EXISTS `tooth`;

CREATE TABLE `tooth` (
  `number` int(2) NOT NULL,
  `type` int(1) DEFAULT NULL COMMENT '1:upper ; 2:lower',
  `name` varchar(25) NOT NULL,
  PRIMARY KEY (`number`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `tooth` */

/*Table structure for table `treatment` */

DROP TABLE IF EXISTS `treatment`;

CREATE TABLE `treatment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `PHASE` int(1) DEFAULT NULL,
  `PATIENT` int(11) DEFAULT NULL,
  `DOCTOR` varchar(15) DEFAULT NULL,
  `tdate` date DEFAULT NULL,
  `ttime` time DEFAULT NULL,
  `room` varchar(45) DEFAULT NULL,
  `created` date DEFAULT NULL,
  `createdBy` varchar(45) DEFAULT NULL,
  `modified` timestamp NULL DEFAULT NULL,
  `modifiedBy` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_TREATMENT_PATIENT` (`PATIENT`),
  KEY `fk_TREATMENT_DOCTOR` (`DOCTOR`),
  KEY `fk_TREATMENT_PHASE` (`PHASE`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `treatment` */

/*Table structure for table `treatment_pfile` */

DROP TABLE IF EXISTS `treatment_pfile`;

CREATE TABLE `treatment_pfile` (
  `TREATMENT` int(11) NOT NULL,
  `PFILE` int(11) NOT NULL,
  PRIMARY KEY (`TREATMENT`,`PFILE`),
  KEY `fk_TREATMENT_has_PFILE_TREATMENT` (`TREATMENT`),
  KEY `fk_TREATMENT_has_PFILE_PFILE` (`PFILE`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `treatment_pfile` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
