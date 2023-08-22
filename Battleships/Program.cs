// See https://aka.ms/new-console-template for more information
using Battleships;
using Battleships.Ships;
using System.Runtime.CompilerServices;

var board = new Board();

var fleet = new List<Ship>
{
    new Destroyer(),
    new Destroyer(),
    new Battleship()
};

foreach (var ship in fleet)
{
    board.PlaceShip(ship);
}

while (fleet.Any(ship => !ship.IsDestroyed()))
{
    //For debugging purposes uncomment this line
    //board.PrintBoard();
    Console.WriteLine("Enter coordinates:");
    var input = Console.ReadLine();

    var x = Coordinates.ValidLetters.FindIndex(letter => letter == input.ToUpper().Substring(0, 1));
    int.TryParse(input.Substring(1), out var y);

    if (x < 0 || y <= 0 || y > Board.Size)
    {
        Console.WriteLine("Invalid coordinates, try again!");
        continue;
    }

    var coordinates = new Coordinates(x, y - 1);
    var isHit = board.IsHit(coordinates);
    if (isHit)
    {
        var shipName = board.FindShip(coordinates, fleet);
        Console.WriteLine($"Hit! {shipName} is hit!");
    }
    else
    {
        Console.WriteLine("Miss!");
    }
}

Console.WriteLine("Fleet was destroyed!");
board.PrintBoard();
Console.ReadLine();
