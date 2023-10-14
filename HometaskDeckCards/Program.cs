using HometaskDeckCards.Scripts;
using System;

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
                userInput.ReadUserInput();

                Player player = new Player(userInput.PlayerName);

                player.AddCards(cardsInHand.GetCardsInHand(userInput.PlayerCountCards));

                Console.WriteLine($"Игрок: {player.Name}\n");

                foreach (var card in player.ReadOnlyPlayerCards)
                    Console.WriteLine($"{card._rank} {card._suit}");

                Console.WriteLine("Для продолжения нажмите любую кнопку.");

                Console.ReadLine();
            }
        }
    }
}
