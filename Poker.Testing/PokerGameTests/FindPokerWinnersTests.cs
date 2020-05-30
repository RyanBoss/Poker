using FizzWare.NBuilder;
using NUnit.Framework;
using Poker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Testing.PokerGameTests
{
  public class FindPokerWinnersTests : TestBase
  {
    [Test]
    public void JoeShouldWinExample1()
    {
      var joeHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Three
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Six
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Eight
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Jack
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.King
        },
      };
      var jenHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Three
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Diamond,
          Value = PokerCardValue.Three
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Spade,
          Value = PokerCardValue.Three
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Eight
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Diamond,
          Value = PokerCardValue.Ten
        },
      };
      var bobHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Two
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Five
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Spade,
          Value = PokerCardValue.Seven
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Ten
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Ace
        },
      };
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = joeHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = jenHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = bobHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");

    }

    [Test]
    public void JenShouldWinExample2()
    {
      var joeHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Three
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Diamond,
          Value = PokerCardValue.Four
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Nine
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Diamond,
          Value = PokerCardValue.Nine
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Queen
        },
      };
      var jenHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Five
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Diamond,
          Value = PokerCardValue.Seven
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Nine
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Spade,
          Value = PokerCardValue.Nine
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Spade,
          Value = PokerCardValue.Queen
        },
      };
      var bobHand = new List<PokerCard>
      {
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Two
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Two
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Spade,
          Value = PokerCardValue.Five
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Club,
          Value = PokerCardValue.Ten
        },
        new PokerCard()
        {
          Suit = PokerCardSuit.Heart,
          Value = PokerCardValue.Ace
        },
      };
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = joeHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = jenHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = bobHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Jen");
    }

    [Test]
    public void FlushShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Suit = PokerCardSuit.Heart)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }


    [Test]
    public void FlushWithHighestShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Suit = PokerCardSuit.Heart)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Suit = PokerCardSuit.Heart)
        .All()
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }


    [Test]
    public void ThreeOfAKindShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void ThreeOfAKindWithHighestValueShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.Queen)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void ThreeOfAKindWithHighestKickerShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.Queen)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void PairShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void PairHighestValueShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void PairHighestKickerShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(2)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void HighestValueShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Nine)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }

    [Test]
    public void HighestValueKickerShouldWin()
    {
      var winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Nine)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ten)
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .TheFirst(1)
          .With(x => x.Name = "Joe")
          .With(x => x.Hand = new PokerHand() { Cards = winningHand })
        .TheNext(1)
          .With(x => x.Name = "Jen")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .TheNext(1)
          .With(x => x.Name = "Bob")
          .With(x => x.Hand = new PokerHand() { Cards = losingHand })
        .Build();

      var winners = _pokerGame.FindPokerWinners(players).ToList();

      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(winners[0].Name == "Joe");
    }
  }
}
