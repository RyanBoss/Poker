using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Testing
{
  [TestFixture]
  public class TestBase
  {
    protected IPokerGame _pokerGame;
    [OneTimeSetUp]
    public void Init()
    {
      _pokerGame = new PokerGame();
    }

  }
}
