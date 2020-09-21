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
                            Console.WriteLine("");
                        }
                        sr.Close();
                    }
                    catch (Exception e) {
                        logger.Error(e.Message);
                    }
                }

                // add another movie to the file
                else if (choice == "2") {
                    try {
                        //movieID will be assigned by counting movies in library
                        Console.WriteLine("Enter the Movie ID: ");
                        string movieID = Console.ReadLine();

                        // ask user for name of title
                        Console.WriteLine("Enter the title of the movie: ");
                        string title = Console.ReadLine();

                        // ask user for genres
                        Console.WriteLine("Enter the genre of the movie: ");
                        string genres = Console.ReadLine();

                        // add additional genres
                        string addGenre;
                        do {
                            Console.WriteLine("\nWould you like to add another movie genre?");
                            Console.WriteLine("1) yes");
                            Console.WriteLine("2) no");
                            addGenre = Console.ReadLine();

                            if (addGenre == "1") {
                                // ask user for additional genres
                                Console.WriteLine("Enter the next genre of the movie: ");
                                string tempGenre = Console.ReadLine();
                                genres += "|" + tempGenre;
                            }

                        } while (addGenre == "1");

                        // check to see if the same movie exists in library
                        Boolean movieExists = true;
                        StreamReader sr = new StreamReader(file);
                        sr.ReadLine(); // skip first line of document

                        while (!sr.EndOfStream) {
                            string line = sr.ReadLine();
                            string[] infoArray = line.Split(',');

                            // get movie title
                            string titleInLibrary = ""; 
                            for (int i = 1; i < (infoArray.Length - 1); i++) {
                                // reinsert any missing commas into title
                                if (i != (infoArray.Length - 2)) 
                                    titleInLibrary += infoArray[i] + ",";
                                else
                                    titleInLibrary += infoArray[i]; // no extra comma to the end of the title
                            }

                            // check for duplicate title each iteration
                            if(title.Equals(titleInLibrary)) {
                                Console.WriteLine($"\n{title} is already in the library and cannot be added.\n");
                                movieExists = true;
                                break;
                            }
                            else {movieExists = false;}
                        }
                        sr.Close();

                        // write movie to file if it does not previously exist in library
                        if (movieExists == false) {
                            StreamWriter sw = new StreamWriter(file, append: true);
                            sw.WriteLine(movieID + "," + title + "," + genres);
                            sw.Close();
                        }
                        Console.WriteLine("");
                    }
                     catch (Exception e) {
                        logger.Error(e.Message);
                    }
                }
                // choice other than 1 or 2 to exit program
                else {
                    Console.WriteLine("\nGoodbye!\n");
                }
                
            }
            logger.Info("End program.");
        }
    }
}
