SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
CREATE SCHEMA IF NOT EXISTS `dentalsmile` DEFAULT CHARACTER SET latin1 ;
USE `dentalsmile`;

-- -----------------------------------------------------
-- Table `dentalsmile`.`DENTIST`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`DENTIST` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`DENTIST` (
  `userid` VARCHAR(15) NOT NULL ,
  `fname` VARCHAR(45) NOT NULL ,
  `lname` VARCHAR(45) NULL ,
  `birthdate` DATE NULL ,
  `birthplace` VARCHAR(45) NULL ,
  `address1` VARCHAR(45) NULL ,
  `address2` VARCHAR(45) NULL ,
  `city` VARCHAR(45) NULL ,
  `phone` VARCHAR(15) NULL ,
  `created` DATE NULL ,
  `createdBy` VARCHAR(45) NULL ,
  `modified` TIMESTAMP NULL ,
  `modifiedBy` VARCHAR(45) NULL ,
  PRIMARY KEY (`userid`) )
ENGINE = MYISAM
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`measurement`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`measurement` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`measurement` (
  `measurement_id` INT(7) NOT NULL AUTO_INCREMENT ,
  `patient_id` CHAR(7) NOT NULL ,
  `file_id` INT(7) NOT NULL ,
  `measurement_status` INT(1) NULL DEFAULT NULL ,
  `measurement_lastupdate` DATE NULL DEFAULT NULL ,
  `measured_by` CHAR(25) NULL DEFAULT NULL ,
  PRIMARY KEY (`measurement_id`, `patient_id`, `file_id`) )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`measurement_detail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`measurement_detail` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`measurement_detail` (
  `measurement_detail_id` INT(7) NOT NULL AUTO_INCREMENT ,
  `measurement_id` INT(7) NOT NULL ,
  `tooth_id` INT(2) NULL DEFAULT NULL ,
  PRIMARY KEY (`measurement_detail_id`, `measurement_id`) )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`PATIENT`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`PATIENT` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`PATIENT` (
  `id` VARCHAR(13) NOT NULL COMMENT 'auto-increment has a limit;\nor generated with format SUN0104130001' ,
  `fname` VARCHAR(45) NOT NULL ,
  `lname` VARCHAR(45) NULL ,
  `birthdate` DATE NOT NULL ,
  `birthplace` VARCHAR(50) NOT NULL ,
  `gender` CHAR(1) NOT NULL COMMENT 'M;F' ,
  `address1` VARCHAR(100) NOT NULL ,
  `address2` VARCHAR(100) NULL ,
  `city` VARCHAR(45) NULL ,
  `phone` VARCHAR(15) NOT NULL ,
  `created` DATE NULL ,
  `createdBy` VARCHAR(45) NULL ,
  `modified` TIMESTAMP NULL ,
  `modifiedBy` VARCHAR(45) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = MyISAM
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`PHASE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`PHASE` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`PHASE` (
  `id` INT(1) NOT NULL ,
  `name` VARCHAR(25) NOT NULL ,
  PRIMARY KEY (`id`) )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`TREATMENT`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`TREATMENT` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`TREATMENT` (
  `id` VARCHAR(16) NOT NULL ,
  `PHASE` INT(1) NULL ,
  `PATIENT` VARCHAR(13) NULL ,
  `DENTIST` VARCHAR(15) NULL ,
  `tdate` DATE NULL ,
  `ttime` TIME NULL ,
  `room` VARCHAR(45) NULL ,
  `refId` VARCHAR(16) NULL ,
  `created` DATE NULL ,
  `createdBy` VARCHAR(45) NULL ,
  `modified` TIMESTAMP NULL ,
  `modifiedBy` VARCHAR(45) NULL ,
  INDEX `fk_TREATMENT_PATIENT` (`PATIENT` ASC) ,
  INDEX `fk_TREATMENT_DOCTOR` (`DENTIST` ASC) ,
  INDEX `fk_TREATMENT_PHASE` (`PHASE` ASC) ,
  PRIMARY KEY (`id`) ,
  CONSTRAINT `fk_TREATMENT_PATIENT`
    FOREIGN KEY (`PATIENT` )
    REFERENCES `dentalsmile`.`PATIENT` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TREATMENT_DOCTOR`
    FOREIGN KEY (`DENTIST` )
    REFERENCES `dentalsmile`.`DENTIST` (`userid` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TREATMENT_PHASE`
    FOREIGN KEY (`PHASE` )
    REFERENCES `dentalsmile`.`PHASE` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`PFILE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`PFILE` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`PFILE` (
  `PATIENT` VARCHAR(13) NOT NULL ,
  `id` VARCHAR(16) NOT NULL ,
  `filename` VARCHAR(35) NOT NULL ,
  `description` VARCHAR(255) NULL DEFAULT NULL ,
  `type` TINYINT(5) NOT NULL DEFAULT 0 ,
  `refId` VARCHAR(16) NULL ,
  `created` DATE NULL DEFAULT NULL COMMENT 'uploaded' ,
  `createdBy` VARCHAR(50) NULL DEFAULT NULL ,
  `modified` TIMESTAMP NULL DEFAULT NULL ,
  `modifiedBy` VARCHAR(50) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_PFILE_PATIENT` (`PATIENT` ASC) ,
  CONSTRAINT `fk_PFILE_PATIENT`
    FOREIGN KEY (`PATIENT` )
    REFERENCES `dentalsmile`.`PATIENT` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = MyISAM
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`TOOTH`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`TOOTH` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`TOOTH` (
  `number` INT(2) NOT NULL ,
  `type` INT(1) NULL DEFAULT NULL COMMENT '1:upper ; 2:lower' ,
  `name` VARCHAR(25) NOT NULL ,
  PRIMARY KEY (`number`) )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dentalsmile`.`TREATMENT_PFILE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`TREATMENT_PFILE` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`TREATMENT_PFILE` (
  `TREATMENT` VARCHAR(16) NOT NULL ,
  `PFILE` VARCHAR(16) NOT NULL ,
  PRIMARY KEY (`TREATMENT`, `PFILE`) ,
  INDEX `fk_TREATMENT_has_PFILE_TREATMENT` (`TREATMENT` ASC) ,
  INDEX `fk_TREATMENT_has_PFILE_PFILE` (`PFILE` ASC) ,
  CONSTRAINT `fk_TREATMENT_has_PFILE_TREATMENT`
    FOREIGN KEY (`TREATMENT` )
    REFERENCES `dentalsmile`.`TREATMENT` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TREATMENT_has_PFILE_PFILE`
    FOREIGN KEY (`PFILE` )
    REFERENCES `dentalsmile`.`PFILE` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- Table `dentalsmile`.`TREATMENT_NOTES`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`TREATMENT_NOTES` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`TREATMENT_NOTES` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `TREATMENT` VARCHAR(16) NULL ,
  `PFILE` VARCHAR(16) NULL ,
  `notes` VARCHAR(255) NULL ,
  `description` VARCHAR(255) NULL ,
  `created` DATETIME NULL ,
  `createdBy` VARCHAR(45) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_TREATMENT_NOTES_TREATMENT_PFILE` (`TREATMENT` ASC, `PFILE` ASC) ,
  CONSTRAINT `fk_TREATMENT_NOTES_TREATMENT_PFILE`
    FOREIGN KEY (`TREATMENT` , `PFILE` )
    REFERENCES `dentalsmile`.`TREATMENT_PFILE` (`TREATMENT` , `PFILE` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dentalsmile`.`SmileUser`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`SmileUser` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`SmileUser` (
  `userid` VARCHAR(25) NOT NULL ,
  `password` VARCHAR(45) NOT NULL ,
  `admin` TINYINT(1) NULL ,
  `created` DATETIME NULL ,
  `createdBy` VARCHAR(45) NULL ,
  `modified` TIMESTAMP NULL ,
  `modifiedBy` VARCHAR(45) NULL ,
  PRIMARY KEY (`userid`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dentalsmile`.`Measurement`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`Measurement` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`Measurement` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `patient` VARCHAR(13) NULL ,
  `treatment` VARCHAR(16) NULL ,
  `pfile` VARCHAR(16) NULL ,
  `type` VARCHAR(45) NULL ,
  `created` DATETIME NULL ,
  `createdBy` VARCHAR(45) NULL ,
  `modified` DATETIME NULL ,
  `modifiedBy` VARCHAR(45) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dentalsmile`.`MeasurementTeeth`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dentalsmile`.`MeasurementTeeth` ;

CREATE  TABLE IF NOT EXISTS `dentalsmile`.`MeasurementTeeth` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `measurementid` INT NULL ,
  `teethid` VARCHAR(255) NULL ,
  `length` DOUBLE NULL ,
  `spoint` VARCHAR(45) NULL ,
  `epoint` VARCHAR(45) NULL ,
  `type` VARCHAR(5) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
