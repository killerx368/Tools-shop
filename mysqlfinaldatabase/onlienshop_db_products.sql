-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: onlienshop_db
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(50) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `Quantity` int NOT NULL,
  `CategoryId` int NOT NULL,
  `BrandId` int NOT NULL,
  `UserId` longtext NOT NULL,
  `IdentityUserId` varchar(255) DEFAULT NULL,
  `ImageURL` longtext NOT NULL,
  `Description` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Products_BrandId` (`BrandId`),
  KEY `IX_Products_CategoryId` (`CategoryId`),
  KEY `IX_Products_IdentityUserId` (`IdentityUserId`),
  CONSTRAINT `FK_Products_AspNetUsers_IdentityUserId` FOREIGN KEY (`IdentityUserId`) REFERENCES `aspnetusers` (`Id`),
  CONSTRAINT `FK_Products_Brands_BrandId` FOREIGN KEY (`BrandId`) REFERENCES `brands` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Products_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (7,'Test6',123.00,0,18,9,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(8,'Test6',12.00,2,19,10,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(9,'Test7',12.00,2,20,11,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(10,'Test6',12.00,3,21,12,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(11,'Test4',34.00,2,22,13,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(12,'Test1',23.00,2,1,3,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','fffhjfhjfhfffhfhgfgfgfhfs'),(13,'Test2',67.00,8,1,14,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','gkhgdgnjgdngfnsgr'),(15,'Test6',2.00,2,4,15,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','jhfyfhftfh'),(16,'Test7',12.00,24,4,16,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','gkhgdgnjgdngfnsgr'),(18,'It',123563.00,4141,23,18,'4b962f28-e087-408f-99d1-661f64a999b3',NULL,'https://freepngimg.com/save/19877-wrench-png/500x500','gkhgdgnjgdngfnsgr');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-11 13:40:12
