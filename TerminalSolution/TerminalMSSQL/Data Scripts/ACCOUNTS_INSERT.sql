PRINT N'Inserting values into [dbo].[ACCOUNTS]...';
INSERT INTO [TerminalMSSQL].[dbo].[ACCOUNTS]
VALUES 
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'm.nowak','5ee23b07c4275dad8e102b05c9636fd4', 3, 0, 1),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'b.lecina','hash02', 3, 0, 2),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  's.kasztan','hash03', 3, 0, 3),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'p.ulan','hash04', 3, 0, 4),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'l.hajdu','hash05', 3, 0, 5),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'a.samotny','hash06', 3, 0, 6),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'emp.konieczna','hash07', 2, 0, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'emp.czarnecki','hash08', 2, 0, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'emp.wieczorek','hash09', 2, 0, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'man.wisniewski','162de50854fb25ea3fd8640282cb67b4', 1, 0, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'man.nowakowski','hash11', 1, 0, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'man.czerwinski','hash12', 1, 1, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'emp.trocki','hash13', 2, 1, null),
(NEXT VALUE FOR [ACCOUNTS_SEQUENCE],  'a.pawlowska','hash14', 3, 1, null)