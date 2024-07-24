using Entity_FrameWork.Constants;
using Entity_FrameWork.Contexts;
using Entity_FrameWork.Entities;
using Entity_FrameWork.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Services
{
    public static class TeacherService
    {
        private static readonly AppDbContext _context;
        static TeacherService()
        {
            _context = new AppDbContext();
        }

        public static void AllTeachers()
        {
            var teachers = _context.Teachers.ToList();
            if (teachers.Count > 0)
            {
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($" Id : {teacher.Id} Name :  {teacher.Name} Surname :  {teacher.Surame} ");
                }
            }
            else
                Messages.NotFoundMessage(" Teacher ");


        }
        public static void CreateTeacher()
        {
        TeacherNameInput: Messages.InputMessage("Teacher name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMessage("Teacher name");
                goto TeacherNameInput;
            }

        TeacherSurnameInput: Messages.InputMessage("Teacher surname");
            string surname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMessage("Teacher surname");
                goto TeacherSurnameInput;
            }

            Teacher teacher = new Teacher();
            teacher.Name = name;
            teacher.Surame = surname;
            _context.Teachers.Add(teacher);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("Teacher", "added");
        }

        public static void UpdateTeacher()
        {
            if (_context.Teachers.ToList().Count ==0)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }

        TeacherIdInput: Messages.InputMessage(" Teacher Id ");
            AllTeachers();
            string IdInput = Console.ReadLine();
            int id;
            bool isSuccedded = int.TryParse(IdInput, out id);
            if (!isSuccedded)
            {
                Messages.InvalidInputMessage("Id");
                goto TeacherIdInput;
            }
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher is null)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }

            string newName = string.Empty;
            string newSurname = string.Empty;

        WantToChangeNameInput: Messages.WantToChangeMessage("Teacher name ? y or y");
            string choiceInput = Console.ReadLine();
            char choice;
            isSuccedded = char.TryParse(choiceInput, out choice);
            if (!isSuccedded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage("Choice");
                goto WantToChangeNameInput;
            }
            if (choice == 'y')
            {
            NewNameInputDesc: Messages.InputMessage("Teacher new name");
                newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Messages.InvalidInputMessage("Teacher name");
                    goto NewNameInputDesc;
                }
            }

        WantToChangeSurnameInput: Messages.WantToChangeMessage("Teacher surname ? y or n");
            choiceInput = Console.ReadLine();
            isSuccedded = char.TryParse(choiceInput, out choice);
            if (!isSuccedded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage("Choice");
                goto WantToChangeSurnameInput;
            }
            if (choice == 'y')
            {
            NewSurnameInputDesc: Messages.InputMessage(" Teacher surname");
                newSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newSurname))
                {
                    Messages.InvalidInputMessage("Teacher surname");
                    goto NewSurnameInputDesc;
                }
            }

            if (!string.IsNullOrWhiteSpace(newName)) teacher.Name = newName;

            if (!string.IsNullOrWhiteSpace(newSurname)) teacher.Surame = newSurname;
            _context.Teachers.Update(teacher);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("Teacher ", "updated");


        }

        public static void DeleteTeacher()
        {
            if (_context.Teachers.ToList().Count < 1)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }

        DeleteIdInputMessages: Messages.InputMessage("Teacher Id");
            AllTeachers();
            string deleteIdInput = Console.ReadLine();
            int deleteId;
            bool isSuccedded = int.TryParse(deleteIdInput, out deleteId);
            if (!isSuccedded)
            {
                Messages.InvalidInputMessage("Teacher Id");
                goto DeleteIdInputMessages;
            }
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == deleteId);
            if (teacher is null)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }
            //SoftDelete
            teacher.IsDeleted = true;
            _context.Teachers.Update(teacher);

            //_context.Teachers.Remove(teacher); HardDelete
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("Teacher ", "deleted");
        }

        public static void DetailsOfTeacher()
        {
            if (_context.Teachers.ToList().Count < 1)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }

        IdInputSection: Messages.InputMessage("Teacher Id");
            AllTeachers();
            string idInput = Console.ReadLine();
            int id;
            bool IsSuccessed = int.TryParse(idInput, out id);
            if (!IsSuccessed) {
                Messages.InvalidInputMessage(" Teacher id");
                goto IdInputSection;
            }
            var teacher = _context.Teachers.Include(x=>x.Groups).FirstOrDefault(t=>t.Id==id);
            if (teacher is null)
            {
                Messages.NotFoundMessage(" Teacher ");
                return;
            }            
            Console.WriteLine($" Id : {teacher.Id} Name :  {teacher.Name} Surname :  {teacher.Surame} Group count : {teacher.Groups.Count} ");

        }

        public static void GroupsOfTeacher()
        {
        TeacherIdSection: Messages.InputMessage("teacher id");
            AllTeachers();
            string teacherIdInput = Console.ReadLine();
            int teacherId;
            bool isSucceeded=int.TryParse(teacherIdInput, out teacherId);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("id");
                goto TeacherIdSection;
            }
            var teacher = _context.Teachers.Include(t => t.Groups).FirstOrDefault(x=>x.Id==teacherId);
            if (teacher is null)
            {
                Messages.NotFoundMessage("teacher");
                goto TeacherIdSection;
            }

            if (teacher.Groups.ToList().Count==0)
            {
                Messages.NotFoundMessage($"{teacher.Name}-s any group");
                return ;
            }
            Console.WriteLine(teacher.Name + " "+ teacher.Surame + " -s groups ");
            foreach (var group in teacher.Groups.ToList())
            {
                Console.WriteLine($" Group Name : {group.Name}  Group Limit : {group.Limit}");
            }
        }
    }
}
