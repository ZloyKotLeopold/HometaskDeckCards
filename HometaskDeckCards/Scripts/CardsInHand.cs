using System;
using System.Collections.Generic;
namespace HometaskDeckCards.Scripts
{
    public class CardsInHand
    {
        private List<Card> _playerCards;
        private DeckFactory _deck;
        private List<Card> _allCards;

        public CardsInHand()
        {
            _playerCards = new List<Card>();
            _deck = new DeckFactory();
            _allCards = _deck.GetShuffleDeck();
        }

        public List<Card> GetCardsInHand(int playerCardsCount)
        {
            _playerCards.Clear();

            Random random = new Random();

            for (int i = 0; i < playerCardsCount; i++)
            {
                int randomCardIndex = random.Next(_allCards.Count);

                Card card = _allCards[randomCardIndex];

                _playerCards.Add(card);

                _allCards.RemoveAt(randomCardIndex);
            }

            return _playerCards;
        }
    }
}