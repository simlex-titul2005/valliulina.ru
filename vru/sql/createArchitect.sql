/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.5.278
 * Time: 04.08.2016 14:52:53
 ************************************************************/

/*******************************************
 * create architect user
 *******************************************/
IF NOT EXISTS (
       SELECT TOP(1) *
       FROM   AspNetUsers AS anu
       WHERE  anu.Email = 'simlex.dev.2014@gmail.com'
   )
BEGIN
    INSERT INTO AspNetUsers
      (
        Id,
        DateCreate,
        DateUpdate,
        NikName,
        AvatarId,
        Email,
        EmailConfirmed,
        PasswordHash,
        SecurityStamp,
        PhoneNumber,
        PhoneNumberConfirmed,
        TwoFactorEnabled,
        LockoutEndDateUtc,
        LockoutEnabled,
        AccessFailedCount,
        UserName
      )
    VALUES
      (
        'f1dec2e7-cc29-47da-9195-0d90041bf65b',
        GETDATE(),
        GETDATE(),
        'simlex',
        NULL,
        'simlex.dev.2014@gmail.com',
        1,
        'ADAI4m7/BAIvv/oQFNTRhdSTjegJvd9Llir6z/zFfSvJhjbxhNDsTKLzCsrkt8W9Pw==',
        '63a46c30-91ef-4401-8cf3-8cfcc483ab7f',
        '+79376376582',
        0,
        0,
        NULL,
        0,
        0,
        'simlex.dev.2014@gmail.com'
      )
END
GO

/*******************************************
 * create user roles
 *******************************************/
DECLARE @roles TABLE(Id NVARCHAR(128), [Name] NVARCHAR(256))

INSERT INTO @roles
VALUES
  (
    '547d3cfb-4304-4597-97e1-67fd5de889dd',
    'user'
  )
  
INSERT INTO @roles
VALUES
  (
    '694b7d6b-b1c1-4736-b435-fc6fb1e41fa9',
    'admin'
  )
  
INSERT INTO @roles
VALUES
  (
    'AF5D1E6D-8608-4F8F-9D84-1E6E4FE7A7DC',
    'architect'
  )
DECLARE @roleId       NVARCHAR(128),
        @roleName     NVARCHAR(256)

DECLARE c CURSOR  
FOR
    SELECT r.Id,
           r.Name
    FROM   @roles AS r

  OPEN c
  FETCH NEXT FROM c INTO @roleId, @roleName
WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (
           SELECT *
           FROM   AspNetRoles AS anr
           WHERE  anr.Name = @roleName
       )
    BEGIN
        INSERT INTO AspNetRoles
          (
            Id,
            NAME
          )
        VALUES
          (
            @roleId,
            @roleName
          )
        
        INSERT INTO AspNetUserRoles
          (
            UserId,
            RoleId
          )
        VALUES
          (
            'f1dec2e7-cc29-47da-9195-0d90041bf65b',
            @roleId
          )
    END
    
    FETCH NEXT FROM c INTO @roleId, @roleName
END
  CLOSE c
  DEALLOCATE c
  GO
  