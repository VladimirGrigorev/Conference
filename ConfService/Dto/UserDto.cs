using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class UserAuthDto
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string PassHash { get; set; }
    }

    public class UserDto : UserAuthDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    
        //public bool IsGlobalAdmin { get; set; }
    }

    public class UserInfoDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
    }
}