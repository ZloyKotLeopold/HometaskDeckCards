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
        private int _cards;

        public Game()
        {
            _handlerInput = new HandlerInput();
            _deckFactory = new DeckFactory();
            _deck = new Deck((List<Card>)_deckFactory.GetNewDeck());
        }

        private enum MenuOption
        {
            CreateCard = 1,
            Exit = 2
        }

        public void Run()
        {
            _player = new Player();

            bool isExit = true;
            string name;

            name = _handlerInput.GetName();

            _player.SetName(name);

            while (isExit)
            {
                Console.Clear();

                Console.WriteLine($"Чтобы игроку дать карт нажмите - {(int)MenuOption.CreateCard}.\nДля выхода из игры нажмите - {(int)MenuOption.Exit}.");

                if (uint.TryParse(Console.ReadLine(), out uint userCommand))
                {
                    switch (userCommand)
                    {
                        case (int)MenuOption.CreateCard:
                            CardHandler();
                            break;

                        case (int)MenuOption.Exit:
                            isExit = false;
                            break;

                        default:
                            Console.WriteLine("Неверное значение.");
                            break;
                    }
                }
              
                Console.WriteLine($"Игрок: {_player.Name}\n");

                _player.ShowCards();

                Console.ReadLine();
            }
        }

        public void CardHandler()
        {
            if (Convert.ToBoolean(_cards))
            {
                Console.WriteLine("Сколько еще дать карт?");

                _cards += _handlerInput.GetCountCards();
            }
            else
            {
                Console.WriteLine("Ведите количество карт которое вы хотите дать игроку.");

                _cards = _handlerInput.GetCountCards();
            }

            _player.AddCards(_deck.GetCards(_cards));
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
        private List<Card> _deckCards;

        public Deck(List<Card> deck)
        {
            _deckCards = deck;
        }

        public IReadOnlyList<Card> GetCards(int playerCardsCount)
        {
            List<Card> playerCards = new List<Card>();

            int firstCard = 0;

            if (_deckCards.Count <= playerCardsCount)
            {
                Console.WriteLine($"Карты в колоде закончились, сданы последние {_deckCards.Count} карт.\n");

                playerCards.AddRange(_deckCards);

                _deckCards.Clear();

                return playerCards;
            }

            for (int index = 0; index < playerCardsCount; index++)
            {
                Card card = _deckCards[firstCard];

                playerCards.Add(card);

                _deckCards.RemoveAt(firstCard);
            }

            if (playerCards != null)
            {
                Console.WriteLine($"Сданы {playerCards.Count} карт, в колоде осталось {_deckCards.Count} карт.\n\n");
            }

            return playerCards;
        }
    }
}
