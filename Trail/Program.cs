using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Trail
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the Trail Analytics console application. Please select what you need to do to proceed\n");            
            System.Console.WriteLine("Press 1 to Get SQL file;\nPress 2 to get a CSV file;\n");
                        
            int userAction = 0;
            string textfilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"output.stream.201611.txt");
            string sqlfilepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"output.stream.201611.sql");
            string csvfilepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"output.stream.201611.csv");
            string[] fileContents = File.ReadAllLines(textfilePath);
            try
            {
                userAction = Convert.ToInt32(Console.ReadLine());
                int direction = getUserinput(userAction);                

                //Convert the file to sql
                if(direction == 1){
                    //if file exists, empty the file. If it isn't there, create the file.
                    if (File.Exists(sqlfilepath))
                    {
                        System.IO.File.WriteAllText(sqlfilepath, string.Empty);
                        Console.WriteLine("Cleared sql file content \n");
                    }
                    else
                    {
                        File.Create(sqlfilepath);
                        Console.WriteLine("Created sql file \n");
                    }

                    Console.WriteLine("Please wait while we write your data... \n");

                    //We have the file created, now we proceed to convert it to sql.

                    for (int x = 0; x < fileContents.Length; x++)
                    {
                        string[] data = fileContents[x].Split(',');
                        
                        string precision1 = data[2];
                        string precision4 = data[5];
                        string precisionScores = data[6];

                        //the input file has an input for date as 2000.01.03 but you need to replace the . with -
                        string precisionDateTime = data[0].Replace('.','-') + '-' + data[1];

                        string sqlInsert = "insert into output_stream(precision1, precision4, precisionScores, precisionDateTime) values('"+precision1+"', '"+precision4+"', '"+precisionScores+"', '"+precisionDateTime+"');";
                        File.AppendAllText(sqlfilepath, sqlInsert + Environment.NewLine);

                    }
                    Console.WriteLine("Your file has been successfully created, press any key to exit");
                    Console.ReadLine();
                }
                //Convert file to CSV
                else if(direction == 2){
                    //if file exists, empty the file. If it isn't there, create the file.
                    if (File.Exists(csvfilepath))
                    {
                        System.IO.File.WriteAllText(csvfilepath, string.Empty);
                        Console.WriteLine("Cleared CSV file content \n");
                    }
                    else
                    {
                        File.Create(csvfilepath);
                        Console.WriteLine("Created CSV file \n");
                    }

                    Console.WriteLine("Please wait while we write your data... \n");

                    //We already have the  the file contents...we just do a string replace and then save the output to the file
                    for (int y=0; y <  fileContents.Length; y++)
                    {  
                        string data = fileContents[y].Replace(',', '|');
                        File.AppendAllText(csvfilepath, data + Environment.NewLine);

                    }

                    Console.WriteLine("Your file has been successfully created, press any key to exit");
                    Console.ReadLine();
                }
                else{

                }                

            }
            catch
            {
                Console.WriteLine("Please only input numbers. Exit and start over");
                Console.ReadLine();
            }          
           
        }
        public static int getUserinput(int userAction) {
            if (userAction == 1)
            {
                Console.Clear();
                Console.WriteLine("You selected SQL output.\n");
                return 1;
            }
            else if (userAction == 2)
            {
                Console.Clear();
                Console.WriteLine("You selected CSV output.\n");
                return 2;
            }
            else
            {
                return 0;
            } 
        }       
    }
}
