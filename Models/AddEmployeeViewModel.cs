namespace Company.Models
{
    public class AddEmployeeViewModel
    {
        // We will have 4 properties in this class
        //this property will be similar to the Domain model
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
