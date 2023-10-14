using System;
using System.Collections.Generic;

namespace HometaskDeckCards.Scripts
{
    public class CardsInHand
    {
        private DeckFactory _deck;
        private List<Card> _allCards;
        private List<Card> _playerCards;
        private IReadOnlyList<Card> _readOnlyCards;

        public CardsInHand()
        {            
            _deck = new DeckFactory();
            _playerCards = new List<Card>();
            _allCards = (List<Card>)_deck.GetShuffleDeck();
        }

        public IReadOnlyList<Card> GetCardsInHand(int playerCardsCount)
        {
            _playerCards.Clear();

            if (_allCards.Count <= playerCardsCount)
            {
                Console.WriteLine($"Карты в колоде закончились, сданы последние {_allCards.Count} карт.\n");

                return _allCards;
            }

            Random random = new Random();

            for (int i = 0; i < playerCardsCount; i++)
            {
                int randomCardIndex = random.Next(_allCards.Count);

                Card card = _allCards[randomCardIndex];

                _playerCards.Add(card);

                _allCards.RemoveAt(randomCardIndex);
            }

            if(_playerCards != null)
            {
                Console.WriteLine($"Сданы {_playerCards.Count} карт, в колоде осталось {_allCards.Count} карт.\n\n");
            }

            _readOnlyCards = (IReadOnlyList<Card>)_playerCards;

            return _readOnlyCards;
        }
    }
}