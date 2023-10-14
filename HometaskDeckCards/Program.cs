using System;
using System.Collections.Generic;

namespace HometaskDeckCards
{
    internal class Program
    {
        static void Main()
        {
            UserInput userInput = new UserInput();
            CardsInHand cardsInHand = new CardsInHand();

            while (!userInput.IsExit)
            {
                userInput.Read();

                Player player = new Player(userInput.PlayerName);

                player.AddCards(cardsInHand.GetCards(userInput.Cards));

                Console.WriteLine($"Игрок: {player.Name}\n");

                foreach (var card in player.ReadOnlyCards)
                    Console.WriteLine($"{card.Rank} {card.Suit}");

                Console.WriteLine("Для продолжения нажмите любую кнопку.");

                Console.ReadLine();
            }
        }
    }

    public class Card
    {
        public string Suit { get; private set; }
        public string Rank { get; private set; }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public IReadOnlyList<Card> ReadOnlyCards { get; private set; }
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

            ReadOnlyCards = _cards;
        }
    }

    public class UserInput
    {
        const int MinCountName = 2;
        const string MassengInputName = "Введите имя игрока:";
        const string MassengErrorInputName = "Имя игрока не может быть пустым и должно быть длиннее: ";
        const string MassengInputCountCard = "Ведите количество карт которое вы хотите дать игроку.";
        const string MassengInputExit = "Введие 1 для выхода, 0 чтоб продолжить.";
        const string MassengErrorInputCountCard = "Либо вы не указали сколько карт дать игроку, либо ввели неверное значение, количество карт нудно вводить цифрами и оно не может быть отрицательным.";
        const string MassengErrorInputExit = "Вы неворно ввели команду на выход.";

        public string PlayerName { get; private set; }
        public int Cards { get; private set; }
        public bool IsExit { get; private set; }

        public void Read()
        {
            Console.Clear();

            do
            {
                Console.WriteLine(MassengInputName);
                PlayerName = Console.ReadLine();

                if (PlayerName.Length < MinCountName)
                    Console.WriteLine($"{MassengErrorInputName}{MinCountName}");
            }
            while (PlayerName.Length < MinCountName);

            Console.WriteLine(MassengInputCountCard);

            Cards = uint.TryParse(Console.ReadLine(), out uint tempPlayerCountCards) ? (int)tempPlayerCountCards : 0;

            if (Cards == 0)
                Console.WriteLine(MassengErrorInputCountCard);

            Console.WriteLine(MassengInputExit);

            try
            {
                IsExit = int.Parse(Console.ReadLine()) != 0;
            }
            catch
            {
                Console.WriteLine(MassengErrorInputExit);
            }
        }
    }

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

        public IReadOnlyList<Card> GetShuffleDeck()
        {
            Random random = new Random();

            List<Card> shuffledDeck = new List<Card>(_deck);

            int deckCount = shuffledDeck.Count;

            while (deckCount > 1)
            {
                deckCount--;

                int randomCardIndex = random.Next(deckCount + 1);

                (shuffledDeck[deckCount], shuffledDeck[randomCardIndex]) = (shuffledDeck[randomCardIndex], shuffledDeck[deckCount]);
            }

            return shuffledDeck;
        }
    }

    public class CardsInHand
    {
        private DeckFactory _deck;
        private List<Card> _allCards;
        private List<Card> _playerCards;

        public CardsInHand()
        {
            _deck = new DeckFactory();
            _playerCards = new List<Card>();
            _allCards = (List<Card>)_deck.GetShuffleDeck();
        }

        public IReadOnlyList<Card> GetCards(int playerCardsCount)
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

            if (_playerCards != null)
            {
                Console.WriteLine($"Сданы {_playerCards.Count} карт, в колоде осталось {_allCards.Count} карт.\n\n");
            }

            return _playerCards;
        }
    }
}
