using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Data
{
  public class PokerPlayer
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public IEnumerable<PokerCard> Hand { get; set; }

  }
}
