﻿using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;
namespace todolist_CC
{
    public class Todo
    {
        public static List<TodoItem> list = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Ready = 3;
        public static string StatusToString(int status) //<----------------METHOD_StatusToString
        {
            switch (status)
            {
                case Active: return "aktiv";
                case Waiting: return "väntande";
                case Ready: return "avklarad";
                default: return "(felaktig)";
            }
        }
        public class TodoItem
        {
            public int status;
            public int priority;
            public string task;
            public string taskDescription;
            public TodoItem(int priority, string task)
            {
                this.status = Active;
                this.priority = priority;
                this.task = task;
                this.taskDescription = "";
            }
            public TodoItem(string todoLine)
            {
                string[] field = todoLine.Split('|');
                status = Int32.Parse(field[0]);
                priority = Int32.Parse(field[1]);
                task = field[2];
                taskDescription = field[3];
            }
            public void print(bool desc) //<----------------METHOD_Print
            {
                string statusString = StatusToString(status);
                Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                if (desc == true) { Write($"{taskDescription,-40}|"); }
                WriteLine();
            }
            public static void prtLoop(bool desc, bool active, bool wait, bool done)
            {
                foreach (TodoItem item in list)
                {
                    if ((active && item.status == 1) || (wait && item.status == 2) || (done && item.status == 3))
                    { item.print(desc); }
                }
            }
        }
        public static void ReadListFromFile() //<----------------METHOD_ReadListFromFile
        {
            string todoFileName = "todo.lis";
            Write($"Läser från fil {todoFileName} ... ");
            StreamReader sr = new StreamReader(todoFileName);
            int numRead = 0;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TodoItem item = new TodoItem(line);
                list.Add(item);
                numRead++;
            }
            sr.Close();
            WriteLine($"Läste {numRead} rader.");
        }
        public static void Print(string command)
        {
            bool desc = false;
            bool active = false;
            bool wait = false;
            bool done = false;
            string[] arg = command.Split(' ');
            if (arg.Length > 1 && arg[1] == "waiting") { wait = true; }
            if (arg.Length > 1 && arg[1] == "done") { done = true; }
            if (arg.Length > 1 && arg[1] == "all") { active = true; done = true; wait = true; }
            if(arg.Length == 1) { active = true; }
            Write("|status      |prio  |namn                |");
            switch (arg[0])
            {
                case "list":
                    WriteLine("\n|------------|------|--------------------|");
                    TodoItem.prtLoop(desc, active, wait, done);
                    WriteLine("|------------|------|--------------------|");
                    break;
                case "describe":
                    desc = true;
                    WriteLine("beskrivning                             |");
                    Write("|------------|------|--------------------|");
                    WriteLine("----------------------------------------|");
                    TodoItem.prtLoop(desc, active, wait, done);
                    Write("|------------|------|--------------------|");
                    WriteLine("----------------------------------------|");
                    break;
                default:
                    break;
            }
        }
        public static void PrintHelp() //<----------------METHOD_Help
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("hjälp    lista denna hjälp");
            Console.WriteLine("lista    lista att-göra-listan");
            Console.WriteLine("sluta    spara att-göra-listan och sluta");
        }
        public static void newTodo(string command)
        {
            string task;
            if (command.Length > 3) { string substring = command.Substring(4); task = substring; }
            else { WriteLine("Task: "); task = ReadLine(); }
            WriteLine("Priority(1-4): ");
            int priority = Convert.ToInt32(ReadLine());
            WriteLine("Status(1=Active, 2=Waiting, 3=Ready): ");
            int status = Convert.ToInt32(ReadLine());
            WriteLine("Description: ");
            string desc = ReadLine();

            TodoItem item = new TodoItem(priority, task);
            item.status = status;
            item.taskDescription = desc;
            list.Add(item);
        }
        public static void editTodo(string command)
        {
            string substring = "";
            if (command.Length > 4) { substring = command.Substring(5); }
            foreach (TodoItem item in list)
            {
                if (item.task == substring)
                {
                    WriteLine("Task: ");
                    item.task = ReadLine();
                    WriteLine("Priority(1-4): ");
                    item.priority = Convert.ToInt32(ReadLine());
                    WriteLine("Status(1=Active, 2=Waiting, 3=Ready): ");
                    item.status = Convert.ToInt32(ReadLine());
                    WriteLine("Description: ");
                    item.taskDescription = ReadLine();
                }
            }
        }
        public static void copyTodo(string command)
        {
            string substring = "";
            if (command.Length > 4) { substring = command.Substring(5); }
            foreach (TodoItem item in list)
            {
                if (item.task == substring)
                {
                    string newTask = item.task;
                    int newStatus = item.status;
                    string newTaskdesc = item.taskDescription;

                    TodoItem item1 = new TodoItem(1, newTask);
                    item.status = newStatus;
                    item.taskDescription = newTaskdesc;
                    list.Add(item1);
                    break;
                }
            }
        }
    }
    class MainClass //<-------------------------------------------------------------------------------------------------------------------------MAIN/------------------------------------------------------------------------------------------------------------------------>
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till att-göra-listan!");
            Todo.ReadListFromFile();
            Todo.PrintHelp();
            string command;
            do
            {
                command = MyIO.ReadCommand("> ");
                switch (command)
                {
                    case "hjälp":
                        Todo.PrintHelp();
                        break;
                    case "sluta":
                        Console.WriteLine("Hej då!");
                        break;
                    case String A when A.StartsWith("list"):
                        Todo.Print(command);
                        WriteLine();
                        break;
                    case string A when A.StartsWith("new"): //new 'task'
                        Todo.newTodo(command);
                        if (command == "new kekw") { WriteLine("kekw!"); }
                        break;
                    case String A when A.StartsWith("describe"): //describe 'all'
                        Todo.Print(command);
                        break;
                    case "save":
                        break;
                    case "load":
                        break;
                    case "activate":
                        break;
                    case "done":
                        break;
                    case "wait":
                        break;
                    case String A when A.StartsWith("edit"):
                        Todo.editTodo(command);
                        break;
                    case String A when A.StartsWith("copy"):
                        Todo.copyTodo(command);
                        break;
                    default:
                        Console.WriteLine($"Okänt kommando: {command}");
                        break;
                }
            }
            while (true);
        }
    } //<-------------------------------------------------------------------------------------------------------------------------/MAIN------------------------------------------------------------------------------------------------------------------------>
    class MyIO
    {
        static public string ReadCommand(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static public bool Equals(string rawCommand, string expected)
        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords[0] == expected) return true;
            }
            return false;
        }
        static public bool HasArgument(string rawCommand, string expected)
        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords.Length < 2) return false;
                if (cwords[1] == expected) return true;
            }
            return false;
        }
    }
}