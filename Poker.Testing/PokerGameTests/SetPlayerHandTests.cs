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
  public class SetPlayerHandTests: TestBase
  {
    [Test]
    public void ShouldReturnFlushIfHandIsSameSuit()
    {
      var hand = new PokerHand()
      {
        WinningType = HandWinningType.HighCard,
        Cards = Builder<PokerCard>.CreateListOfSize(5)
          .All()
          .With(x => x.Suit = PokerCardSuit.Club).Build()
      };

      var winningHand = _pokerGame.SetPlayerHandWinningData(hand);

      Assert.IsTrue(winningHand.WinningType == HandWinningType.Flush);
    }

    [Test]
    public void ShouldReturnThreeOfAKindIfHandHasThreeSameValue()
    {
      var hand = new PokerHand()
      {
        WinningType = HandWinningType.HighCard,
        Cards = Builder<PokerCard>.CreateListOfSize(5)
          .TheFirst(3)
            .With(x => x.Value = PokerCardValue.Ace)
          .TheNext(2)
            .With(x => x.Value = PokerCardValue.King).Build()
      };

      var winningHand = _pokerGame.SetPlayerHandWinningData(hand);

      Assert.IsTrue(winningHand.WinningType == HandWinningType.ThreeOfAKind);
    }

    [Test]
    public void ShouldReturnPairIfHandHasTwoOfSameValue()
    {
      var hand = new PokerHand()
      {
        WinningType = HandWinningType.HighCard,
        Cards = Builder<PokerCard>.CreateListOfSize(5)
          .TheFirst(2)
            .With(x => x.Value = PokerCardValue.Ace)
          .TheNext(1)
            .With(x => x.Value = PokerCardValue.King)
          .TheNext(1)
            .With(x => x.Value = PokerCardValue.Queen)
          .TheNext(1)
            .With(x => x.Value = PokerCardValue.Jack)
            .Build()
      };

      var winningHand = _pokerGame.SetPlayerHandWinningData(hand);

      Assert.IsTrue(winningHand.WinningType == HandWinningType.OnePair);
    }

    [Test]
    public void ShouldReturnHighCardIfHandHasAllDifferentCards()
    {
      var hand = new PokerHand()
      {
        //set wining other than high card to demonstrate it actually changes
        WinningType = HandWinningType.Flush,
        Cards = Builder<PokerCard>.CreateListOfSize(5)
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
            .Build()
      };

      var winningHand = _pokerGame.SetPlayerHandWinningData(hand);

      Assert.IsTrue(winningHand.WinningType == HandWinningType.HighCard);
    }

  }
}
