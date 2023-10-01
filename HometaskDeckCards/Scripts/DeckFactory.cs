﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HometaskDeckCards.Scripts
{
    public class DeckFactory
    {
        private List<Card> _deck;

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

        public List<Card> GetShuffleDeck(List<Card> deck = null)
        {
            if (deck == null)
                deck = _deck;

            Random random = new Random();

            int deckCount = deck.Count;

            while (deckCount > 1)
            {
                deckCount--;

                int randomCardIndex = random.Next(deckCount + 1);

                Card card = deck[randomCardIndex];

                deck[randomCardIndex] = deck[deckCount];
                deck[deckCount] = card;
            }

            return deck;
        }

        public IReadOnlyList<Card> ReadOnlyShuffleDeck => GetShuffleDeck();
    }
}