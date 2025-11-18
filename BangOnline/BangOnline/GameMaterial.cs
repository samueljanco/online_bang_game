using System;
using System.Collections.Generic;
using System.Linq;
namespace BangOnline
{
    public class GameMaterial
    {
        public GameMaterial()
        {
        }

        private Random rnd = new Random();

        

        public Dictionary<string, Character> Characters = new Dictionary<string, Character>
        {
            { "Bart Cassidy", new BartCassidy("Bart Cassidy", "Bart Cassidy: Each time he loses a life point, he immediately draws a card from the deck.",4 )},
            {"Black Jack", new BlackJack("Black Jack", "Black Jack: He must show the second card he draws. If it's Heart or Diamonds, he draws one additional card.", 4) },
            {"Calamity Janet", new CalamityJanet("Calamity Janet", "Calamity Janet: She can use BANG! cards as Missed cards and vice versa.", 4) },
            {"El Gringo", new ElGringo("El Gringo", "El Gringo: Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player.", 3) },
            {"Jesse Jones", new JesseJones("Jesse Jones", "Jesse Jones: He may choose to draw the first card from the deck, or randomly from the hand of any other player. ", 4) },
            {"Jourdonnais", new Jourdonais("Jourdonnais", "Jourdonnais: He can draw when he is the target of a BANG!, and on a Heart he is missed.", 4) },
            {"Kit Carlson", new KitCarlson("Kit Carlson", "Kit Carlson: He looks at the top three cards of the deck. He chooses 2 to draw, and puts the other one back on the top of the deck", 4) },
            {"Lucky Duke", new LuckyDuke("Lucky Duke", "Lucky Duke: Each time he is required to draw, he flips the top two cards from the deck and chooses the result he prefers.", 4) },
            {"Paul Regret", new PaulRegret("Paul Regret", "Paul Regret: He is considered to have a Mustang in play at all times. All other players must add 1 to the distance to him.", 3) },
            {"Pedro Ramirez", new PedroRamirez("Pedro Ramirez", "Pedro Ramirez: He may choose to draw the first card from the top of the discard pile or from the deck.", 4) },
            {"Rose Doolan", new RoseDoolan("Rose Doolan", "Rose Doolan: She is considered to have a Scope in play at all times. She sees the other players at a distance decreased by 1.", 4) },
            {"Sid Ketchum", new SidKetchum("Sid Ketchum", "Sid Ketchum: At any time, he may discard 2 cards from his hand to regain one life point. ", 4) },
            {"Slab the Killer", new SlabTheKiller("Slab the Killer", "Slab the Killer: Players trying to cancel his BANG! cards need to play 2 Missed.", 4) },
            {"Suzy Lafayette", new SuzyLafayette("Suzy Lafayette", "Suzy Lafayette: As soon as she has no cards in her hand, she draws a card from the draw pile.", 4) },
            {"Vulture Sam", new VultureSam("Vulture Sam", "Vulture Sam: Whenever a character is eliminated from the game, Sam takes all the cards that player had.", 4) },
            {"Willy the Kid", new WillyTheKid("Willy the Kid", "Willy the Kid: He can play any number of BANG! cards during his turn.", 4) },
            {"", new Character("", "", 0) }

        };

        public Dictionary<string, Role> Roles = new Dictionary<string, Role>
        {
            {"Sheriff" , new Role("Sheriff", "You must eliminate all the Bandits and the Renegade, to protect law and order.")},
            {"Vice" , new Role("Vice", "You must help and protect the Sheriff")},
            {"Bandit" , new Role("Bandit", "Kill the Sheriff")},
            {"Renegade" , new Role("Renegade", "Your goal is to be the last character in play.")}
        };

        public List<string> BlueCards = new List<string> { "Barrel", "Dynamite", "Scope", "Mustang", "Schofield", "Remington", "Rev_Carabine", "Volcanic", "Winchester" };

        public Dictionary<string, int> GunsRanges = new Dictionary<string, int> {
            {"Schofield", 2},
            {"Remington", 3},
            {"Rev_Carabine",4},
            {"Winchester", 5 }
        };

        public void GetShuffledDeck(GameState gs)
        {
            List<string> Cards = new List<string>{ "QS-Barrel", "KS-Barrel","2H-Dynamite","JS-Jail","4H-Jail","10S-Jail",
                                                      "8H-Mustang","9H-Mustang","KC-Remington","AC-Rev_Carabine","JC-Schofield",
                                                      "QC-Schofield","KS-Schofield","AS-Scope","10S-Volcanic","10C-Volcanic",
                                                      "8S-Winchester","AS-Bang","2D-Bang","3D-Bang","4D-Bang","5D-Bang","6D-Bang",
                                                      "7D-Bang","8D-Bang","9D-Bang","10D-Bang","JD-Bang","QD-Bang","KD-Bang",
                                                      "AD-Bang","2C-Bang","3C-Bang","4C-Bang","5C-Bang","6C-Bang","7C-Bang",
                                                      "8C-Bang","9C-Bang","QH-Bang","KH-Bang","AH-Bang","6H-Beer","7H-Beer",
                                                      "8H-Beer","9H-Beer","10H-Beer","JH-Beer","KH-Cat_Balou","9D-Cat_Balou",
                                                      "10D-Cat_Balou","JD-Cat_Balou","QD-Duel","JS-Duel","8C-Duel","10J-Gatling",
                                                      "9C-Emporio","QS-Emporio","KD-Indians","AD-Indians","10C-Missed","JC-Missed",
                                                      "QC-Missed","KC-Missed","AC-Missed","2S-Missed","3S-Missed","4S-Missed",
                                                      "5S-Missed","6S-Missed","7S-Missed","8S-Missed","JH-Panic","QH-Panic",
                                                      "AH-Panic","8D-Panic","5H-Salon","9S-Stagecoach","9S-Stagecoach","3H-Wells_Fargo"};

            gs.DeckOfCards.UnusedCards = Cards.OrderBy(item => rnd.Next()).ToList();
        }

        public void GetRoles(GameState gs)
        {
            List<string> Roles = new List<string>();
            switch (gs.Players.Count)
            {
                case 4:
                    Roles = new List<string> { "Sheriff", "Bandit", "Bandit", "Renegade" };
                    break;
                case 5:
                    Roles = new List<string> { "Sheriff", "Vice", "Bandit", "Bandit", "Renegade" };
                    break;
                case 6:
                    Roles = new List<string> { "Sheriff", "Vice", "Bandit", "Bandit", "Bandit", "Renegade" };
                    break;
                case 7:
                    Roles = new List<string> { "Sheriff", "Vice", "Vice", "Bandit", "Bandit", "Bandit", "Renegade" };
                    break;
                default:
                    break;
            }
            for (int i = 0; i < gs.Players.Count(); i++)
            {
                int index = rnd.Next(Roles.Count);
                gs.Players[i].Role = Roles[index];
                if (Roles[index] == "Sheriff")
                {
                    gs.CurrentPlayerID = i;
                }

                Roles.RemoveAt(index);
            }

        }

        public void GetCharacters(GameState gs)
        {

            List<string> Characters = new List<string> { "Willy the Kid", "Bart Cassidy", "Black Jack", "Calamity Janet", "El Gringo",
                                                            "Jesse Jones", "Jourdonnais", "Kit Carlson", "Lucky Duke","Paul Regret",
                                                            "Pedro Ramirez","Rose Doolan","Sid Ketchum", "Slab the Killer","Suzy Lafayette",
                                                            "Vulture Sam",};

            for (int i = 0; i < gs.Players.Count(); i++)
            {

                gs.Players[i].Characters = new List<string>();
                for (int j = 0; j < 2; j++)
                { // look at
                    int index = rnd.Next(Characters.Count);

                    gs.Players[i].Characters.Add(Characters[index]);
                    Characters.RemoveAt(index);

                }


            }

        }


        public void DistributeCards(GameState gs)
        {

            for (int i = 0; i < gs.Players.Count(); i++)
            {
                for (int j = 0; j < gs.Players[i].Lives; j++)
                {
                    gs.Players[i].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                    gs.DeckOfCards.UnusedCards.RemoveAt(0);
                }
            }

        }



    }
}