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
  public class GetWinnersFromWinningHandValueTests : TestBase
  {
    private IEnumerable<PokerCard> _winningHand;
    private IEnumerable<PokerCard> _losingHand;
    private IEnumerable<PokerCard> _losingHandSameSetAsWinning;

    public GetWinnersFromWinningHandValueTests()
    {
      _winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(2)
          .With(x => x.Value = PokerCardValue.King)
        .Build();
      _losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Jack)
        .Build();
      _losingHandSameSetAsWinning = Builder<PokerCard>.CreateListOfSize(5)
        .TheFirst(3)
          .With(x => x.Value = PokerCardValue.Ace)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.King)
        .TheNext(1)
          .With(x => x.Value = PokerCardValue.Queen)
        .Build();
    }

    [Test]
    public void ShouldFindWinnersWhereWinningHandValueIsHighest()
    {
      var players = Builder<PokerPlayer>.CreateListOfSize(4)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.King })
        .Build().ToList();
      var winners = Builder<PokerPlayer>.CreateListOfSize(1)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _winningHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build();

      players.AddRange(winners);
      var gameWinners = _pokerGame.GetWinnersFromWinningHandInfo(players).ToList();
      Assert.IsTrue(winners.Count() == 1);
      Assert.IsTrue(gameWinners[0].Id == winners[0].Id);
    }

    [Test]
    public void ShouldFindWinnersWhereWinningHandValueIsSameAndHasHighestCard()
    {
      var players = Builder<PokerPlayer>.CreateListOfSize(4)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHandSameSetAsWinning, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build().ToList();
      var winners = Builder<PokerPlayer>.CreateListOfSize(1)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _winningHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build();

      players.AddRange(winners);
      var gameWinners = _pokerGame.GetWinnersFromWinningHandInfo(players).ToList();

      Assert.IsTrue(gameWinners.Count() == 1);
      Assert.IsTrue(gameWinners[0].Id == winners[0].Id);
    }

    [Test]
    public void ShouldFindAllWinnersIfHandsAreSame()
    {

      var players = Builder<PokerPlayer>.CreateListOfSize(5)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _winningHand, WinningType = HandWinningType.ThreeOfAKind, WinningValue = PokerCardValue.Ace })
        .Build();

      var winners = _pokerGame.GetWinnersFromWinningHandInfo(players);

      Assert.IsTrue(winners.Count() == 5);
    }
  }
}
