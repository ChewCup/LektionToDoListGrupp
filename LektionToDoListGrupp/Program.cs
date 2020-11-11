using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LektionToDoListGrupp
{
    class Program
    {
        class Activity
        {
            string date;
            string status;
            string title;
            public Activity(string d, string s, string t)
            {
                date = d;
                status = s;
                title = t;
            }
            public string GetStatus()
            {
                return status;
            }
            public string GetActivity() // metod i klassen
            {
                if (date == "--")
                {
                    return $"  {date}  \t{status} {title}";
                }
                else
                {
                    return $"{date}\t{status}\t{title}";
                }
            }
            public string SaveToFileActivity() // metod i klassen
            {
                return $"{date}#{status}#{title}";
            }
            public void NewStatus(string newStatus)
            {
                if (newStatus == "done")
                {
                    status = "*";
                }
                else if (newStatus == "ongoing")
                {
                    status = "P";
                }
                else if (newStatus == "pending")
                {
                    status = "v";
                }
                else
                {
                    Console.WriteLine("pending (v), done (*), ongoing (p)");
                }
                status = newStatus;
            }
        }
        static void Main(string[] args)
        {
            bool exit = false;
            string command;
            List<Activity> todoList = new List<Activity>();
            string file_name = ""; //@"C:\Users\vikke\Desktop\todo.lis";
            Console.WriteLine("welcome to your ToDoList");
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                string[] commandArg = command.Split(' ');
                if (command.ToLower() == "exit")
                {
                    Console.WriteLine("Bye!");
                    exit = true;
                }
                else if (commandArg[0].ToLower() == "cls")
                {
                    Console.Clear();
                }
                else if (commandArg[0] == "")
                {
                    Console.WriteLine("incorrect input!");
                }
                else if (commandArg[0] == "load" && commandArg.Length > 1 && File.Exists(commandArg[1]))
                {
                    file_name = commandArg[1];
                    Console.WriteLine("Open file {0}\n", commandArg[1]);
                    string[] lines = File.ReadAllLines(file_name);

                    foreach (string line in lines)
                    {
                        //Console.WriteLine(line); // test
                        string[] activitystrings = line.Split('#');
                        todoList.Add(new Activity(activitystrings[0], activitystrings[1], activitystrings[2]));
                    }
                    foreach (Activity a in todoList)
                    {
                        Console.WriteLine(a.GetActivity());
                    }
                }
                else if (command == "save")
                {
                    string[] writeToFile = new string[todoList.Count];
                    for (int i = 0; i < todoList.Count; i++)
                    {
                        writeToFile[i] = todoList[i].SaveToFileActivity();
                    }
                    File.WriteAllLines(file_name, writeToFile);
                }
                else if (commandArg[0] == "show")
                {
                    Console.WriteLine("N datum S rubrik");
                    Console.WriteLine("-----------------------------------------");
                    //for (int i = 0; i < todoList.Count; i++)
                    //{
                    //    if (todoList[i].status != "*") ;
                    //    Console.WriteLine(todoList[i].GetActivity());
                    //}
                    int i = 1;
                    foreach (var a in todoList)
                    {
                        if (commandArg[1] == "done")
                        {
                            if (todoList[i].GetStatus() == "*")
                            { 
                            Console.Write("{0}: ", i++);
                            Console.WriteLine(a.GetActivity());
                            }
                        }
                        else if (commandArg[1] == "show all")
                        {
                            Console.Write("{0}: ", i++);
                            Console.WriteLine(a.GetActivity());
                        }
                        //if (a.GetStatus() != "*")
                        //{
                        //    Console.Write("{0}: ", i++);
                        //    Console.WriteLine(a.GetActivity());
                        //}
                    }
                    Console.WriteLine("-----------------------------------------");
                }
                else if (commandArg[0] == "move" && commandArg.Length > 1)
                {
                    int select = int.Parse(commandArg[1]) - 1;
                    Activity tmp = todoList[select];
                    Console.WriteLine(tmp.GetActivity());
                    //Console.WriteLine(todoList[select - 1 ].GetActivity());

                    if (commandArg[2] == "up")
                    {
                        todoList[select] = todoList[select - 1];
                        todoList[select - 1] = tmp;
                    }
                    else if (commandArg[2] == "down")
                    {
                        Console.WriteLine(todoList[select + 1].GetActivity());
                        todoList[select] = todoList[select + 1];
                        todoList[select + 1] = tmp;
                    }
                }
                else if (commandArg[0] == "delete" && commandArg.Length > 1)
                {
                    int select = int.Parse(commandArg[1]) - 1;
                    Console.WriteLine(todoList[select].GetActivity());
                    todoList.RemoveAt(select);
                }
                else if (commandArg[0] == "add" && commandArg.Length > 2)
                {
                    if (commandArg[1] == "--")
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 2; i < commandArg.Length; i++)
                        {
                            sb.Append(commandArg[i]);
                            sb.Append(" ");
                        }
                        //Console.WriteLine(sb.ToString());
                        todoList.Add(new Activity(commandArg[1], "v", sb.ToString()));
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 2; i < commandArg.Length; i++)
                        {
                            sb.Append(commandArg[i]);
                            sb.Append(" ");
                        }
                        //Console.WriteLine(sb.ToString());
                        todoList.Add(new Activity(commandArg[1], "v", sb.ToString()));
                    }
                }
                else if (commandArg[0] == "set")
                {
                    int select = int.Parse(commandArg[1]) - 1;
                    if (commandArg[2] == "done" || commandArg[2] == "pending" || commandArg[2] == "ongoing")
                    {
                        todoList[select].NewStatus(commandArg[2]);
                    }
                }
                else
                {
                    Console.WriteLine("incorrect command");
                }
            } while (exit != true);
        }
    }
}
