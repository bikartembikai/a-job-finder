-- Create table for saving job applications
CREATE TABLE IF NOT EXISTS SavedApplications (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    JobId BIGINT NOT NULL,
    JobTitle VARCHAR(255) NOT NULL,
    CompanyName VARCHAR(255) NOT NULL,
    ApplicantName VARCHAR(255) NOT NULL,
    ApplicantEmail VARCHAR(255) NOT NULL,
    ResumeUrl VARCHAR(1024),
    AppliedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

DELIMITER //

-- Drop procedures if they exist to allow re-running
DROP PROCEDURE IF EXISTS sp_SaveJobApplication //
DROP PROCEDURE IF EXISTS sp_GetApplicationsByEmail //

-- Stored procedure to save an application
CREATE PROCEDURE sp_SaveJobApplication(
    IN p_JobId BIGINT,
    IN p_JobTitle VARCHAR(255),
    IN p_CompanyName VARCHAR(255),
    IN p_ApplicantName VARCHAR(255),
    IN p_ApplicantEmail VARCHAR(255),
    IN p_ResumeUrl VARCHAR(1024)
)
BEGIN
    INSERT INTO SavedApplications (
        JobId, JobTitle, CompanyName, ApplicantName, ApplicantEmail, ResumeUrl, AppliedAt
    )
    VALUES (
        p_JobId, p_JobTitle, p_CompanyName, p_ApplicantName, p_ApplicantEmail, p_ResumeUrl, NOW()
    );
END //

-- Stored procedure to get saved applications for an email
CREATE PROCEDURE sp_GetApplicationsByEmail(
    IN p_ApplicantEmail VARCHAR(255)
)
BEGIN
    SELECT 
        Id, JobId, JobTitle, CompanyName, ApplicantName, ApplicantEmail, ResumeUrl, AppliedAt 
    FROM 
        SavedApplications
    WHERE 
        ApplicantEmail = p_ApplicantEmail
    ORDER BY 
        AppliedAt DESC;
END //

DELIMITER ;
