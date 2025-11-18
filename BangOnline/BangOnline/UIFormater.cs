using System;
using System.Collections.Generic;
using System.Linq;

namespace BangOnline
{
    public class UIFormater
    {

        public UIFormater()
        {
        }

        string divider = "------------------------------";
        PlayerInfo emptySeat;

        GameMaterial material = new GameMaterial();
       

        private void SetUpEmptySeat() {
            emptySeat = new PlayerInfo();
            emptySeat.Name = "Empty Seat";
            emptySeat.Lives = 0;
            emptySeat.Role = "";
            emptySeat.ChosenCharacter = "";
            emptySeat.CardsInHand = new List<string>();
            emptySeat.CardsOnTable = new List<string>();
        }

        public void RenderRequiredMessurments() {
            Console.WriteLine("Entire cross shoudld fin int your screen. Otherwise game won't render correctly.");
            for (int i = 0; i < 50; i++)
            {
                if (i == 25)
                {

                    Console.WriteLine(new string('-', 180));
                }
                else {
                    Console.WriteLine(new string(' ', 90)+"|");
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void RenderAll(GameState gs, int myID) {
            if (gs.Players.Count < 7) {
                SetUpEmptySeat();
            }
            
            switch (gs.Players.Count)
            {
                case 4:
                    RenderTable(gs, new List<PlayerInfo> { emptySeat, gs.Players[(myID + 2) % 4 ], gs.Players[(myID + 3) % 4], emptySeat, emptySeat, gs.Players[(myID + 1) % 4] });
                    break;
                case 5:
                    RenderTable(gs, new List<PlayerInfo> { gs.Players[(myID + 3) % 5], gs.Players[(myID + 2) % 5], gs.Players[(myID + 4) % 5], gs.Players[(myID + 1) % 5], emptySeat, emptySeat });
                    break;
                case 6:
                    RenderTable(gs, new List<PlayerInfo> { gs.Players[(myID + 3) % 6], gs.Players[(myID + 2) % 6], gs.Players[(myID + 4) % 6], gs.Players[(myID + 1) % 6], gs.Players[(myID + 5) % 6], emptySeat });
                    break;
                case 7:
                    RenderTable(gs, new List<PlayerInfo> { gs.Players[(myID + 4) % 7], gs.Players[(myID + 3) % 7], gs.Players[(myID + 5) % 7], gs.Players[(myID + 2) % 7], gs.Players[(myID + 6) % 7], gs.Players[(myID + 1) % 7] });
                    break;
                default:
                    break;
            }

            RenderMyInfo(gs.Players[myID]);



        }

        private void RenderTable(GameState gs, List<PlayerInfo> playersInfo) {
            string lastCard = (gs.DeckOfCards.UsedCards.Count == 0) ? "None" : gs.DeckOfCards.UsedCards[gs.DeckOfCards.UsedCards.Count - 1];
            RenderRowOfPlayers((playersInfo[0], playersInfo[1]), (50, 20));
            Console.WriteLine();
            Console.WriteLine();
            RenderRowOfPlayers((playersInfo[2], playersInfo[3]), (10, 100));
            Console.WriteLine();
            RenderDiscardPile(lastCard);
            Console.WriteLine();
            RenderLastEvent(gs);
            Console.WriteLine();
            RenderRowOfPlayers((playersInfo[4], playersInfo[5]), (10, 100));

        }

        private void RenderRowOfPlayers((PlayerInfo, PlayerInfo) playerInfo, (int, int) spacing) {
            RenderBothNamesAndLives(playerInfo, spacing);
            RenderDivider(spacing);
            RenderCharacters(playerInfo, spacing);
            RenderDivider(spacing);
            RenderCardsInHand(playerInfo,spacing);
            RenderCardsOnTable(playerInfo, spacing);
        }
        
        private void RenderLives(int Lives) {
            for (int i = 0; i < Lives; i++)
            {
                Console.Write(" 0");
            }
        }

        private void RenderNameAndLives(string name, int Lives, bool isSheriff, bool isAlive, string role) {

            if(isSheriff) Console.Write("* ");
            Console.Write(name);
            int spacer;
            if (isAlive)
            {
                RenderLives(Lives);
                spacer = 30 - name.Length - (Lives * 2) - ((isSheriff) ? 2 : 0);
                
            }
            else {
                RenderRole(role);
                spacer = 30 - name.Length - role.Length;
            }

            RenderSpaces(spacer);


        }

        private void RenderRole(string role) {
            Console.Write(role);
        }
        
        private void RenderBothNamesAndLives((PlayerInfo,PlayerInfo) playersInfo, (int,int) spacing) {
            RenderSpaces(spacing.Item1);
            RenderNameAndLives(playersInfo.Item1.Name, playersInfo.Item1.Lives, playersInfo.Item1.Role == "Sheriff", playersInfo.Item1.IsAlive, playersInfo.Item1.Role);
            RenderSpaces(spacing.Item2);
            RenderNameAndLives(playersInfo.Item2.Name, playersInfo.Item2.Lives, playersInfo.Item2.Role == "Sheriff", playersInfo.Item2.IsAlive, playersInfo.Item2.Role);
            RenderSpaces(spacing.Item1);
            Console.WriteLine("");
        }

        private void RenderDivider((int, int) spacing) {
            RenderSpaces(spacing.Item1);
            Console.Write(divider);
            RenderSpaces(spacing.Item2);
            Console.Write(divider);
            RenderSpaces(spacing.Item1);
            Console.WriteLine("");
        }

        private void RenderDivider() {
            Console.WriteLine(new string('=', 180));
        }

        private void RenderCharacters((PlayerInfo, PlayerInfo) playersInfo, (int, int) spacing) {

            (List<string>, List<string>) characterDescriptions = AlignRows(
                                                                 StringDivider(material.Characters[playersInfo.Item1.ChosenCharacter].Ability,30),
                                                                 StringDivider(material.Characters[playersInfo.Item2.ChosenCharacter].Ability,30)
                                                                 );



            for (int i = 0; i < characterDescriptions.Item1.Count; i++)
            {
                RenderSpaces(spacing.Item1);
                Console.Write(characterDescriptions.Item1[i]);
                int spacer = 30 - characterDescriptions.Item1[i].Length;
                RenderSpaces(spacing.Item2 + spacer );
                Console.Write(characterDescriptions.Item2[i]);
                spacer = 30 - characterDescriptions.Item2[i].Length;
                RenderSpaces(spacing.Item1 + spacer);
                Console.WriteLine("");
            }

            
        }

        private void RenderCardsInHand((PlayerInfo, PlayerInfo) playersInfo, (int, int) spacing) {
            string text = "Cards in hand: ";
            RenderSpaces(spacing.Item1);
            Console.Write(text + playersInfo.Item1.CardsInHand.Count);
            int spacer = 30 - text.Length - ((playersInfo.Item1.CardsInHand.Count > 9) ? 2 : 1);
            RenderSpaces(spacing.Item2 + spacer);
            Console.Write(text + playersInfo.Item2.CardsInHand.Count);
            spacer = 30 - text.Length - ((playersInfo.Item2.CardsInHand.Count > 9) ? 2 : 1);
            RenderSpaces(spacing.Item1 + spacer);
            Console.WriteLine("");
        }

        private void RenderCardsOnTable((PlayerInfo, PlayerInfo) playersInfo, (int, int) spacing) {
            (List<string>, List<string>) cards = AlignRows(
                                                                 StringDivider(string.Join(" ", playersInfo.Item1.CardsOnTable),30),
                                                                 StringDivider(string.Join(" ", playersInfo.Item2.CardsOnTable),30)
                                                                 );
            for (int i = 0; i < cards.Item1.Count; i++)
            {
                RenderSpaces(spacing.Item1);
                Console.Write(cards.Item1[i]);
                int spacer = 30 - cards.Item1[i].Length;
                RenderSpaces(spacing.Item2 + spacer);
                Console.Write(cards.Item2[i]);
                spacer = 30 - cards.Item2[i].Length;
                RenderSpaces(spacing.Item1 + spacer);
                Console.WriteLine("");
            }


        }

        private void RenderDiscardPile(string lastCard)
        {
            RenderSpaces(90 - (lastCard.Length / 2));
            Console.WriteLine(lastCard);
        }

        private void RenderLastEvent(GameState gs) {
            string playedOn = (gs.LastEvent.PlayedOn == -1) ? "" : "=> "+gs.Players[gs.LastEvent.PlayedOn].Name;
            string text = $"{gs.Players[gs.LastEvent.PlayedBy].Name} => {gs.LastEvent.Event} {playedOn}";
            RenderSpaces(90 - (text.Length / 2));
            Console.WriteLine(text);
        }

        private void RenderSpaces(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.Write(" ");
            }

        }

        private (List<string>, List<string>) AlignRows(List<string> leftPlayer, List<string> rightPlayer) {
            string space = new string(' ', 30);
            if (leftPlayer.Count < rightPlayer.Count)
            {
                while (leftPlayer.Count != rightPlayer.Count)
                    leftPlayer.Add(space);
            }
            else
            {
                while (leftPlayer.Count != rightPlayer.Count)
                    rightPlayer.Add(space);
            }
            return (leftPlayer, rightPlayer);

        }

        private List<string> StringDivider(string stringToDivide, int rowLenght) {
            List<string> words = stringToDivide.Split(' ').ToList();
            List<string> rows = new List<string>();
            int wordIndex = 0;
            while (wordIndex < words.Count)
            {
                string row = "";
                while (wordIndex < words.Count && row.Length + words[wordIndex].Length < rowLenght)
                {
                    row += words[wordIndex] + " ";
                    wordIndex++;
                }
                rows.Add(row);
            }
            
            return rows;

        }

        private void RenderMyInfo(PlayerInfo myInfo) {
            Console.WriteLine();
            RenderMyCardsOnTable(myInfo.CardsOnTable);
            Console.WriteLine();
            Console.WriteLine();
            RenderDivider();
            Console.WriteLine();
            RenderMyName(myInfo.Name, myInfo.ChosenCharacter == "Sheriff");
            RenderMyLives(myInfo.Lives);
            RenderMyCardsInHand(myInfo);
            Console.WriteLine();
            RenderMyCharacter(myInfo.ChosenCharacter);
            Console.WriteLine();
            RenderMyRole(myInfo.Role);
            Console.WriteLine();



        }

        private void RenderMyName(string myName, bool isSheriff) {
            Console.Write((isSheriff ? "*" : "") + myName);
        }

        private void RenderMyLives(int myLives) {
            RenderLives(myLives);
        }

        private void RenderMyCardsInHand(PlayerInfo myInfo) {
            string cards = string.Join(" ", myInfo.CardsInHand);
            RenderSpaces(90 - myInfo.Name.Length - (myInfo.Lives*2) - (cards.Length/2));
            Console.WriteLine(cards);
        }

        private void RenderMyCardsOnTable(List<string> myCards)
        {
            string cards = string.Join(" ", myCards);
            RenderSpaces(90 - (cards.Length / 2));
            Console.WriteLine(cards);
        }

        private void RenderMyCharacter(string myCharacter) {
            List<string> rows = StringDivider(material.Characters[myCharacter].Ability, 80);
            foreach (string row in rows)
            {
                Console.WriteLine(row);
            }
        }

        private void RenderMyRole(string myRole) {
            Console.WriteLine($"Role: {myRole} ({material.Roles[myRole].Mission})");
        }

        public void RenderResults(GameState gs, string winner) {
            Console.Clear();
            Console.WriteLine($"The winner is/are {winner}");
            Console.WriteLine();
            foreach (PlayerInfo player in gs.Players.Values)
            {
                Console.WriteLine($"{player.Name} was {player.Role}");
            }
            Console.WriteLine();
            Console.WriteLine("Thaks for playing!");

        }

     }
}
