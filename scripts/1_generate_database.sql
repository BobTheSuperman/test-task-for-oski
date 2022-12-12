DROP DATABASE IF EXISTS `oskitest`;

CREATE DATABASE IF NOT EXISTS `oskitest` CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

USE `oskitest`;

-- Table `Accounts`

DROP TABLE IF EXISTS `Accounts`;

CREATE TABLE `Accounts` (
    `ID`                     BIGINT UNSIGNED     NOT NULL         AUTO_INCREMENT,
    `Role`                   NVARCHAR(30)         DEFAULT NULL,
    `Name`                   NVARCHAR(30)         NOT NULL,
    `Email`                  NVARCHAR(50)         NOT NULL,
    `PasswordHash`           NVARCHAR(64)         NOT NULL         COMMENT 'SHA265 output size is 256 bits or 64 characters',
    `Salt`                   NVARCHAR(32)         NOT NULL         COMMENT 'Standard salt size is 128 bits or 32 characters',

    CONSTRAINT    `PK_Account`           PRIMARY KEY (`ID`),
    CONSTRAINT    `UQ_EmailAccounts`     UNIQUE (`Email`)
);

-- Table `Tests`

CREATE TABLE `Tests` (
    `ID`                     BIGINT UNSIGNED     NOT NULL         AUTO_INCREMENT,
	`Name`			         NVARCHAR(40)         NOT NULL,
    `Category`               NVARCHAR(100)        NOT NULL,
    `Description`            NVARCHAR(255)        NOT NULL,

    CONSTRAINT    `PK_Test`           PRIMARY KEY (`ID`)
);

-- Table `Questions`

CREATE TABLE `Questions` (
    `ID`                     BIGINT UNSIGNED     NOT NULL         AUTO_INCREMENT,
    `TestID`                 BIGINT UNSIGNED     NOT NULL,
    `Text`					 TEXT       		 NOT NULL,

    CONSTRAINT    `PK_Question`           PRIMARY KEY (`ID`),
    CONSTRAINT    `FK_TestQuestions`    FOREIGN KEY (`TestID`)    REFERENCES `Tests` (`ID`)
);

-- Table `Answers`

CREATE TABLE `Answers` (
    `ID`                     BIGINT UNSIGNED     NOT NULL         AUTO_INCREMENT,
    `QuestionID`             BIGINT UNSIGNED     NOT NULL,
    `Text`                   NVARCHAR(255)       NOT NULL,
    `IsCorrect`              BIT                 DEFAULT FALSE,

    CONSTRAINT    `PK_Answer`           PRIMARY KEY (`ID`),
    CONSTRAINT    `FK_QuestionAnswer`   FOREIGN KEY (`QuestionID`)    REFERENCES `Questions` (`ID`)
);

-- Table `Results`

CREATE TABLE `Results` (
    `ID`                     BIGINT UNSIGNED     NOT NULL         AUTO_INCREMENT,
    `CorrectAnswerAmount`    INT       		 	 NOT NULL,
    `TestID`                 BIGINT UNSIGNED     NOT NULL,
    `UserID`                 BIGINT UNSIGNED     NOT NULL,

    CONSTRAINT    `PK_Result`           PRIMARY KEY (`ID`),
    CONSTRAINT    `FK_TestResult`   FOREIGN KEY (`TestID`)    REFERENCES `Tests` (`ID`),
    CONSTRAINT    `FK_UserResult`   FOREIGN KEY (`UserID`)    REFERENCES `Accounts` (`ID`)
);