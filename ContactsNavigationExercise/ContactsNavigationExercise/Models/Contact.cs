using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsNavigationExercise.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        public bool IsBlocked { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + SurName;
            }
        }
    }
}
