using System.ComponentModel.DataAnnotations;

namespace zad9.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest obowiązkowe!"), Display(Name = "Nazwa")]
        public string name { get; set; }
        [Required(ErrorMessage = "Pole Cena jest obowiązkowe!"), Display(Name = "Cena"),
            Range(0, double.MaxValue, ErrorMessage = "Podano niepoprawną cene."),
            DataType(DataType.Currency, ErrorMessage = "Podana wartość jest zła")]
        public decimal price { get; set; }

    }
}
/*
 CREATE TABLE [dbo].[Product] (
    [Id]    INT         IDENTITY (1, 1) NOT NULL,
    [name]  NCHAR (100) NOT NULL,
    [price] MONEY       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE PROCEDURE [dbo].[sp_productAdd]
@name VARCHAR (100),
@price MONEY,
@productID int OUTPUT
AS
INSERT INTO Product (name, price) VALUES (@name, @price)
SET @productID = @@IDENTITY

CREATE PROCEDURE [dbo].[sp_productDelete]
@productID int OUTPUT
AS
DELETE FROM Product WHERE Id = @productID

CREATE PROCEDURE [dbo].[sp_productDisplay]
AS
SELECT *
FROM Product

CREATE PROCEDURE [dbo].[sp_productGet]
@productID int OUTPUT
AS
SELECT *
FROM Product
WHERE Id = @productID

CREATE PROCEDURE [dbo].[sp_productUpdate]
@name VARCHAR (50),
@price MONEY,
@productID int
AS
UPDATE Product SET price= @price, name=@name WHERE Id=@productID
*/
