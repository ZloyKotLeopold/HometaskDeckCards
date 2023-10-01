using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HometaskDeckCards.Scripts
{
    public class CardsInHand
    {

        private List<Card> GetCards()
        {
            DeckFactory deck = new DeckFactory();

            foreach (var card in deck.ReadOnlyShuffleDeck)            
                Console.WriteLine($"{card._rank} {card._suit}\n");           

            return GetCards();
        }
    }
}
