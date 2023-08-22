using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Ships;

namespace Battleships.Tests
{
    [TestClass()]
    public class BoardTests : Board
    {
        [TestCleanup]
        public void Setup()
        {
            InitializeBoard();
        }
        
        [TestMethod()]
        public void PlaceShip_AllFieldsOccupied_ThrowsAnException()
        {
            //Arrange
            foreach (var field in Fields)
            {
                field.IsOccupied = true;
            }
            var ship = new Destroyer();

            //Act

            //Assert
            Assert.ThrowsException<Exception>(() => PlaceShip(ship));
        }

        [TestMethod()]
        public void PlaceShip_OneDestroyerOnEmptyBoard_PlacesShipOnBoard()
        {
            //Arrange
            var ship = new Destroyer();

            //Act
            PlaceShip(ship);

            //Assert
            var countFields = Fields.Cast<Field>().Count(field => field.IsOccupied);
            Assert.AreEqual(ship.Size, countFields);
        }

        [TestMethod()]
        public void PlaceShip_TwoDestroyersOneBattleshipOnEmptyBoard_PlacesShipsOnBoard()
        {
            //Arrange
            var destroyer1 = new Destroyer();
            var destroyer2 = new Destroyer();
            var battleship = new Battleship();

            //Act
            PlaceShip(destroyer1);
            PlaceShip(destroyer2);
            PlaceShip(battleship);

            //Assert
            var countFields = Fields.Cast<Field>().Count(field => field.IsOccupied);
            Assert.AreEqual(destroyer1.Size + destroyer2.Size + battleship.Size, countFields);
        }

        [TestMethod()]
        public void PlaceShip_PlaceAndDestroyShip_EnsureShipIsDestroyed()
        {
            //Arrange
            var destroyer = new Destroyer();
            PlaceShip(destroyer);

            //Act
            foreach (var field in destroyer.PositionFields)
            {
                field.IsHit = true;
            }

            //Assert
            var occupiedAndHitFields = Fields.Cast<Field>().Count(field => field.IsOccupied && field.IsHit);
            Assert.AreEqual(destroyer.Size, occupiedAndHitFields);
        }
    }
}