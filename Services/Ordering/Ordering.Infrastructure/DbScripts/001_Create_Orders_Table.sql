IF NOT EXISTS (
    SELECT 1
    FROM sys.tables
    WHERE name = 'Orders'
      AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE dbo.Orders
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,

        UserName NVARCHAR(100) NOT NULL,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        EmailAddress NVARCHAR(256) NOT NULL,

        AddressLine NVARCHAR(200) NOT NULL,
        State NVARCHAR(100) NOT NULL,
        Country NVARCHAR(100) NOT NULL,
        ZipCode NVARCHAR(20) NOT NULL,

        CardName NVARCHAR(100) NOT NULL,
        CardNumber NVARCHAR(50) NOT NULL,
        Expiration NVARCHAR(10) NOT NULL,
        Cvv NVARCHAR(10) NOT NULL,
        PaymentMethod INT NOT NULL,

        TotalPrice DECIMAL(18,2) NULL,

        CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );
END;
GO
