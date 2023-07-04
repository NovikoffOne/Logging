using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pathfinder LoggingInFile = new Pathfinder(new LoggingInFile());
            Pathfinder LoggingInConsole = new Pathfinder(new LoggingInConsole());
            Pathfinder LoggingInFileOnFridays = new Pathfinder(new LoggingInFileOnFridays());
            Pathfinder LoggingInCosoleOnFridays = new Pathfinder(new LoggingInCosoleOnFridays());
            Pathfinder LoggingInCosoleAndFileOnFridays = new Pathfinder(new LoggingInCosoleAndFileOnFridays());
        }

        class ConsoleLogWritter
        {
            public virtual void WriteError(string message)
            {
                Console.WriteLine(message);
            }
        }

        class FileLogWritter
        {
            public virtual void WriteError(string message)
            {
                File.WriteAllText("log.txt", message);
            }
        }

        class SecureConsoleLogWritter : ConsoleLogWritter
        {
            public override void WriteError(string message)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    base.WriteError(message);
                }
            }
        }
        public interface ILogger
        {
            void Find();

            string WriteLog();
        }

        public class Pathfinder : ILogger
        {
            private ILogger _logger;

            public Pathfinder(ILogger logger)
            {
                _logger = logger;
            }

            public void Find()
            {
                _logger.Find();
            }

            public string WriteLog()
            {
                return _logger.WriteLog();
            }
        }

        public class LoggingInFile : ILogger
        {
            public void Find()
            {
                File.WriteAllText("log.txt", WriteLog());
            }

            public string WriteLog()
            {
                Random random = new Random();

                return random.Next(1000).ToString();
            }
        }

        public class LoggingInConsole : ILogger
        {
            public void Find()
            {
                Console.WriteLine("log.txt", WriteLog());
            }

            public string WriteLog()
            {
                Random random = new Random();

                return random.Next(1000).ToString();
            }
        }

        public class LoggingInFileOnFridays : ILogger
        {
            public void Find()
            {
                if(TryTodayFridays() == true)
                    File.WriteAllText("log.txt", WriteLog());
            }

            public string WriteLog()
            {
                Random random = new Random();

                return random.Next(1000).ToString();
            }

            private bool TryTodayFridays()
            {
                var dataTime = new DateTime();

                if (dataTime.DayOfWeek == DayOfWeek.Friday)
                    return true;

                return false;
            }
        }

        public class LoggingInCosoleOnFridays : ILogger
        {
            public void Find()
            {
                if (TryTodayFridays() == true)
                    Console.WriteLine(WriteLog());
            }

            public string WriteLog()
            {
                Random random = new Random();

                return random.Next(1000).ToString();
            }

            private bool TryTodayFridays()
            {
                var dataTime = new DateTime();

                if (dataTime.DayOfWeek == DayOfWeek.Friday)
                    return true;

                return false;
            }
        }

        public class LoggingInCosoleAndFileOnFridays : ILogger
        {
            public void Find()
            {
                Console.WriteLine(WriteLog());

                if (TryTodayFridays() == true)
                    File.WriteAllText("log.txt", WriteLog());
            }

            public string WriteLog()
            {
                Random random = new Random();

                return random.Next(1000).ToString();
            }

            private bool TryTodayFridays()
            {
                var dataTime = new DateTime();

                if (dataTime.DayOfWeek == DayOfWeek.Friday)
                    return true;

                return false;
            }
        }
    }
}
