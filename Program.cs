using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGoogleCalendar;

namespace CalendarQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static int winX = 0;
        static int winY = 0;

        static void Main(string[] args)
        {
            //Establish Service
            Console.WriteLine("Establishing Service");
            var service = ConsoleGoogleCalendar.Service.EstablishService();
            Console.WriteLine((service != null) ? "Serivce Established" : "Service Failed");
            Console.WriteLine("Select Option.");
            string option = UserInput.OptionSelect();
            //CRUD.ReadEvents(service);

            //Select Option: Create, Read, Update, Delete
                Console.Read();
        }
        public static void StartScreen()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            Console.Title="Google Calendar Terminal";
            string[] banner = new string[]
            {" _____ _____ ____  __  __ ___ _   _    _    _        ____    _    _     _____ _   _ ____    _    ____  ",
            @"|_   _| ____|  _ \|  \/  |_ _| \ | |  / \  | |      / ___|  / \  | |   | ____| \ | |  _ \  / \  |  _ \ ",
            @"  | | |  _| | |_) | |\/| || ||  \| | / _ \ | |     | |     / _ \ | |   |  _| |  \| | | | |/ _ \ | |_) |",
            @"  | | | |___|  _ <| |  | || || |\  |/ ___ \| |___  | |___ / ___ \| |___| |___| |\  | |_| / ___ \|  _ < ",
            @"  |_| |_____|_| \_\_|  |_|___|_| \_/_/   \_\_____|  \____/_/   \_\_____|_____|_| \_|____/_/   \_\_| \_\"
            };
            Array.Reverse(banner);
            int f = 0;
            foreach(string s in banner)
            {
                CenterText();
                WriteAt(s, winX, winY - f);
                f++;
            }
            //List options: List events for day or date range (not done)
            //              Create new events/tasks/reminders(kinda started)
            //                  "Create"
            //              Create with shorthand(not done)
            //              Delete events from a list (not even kinda)
            //              Exit the program
            //                  make a menu for options or type for shorthand:)

            Console.WriteLine(Environment.NewLine,"Select an option: ");
            Console.WriteLine("");
        }
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x-26, y);
                
                foreach(char c in s)
                {
                    Console.Write(c);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void CenterText()
        {
            //77: will have to change this value for each font
            int width = 52;
            winX = (Console.WindowWidth / 2) - (width / 2);
            winY = Console.WindowHeight / 2;
        }
                    
        static void RemoveScrollBars()
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(20,20);
            Console.SetWindowSize(100, 20);
        }
    }
    class Details
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
    }
}
