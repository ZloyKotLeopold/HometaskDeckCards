using System;
using System.Collections.Generic;

namespace HometaskDeckCards
{
    internal class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Run();
        }
    }

    public class Card
    {
        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public string Suit { get; private set; }
        public string Rank { get; private set; }
    }

    public class Player
    {
        private List<Card> _cards;

        public Player()
        {
            _cards = new List<Card>();
        }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            Name = name;
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

    public class Game
    {
        private DeckFactory _deckFactory;
        private HandlerInput _handlerInput;
        private Player _player;
        private Deck _deck;

        public Game()
        {
            _handlerInput = new HandlerInput();
            _deckFactory = new DeckFactory();
            _deck = new Deck((List<Card>)_deckFactory.GetNewDeck());
        }

        private enum MenuOptions
        {
            CreateCard = 1,
            Exit = 2
        }

        int cards;

        public void Run()
        {
            _player = new Player();

            int input;
            int lengthMenuItems = Enum.GetValues(typeof(MenuOptions)).Length;
            bool isInput;
            bool isExit = true;
            string name;

            name = _handlerInput.GetName();

            _player.SetName(name);

            while (isExit)
            {
                Console.Clear();

                Console.WriteLine($"Чтобы игроку дать карт нажмите - {(int)MenuOptions.CreateCard}.\nДля выхода из игры нажмите - {(int)MenuOptions.Exit}.");

                uint.TryParse(Console.ReadLine(), out uint tempInput);
                input = (int)tempInput;

                isInput = Convert.ToBoolean(input);

                if (input <= lengthMenuItems && isInput)
                {
                    if (input == (int)MenuOptions.CreateCard)
                    {
                        if (Convert.ToBoolean(cards))
                        {
                            Console.WriteLine("Сколько еще дать карт?");

                            cards += _handlerInput.GetCountCards();

                            _player.AddCards(_deck.GetCards(cards));
                        }
                        else
                        {
                            Console.WriteLine("Ведите количество карт которое вы хотите дать игроку.");

                            _player.AddCards(_deck.GetCards(_handlerInput.GetCountCards()));
                        }
                    }
                    else if (input == (int)MenuOptions.Exit)
                    {
                        isExit = false;
                    }
                }
                else
                {
                    Console.WriteLine("Неверное значение.");
                }

                Console.WriteLine($"Игрок: {_player.Name}\n");

                _player.ShowCards();

                Console.ReadLine();
            }
        }
    }

    public class HandlerInput
    {
        public string GetName()
        {
            const int MinCountName = 2;

            string playerName;

            do
            {
                Console.Write("Введите имя игрока: ");

                playerName = Console.ReadLine();

                if (playerName.Length < MinCountName)
                    Console.WriteLine($"Имя игрока не может быть пустым и должно быть длиннее: {MinCountName}");
            }
            while (playerName.Length < MinCountName);

            return playerName;
        }

        public int GetCountCards()
        {
            return uint.TryParse(Console.ReadLine(), out uint tempPlayerCountCards) ? (int)tempPlayerCountCards : 0;
        }
    }

    public class DeckFactory
    {
        public IReadOnlyList<Card> GetNewDeck() => ShuffleDeck();

        private List<Card> ShuffleDeck()
        {
            Random random = new Random();

            List<Card> shuffledDeck = CreateCards();

            int deckCount = shuffledDeck.Count;

            while (deckCount > 1)
            {
                deckCount--;

                int randomCardIndex = random.Next(deckCount + 1);

                (shuffledDeck[deckCount], shuffledDeck[randomCardIndex]) = (shuffledDeck[randomCardIndex], shuffledDeck[deckCount]);
            }

            return shuffledDeck;
        }

        private List<Card> CreateCards()
        {
            List<Card> deck = new List<Card>();

            string[] suits = { "Черви", "Бубны", "Трефы", "Пики" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (var suit in suits)
                foreach (var rank in ranks)
                    deck.Add(new Card(suit, rank));

            return deck;
        }
    }

    public class Deck
    {
        private List<Card> _deck;

        public Deck(List<Card> deck)
        {
            _deck = deck;
        }

        public IReadOnlyList<Card> GetCards(int playerCardsCount)
        {
            List<Card> playerCards = new List<Card>();

            int firstCard = 0;

            if (_deck.Count <= playerCardsCount)
            {
                Console.WriteLine($"Карты в колоде закончились, сданы последние {_deck.Count} карт.\n");

                return _deck;
            }

            for (int index = 0; index < playerCardsCount; index++)
            {
                Card card = _deck[firstCard];

                playerCards.Add(card);

                _deck.RemoveAt(firstCard);
            }

            if (playerCards != null)
            {
                Console.WriteLine($"Сданы {playerCards.Count} карт, в колоде осталось {_deck.Count} карт.\n\n");
            }

            return playerCards;
        }
    }
}
