using System;
using NLog.Web;
using System.IO;

namespace movie_library
{
    class Program
    {
        static void Main(string[] args)
        {
            // create instance of Logger
            string path = "/Users/peterkovacik/Documents/WCTC/dotNet database/movie_library/nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Begin logger.");



            logger.Info("End program.");
        }
    }
}
