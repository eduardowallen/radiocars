using System;
using System.Collections.Generic;
using System.Text;

namespace radiocars
{
    class Car
    {
        public int posx;
        public int posy;
        public char direction;
        public string model = "Monster Truck"; // Our default model type.

        private bool Crash(int x, int y)
        {
            Console.WriteLine("Our " + this.model + " crashed into a wall while trying to reach [" + x + "," + y + "]\n");
            return false;
        }
        private void Move(Car car, int x, int y)
        {
            car.posx = x;
            car.posy = y;
            Console.WriteLine(this.model + " moved to [" + x + "," + y + "]\n");
        }
        public bool Navigate(Grid room, char direction, char move)
        {
            // [0] = x coordinate, [1] = y coordinate
            int[] next_pos = new int[] { 0, 0 };
            // N = y+1, S = y+1, W = x-1, E = x+1
            if (direction.Equals('N'))
                next_pos[1] = 1;
            else if (direction.Equals('S'))
                next_pos[1] = -1;
            else if (direction.Equals('E'))
                next_pos[0] = 1;
            else if (direction.Equals('W'))
                next_pos[0] = -1;
            else
                return false;
            int[] next_step = new int[] { this.posx, this.posy };

            if (move.Equals('F'))
            // F = N S W E
            {
                next_step[0] += next_pos[0];
                next_step[1] += next_pos[1];
            }
            else if (move.Equals('B'))
            // B = N' S' W' E'
            {
                next_step[0] += next_pos[0] * -1;
                next_step[1] += next_pos[1] * -1;
            }
            else if (move.Equals('R'))
            // R: N=E, S=W, W=N, E=S
            {
                if (direction.Equals('N'))
                {
                    next_step[0] += next_pos[0] + 1;
                    next_step[1] += next_pos[1] - 1;
                }
                else if (direction.Equals('S'))
                {
                    next_step[0] += next_pos[0] - 1;
                    next_step[1] += next_pos[1] + 1;
                }
                else if (direction.Equals('W'))
                {
                    next_step[0] += next_pos[0] + 1;
                    next_step[1] += next_pos[1] + 1;
                }
                else if (direction.Equals('E'))
                {
                    next_step[0] += next_pos[0] - 1;
                    next_step[1] += next_pos[1] - 1;
                }
                else
                    return false;
            }
            else if (move.Equals('L'))
            // L: N=W, S=E, W=S, E=N
            {
                if (direction.Equals('N'))
                {
                    next_step[0] += next_pos[0] - 1;
                    next_step[1] += next_pos[1] - 1;
                }
                else if (direction.Equals('S'))
                {
                    next_step[0] += next_pos[0] + 1;
                    next_step[1] += next_pos[1] + 1;
                }
                else if (direction.Equals('W'))
                {
                    next_step[0] += next_pos[0] + 1;
                    next_step[1] += next_pos[1] - 1;
                }
                else if (direction.Equals('E'))
                {
                    next_step[0] += next_pos[0] - 1;
                    next_step[1] += next_pos[1] + 1;
                }
                else
                    return false;
            }
            else
                return false;

            /* check if the next position is within our domain.
             * If not, the car crashed into a wall. */
            if (room.WithinDomain(next_step[0], next_step[1]))
                this.Move(this, next_step[0], next_step[1]);
            else
                return this.Crash(next_step[0], next_step[1]);

            return true;
        }
        public bool Drive(Grid room, string input)
        {
            Console.WriteLine(
                "Input from the user into Drive function: " +
                "\nInput:" + input +
                "\nRoom dimensions: " + room.height + "x" + room.width +
                "\nCar starting position: " + this.posx + "," + this.posy +
                "\nCar facing: " + this.direction + "\n"
                );
            int i = 0;
            // loop through each step we take
            foreach (char c in input)
                if (this.Navigate(room, this.direction, input[i]))
                    i++;
                else
                    return false;
            return true;
        }
    }
}
