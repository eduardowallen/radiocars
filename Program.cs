using System.Text.RegularExpressions;
using System;

namespace radiocars
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid room = new Grid();
            Car car = new Car();
            Console.WriteLine(
                "Developer test written by Eduardo Wallén 2021-03-18\n" +
                "To start, please enter the size of the room that our car can move (height and width)\n" +
                "To keep things sane for now, we only allow integers between 0-9\n" + 
                "Accepted format is int whitespace int, eg: 4 4 for a 4x4 grid\n"
                );
            while (true)
            {
                string input = Console.ReadLine();
                Match match = Regex.Match(input, @"^\d\s\d$");
                if (match.Success) 
                {
                    room.height = (int)Char.GetNumericValue(input[0]);
                    room.width = (int)Char.GetNumericValue(input[2]);
                    Console.WriteLine(
                        "The grid " + input[0] + "x" + input[2] + " will be selected.\n" +
                        "Now, enter the starting position for our car as well as the direction.\n" +
                        "Input with two integers and one letter, all separated with spaces, eg: 1 2 E\n"
                        );
                    input = Console.ReadLine();
                    match = Regex.Match(input, @"^\d\s\d\s[NSWE]$");
                    Console.WriteLine(room.height + ", " + room.width);
                    while (!match.Success && !room.WithinDomain(input[0], input[2]))
                    {
                        Console.WriteLine("Bad format. Try again. Eg: 1 2 E\n");
                        input = Console.ReadLine();
                        match = Regex.Match(input, @"^\d\s\d\s[NSWE]$");
                    }
                    Console.WriteLine(
                        "The starting position for our car will be [" + input[0] + "," + input[2] + "], facing " + input[4] + "\n" +
                        "Now, please enter the commands that you want to take.\n" +
                        "The commands are:\n" +
                        "F = Forward\n" +
                        "B = Back\n" +
                        "L = Left\n" +
                        "R = Right\n" +
                        "Please write the commands without whitespace.\n"
                        );

                    car.posx = (int)Char.GetNumericValue(input[0]);
                    car.posy = (int)Char.GetNumericValue(input[2]);
                    car.direction = input[4];
                    Console.WriteLine(
                        "car.posx: " + car.posx +
                        "\ncar.posy: " + car.posy +
                        "\ncar.direction: " + car.direction + "\n");
                    input = Console.ReadLine();
                    match = Regex.Match(input, @"[FBLR]$");
                    while (!match.Success)
                    {
                        Console.WriteLine("Bad format. Try again. Eg: BLFLFFR\n");
                        input = Console.ReadLine();
                        match = Regex.Match(input, @"[FBLR]$");
                    }
                    Console.WriteLine("Let's go!\n");
                    if (car.Drive(room, input))
                        Console.WriteLine("Seems like we made it!\n");
                    else
                        Console.WriteLine("Don't drink and drive.\n");
                    Console.WriteLine("End of program. Restarting!\n");
                } else {
                    Console.WriteLine("Bad format. Try again.\n");
                }
            }
        }
    }
}
