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
            
            foreach (var card in cardsInHand.GetCardsInHand(1))
            {
                Console.WriteLine($"{card._rank} {card._suit}");
            }
        }
    }
}
