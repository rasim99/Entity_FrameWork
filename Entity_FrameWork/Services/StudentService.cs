using Entity_FrameWork.Constants;
using Entity_FrameWork.Contexts;
using Entity_FrameWork.Entities;
using Entity_FrameWork.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Services
{
    public static class StudentService
    {
        private static readonly AppDbContext _context;

        static StudentService()
        {
            _context = new AppDbContext();
        }

        public static void AllStudents()
        {
            if (_context.Students.ToList().Count == 0)
            {
                Messages.NotFoundMessage("any student");
                return;
            }
            foreach (var student in _context.Students.ToList())
            {
                Console.WriteLine($"Id {student.Id} Name : {student.Name} BirthDate :  {student.BirthDate.ToString("dd.MM.yyyy")}");

            }
        }

        public static void CreateStudent()
        {
            if (_context.Groups.ToList().Count == 0)
            {
                Messages.NotFoundMessage("Firstly Create group! any group ");
                return;
            }
            GroupService.AllGroups();

        GroupNameSect: Messages.InputMessage("group name ");
            string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                Messages.InvalidInputMessage("group");
                goto GroupNameSect;
            }
            var existGroup = _context.Groups.FirstOrDefault(t => t.Name.ToLower() == groupName.ToLower());
            if (existGroup is null)
            {
                Messages.NotFoundMessage(" group");
                goto GroupNameSect;
            }

        StudentNameSect: Messages.InputMessage("Student name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMessage("Name");
                goto StudentNameSect;
            }
        StudentSurnameSect: Messages.InputMessage("Student surname");
            string surname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname))
            {
                Messages.InvalidInputMessage("surname");
                goto StudentSurnameSect;
            }

        StudentBirthDateSect: Messages.InputMessage("Student birthdate (dd.MM.yyyy)");
            string birthDateInput = Console.ReadLine();
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(birthDateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Birthdate");
                goto StudentBirthDateSect;
            }

            if (birthDate > DateTime.Now.AddYears(-10))
            {
                Messages.GreaterAgeMessage("10");
                goto StudentBirthDateSect;
            }

            Student student = new Student
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                GroupId = existGroup.Id,
            };
            _context.Students.Add(student);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("student", "added");
        }

        public static void UpdateStudent()
        {
        UpdateStudentIdSect: Messages.InputMessage(" student id");
            AllStudents();
            string idInput = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(idInput, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("id");
                goto UpdateStudentIdSect;
            }
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                Messages.NotFoundMessage("student");
                return;
            }

            string newName = string.Empty;
            string newSurname = string.Empty;
            DateTime newBirthDate = default;
            int newGroupId = default;

        NewNameWantToChangeInput: Messages.WantToChangeMessage("student name ? y or n");
            string choicInput = Console.ReadLine();
            char choice;
            isSucceeded = char.TryParse(choicInput, out choice);
            if (!isSucceeded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage("Choice");
                goto NewNameWantToChangeInput;
            }

            if (choice == 'y')
            {
            NewNameInput: Messages.InputMessage(" new name");
                newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Messages.InvalidInputMessage("new name");
                    goto NewNameInput;
                }
            }

        NewSurnameWantToChangeInput: Messages.WantToChangeMessage("student surname ? y or n");
            choicInput = Console.ReadLine();
            isSucceeded = char.TryParse(choicInput, out choice);
            if (!isSucceeded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage("Choice");
                goto NewSurnameWantToChangeInput;
            }

            if (choice == 'y')
            {
            NewSurnameInput: Messages.InputMessage(" new surname");
                newSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Messages.InvalidInputMessage("new surname");
                    goto NewSurnameInput;
                }
            }

        NewBirthDateWantToChangeInput: Messages.WantToChangeMessage("student birthdate ? y or n");
            choicInput = Console.ReadLine();
            isSucceeded = char.TryParse(choicInput, out choice);
            if (!isSucceeded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage("Choice");
                goto NewSurnameWantToChangeInput;
            }

            if (choice == 'y')
            {
            NewBirthDateInput: Messages.InputMessage(" new birthdate (dd.MM.yyyy)");
                string newBirthDateInput = Console.ReadLine();
                isSucceeded = DateTime.TryParseExact(newBirthDateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newBirthDate);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("new birthDate");
                    goto NewBirthDateInput;
                }

                if (newBirthDate>DateTime.Now.AddYears(-10))
                {
                    Messages.GreaterAgeMessage("10");
                    goto NewBirthDateWantToChangeInput;
                }
            }

         
            if (_context.Groups.ToList().Count > 1)
            {
            NewGroupOfStudentSect: Messages.WantToChangeMessage("group ? y or n");
                choicInput= Console.ReadLine();
                isSucceeded= char.TryParse(choicInput,out choice);
                if (!isSucceeded || !choice.isValidChoice())
                {
                    Messages.InvalidInputMessage("Choice");
                    goto NewGroupOfStudentSect;
                }

                if (choice=='y')
                {
                NewGroupInput: Messages.InputMessage("new  group id");
                    GroupService.AllGroups();
                   string newGroupIdInput = Console.ReadLine();
                    isSucceeded = int.TryParse(newGroupIdInput, out newGroupId);
                    if (!isSucceeded)
                    {
                        Messages.InvalidInputMessage("group id");
                        goto NewGroupInput;
                    }

                    var group=_context.Groups.FirstOrDefault(g=>g.Id==newGroupId);
                    if (group is null)
                    {
                        Messages.NotFoundMessage(" group");
                        goto NewGroupInput;
                    }

                }
            }
            
            if (!string.IsNullOrWhiteSpace(newName)) student.Name = newName;
            if (!string.IsNullOrWhiteSpace(newSurname)) student.Surname = newSurname;
            if (newBirthDate!=default) student.BirthDate = newBirthDate;
            if (newGroupId!=default) student.GroupId = newGroupId;
            _context.Update(student);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }

            Messages.SuccesMessage("student","updated");
        }
   
        public static void DeleteStudent()
        {
            if (_context.Students.ToList().Count==0)
            {
                Messages.NotFoundMessage("any student");
                return;
            }

        DeleteStudentInput: Messages.InputMessage("student id");
            AllStudents();
            string idInput=Console.ReadLine();
            int id;
            bool isSucceeded=int.TryParse(idInput, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("id");
                goto DeleteStudentInput;
            }
            var student = _context.Students.FirstOrDefault(s=>s.Id==id);
            if (student is null)
            {
                Messages.NotFoundMessage("student");
                return ;
            }
            student.IsDeleted = true;
            _context.Update(student);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("student","deleted");
        }
    
        public static void DetailsOfStudent()
        {
            if (_context.Students.ToList().Count==0)
            {
                Messages.NotFoundMessage(" any student");
                return;
            }
        StudentDetailInput: Messages.InputMessage("student id");
            AllStudents();
            string idInput=Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(idInput, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("id");
                goto StudentDetailInput;
            }

            var student = _context.Students.Include(i=>i.Group).FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                Messages.NotFoundMessage("student");
                return;
            }

            Console.WriteLine($"Name : {student.Name} Surname : {student.Surname} Birthdate : {student.BirthDate.ToString("dd.MM.yyyy")}  {student.Group.Name} ");
        }
    }
}
