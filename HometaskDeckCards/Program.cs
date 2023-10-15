using System;
using System.Collections.Generic;

namespace HometaskDeckCards
{
    internal class Program
    {
        static void Main()
        {
            UserInput userInput = new UserInput();
            Croupier cardsInHand = new Croupier();

            userInput.SetName();

            Player player = new Player(userInput.PlayerName);

            while (userInput.IsExit == false)
            {
                userInput.Menu();

                player.AddCards(cardsInHand.GetCards(userInput.Cards));

                Console.WriteLine($"Игрок: {player.Name}\n");

                player.ShowCards();

                Console.WriteLine("\nДля продолжения нажмите любую кнопку.");

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

        private List<Card> _cards;

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public void ShowCards()
        {
            foreach (var card in _cards)
                Console.WriteLine($"{card.Rank} {card.Suit}");
        }

        public void AddCards(IReadOnlyList<Card> cards)
        {
            if (cards != null)
                _cards.AddRange(cards);
        }
    }

    public class UserInput
    {       
        public string PlayerName { get; private set; }
        public int Cards { get; private set; }
        public bool IsExit { get; private set; }

        public void Menu()
        {            
            const int ParameterCriatePlayer = 1;
            const int ParameterExit = 2;
            const int CountMenuItems = 2;

            int input;
            bool isActive = true;

            Console.Clear();

            Console.WriteLine($"Чтобы игроку дать карт нажмите - {ParameterCriatePlayer}.\nДля выхода из игры нажмите - {ParameterExit}.");

            while (isActive)
            {
                input = uint.TryParse(Console.ReadLine(), out uint tempInput) ? (int)tempInput : 0;

                if (input <= CountMenuItems && input != 0)
                {
                    if (input == 1)
                    {
                        if (Cards >= 1)
                        {
                            Console.WriteLine("Сколько еще дать карт?");

                            Cards += SetCountCards();
                        }
                        else
                        {
                            Console.WriteLine("Ведите количество карт которое вы хотите дать игроку.");

                            Cards = SetCountCards();
                        }
                    }

                    if (input == 2) IsExit = true; //Можно ли так писать?

                    isActive = false;
                }
                else
                {
                    Console.WriteLine("Неверное значение.");
                }
            }
        }

        public void SetName()
        {
            const int MinCountName = 2;

            do
            {
                Console.Write("Введите имя игрока: ");

                PlayerName = Console.ReadLine();

                if (PlayerName.Length < MinCountName)
                    Console.WriteLine($"Имя игрока не может быть пустым и должно быть длиннее: {MinCountName}");
            }
            while (PlayerName.Length < MinCountName);
        }

        private int SetCountCards()
        {
            return uint.TryParse(Console.ReadLine(), out uint tempPlayerCountCards) ? (int)tempPlayerCountCards : 0;
        }
    }

    public class DeckFactory
    {
        private List<Card> _deck;

        public DeckFactory()
        {
            InitializeDeck();
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

        private void InitializeDeck()
        {
            _deck = new List<Card>();

            string[] suits = { "Черви", "Бубны", "Трефы", "Пики" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (var suit in suits)
                foreach (var rank in ranks)
                    _deck.Add(new Card(suit, rank));
        }
    }

    public class Croupier
    {
        private DeckFactory _deckFactory;
        private List<Card> _allCards;

        public Croupier()
        {
            _deckFactory = new DeckFactory();
            _allCards = (List<Card>)_deckFactory.GetShuffleDeck();
        }

        public IReadOnlyList<Card> GetCards(int playerCardsCount)
        {
            List<Card> playerCards = new List<Card>();


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

                playerCards.Add(card);

                _allCards.RemoveAt(randomCardIndex);
            }

            if (playerCards != null)
            {
                Console.WriteLine($"Сданы {playerCards.Count} карт, в колоде осталось {_allCards.Count} карт.\n\n");
            }

            return playerCards;
        }
    }
}
