using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoogleCalendar
{
    class UserInput
    {
        public static void BasicDataInput()
        {
             DateTime start = arrowKeysDate();
             DateTime startTime = arrowKeysTime("start");
             DateTime startDateTime = start.Date.Add(startTime.TimeOfDay);
             DateTime endDateTime = new DateTime();
             switch (timeOrDuration())
             {
                 case 'd':
                     endDateTime = startDateTime.Add(setDuration());
                         break;
                 case 'e':
                     endDateTime = startDateTime.Date.Add(arrowKeysTime("end").TimeOfDay); 
                         break;
             }
             string[] eventDetails = setDetails();
        }
        public static string OptionSelect()
        {
            //cycle through options 
            bool done = false;
            Int16 eventIndex = 0;
            string[] optionList = new string[]{"Create New Event","List Upcoming Events","Update Events","Delete Events"};
            Console.WriteLine("Use Arrow Keys to Select Option");

            do
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case (ConsoleKey.UpArrow):
                        if(eventIndex < optionList.Length)
                        {
                            eventIndex += eventIndex;
                        }
                        break;
                    case (ConsoleKey.RightArrow):
                        if(eventIndex < optionList.Length)
                        {
                            eventIndex += eventIndex;
                        }
                        break;
                    case (ConsoleKey.DownArrow):
                        if(eventIndex < optionList.Length)
                        {
                            eventIndex -= eventIndex;
                        }
                        break;
                    case (ConsoleKey.LeftArrow):
                        if(eventIndex < optionList.Length)
                        {
                            eventIndex -= eventIndex;
                        }
                        break;
                }
                Console.Write(optionList[eventIndex]);
            } while (!done);
            return optionList[eventIndex];

        }
        public static DateTime arrowKeysDate()
        {//press up,down,right,left to highlight and change date values
            DateTime working = DateTime.Now;
            DateTime newWorking = working;
            bool done = false;
            //timeScope for changeing month, day, year. left-right arrows changes timeScope
            int timeScope = 1;
            string strTimeScope="Month";
            Console.Write("\r{1}:{0}  ", newWorking.ToShortDateString(),strTimeScope);
            do
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {

                    case ConsoleKey.UpArrow:
                        switch (timeScope) {
                            case (1): newWorking = newWorking.AddMonths(1); break;
                            case (2): newWorking = newWorking.AddDays(1); break;
                            case (3): newWorking = newWorking.AddYears(1); break;
                        }
                    break;
                    case ConsoleKey.DownArrow:
                        switch (timeScope) {
                            case (1): newWorking = newWorking.AddMonths(-1); break;
                            case (2): newWorking = newWorking.AddDays(-1); break;
                            case (3): newWorking = newWorking.AddYears(-1); break;
                        }
                    break;
                    case ConsoleKey.RightArrow:
                        if (timeScope < 3)
                        {
                            timeScope = timeScope + 1;
                        }
                            break;
                    case ConsoleKey.LeftArrow:
                        if (timeScope > 1)
                        {
                        timeScope = timeScope -1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine("Event Date: " + newWorking.ToShortDateString());
                        done =true; break;

                }
            switch (timeScope) { case (1): strTimeScope = "Month"; break; case (2): strTimeScope = "Day"; break; case (3): strTimeScope = "Year"; break; };
                if (!done)
                {
                Console.Write("\r{1}:{0}  ", newWorking.ToShortDateString(),strTimeScope);
                }

            } while (!done);
            return newWorking;
        }
        public static DateTime arrowKeysTime(string Type)
        {//press up,down,right,left to highlight and change date values
            //type determines Start or EndTime we are creating
            DateTime working = DateTime.Now;
            DateTime newWorking = working;
            string formattedType = Type.Trim();
            string finalType = char.ToUpper(formattedType[0]) + formattedType.Substring(1);
            bool done = false;
            //timeScope for changeing month, day, year. left-right arrows changes timeScope
            int timeScope = 1;
            string strTimeScope="Hour";
            Console.Write("\r{1}:{0}  ", newWorking.ToShortTimeString(),strTimeScope, finalType);
            do
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        switch (timeScope) {
                            case (1): newWorking = newWorking.AddHours(1); break;
                            case (2): newWorking = newWorking.AddMinutes(1); break;
                        }
                    break;
                    case ConsoleKey.DownArrow:
                        switch (timeScope) {
                            case (1): newWorking = newWorking.AddHours(-1); break;
                            case (2): newWorking = newWorking.AddMinutes(-1); break;
                        }
                    break;
                    case ConsoleKey.RightArrow:
                        if (timeScope < 2)
                        {
                            timeScope = timeScope + 1;
                        }
                            break;
                    case ConsoleKey.LeftArrow:
                        if (timeScope > 1)
                        {
                        timeScope = timeScope -1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine($"Event {finalType} Time: "+ newWorking.ToShortTimeString());

                        done =true; break;
                }
            switch (timeScope) { case (1): strTimeScope = "Hour"; break; case (2): strTimeScope = "Minute"; break;};
                if (!done)
                {
                    Console.Write("\r{2} {1}:{0}  ", newWorking.ToShortTimeString(), strTimeScope, finalType);
                }
            } while (!done);
            return newWorking;
        }
        public static char timeOrDuration()
        {//user sets duration or picks end time
            Console.WriteLine("Set Duration or End Time. D/E");
            string resp = "";
            bool done = false;
            char option='a';
            do
            {
                Console.WriteLine("Please Enter 'D' or 'E'");
                resp = Console.ReadLine().Trim().ToUpper();
                if (resp == "D" || resp == "E")
                {
                    done = true;
                    break;
                }else if (resp != "D" || resp != "E")
                {
                    Console.WriteLine("Bad Input");
                }

            } while (!done);

            switch (resp)
            {
                case "D":
                    option = 'd';
                    break;
                case "E":
                    option = 'e';
                    break;
            }
            
            return option;
        }
        public static TimeSpan setDuration()
        {//different because of timespan
            TimeSpan duration = new TimeSpan(0,0,0);
            TimeSpan ts15Min = new TimeSpan(0,15,0);
            TimeSpan ts1Hr = new TimeSpan(1, 0, 0);
            bool done = false;
            //timeScope for changeing month, day, year. left-right arrows changes timeScope
            int timeScope = 1;
            string strTimeScope="Hours";
            Console.Write("\r{1}:{0}  ", duration.ToString(),strTimeScope);
            do
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        switch (timeScope) {
                            case (2): duration = duration.Add(ts15Min); break;
                            case (1): duration = duration.Add(ts1Hr); break;
                        }
                    break;
                    case ConsoleKey.DownArrow:
                        switch (timeScope) {
                            case (2): duration = duration.Subtract(ts15Min); break;
                            case (1): duration = duration.Subtract(ts1Hr); break;
                        }
                    break;
                    case ConsoleKey.RightArrow:
                        if (timeScope < 2)
                        {
                            timeScope = timeScope + 1;
                        }
                            break;
                    case ConsoleKey.LeftArrow:
                        if (timeScope > 1)
                        {
                        timeScope = timeScope -1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine("Duration Time: "+ duration.ToString());

                        done =true; break;
                }
            switch (timeScope) { case (1): strTimeScope = "Hour"; break; case (2): strTimeScope = "Minute"; break;};
                if (!done)
                {
                    Console.Write("\r{1}:{0}  ", duration.ToString(), strTimeScope);
                }
            } while (!done);
            return duration;
        }

        public static string[] setDetails()
        {//here we will set summary, desc, color, location
            List<string>dets = new List<string>();
            string[] enterDetails = new string[] { "Title", "Description", "Color", "Location" };
            bool done = false;
            int index = 0;
            Console.WriteLine("Enter Details. If none, omit field");
            foreach(string s in enterDetails)
            {
                Console.WriteLine($"Enter {s}");
                        string res = Console.ReadLine();
                        dets.Add(res);
            }
            foreach(string s in dets)
            {
                Console.WriteLine($"{enterDetails[index]} set to {s}");
                index++;
            }
            return dets.ToArray();
        }
        public static int ReturnColorID(string color)
        {
            string c = color.Trim().ToLower();
            int colorID;
            switch (c)
            {
                case "blue":
                    colorID = 1;
                    break;
                case "green":
                    colorID = 2;
                    break;
                case "purple":
                    colorID = 3;
                    break;
                case "red":
                    colorID = 4;
                    break;
                case "yellow":
                    colorID = 5;
                    break;
                case "orange":
                    colorID = 6;
                    break;
                case "turquoise":
                    colorID = 7;
                    break;
                case "gray":
                    colorID = 8;
                    break;
                case "dblue":
                    colorID = 9;
                    break;
                case "dgreen":
                    colorID = 10;
                    break;
                case "dred":
                    colorID = 11;
                    break;
                default:
                    colorID = 1;
                    break;
            }
            return colorID;
        }
    }
}
