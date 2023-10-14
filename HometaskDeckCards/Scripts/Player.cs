using System.Collections.Generic;

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

        public void AddCards(IReadOnlyList<Card> cards)
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
