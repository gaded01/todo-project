using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Activity
    {
        [Key]
        public int ActId {get; set;}

        public string EventActivity {get; set;}

        public string Details {get ; set;}
 
        public DateTime Date {get; set;}

        public DateTime Time {get; set;}

        public string Setting {get; set;}

        public User User {get; set;}
    }
}