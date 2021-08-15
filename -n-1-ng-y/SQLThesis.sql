CREATE TABLE TENKHO
(	
	id  int(11) NOT NULL  AUTO_INCREMENT,
	StorageName  varchar(100) not null default Kho chưa có tên,
	status  varchar(100) not null default Chưa kết nối,
	PRIMARY KEY (id) 
);

BEGIN
DECLARE i INT DEFAULT 1
	WHILE i<=30 
		INSERT dbo.TENKHO (StorageName) VALUES ( N'Kho ' + CAST(i AS nvarchar(100)))
		SET i = i + 1
	END WHILE;
END$$

CREATE TABLE THONGTINKHO
(
	id INT NOT NULL AUTO_INCREMENT,
	StackName nvarchar(100),
	idKho INT(11) NOT NULL
	
	PRIMARY KEY (id)	
	FOREIGN KEY (idKho) REFERENCES dbo.TENKHO(id)

);




INSERT INTO THONGTINKHO (StackName , idKho) VALUES
 ('5123453',1),
('542123453',4),
('56734123',3),
 ('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
 ('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15),
('59684123',15);
