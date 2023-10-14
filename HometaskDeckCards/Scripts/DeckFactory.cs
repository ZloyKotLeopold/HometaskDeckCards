using System;
using System.Collections.Generic;

namespace HometaskDeckCards.Scripts
{
    public class DeckFactory
    {
        private List<Card> _deck;
        private IReadOnlyList<Card> _readOnlyDeck;

        public DeckFactory()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            _deck = new List<Card>();

            string[] suits = { "Черви", "Бубны", "Трефы", "Пики" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (var suit in suits)
                foreach (var rank in ranks)
                    _deck.Add(new Card(suit, rank));
        }

        public IReadOnlyList<Card> GetShuffleDeck()
        {
            Random random = new Random();

            int deckCount = _deck.Count;

            while (deckCount > 1)
            {
                deckCount--;

                int randomCardIndex = random.Next(deckCount + 1);

                Card card = _deck[randomCardIndex];

                _deck[randomCardIndex] = _deck[deckCount];
                _deck[deckCount] = card;
            }

            _readOnlyDeck = (IReadOnlyList<Card>) _deck;

            return _readOnlyDeck;
        }
    }
}
