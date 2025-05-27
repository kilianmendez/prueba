using System.ComponentModel.DataAnnotations;
using Backend.Models.Database.Entities;

namespace Backend.Models.Dtos;

public class RegisterRequest
{
      [EmailAddress]
      public required string Mail {get; set;}
      public required string Password {get; set;}
      public required string Name {get; set;}
      [Phone]
      public required string Phone {get; set;}


}
