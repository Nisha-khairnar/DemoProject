using System.ComponentModel.DataAnnotations;
namespace DemoProject.Models;
public class Users 
{
    public class User   
    { 
        public int Id { get; set; }   
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }   
        [Required(ErrorMessage ="Please enter email address")]
        [EmailAddress(ErrorMessage = "Invalid email id")]
        public string EmailId { get; set; }   
        [Required(ErrorMessage ="Please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string Phone { get; set; }   
        public string Address { get; set; }  
        [Required(ErrorMessage ="Please select State")]
        public int StateId { get; set; }   
        [Required(ErrorMessage ="Please select City")]
        public int CityId { get; set; }          
        public string StateName { get; set; }          
        public string CityName { get; set; }          
        public bool Agree { get; set; }          
    } 

    public class States
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    } 
    public class Cities
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }  
}
