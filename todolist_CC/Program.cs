using System.ComponentModel;
using System.Data;
using System.IO;
using System;
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
        public const int Done = 3;
        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "Active";
                case Waiting: return "Waiting";
                case Done: return "Done";
                default: return "Error: Status unknown";
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
            /*
             Todo.print Method VVV
             */
            public void print(bool desc)
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
        /*
         Todo.Print Method VVV
         */
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
            if (arg.Length == 1) { active = true; }
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
        /*
        Todo.PrintHelp Method VVV
        */
        public static void PrintHelp(string command) //<----------------METHOD_Help
        {
            string[] arg = command.Split(' ');
            if (arg.Length < 2)
            {
                WriteLine("Commands(type help 'command' for more information if the command got * after it's name):");
                WriteLine("help         dis help, yis");
                WriteLine("list*        lists active items in Todo-list");
                WriteLine("describe*    lists active items in Todo-list with description");
                WriteLine("new*         create new item. 'new test' created a item with the name test");
                WriteLine("edit         lets you edit an item. 'edit test' edits item with name test");
                WriteLine("copy         copies item and sets status to active. 'copy test' copies test and sets it to active");
                WriteLine("save*        save current loaded list.");
                WriteLine("load*        loads default .lis. todo.lis");
                WriteLine("active       set item status to active. 'activate test' set test to active");
                WriteLine("waiting      set item status to waiting item. 'waiting test' set test to waiting");
                WriteLine("done         set item status to done. 'done test' set test status to done.");
                WriteLine("stop         save and close");
            }
            else if (arg.Length > 1)
            {
                switch(arg[1])
                {
                    case "list":
                        WriteLine("'list ': 'all': lists all items w/o description.\n'waiting': lists all items with status waiting.\n'done': lists all items with status done.");
                        break;
                    case "describe":
                        WriteLine("'describe ': 'all': lists all items.\n'waiting': lists all items with status waiting.\n'done': lists all items with status done.\n!!With Description!!");
                        break;
                    case "new":
                        WriteLine("'new': create item.\n'new test': create item with name 'test'");
                        break;
                    case "save":
                        WriteLine("'save': save list to 'todo.lis'(default).\n'save test.lis': saves list to file 'test.lis'.");
                        break;
                    case "load":
                        WriteLine("'load': load list 'todo.lis'(default).\n'load test.lis': loads list 'test.lis'.");
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
        }
        /*
        Todo.newTodo Method VVV
        */
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
        /*
        Todo.editTodo Method VVV
        */
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
        /*
        Todo.copyTodo Method VVV
        */
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
        /*
        Todo.loadTodo Method VVV
         */
        public static void loadTodo(string command)
        {
            string file = "todo.lis";
            string[] arg = command.Split(' ');
            if (arg.Length > 1) { file = arg[1]; }
            list.Clear();
            Write($"Reading from file {file} ... ");
            StreamReader sr = new StreamReader(file);
            int numRead = 0;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TodoItem item = new TodoItem(line);
                list.Add(item);
                numRead++;
            }
            sr.Close();
            WriteLine($"Read {numRead} rows.");

        }
        /*
        Todo.saveTodo Method VVV
         */
        public static void saveTodo(string command)
        {
            string file = "todo.lis";
            string[] arg = command.Split(' ');
            if (arg.Length > 1) { file = arg[1]; }
            File.WriteAllText(file, String.Empty);
            using (StreamWriter sw = new StreamWriter(file, false))
            {
                foreach (TodoItem item in list)
                {
                    sw.WriteLine($"{item.status}|{item.priority}|{item.task}|{item.taskDescription}");
                }
            }
        }
        public static void setStatus(string command)
        {
            int setTo;
            string[] arg = command.Split(' ', 2);
            string name = arg[1];
            if (arg[0] == "active") { setTo = 1; }
            else if (arg[0] == "waiting") { setTo = 2; }
            else if (arg[0] == "done") { setTo= 3; }
            else { setTo = -1; }
            if (setTo == -1) { WriteLine("Error: This will never happen, but you never know."); return; }
            foreach (TodoItem item in list)
            {
                if (item.task == name) { item.status = setTo; }
            }
        }
    }
    class MainClass //<-------------------------------------------------------------------------------------------------------------------------MAIN/------------------------------------------------------------------------------------------------------------------------>
    {
        public static void Main(string[] args)
        {

            string command;
            Console.WriteLine("Welcome to the Todo-list");
            Todo.loadTodo("load");
            Todo.PrintHelp("help");
            do
            {
                command = MyIO.ReadCommand("> ");
                switch (command)
                {
                    case String A when A.StartsWith("help"):
                        Todo.PrintHelp(command);
                        break;
                    case "stop":
                        Todo.saveTodo("save");
                        Console.WriteLine("Bye");
                        break;
                    case String A when A.StartsWith("list"):
                        Todo.Print(command);
                        WriteLine();
                        break;
                    case string A when A.StartsWith("new"):
                        Todo.newTodo(command);
                        break;
                    case String A when A.StartsWith("describe"):
                        Todo.Print(command);
                        break;
                    case String A when A.StartsWith("save"):
                        Todo.saveTodo(command);
                        break;
                    case String A when A.StartsWith("load"):
                        Todo.loadTodo(command);
                        break;
                    case String A when A.StartsWith("edit"):
                        Todo.editTodo(command);
                        break;
                    case String A when A.StartsWith("copy"):
                        Todo.copyTodo(command);
                        break;
                    case String A when A.StartsWith("active"):
                        Todo.setStatus(command);
                        break;
                    case String A when A.StartsWith("waiting"):
                        Todo.setStatus(command);
                        break;
                    case String A when A.StartsWith("done"):
                        Todo.setStatus(command);
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
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