IF NOT EXISTS (
    SELECT 1
    FROM dbo.Orders
    WHERE UserName = 'rahul'
      AND EmailAddress = 'rahulsahay@ecommerce.net'
)
BEGIN
    INSERT INTO dbo.Orders
    (
        UserName,
        FirstName,
        LastName,
        EmailAddress,
        AddressLine,
        State,
        Country,
        ZipCode,
        CardName,
        CardNumber,
        Expiration,
        Cvv,
        PaymentMethod,
        TotalPrice,
        CreatedBy,
        CreatedDate,
        LastModifiedBy,
        LastModifiedDate
    )
    VALUES
    (
        'rahul',
        'Rahul',
        'Sahay',
        'rahulsahay@ecommerce.net',
        'Ranchi',
        'JH',
        'India',
        '834009',
        'Visa',
        '4111111111111111',
        '12/25',
        '123',
        1,
        0.00,
        'Rahul',
        SYSUTCDATETIME(),
        'Rahul',
        SYSUTCDATETIME()
    );
END;
GO