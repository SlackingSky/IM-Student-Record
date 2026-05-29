-- SQL script to create a database and a table for student records.
-- Creates a database name "student_record_db" if it does not exist
CREATE DATABASE IF NOT EXISTS `student_record_db` 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_general_ci;

USE `student_record_db`;

-- Drops the students table if it already exists to avoid errors when creating a new one.
DROP TABLE IF EXISTS `students`;

-- Columns for the students table:
CREATE TABLE `students` (
  `student_id` VARCHAR(20) NOT NULL,
  `full_name` VARCHAR(100) NOT NULL,
  `date_of_birth` DATE NOT NULL,
  `gender` VARCHAR(10) NOT NULL,
  `course` VARCHAR(50) NOT NULL,
  `year_level` INT NOT NULL,
  `section` INT NOT NULL,
  `email` VARCHAR(100) NOT NULL,
  `phone` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`student_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;