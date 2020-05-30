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
  public class GetWinningBasedOnNextHighestTests :TestBase
  {
    private IEnumerable<PokerCard> _winningHand;
    private IEnumerable<PokerCard> _losingHand;

    public GetWinningBasedOnNextHighestTests()
    {
      _winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Two)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Three)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Four)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Five)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Ace)
        .Build();
      _losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(1)
          .With(x => x.Value = PokerCardValue.Three)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Two)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Five)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Five)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .Build();
    }
    [Test]
    public void ShouldFindAllWinnersWithHighestCardOverall()
    {
      var winners = Builder<PokerPlayer>.CreateListOfSize(2)
        .All()
         .With(x => x.Hand = new PokerHand() { Cards = _winningHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(3)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build().ToList();

      players.AddRange(winners);
      var gameWinners = _pokerGame.GetWinningPlayersBasedOnNextHighestCard(players).ToList();

      Assert.IsTrue(gameWinners.Count() == 2);
      Assert.IsTrue(gameWinners[0].Id == winners[0].Id);
      Assert.IsTrue(gameWinners[1].Id == winners[1].Id);

    }


    [Test]
    public void ShouldFindSingleWinnersWithHighestCardOverall()
    {
      var winners = Builder<PokerPlayer>.CreateListOfSize(1)
        .All()
         .With(x => x.Hand = new PokerHand() { Cards = _winningHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build();
      var players = Builder<PokerPlayer>.CreateListOfSize(4)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build().ToList();

      players.AddRange(winners);

      var gameWinners = _pokerGame.GetWinningPlayersBasedOnNextHighestCard(players).ToList();

      Assert.IsTrue(gameWinners.Count() == 1);
      Assert.IsTrue(gameWinners[0].Id == winners[0].Id);
    }
  }
}
