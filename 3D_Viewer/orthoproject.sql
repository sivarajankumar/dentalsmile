/*
SQLyog Community v11.01 (32 bit)
MySQL - 5.1.37 : Database - orthoproject
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`orthoproject` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `orthoproject`;

/*Table structure for table `doctor_lookup` */

DROP TABLE IF EXISTS `doctor_lookup`;

CREATE TABLE `doctor_lookup` (
  `doctor_Id` int(7) NOT NULL AUTO_INCREMENT,
  `doctor_name` char(25) NOT NULL,
  `doctor_password` varchar(5) NOT NULL,
  PRIMARY KEY (`doctor_Id`,`doctor_password`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `doctor_lookup` */

insert  into `doctor_lookup`(`doctor_Id`,`doctor_name`,`doctor_password`) values (1,'Asri',''),(2,'Dwi',''),(3,'Fuad',''),(4,'Nunu','');

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
  `patient_id` int(7) NOT NULL AUTO_INCREMENT,
  `patient_name` char(25) NOT NULL,
  `patient_dateofbirth` date NOT NULL,
  `patient_placeofbirth` char(25) NOT NULL,
  `patient_sex` char(1) NOT NULL COMMENT 'M;F',
  `patient_address` varchar(50) NOT NULL,
  `patient_phone` varchar(12) NOT NULL,
  PRIMARY KEY (`patient_id`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `patient` */

insert  into `patient`(`patient_id`,`patient_name`,`patient_dateofbirth`,`patient_placeofbirth`,`patient_sex`,`patient_address`,`patient_phone`) values (1,'Asyirini','0000-00-00','Jkarta','\0','cmh','4545'),(2,'One','0000-00-00','Jakarta','\0','Jakarta','878676'),(3,'','0000-00-00','','\0','',''),(4,'Aaaa','0000-00-00','Bandung','\0','wewe','wewewew'),(5,'Hana','0000-00-00','Jakarta','\0','Jakarta','0876888');

/*Table structure for table `patient_treatment` */

DROP TABLE IF EXISTS `patient_treatment`;

CREATE TABLE `patient_treatment` (
  `patient_id` int(7) NOT NULL,
  `doctor_id` int(7) NOT NULL,
  `phase` int(1) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `patient_treatment` */

insert  into `patient_treatment`(`patient_id`,`doctor_id`,`phase`) values (2,1,1),(2,1,1);

/*Table structure for table `phase_lookup` */

DROP TABLE IF EXISTS `phase_lookup`;

CREATE TABLE `phase_lookup` (
  `phase_id` int(1) NOT NULL,
  `phase_name` char(25) NOT NULL,
  PRIMARY KEY (`phase_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `phase_lookup` */

insert  into `phase_lookup`(`phase_id`,`phase_name`) values (1,'Discussion'),(2,'Scanning'),(3,'Measuring'),(4,'Analyzing'),(5,'Treatment');

/*Table structure for table `teeth_files` */

DROP TABLE IF EXISTS `teeth_files`;

CREATE TABLE `teeth_files` (
  `patient_id` int(7) DEFAULT NULL,
  `file_id` int(7) NOT NULL AUTO_INCREMENT,
  `file_name` varchar(35) NOT NULL,
  `file_desc` varchar(50) DEFAULT NULL,
  `created_date` date DEFAULT NULL COMMENT 'uploaded',
  `created_by` char(7) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `modified_by` char(7) DEFAULT NULL,
  PRIMARY KEY (`file_id`,`file_name`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `teeth_files` */

insert  into `teeth_files`(`patient_id`,`file_id`,`file_name`,`file_desc`,`created_date`,`created_by`,`modified_date`,`modified_by`) values (2,1,'D:KULIAH TMDGThesis 2013- OrthoRefe','D:KULIAH TMDGThesis 2013- OrthoReferences3D models','0000-00-00','Asri','0000-00-00','Asri'),(2,2,'','','0000-00-00','Asri','0000-00-00','Asri'),(2,3,'D:KULIAH TMDGThesis 2013- OrthoRefe','D:KULIAH TMDGThesis 2013- OrthoReferences3D models','0000-00-00','Asri','0000-00-00','Asri'),(2,4,'D:KULIAH TMDGThesis 2013- OrthoRefe','D:KULIAH TMDGThesis 2013- OrthoReferences3D models','0000-00-00','Asri','0000-00-00','Asri');

/*Table structure for table `tooth_lookup` */

DROP TABLE IF EXISTS `tooth_lookup`;

CREATE TABLE `tooth_lookup` (
  `tooth_id` int(2) NOT NULL,
  `tooth_type` int(1) DEFAULT NULL COMMENT '1:upper ; 2:lower',
  `tooth_name` char(25) NOT NULL,
  PRIMARY KEY (`tooth_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `tooth_lookup` */

insert  into `tooth_lookup`(`tooth_id`,`tooth_type`,`tooth_name`) values (1,1,'Third Molar'),(2,1,'Second Molar'),(3,1,'First Molar'),(4,1,'Second Premolar'),(5,1,'Fisrt Premolar'),(6,1,'Canine'),(7,1,'Lateral Incisor'),(8,1,'Cantral Incisor'),(9,2,'Third Molar'),(10,2,'Second Molar'),(11,2,'First Molar'),(12,2,'Second Premolar'),(13,2,'First Premolar'),(14,2,'Canine'),(15,2,'Lateral'),(16,2,'');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
