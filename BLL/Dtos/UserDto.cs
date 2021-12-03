using System;

namespace BLL.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthStr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CountryId { get; set; }
        public string CountryTitle { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int Status { get; set; }
        public string StatusTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnStr { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedOnStr { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class PasswordDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ImageModel
    {
        public ImageModel()
        {
            FolderName = "Users";
        }
        public string ImageBase64 { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public string FolderName { get; set; }
    }
}
