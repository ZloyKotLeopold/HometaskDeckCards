using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HometaskDeckCards.Scripts
{
    public class Player
    {
        public string Name { get; private set; }
        public IReadOnlyList<Card> ReadOnlyPlayerCards { get; private set; }
        private List<Card> _cards;

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public void AddCards(List<Card> cards)
        {
            foreach (var card in cards) 
            {
                _cards.Add(card);
            }

            ReadOnlyPlayerCards = GetPlayerCards(_cards);
        }

        private IReadOnlyList<Card> GetPlayerCards(List<Card> cards)
        {
            return (IReadOnlyList<Card>)cards;
        }
    }
}
