using Shared;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Login {  get; set; }
        public string? ConnectionId {  get; set; }
        public DateTime DateConnect {  get; set; }

        public static implicit operator UserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
            };
        }
    }
}