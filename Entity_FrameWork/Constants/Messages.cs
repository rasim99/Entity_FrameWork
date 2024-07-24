using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Entity_FrameWork.Constants
{
    public static class Messages
    {
        public static void InvalidInputMessage(string title) => Console.WriteLine($" {title} is Invalid");
        public static void InputMessage(string title) => Console.WriteLine($"Input {title}");
        public static void SuccesMessage(string title,string operation) => Console.WriteLine($" {title} succesfulye {operation}");
        public static void ErrorOcuredMessage() => Console.WriteLine($" Errorr Ocurred");
        public static void AlreadyExistMessage(string title) => Console.WriteLine($"{title} already exists");
        public static void NotFoundMessage(string title) => Console.WriteLine($"{title} not found");
        public static void WantToChangeMessage(string title) => Console.WriteLine($"Do you want {title} ? Y or N");
        public static void GreaterAgeMessage(string title) => Console.WriteLine($"Age is greater {title}");


    }
}
