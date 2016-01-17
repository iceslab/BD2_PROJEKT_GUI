﻿CREATE TABLE [dbo].[AIRCRAFT_MODELS]
(
	[AIRCRAFT_MODEL_ID] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR [AIRCRAFT_MODELS_SEQUENCE], 
    [TYPE] VARCHAR(8) NOT NULL, 
    [SIZE] VARCHAR(8) NOT NULL, 
    [MODEL_NAME] VARCHAR(8) NULL, 
    [MANUFACTURER] VARCHAR(8) NULL
	CONSTRAINT U_AIRCRAFT_MODELS UNIQUE([TYPE], [SIZE], [MODEL_NAME], [MANUFACTURER])
)
