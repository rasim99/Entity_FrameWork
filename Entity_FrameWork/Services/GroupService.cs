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
    public static class GroupService
    {
        private static readonly AppDbContext _context;
        static GroupService()
        {
            _context = new AppDbContext();
        }

        public static void AllGroups()
        {

            var groups = _context.Groups.ToList();
            if (groups.Any())
            {
                foreach (var group in groups)
                {
                    Console.WriteLine($" Id {group.Id}  Name {group.Name}  Limit {group.Limit}");
                }
            }
            else
            {
                Messages.NotFoundMessage(" Group");
            }
        }

        public static void CreateGroup()
        {
            if (_context.Teachers.ToList().Count == 0)
            {
                Messages.NotFoundMessage("Create teacher! Because Teacher ");
                return;
            }
        GroupNameSection: Messages.InputMessage(" Group name");
            string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                Messages.InvalidInputMessage("group name");
                goto GroupNameSection;
            }
            var existGroup = _context.Groups.FirstOrDefault(g => g.Name.ToLower() == groupName.ToLower());
            if (existGroup is not null)
            {
                Messages.AlreadyExistMessage(groupName);
                return;
            }

        GroupLimitSection: Messages.InputMessage(" Group limit");
            string limitInput = Console.ReadLine();
            int limit;
            bool isSucceeded = int.TryParse(limitInput, out limit);
            if (!isSucceeded || limit < 5)
            {
                Messages.InvalidInputMessage(" limit");
                goto GroupLimitSection;
            }
         TeacherIdInputSect: Messages.InputMessage(" Teacher Id");
            TeacherService.AllTeachers();
            string teacherIdInput = Console.ReadLine();
            int teacherId;
            isSucceeded = int.TryParse(teacherIdInput, out teacherId);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("teacher Id");
                goto TeacherIdInputSect;
            }
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher is null)
            {
                Messages.NotFoundMessage(" teacher ");
                goto TeacherIdInputSect;
            }
            TeacherService.AllTeachers();
            Group group = new Group();
            group.Name = groupName;
            group.Limit = limit;
            group.TeacherId = teacherId;

            _context.Groups.Add(group);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }
            Messages.SuccesMessage("group", "added");
        }

        public static void UpdateGroup()
        {
            if (_context.Groups.ToList().Count == 0)
            {
                Messages.NotFoundMessage("Any Group");
                return;
            }

        GroupInputSect: Messages.InputMessage(" Group id");
            AllGroups();
            string groupIdInput = Console.ReadLine();
            int groupId;
            bool isSuccedded = int.TryParse(groupIdInput, out groupId);
            if (!isSuccedded)
            {
                Messages.InvalidInputMessage(" Group Id");
                goto GroupInputSect;
            }
            var group = _context.Groups.FirstOrDefault(g => g.Id == groupId);
            if (group is null)
            {
                Messages.NotFoundMessage("Group");
                return;
            }
            string newName = string.Empty;
            int newLimit = default;
            int newTeacherId = default;

        GroupNameWantToChangeInput: Messages.WantToChangeMessage("Group name ? y or n");
            string choiseInput = Console.ReadLine();
            char choice;
            isSuccedded = char.TryParse(choiseInput, out choice);
            if (!isSuccedded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage(" Choice");
                goto GroupNameWantToChangeInput;
            }
            if (choice == 'y')
            {
            newNameInputSect: Messages.InputMessage(" new name");
                newName = Console.ReadLine();
                if (string.IsNullOrEmpty(newName))
                {
                    Messages.InvalidInputMessage("name");
                    goto newNameInputSect;
                }
                var existGroup = _context.Groups.FirstOrDefault(g => g.Name.ToLower() == newName.ToLower() && g.Id != groupId);
                if (existGroup is not null)
                {
                    Messages.AlreadyExistMessage(newName);
                    goto GroupInputSect;
                }
            }

        GroupLimitWantToChangeSection: Messages.WantToChangeMessage("Group limit ? y or n");
            choiseInput = Console.ReadLine();
            isSuccedded = char.TryParse(choiseInput, out choice);
            if (!isSuccedded || !choice.isValidChoice())
            {
                Messages.InvalidInputMessage(" choice");
                goto GroupLimitWantToChangeSection;
            }
            if (choice == 'y')
            {
            newLimitSect: Messages.InputMessage(" new limit");
                string newLimitInput = Console.ReadLine();
                isSuccedded = int.TryParse(newLimitInput, out newLimit);
                if (!isSuccedded || newLimit < 5)
                {
                    Messages.InvalidInputMessage("new limit");
                    goto newLimitSect;
                }
            }

            if (_context.Teachers.ToList().Count > 1)
            {

            GroupTeacherWantToChangeSection: Messages.WantToChangeMessage(" Group-s Teacher ? y or n");
                choiseInput = Console.ReadLine();
                isSuccedded = char.TryParse(choiseInput, out choice);
                if (!isSuccedded || !choice.isValidChoice())
                {
                    Messages.InvalidInputMessage("Choice");
                    goto GroupTeacherWantToChangeSection;
                }
                if (choice == 'y')
                {
                    TeacherService.AllTeachers();
                NewTeacherSection: Messages.InputMessage(" Teacher Id");
                    string newTeacherIdInput = Console.ReadLine();
                    isSuccedded = int.TryParse(newTeacherIdInput, out newTeacherId);
                    if (!isSuccedded)
                    {
                        Messages.InvalidInputMessage("teacher id");
                        goto NewTeacherSection;
                    }

                    var teacher = _context.Teachers.FirstOrDefault(t => t.Id == newTeacherId);
                    if (teacher is null)
                    {
                        Messages.NotFoundMessage(" Teacher");
                        goto GroupTeacherWantToChangeSection;
                    }
                }

            }

            if (!string.IsNullOrEmpty(newName)) group.Name = newName;
            if (newLimit !=default) group.Limit = newLimit;
            if (newTeacherId !=default) group.TeacherId = newTeacherId;
            _context.Groups.Update(group);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Messages.ErrorOcuredMessage();
            }
            if (!string.IsNullOrWhiteSpace(newName) || newLimit != default || newTeacherId != default) Messages.SuccesMessage("Group"," Updated");
         
        }
    
        public static void DeleteGroup()
        {
            if (_context.Groups.ToList().Count==0)
            {
                Messages.NotFoundMessage(" Any group ");
                return;
            }
          DeleteGroupSect:  Messages.InputMessage(" group name");
            AllGroups();
            string name=Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Messages.InvalidInputMessage(" group name");
                goto DeleteGroupSect;
            }
            var group = _context.Groups.FirstOrDefault(g=>g.Name.ToLower()==name.ToLower());  
            if (group is null)
            {
                Messages.NotFoundMessage(" group ");
               goto DeleteGroupSect;
            }
            group.IsDeleted=true;
            _context.Update(group);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                Messages.ErrorOcuredMessage();
            }

            Messages.SuccesMessage(name, "deleted");
        }
       
        
        public static void DetailsOfGroup()
        {
            if (_context.Groups.ToList().Count==0)
            {
                Messages.NotFoundMessage("Any group");
                return;
            }

        DetailsOfGroupSection: Messages.InputMessage(" group name");
            AllGroups();
            string name= Console.ReadLine();
            if (string.IsNullOrEmpty(name)) 
            { 
                Messages.InputMessage("name");    
                goto DetailsOfGroupSection;
            }
            var group = _context.Groups.Include(x=>x.Teacher).FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
            if (group is null )
            {
                Messages.NotFoundMessage("group (or group-s Teacher) ");
                return ;
            }

            Console.WriteLine($"Name : {group.Name} Limit  {group.Limit}  Teacher name:  " +
                $"{group.Teacher.Name} Teacher Surname : {group.Teacher.Surame}");
        }
    
        public static void StudentsOfGroup()
        {
            if (_context.Groups.ToList().Count == 0)
            {
                Messages.NotFoundMessage("Any group");
                return;
            }

        StudentsOfGroupSection: Messages.InputMessage("group name");
            AllGroups();
            string name= Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Messages.InputMessage("name");
                goto StudentsOfGroupSection;
            }
            var group = _context.Groups.Include(x=>x.Students).FirstOrDefault(g=>g.Name.ToLower() == name.ToLower()); 
            if (group is null )
            {
                Messages.NotFoundMessage("group");
                goto StudentsOfGroupSection;
            }

            if (group.Students.ToList().Count==0)
            {
                Messages.NotFoundMessage($"{group.Name}_s  any student");
                return;
            }
            Console.WriteLine($"{group.Name}_s Students : ");
            foreach (var student in group.Students.ToList())
            {
                Console.WriteLine($" Name : {student.Name}  Surname : {student.Surname} Birthdate : {student.BirthDate.ToString("yyyy.MM.dd")}");
            }
        }
    }
}
