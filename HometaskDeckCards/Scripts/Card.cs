namespace HometaskDeckCards.Scripts
{
    public class Card
    {
        public string _suit {  get; private set; }
        public string _rank {  get; private set; }

        public Card(string suit, string rank)
        {
            _suit = suit;
            _rank = rank;
        }
    }
}
