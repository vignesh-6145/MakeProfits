DROP TABLE MakeProfits.Roles;
DROP TABLE Advisors;
DROP TABLE Agents;
DROP TABLE Users;
CREATE TABLE Users(
	UserID int PRIMARY KEY,
	RoleID int NOT NULL,
	AddressLine nvarchar(75),
	City nvarchar(50),
	State nvarchar(50),
	EmailAddress nvarchar(50),
	AdvisorID int NOT NULL,
	AgentID int NOT NULL,

	FirstName nvarchar(50),
	LastName nvarchar(50),
	UserName nvarchar(50),
	Password nvarchar(50),
	PhoneNumber nvarchar(12),
	Company nvarchar(75),

	IsActive int,

	CONSTRAINT CHL_ISACTIVE CHECK (IsActive =1 OR IsActive=0),
	CONSTRAINT FK_users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
	CONSTRAINT FK_users_advisors FOREIGN KEY (AdvisorID) REFERENCES Advisors(AdvisorID),
	CONSTRAINT FK_users_gent FOREIGN KEY (AgentID) REFERENCES Agents(AgentID)
);

CREATE TABLE Advisors(
	AdvisorID int PRIMARY KEY,
	AdvisorName nvarchar(50) NOT NULL
);

CREATE TABLE Agents(
	AgentID int PRIMARY KEY,
	AgentName nvarchar(50) NOT NULL
);
CREATE TABLE Roles(
	RoleID int PRIMARY KEY,
	RoleName nvarchar(50) NOT NULL
);

INSERT INTO Roles VALUES (1,'User');

INSERT INTO Advisors VALUES (1,'DummyAdvisor');

INSERT INTO Agents VALUES (1,'DummyAgent');
SELECT * FROM Roles,Advisors,Agents;

SELECT * FROM Users;


CREATE PROCEDURE InsertUser 
	-- Add the parameters for the stored procedure here
	@UserID int = 0, --  
	@RoleID int = 1, -- Default Rol
	@AddressLine nvarchar(75),
	@City nvarchar(50),
	@State nvarchar(50),
	@EmailAddress nvarchar(50),
	@AdvisorID int = 1, -- Default Advisor
	@AgentID int  = 1, -- Default Agent
	@FirstName nvarchar(50),
	@PhoneNumber nvarchar(12),
	@LastName nvarchar(50),
	@UserName nvarchar(50),
	@Password nvarchar(50),
	@Company nvarchar(75)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Users(UserID,RoleID,AddressLine,City,State,EmailAddress,AdvisorID,AgentID,FirstName,LastName,UserName,Password,PhoneNumber,Company,IsActive) 
	VALUES(@UserID,@RoleID,@AddressLine,@City,@State,@EmailAddress,@AdvisorID,@AgentID,@FirstName,@LastName,@UserName,@Password,@PhoneNumber,@Company,1);
	
END
CREATE PROCEDURE GetUserByID
	@UserID int
AS
BEGIN
	
SELECT r.RoleName
			,u.AddressLine
			,u.City
			,u.State
			,u.EmailAddress
			,adv.AdvisorName
			,a.AgentName
			,u.FirstName
			,u.LastName
			,u.UserName
			,u.PhoneNumber
			,u.Company
	FROM Users u,Agents a, Roles r, Advisors adv
	WHERE u.UserID=@UserID AND a.AgentID=u.AgentID AND u.AdvisorID = adv.AdvisorID AND u.RoleID = r.RoleID 
END
CREATE PROCEDURE GetUserByName
	@UserName nvarchar(50)
AS
BEGIN
	
SELECT u.FirstName
			,u.LastName
			,u.UserName
			,u.PhoneNumber		
			,u.AddressLine
			,u.City
			,u.State
			,u.EmailAddress
			,adv.AdvisorName
			,a.AgentName
			,u.Company
			,r.RoleName
	FROM Users u,Agents a, Roles r, Advisors adv
	WHERE u.UserName=@UserName AND a.AgentID=u.AgentID AND u.AdvisorID = adv.AdvisorID AND u.RoleID = r.RoleID 
END
CREATE PROCEDURE ValidateUser
	-- Add the parameters for the stored procedure here
	@UserName VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT  CASE 
		WHEN UserName = @UserName AND Password =@Password THEN 1
		ELSE 0
	END AS IsValidUser
	FROM Users
END


