using HometaskDeckCards.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HometaskDeckCards
{
    internal class Program
    {
        static void Main()
        {
            CardsInHand cardsInHand = new CardsInHand();

            Player player = new Player("Jhon");

            player.AddCards(cardsInHand.GetCardsInHand(12));

            Console.WriteLine($"Игрок: {player.Name}\n");

            foreach (var card in player.ReadOnlyPlayerCards)
            {
                Console.WriteLine($"{card._rank} {card._suit}");
            }

        }
    }
}
