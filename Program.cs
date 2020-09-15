using System;
using NLog.Web;
using System.IO;

namespace movie_library
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movie Library Application\n");
            
            // create instance of Logger
            string path = "/Users/peterkovacik/Documents/WCTC/dotNet database/movie_library/nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Begin program.");

            // movie data stored in "movies.csv"
            string file = "movies.csv";
            
            // if movie file does not exist
            if(!File.Exists(file)) {
                logger.Info("The file " + file + " does not exist.");
            }
            // if movie file exists
            else {
                StringReader sr = new StringReader(file);

                
                // print menu
                Console.WriteLine("Enter 1 to read data file.");
                Console.WriteLine("Enter 2 to add a movie to the file.");
                Console.WriteLine("Enter anything else to quit.");
                string choice = Console.ReadLine();

                if (choice == "1") {
                    // skip first line of file
                    sr.ReadLine();
                }
                else if (choice == "2") {
                    //assign movieID
                    int movieID;

                    // ask user for name of title
                    string title;

                    // ask user for genres
                    string genre;

                    string addGenre;
                    do {
                        Console.WriteLine("\nWould you like to add another genere?");
                        Console.WriteLine("1) yes");
                        Console.WriteLine("2) no");
                        addGenre = Console.ReadLine();
                    } while (addGenre == "1");
                }
                else {
                    Console.WriteLine("\nGoodbye!\n");
                }
                
                sr.Close();
            }
            logger.Info("End program.");
        }
    }
}
