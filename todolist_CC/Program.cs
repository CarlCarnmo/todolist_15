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
            public void Print(bool verbose = false) //<----------------METHOD_Print
            {
                string statusString = StatusToString(status);
                Console.Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                if (verbose)
                    Console.WriteLine($"{taskDescription,-40}|");
                else
                    Console.WriteLine();
            }
        }
        public static void ReadListFromFile() //<----------------METHOD_ReadListFromFile
        {
            string todoFileName = "todo.lis";
            Console.Write($"Läser från fil {todoFileName} ... ");
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
            Console.WriteLine($"Läste {numRead} rader.");
        }
        private static void PrintHeadOrFoot(bool head, bool verbose) ///<-------------------------------------------------------------------------------------------------------------------------METHOD_Print_extra/------------------------------------------------------------------------------------------------------------------------>
        {
            if (head)
            {
                Console.Write("|status      |prio  |namn                |");
                if (verbose) Console.WriteLine("beskrivning                             |");
                else Console.WriteLine();
            }
            Console.Write("|------------|------|--------------------|");
            if (verbose) Console.WriteLine("----------------------------------------|");
            else Console.WriteLine();
        }
        private static void PrintHead(bool verbose) //<----------------METHOD_Print_extra>
        {
            PrintHeadOrFoot(head: true, verbose);
        } 
        private static void PrintFoot(bool verbose) //<----------------METHOD_Print_extra
        {
            PrintHeadOrFoot(head: false, verbose);
        }
        public static void PrintTodoList(bool verbose = false) //<----------------METHOD_Print_extra
        {
            PrintHead(verbose);
            foreach (TodoItem item in list)
            {
                item.Print(verbose);
            }
            PrintFoot(verbose);
        } //<-------------------------------------------------------------------------------------------------------------------------/METHOD_Print_extra------------------------------------------------------------------------------------------------------------------------>
        public static void PrintHelp() //<----------------METHOD_Help
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("hjälp    lista denna hjälp");
            Console.WriteLine("lista    lista att-göra-listan");
            Console.WriteLine("sluta    spara att-göra-listan och sluta");
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
                    case "lista":
                    case "lista allt":
                        Todo.PrintTodoList(verbose: MyIO.HasArgument(command, "allt"));
                        break;
                    case "new": //new 'task'
                        break;
                    case "describe": //describe 'all'
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
                    case "edit":
                        break;
                    case "copy":
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