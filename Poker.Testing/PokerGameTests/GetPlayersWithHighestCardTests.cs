using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Poker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Testing.PokerGameTests
{
  public class GetPlayersWithHighestCardTests : TestBase
  {
    private IEnumerable<PokerCard> _winningHand;
    private IEnumerable<PokerCard> _losingHand;

    public GetPlayersWithHighestCardTests()
    {
      _winningHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Value = PokerCardValue.Ace)
        .Build();
      _losingHand = Builder<PokerCard>.CreateListOfSize(5)
        .All()
          .With(x => x.Value = PokerCardValue.King)
        .Build();
    }
    [Test]
    public void ShouldReturnPlayerWithHighCard()
    {
      var testPlayers = Builder<PokerPlayer>.CreateListOfSize(4)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHand })
        .Build()
        .ToList();
      var testWinner = Builder<PokerPlayer>.CreateNew()
        .With(x => x.Hand = new PokerHand() { Cards = _winningHand })
        .Build();
      testPlayers.Add(testWinner);

      var winners = _pokerGame.GetPlayersWithHighestCard(testPlayers);

      Assert.IsTrue(winners.First().Id == testWinner.Id);
    }

    [Test]
    public void ShouldReturnManyPlayersWithHighCard()
    {
      var testPlayers = Builder<PokerPlayer>.CreateListOfSize(2)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _losingHand })
        .Build()
        .ToList();
      var testWinners = Builder<PokerPlayer>.CreateListOfSize(3)
        .All()
          .With(x => x.Hand = new PokerHand() { Cards = _winningHand })
        .Build()
        .ToList();

      testPlayers.AddRange(testWinners);

      var winners = _pokerGame.GetPlayersWithHighestCard(testPlayers).ToList();

      Assert.IsTrue(winners.Count() == testWinners.Count());
      Assert.IsTrue(winners[0].Id == testWinners[0].Id);
      Assert.IsTrue(winners[1].Id == testWinners[1].Id);
      Assert.IsTrue(winners[2].Id == testWinners[2].Id);
    }
  }
}
