using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class User
    {   
        [Key]
        public int UserId {get; set;}

        public string Name {get; set;}

        public string Email {get; set;}
  
        public string MobileNumber {get; set;}

        public DateTime BirthDate {get; set;}

        public string Username {get; set;}

        public string Password {get; set;} 

        public string Token {get; set;}
        
        public ICollection<Activity> Activities{get; set;} 


    }
}