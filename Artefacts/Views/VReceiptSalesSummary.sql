USE fishportms_db

IF OBJECT_ID('dbo.VReceiptSalesSummary', 'V') IS NOT NULL
    DROP VIEW dbo.vw_ReceiptSalesSummary;
GO

CREATE VIEW VReceiptSalesSummary AS
SELECT
    r.Id,
    r.DateCreated,
	r.BSId,
    CAST(r.DateCreated AS DATE) AS DateOnly,
    DATEPART(HOUR, r.DateCreated) AS Hour,
    DATENAME(WEEKDAY, r.DateCreated) AS DayOfWeek,
    MONTH(r.DateCreated) AS Month,
    YEAR(r.DateCreated) AS Year,
    r.GrossSales,
    r.NetSales,
    r.DeductedAmount
FROM Receipts r;