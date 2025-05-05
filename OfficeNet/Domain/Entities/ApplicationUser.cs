using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OfficeNet.Domain.Entities
{
    [Table("Users")] // This will map it to "Users" table instead of default "AspNetUsers"
    public class ApplicationUser : IdentityUser
    {
        public string? EmpCode { get; set; }
        public string FirstName { get; set; }
        public string? EmpMIDdlename { get; set; }
        public string LastName { get; set; }
        public string? MobileNum { get; set; }
        public int DesgID { get; set; }
        public int DepartmentID { get; set; }
        public int SubDeptID { get; set; }
        public int LocationID { get; set; }
        public int LeaveLocnID { get; set; }
        public int GradeID { get; set; }
        public int PlantID { get; set; }
        public int CompanyID { get; set; }
        public int DivisionID { get; set; }
        public int UnitCodeID { get; set; }
        public int BloodGroupID { get; set; }
        public int MaritalStatusID { get; set; }
        public string? Gender { get; set; }
        public int GenderID { get; set; }
        public int AreaID { get; set; }
        public int SubAreaID { get; set; }
        public int CostCenterID { get; set; }
        public int SubCostCenterID { get; set; }
        public int PositionCodeID { get; set; }
        public int RegionID { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOA { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOL { get; set; }
        public DateTime DtLastWorkingDay { get; set; }
        public int CouncelorID { get; set; }
        public bool IsCouncelor { get; set; }
        public int CouncelorID2 { get; set; }
        public bool IsCouncelorID2 { get; set; }
        public string? Emppic { get; set; }
        public bool ISChangePswd { get; set; }
        public DateTime ChangepwdDate { get; set; }
        public bool IsRole { get; set; }
        public bool IsModerator { get; set; }
        public bool IsTopEmpOrgChart { get; set; }
        public bool IsGuest { get; set; }
        public int AppDataID { get; set; }
        public bool Status { get; set; }
        public bool Archive { get; set; }
        public string? DeviceID { get; set; }
        public int SubUnitID { get; set; }
        public int Band { get; set; }
        public int SalesCategory { get; set; }
        public int Segmentation { get; set; }
        public int ProductCategory { get; set; }
        public int CustomerSegment { get; set; }
        public int DefaultShiftID { get; set; }
        public byte IsOverTime { get; set; }
        public int AttEmpCode { get; set; }
        public int ESI { get; set; }
        public int BUHEAD { get; set; }
        public int MRFCandidate { get; set; }
        public bool IsConfirmMailSend { get; set; }
        public int BDMID { get; set; }
        public bool IsAllowCAPEXFORM { get; set; }
        public string? FaceRegonisationImagePath { get; set; }
        public string? FaceRegonisationImage { get; set; }
        public bool IsFHPolicy { get; set; }

        [MaxLength(400)]
        public string? RefreshToken { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
