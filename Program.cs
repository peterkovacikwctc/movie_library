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
            // string path = "/Users/peterkovacik/Documents/WCTC/dotNet database/movie_library/nlog.config"; // macOS
            string path = Directory.GetCurrentDirectory() + "\\nlog.config"; // Windows 10
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Begin program.");

            // movie data stored in "movies.csv"
            string file = "movies.csv";
            
            // check if file exists
            if(!File.Exists(file)) {
                logger.Info("The file " + file + " does not exist.");
            }
            else {
               

                // print menu
                Console.WriteLine("Enter 1 to read movies from data file.");
                Console.WriteLine("Enter 2 to add a movie to the file.");
                Console.WriteLine("Enter anything else to quit.");
                string choice = Console.ReadLine();

                // read movies from data file
                if (choice == "1") {
                    try {
                        StreamReader sr = new StreamReader(file);
                        // skip first line of file
                        sr.ReadLine();

                        while (!sr.EndOfStream) {
                            string line = sr.ReadLine();
                            string[] infoArray = line.Split(',');

                            // movieID (before first comma)
                            string movieID = infoArray[0]; 

                            // title (everything between first comma and last comma)
                            string title = ""; 
                            for (int i = 1; i < (infoArray.Length - 1); i++) {
                                // reinsert any missing commas into title
                                if (i != (infoArray.Length - 2)) 
                                    title += infoArray[i] + ",";
                                else
                                    title += infoArray[i]; // no extra comma to the end of the title
                            }
                            
                            // genres (after last comma)
                            string genreList = infoArray[(infoArray.Length - 1)]; 
                            string[] genreArray = genreList.Split('|');
                            string seperator = ", ";
                            string genres = "";
                            genres += String.Join(seperator, genreArray);
                            
                            // output information
                            Console.WriteLine($"Movie ID: {movieID}");
                            Console.WriteLine($"Title: {title}");
                            Console.WriteLine($"Genres: {genres}");
                            Console.WriteLine("\n");
                        }
                        sr.Close();
                    }
                    catch (Exception e) {
                        logger.Error(e.Message);
                    }
                }

                // add another movie to the file
                else if (choice == "2") {
                    //assign movieID
                    int movieID;

                    // ask user for name of title
                    string title;

                    // ask user for genres
                    string genre;

                    string addGenre;
                    do {
                        Console.WriteLine("\nWould you like to add another movie genre?");
                        Console.WriteLine("1) yes");
                        Console.WriteLine("2) no");
                        addGenre = Console.ReadLine();
                    } while (addGenre == "1");
                }
                else {
                    Console.WriteLine("\nGoodbye!\n");
                }
                
            }
            logger.Info("End program.");
        }
    }
}
