using HometaskDeckCards.Scripts;
using System;

namespace HometaskDeckCards
{
    internal class Program
    {
        static void Main()
        {
            UserInput userInput = new UserInput();
            userInput.ReadUserInput();

            CardsInHand cardsInHand = new CardsInHand();

            Player player = new Player(userInput.PlayerName);

            player.AddCards(cardsInHand.GetCardsInHand(userInput.PlayerCountCards));

            Console.WriteLine($"Игрок: {player.Name}\n");

            foreach (var card in player.ReadOnlyPlayerCards)           
                Console.WriteLine($"{card._rank} {card._suit}");          
        }
    }
}
