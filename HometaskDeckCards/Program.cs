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
            Deck deck = new Deck();
            foreach (var card in deck.ReadOnlyDeck)
            {
                Console.WriteLine($"{card._rank} {card._suit}\n");
            }
        }
    }
}
