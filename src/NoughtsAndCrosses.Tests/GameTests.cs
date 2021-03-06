﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses.Domain;
using System.Linq;

namespace NoughtsAndCrosses.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void StartGame_IntialisesTheGameWithTheCorrectBoard()
        {
            var game = Game.StartNew();

            Assert.AreEqual(9, game.Board.TotalSpaces);
            Assert.AreEqual(9, game.Board.FreeSpaces, "Free spaces should be the same as total spaces for a new game");
        }

        [TestMethod]
        public void StartGame_IntialisesTheGameWithTheCorrectPlayers()
        {
            var game = Game.StartNew();
            Assert.IsNotNull(game.Players, "Players is null, expected not null");
            Assert.AreEqual(2, game.Players.Count);
            var player1 = game.Players.ToList()[0];
            Assert.AreEqual(1, player1.Number);
            Assert.AreEqual("Player 1", player1.Name);
            Assert.AreEqual("X", player1.Tag);
            var player2 = game.Players.ToList()[1];
            Assert.AreEqual(2, player2.Number);
            Assert.AreEqual("Player 2", player2.Name);
            Assert.AreEqual("0", player2.Tag);
        }

        [TestMethod]
        public void StartGame_InitialisesTheGameWithTheCorrectTrackingValues()
        {
            var game = Game.StartNew();
            Assert.IsNull(game.LastPlayer);
            Assert.IsTrue(game.IsInProgress());
            Assert.IsNull(game.Winner);
        }

        [TestMethod]
        public void TakeTurn_AllowsPlayer1ToTakeTheirTurn()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            TakeTurnFor(game, player1, 1, 1);
            Assert.AreEqual(8, game.Board.FreeSpaces);
        }

        [TestMethod]
        public void TakeTurn_TracksTheLastPlayerTakeATurnCorrectly()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player2, 2, 2);
            Assert.IsNotNull(game.LastPlayer);
            Assert.AreEqual(2, game.LastPlayer.Number);
            Assert.AreEqual(7, game.Board.FreeSpaces);
        }

        [TestMethod]
        [ExpectedException(typeof(SimultaneousTurnsException))]
        public void TakeTurn_DoesNotAllowAPlayerToTakeSimultaneousTurns()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player1, 2, 2);
            Assert.AreEqual(8, game.Board.FreeSpaces);
        }

        [TestMethod]
        [ExpectedException(typeof(SpaceAlreadyOccupiedException))]
        public void TakeTurn_DoesNotAllowAPlayerToOccupyAnOccupiedSpace()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 3, 2);
            TakeTurnFor(game, player2, 3, 2);
            Assert.AreEqual(8, game.Board.FreeSpaces);
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromTopLeftToBottomRight()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player2, 2, 1);
            TakeTurnFor(game, player1, 2, 2);
            TakeTurnFor(game, player2, 3, 2);
            TakeTurnFor(game, player1, 3, 3);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromMiddleLeftToMiddleRight()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 2, 1);
            TakeTurnFor(game, player2, 1, 1);
            TakeTurnFor(game, player1, 2, 2);
            TakeTurnFor(game, player2, 1, 2);
            TakeTurnFor(game, player1, 2, 3);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromTopMiddleToBottomMiddle()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 2);
            TakeTurnFor(game, player2, 1, 3);
            TakeTurnFor(game, player1, 2, 2);
            TakeTurnFor(game, player2, 2, 3);
            TakeTurnFor(game, player1, 3, 2);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromTopLeftToTopRight()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player2, 2, 1);
            TakeTurnFor(game, player1, 1, 2);
            TakeTurnFor(game, player2, 3, 2);
            TakeTurnFor(game, player1, 1, 3);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromTopLeftToBottomLeft()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player2, 1, 3);
            TakeTurnFor(game, player1, 2, 1);
            TakeTurnFor(game, player2, 2, 3);
            TakeTurnFor(game, player1, 3, 1);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromBottomLeftToBottomRight()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 3, 1);
            TakeTurnFor(game, player2, 1, 1);
            TakeTurnFor(game, player1, 3, 2);
            TakeTurnFor(game, player2, 2, 1);
            TakeTurnFor(game, player1, 3, 3);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        public void TakeTurn_SetsTheWinner_FromTopLRightToBottomRight()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 3);
            TakeTurnFor(game, player2, 1, 1);
            TakeTurnFor(game, player1, 2, 3);
            TakeTurnFor(game, player2, 1, 2);
            TakeTurnFor(game, player1, 3, 3);

            Assert.IsNotNull(game.Winner);
            Assert.AreEqual(1, game.Winner.Number);
            Assert.IsFalse(game.IsInProgress());
        }

        [TestMethod]
        [ExpectedException(typeof(NoMoreTurnsAllowedException))]
        public void TakeTurn_DoesNotAllowAnotherTurn_WhenTheGameFinishesAsAStalemate()
        {
            var game = Game.StartNew();
            var player1 = game.Players.ToList()[0];
            var player2 = game.Players.ToList()[1];
            TakeTurnFor(game, player1, 1, 1);
            TakeTurnFor(game, player2, 1, 2);
            TakeTurnFor(game, player1, 1, 3);
            TakeTurnFor(game, player2, 2, 1);
            TakeTurnFor(game, player1, 2, 3);
            TakeTurnFor(game, player2, 2, 2);
            TakeTurnFor(game, player1, 3, 1);
            TakeTurnFor(game, player2, 3, 3);
            TakeTurnFor(game, player1, 3, 2);

            TakeTurnFor(game, player2, 3, 2);

            Assert.IsNull(game.Winner);
        }

        private void TakeTurnFor(Game game, Player player, int x, int y)
        {
            game.TakeTurn(player, new BoardSpace(x, y));
        }
    }
}
