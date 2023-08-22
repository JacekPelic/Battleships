using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Ships;

namespace Battleships
{
    public class Board
    {
        public static int Size = 10;
        protected Field[,] Fields = new Field[Size, Size];

        public Board()
        {
            InitializeBoard();
        }

        protected void InitializeBoard()
        {
            for (int x = 0; x < Fields.GetLength(0); x++)
            {
                for (int y = 0; y < Fields.GetLength(1); y++)
                {
                    Fields[x, y] = new Field();
                }
            }
        }

        public void PlaceShip(Ship ship)
        {
            Random random = new Random();

            //Fail-safe mechanism for infinite loop
            for (int counter = 0; counter < 10000; counter++)
            {
                // Assume square board
                var maxIndexOfShipStart = 10 - ship.Size;
                int x = random.Next(0, maxIndexOfShipStart);
                int y = random.Next(0, maxIndexOfShipStart);
                var initialCoordinates = new Coordinates(x, y);
                OrientationEnum orientation = (OrientationEnum)random.Next(0, 1);

                if (PlaceShip(initialCoordinates, orientation, ship))
                {
                    return;
                }
            }

            throw new Exception("Could not place ship on board!");
        }

        public bool IsHit(Coordinates coordinates)
        {
            var field = Fields[coordinates.X, coordinates.Y];
            field.IsHit = true;
            return field.IsOccupied;
        }

        public string FindShip(Coordinates coordinates, List<Ship> fleet)
        {
            var field = Fields[coordinates.X, coordinates.Y];

            if (field.IsOccupied)
            {
                var ship = fleet.FirstOrDefault(x => x.PositionFields.Contains(field));
                if (ship != null)
                {
                    return ship.Name;
                }
            }
            return string.Empty;
        }

        private bool PlaceShip(Coordinates initialCoordinates, OrientationEnum orientation, Ship ship)
        {
            var positionFields = new List<Field>();

            for (int i = 1; i <= ship.Size; i++)
            {
                Field field = new Field();

                if (orientation == OrientationEnum.Horizontal)
                {
                    field = Fields[initialCoordinates.X + i, initialCoordinates.Y];
                    positionFields.Add(field);
                    Console.WriteLine($"Placing ship on field {initialCoordinates.X + i}, {initialCoordinates.Y}");
                }
                else if (orientation == OrientationEnum.Vertical)
                {
                    field = Fields[initialCoordinates.X, initialCoordinates.Y + i];
                    positionFields.Add(field);
                    Console.WriteLine($"Placing ship on field {initialCoordinates.X + i}, {initialCoordinates.Y}");
                }
                else
                {
                    throw new Exception("Invalid orientation");
                }
            }

            if (positionFields.Any(x => x.IsOccupied))
            {
                return false;
            }
            else
            {
                foreach (var field in positionFields)
                {
                    field.IsOccupied = true;
                }
                ship.PositionFields = positionFields;
                return true;
            }
        }

        private Coordinates FindFieldOnBoard(Field field)
        {
            for (int x = 0; x < Fields.Length; x++)
            {
                for (int y = 0; y < Fields.Length; y++)
                {
                    if (Fields[x, y] == field)
                    {
                        return new Coordinates(x, y);
                    }
                }
            }

            return null;
        }

        public void PrintBoard()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int x = 0; x < Fields.GetLength(0); x++)
            {
                Console.Write(Coordinates.ValidLetters[x] + " ");
                for (int y = 0; y < Fields.GetLength(1); y++)
                {
                    var field = Fields[x, y];

                    if(field.IsOccupied && field.IsHit)
                    {
                        Console.Write("H ");
                    }
                    else if (field.IsOccupied)
                    {
                        Console.Write("X ");
                    }
                    else if (field.IsHit)
                    {
                        Console.Write("M ");
                    }
                    else
                    {
                        Console.Write("O ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
