/*mini-project that involves CRUD (Create, Read, Update, Delete) operations using a list, for and  foreach loops, if-else statements, and a switch case in C#. In this project, we'll create a basic task  list application. 
Project Description: Simple Task List Application 
Features: 
Create a task: Add a new task with a title  
Read tasks: Display the list of tasks with their titles and descriptions. 
Update a task: Modify the title or description of an existing task. 
Delete a task: Remove a task from the list. 
Exit: Exit the application. 
*/

using System; 
using System.Collections.Generic;
class Program 
{ 
 static List<Task> tasks = new List<Task>(); 
 static void Main(string[] args) 
 { 
 while (true) 
 { 
 Console.WriteLine("Task Management Application"); 
 Console.WriteLine("1. Create Task"); 
 Console.WriteLine("2. Read Tasks"); 
 Console.WriteLine("3. Update Task"); 
 Console.WriteLine("4. Delete Task"); 
 Console.WriteLine("5. Exit"); 
 Console.Write("Enter your choice (1-5): "); 
 if (int.TryParse(Console.ReadLine(), out int choice))
 { 
 switch (choice) 
 { 
 case 1: 
 CreateTask(); 
 break; 
 case 2: 
 ReadTasks(); 
 break; 
 case 3: 
 UpdateTask(); 
 break; 
 case 4: 
 DeleteTask(); 
 break; 
 case 5: 
 Console.WriteLine("Goodbye!"); 
 return; 
 default: 
 Console.WriteLine("Invalid choice. Please enter a valid option.");  break; 
 } 
 } 
 else 
 { 
 Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");  } 
 Console.WriteLine(); 
 } 
 }
 static void CreateTask() 
 { 
 Console.Write("Enter Task Title: "); 
 string title = Console.ReadLine(); 
 Console.Write("Enter Task Description: "); 
 string description = Console.ReadLine(); 
 tasks.Add(new Task(title, description)); 
 Console.WriteLine("Task created successfully!"); 
 } 
 static void ReadTasks() 
 { 
 if (tasks.Count == 0) 
 { 
 Console.WriteLine("No tasks to display."); 
 } 
 else 
 { 
 Console.WriteLine("Tasks:"); 
 foreach (Task task in tasks) 
 { 
 Console.WriteLine($"Title: {task.Title}, Description: {task.Description}");  } 
 } 
 } 
 static void UpdateTask() 
 { 
 Console.Write("Enter the index of the task you want to update: ");
 if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < tasks.Count)  { 
 Console.Write("Enter Task Title: "); 
 string title = Console.ReadLine(); 
 Console.Write("Enter Task Description: "); 
 string description = Console.ReadLine(); 
 Task task = tasks[index]; 
 task.Title = title; 
 task.Description = description; 
 Console.WriteLine("Task updated successfully!"); 
 } 
 else 
 { 
 Console.WriteLine("Invalid index. Please enter a valid task index.");  } 
 } 
 static void DeleteTask() 
 { 
 Console.Write("Enter the index of the task you want to delete: ");  if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < tasks.Count)  { 
 tasks.RemoveAt(index); 
 Console.WriteLine("Task deleted successfully!"); 
 } 
 else 
 { 
 Console.WriteLine("Invalid index. Please enter a valid task index.");  } 
 }
} 
class Task 
{ 
 public string Title { get; set; } 
 public string Description { get; set; } 
 public Task(string title, string description)  { 
 Title = title; 
 Description = description; 
 } 
}
