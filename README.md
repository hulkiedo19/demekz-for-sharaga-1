# demekz3

before building, delete connection string in App.config, also create data base and connect it's through ADO.NET to project.

sql code to create data base:
```sql
CREATE TABLE Products (
	Id INT PRIMARY KEY IDENTITY,
	ProductTypeName NVARCHAR(100) NOT NULL,
	ProductName NVARCHAR(100) NOT NULL,
	VendorCode INT NOT NULL,
	Materials NVARCHAR(MAX),
	ProductCost INT NOT NULL,
	ImagePreview NVARCHAR(MAX)
);

INSERT Products VALUES
	(N'Type1', N'Name1', 1234, N'Material1, Material2', 1000, N'D:\images\Flat_1.png'),
	(N'Type2', N'Name2', 1235, N'Material3, Material4', 2000, N'D:\images\Flat_2.png'),
	(N'Type3', N'Name3', 1236, N'Material6', 3000, N'D:\images\Flat_3.png'),
	(N'Type4', N'Name4', 1237, N'Material5, Material1, Material7', 4000, N'D:\images\Flat_4.png'),
	(N'Type5', N'Name5', 1238, NULL, 5000, N'D:\images\Flat_5.png'),
	(N'Type6', N'Name6', 1239, N'Material4, Material2', 6000, N'D:\images\Flat_6.png'),
	(N'Type7', N'Name7', 1240, N'Material2', 7000, N'D:\images\Flat_7.png'),
	(N'Type8', N'Name8', 1241, N'Material2, Material2', 8000, N'D:\images\Flat_8.png'),
	(N'Type9', N'Name9', 1242, NULL, 9000, N'D:\images\Flat_9.png'),
	(N'Type10', N'Name10', 1243, NULL, 100, NULL);

SELECT * FROM Products;
```
