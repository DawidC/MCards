/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [PK_Card]
      ,[CardName]
      ,[FK_CardType]
      ,[FK_Expansion]
      ,[CardNumber]
      ,[IsFoil]
      ,[FK_Condition]
  FROM [MTGCards].[dbo].[Card]

  ALTER TABLE [Card]
ADD CONSTRAINT FK_CardType_FK_CardType FOREIGN KEY (FK_Cardtype)
    REFERENCES CardType(PK_CardType);

	  ALTER TABLE [Card]
ADD CONSTRAINT FK_Expansion_FK_Expansion FOREIGN KEY (FK_Expansion)
    REFERENCES Expansion(PK_Expansion);

	--truncate table dbo.Card