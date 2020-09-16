using System.ComponentModel.DataAnnotations;

namespace APIDesafio.DTOs
{
    public class UserInfoDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
