/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r "../Data Scripts/ACCOUNTS_INSERT.sql"
:r "../Data Scripts/AGENTS_DATA_INSERT.sql"
:r "../Data Scripts/AIRCRAFT_MODELS_INSERT.sql"
:r "../Data Scripts/AIRCRAFTS_INSERT.sql"
:r "../Data Scripts/CLIENTS_DATA_INSERT.sql"
:r "../Data Scripts/CONTACT_DATA_INSERT.sql"
:r "../Data Scripts/INFRASTRUCTURE_INSERT.sql"
:r "../Data Scripts/MAINTENANCE_INSERT.sql"
:r "../Data Scripts/RESERVATIONS_INSERT.sql"

:r "../Data Scripts/ALTER_GRANT_SCRIPT.sql"
GO