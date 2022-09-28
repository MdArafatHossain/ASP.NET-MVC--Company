using Company.Data;
using Company.Models;
using Company.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        //we can use this private read only field talk to our database


        //Create a constructor 

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext) //inject the injected services here from Program.cs
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }


        //Create index method which will be get method
        [HttpGet]
        public async Task <IActionResult> Index() //this will be the list we will show to the user. To show this list we need to talk to the database and get the employees that stored in the database
        {
            //now we can use the private field mvcDemoDbContext (dbcontex) to talk to the Employees folder
          var employees =  await mvcDemoDbContext.Employees.ToListAsync();
            //we are getting the employees into a variable "employees"
            return View(employees);
        }


        // In this controller we will have index methods so that 
        // we can show list of employees

        //lets add all the functionality 


        //create a functionality to add a new employee 

        //once we have an employee in our database then we will ]
        //use that employee to show it in the list of employees 
        //that we want to show on the screen




        //Add() method this will be use as employees/add ...to add a new employee
        //this will be the get method so we have to write [HttpGet] on top of the method

        [HttpGet]
        public IActionResult Add()
        {
            return View(); //add a view for this method. by right clicking on View() we will do Add View ( it will create a Add.cshtml in Views)
                           //so we will use this view to add a new employee. Also under the Shared folder there is a layout.cshtml file we have to add another tab as in Add employee
        }

        //when we submit the button from Add.cshtml we need to create a Post method


        [HttpPost]
        public async Task <IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)

        //addEmployeeRequest use this value to call EFC so that it can save our values to the database
        //before that we have to do a conversion from AddEmployeeViewModel model to Employee model
        {

            var employee = new Employee() //employee we have our entity or domain model ready 
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department

            };
            //mvcDemoDbContext.Employees.Add(employee);
          
                await mvcDemoDbContext.Employees.AddAsync(employee);
                await mvcDemoDbContext.SaveChangesAsync();

    //after adding a single employee in the database we have to show it in UI
         
            return RedirectToAction("Index");

            //and now its time to use EFC and specifically the db contet to save this entity to the database
        }
        [HttpGet]
        public async Task <IActionResult> View(Guid id)
        {
            //FirstOrDefault is to retrieve a single entry from the Employees table
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
           
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department

                };
                return await Task.Run (() => View("View", viewModel));

            }

            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public async Task<IActionResult>View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;   
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

               await  mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Delete (UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
