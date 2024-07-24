using Entity_FrameWork.Constants;
using Entity_FrameWork.Services;

namespace Entity_FrameWork
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
            ChoiceInputDesc: Messages.InputMessage("Choice");
                string choiceInput = Console.ReadLine();
                int choice;
                bool isSuccedded = int.TryParse(choiceInput, out choice);
                if (!isSuccedded)
                {
                    Messages.InvalidInputMessage("Choice");
                    goto ChoiceInputDesc;
                }

                switch ((Operations)choice)
                {
                    case Operations.Exit:
                        return;
                    case Operations.AllTeachers:
                        TeacherService.AllTeachers();
                        break;
                    case Operations.CreateTeacher:
                        TeacherService.CreateTeacher();
                        break;
                    case Operations.UpdateTeacher:
                        TeacherService.UpdateTeacher();
                        break;
                    case Operations.DeleteTeacher:
                        TeacherService.DeleteTeacher();
                        break;
                    case Operations.DetailsOfTeacher:
                        TeacherService.DetailsOfTeacher();
                        break;
                    case Operations.GroupsOfTeacher:
                        TeacherService.GroupsOfTeacher();
                        break;
                    case Operations.AllGroups:
                        GroupService.AllGroups();
                        break;
                    case Operations.CreateGroup:
                        GroupService.CreateGroup();
                        break;
                    case Operations.UpdateGroup:
                        GroupService.UpdateGroup();
                        break;
                    case Operations.DeleteGroup:
                        GroupService.DeleteGroup();
                        break;
                        case Operations.DetailsOfGroup:
                            GroupService.DetailsOfGroup();
                        break;
                    case Operations.AllStudents:
                        StudentService.AllStudents();
                        break;
                        case Operations.CreateStudent:
                            StudentService.CreateStudent();
                        break;
                        case Operations.UpdateStudent:
                            StudentService.UpdateStudent();
                        break;
                        case Operations.DeleteStudent:
                            StudentService.DeleteStudent();
                        break;
                        case Operations.DetailsOfStudent:
                            StudentService.DetailsOfStudent();
                        break;
                        case Operations.StudentsOfGroup:
                            GroupService.StudentsOfGroup();
                        break;
                    default:
                        Messages.InvalidInputMessage("Choice");
                        break;
                }

            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine(" ---  MENU  ---");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - All Teachers");
            Console.WriteLine("2 - Create Teacher");
            Console.WriteLine("3 - Update Teacher");
            Console.WriteLine("4 - Delete Teacher");
            Console.WriteLine("5 - Details Of Teacher");
            Console.WriteLine("6 - Groups Of Teacher");
            Console.WriteLine("7 - All Groups");
            Console.WriteLine("8 - Create Group");
            Console.WriteLine("9 - Update Group");
            Console.WriteLine("10 - Delete Group");
            Console.WriteLine("11 - Details of Group");
            Console.WriteLine("12 - All Students");
            Console.WriteLine("13 - Create Student");
            Console.WriteLine("14 - Update Student");
            Console.WriteLine("15 - Delete Student");
            Console.WriteLine("16 - Details of Student");
            Console.WriteLine("17 - Students of Group");
        }
    }


}
