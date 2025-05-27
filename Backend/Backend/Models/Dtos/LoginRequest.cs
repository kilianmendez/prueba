
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Dtos;

public class LoginRequest
{
    [EmailAddress]
      public required string Mail {get;set;}
      public required string Password {get;set;}
}
