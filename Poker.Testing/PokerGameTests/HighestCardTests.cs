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
  public class HighestCardTests : TestBase
  {
    [Test]
    public void ShouldReturnHighCardIfLastCard()
    {
      var testHand = Builder<PokerCard>.CreateListOfSize(4)
        .All()
          .With(x => x.Value = PokerCardValue.King)
        .Build()
        .ToList();
      var testHighCard = Builder<PokerCard>.CreateNew()
        .With(x => x.Value = PokerCardValue.Ace)
        .Build();
      testHand.Add(testHighCard);

      var highCard = _pokerGame.FindHighestCard(testHand);

      Assert.IsTrue(highCard.Value == testHighCard.Value);
      Assert.IsTrue(highCard.Suit == testHighCard.Suit);
    }

    [Test]
    public void ShouldReturnHighCardIfFirstCard()
    {
      var testHand = Builder<PokerCard>.CreateListOfSize(1)
        .All()
          .With(x => x.Value = PokerCardValue.Ace)
        .Build()
        .ToList();
      var testHighCard = testHand.First();
      testHand.AddRange(Builder<PokerCard>.CreateListOfSize(4)
        .All()
          .With(x => x.Value != PokerCardValue.Ace)
        .Build());

      var highCard = _pokerGame.FindHighestCard(testHand);

      Assert.IsTrue(highCard.Value == testHighCard.Value);
      Assert.IsTrue(highCard.Suit == testHighCard.Suit);
    }
  }
}
