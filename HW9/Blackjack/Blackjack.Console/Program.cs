using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace Blackjack.Console
{
    class Program
    {
        private int _wager;
        public int wager { get { return _wager; } }

        private bool isQuit = false;
        private string wagerInput;
        private string actionInput;
        private string replayInput;

        private bool _isDealerBusted = false;
        public bool isDealerBusted { get { return _isDealerBusted; } }

        private bool _isPlayerBusted = false;
        public bool isPlayerBusted { get { return _isPlayerBusted; } }

        private int _winnings;
        public int winnings { get { return _winnings; } }

        private int _playerCardTotal = 0;
        public int playerCardTotal { get { return _playerCardTotal; } }

        private int _dealerCardTotal = 0;
        public int dealerCardTotal {  get { return _dealerCardTotal; } }

        Random r = new Random();

        public void GetPlayerAction()
        {

            System.Console.WriteLine($"Would you like another card? Y N");
            actionInput = System.Console.ReadLine().ToLower();
            if (actionInput == "y")
            {
                Hit(ref _playerCardTotal);
                System.Console.WriteLine($"Player Total: {_playerCardTotal}");
                if (_playerCardTotal > 21)
                {
                    System.Console.WriteLine("Player Busts");
                    _isPlayerBusted = true;
                    actionInput = "exit";
                }
            }
            else if (actionInput == "n")
            {
                GetDealerAction();
                actionInput = "exit";
            }
            else if (actionInput != "exit")
            {
                throw new Exception($"{actionInput} is not a valid command.");
            }
            else
            {
                isQuit = true;
            }
        }
                 
        public void GetDealerAction()
        {
            while(_dealerCardTotal <= 17)
            {
                Hit(ref _dealerCardTotal);
            }

            if(_dealerCardTotal > 21)
            {
                System.Console.WriteLine("Dealer busts.");
                _isDealerBusted = true;
            }
            else
            {
                System.Console.WriteLine($"Dealer stands at {_dealerCardTotal}");
            }
        }

        public void GetWager()
        {
            System.Console.WriteLine("How much would you like to wager?");
            wagerInput = System.Console.ReadLine().ToLower();
            if (Int32.TryParse(wagerInput, out _wager) == false)
            {
                if (wagerInput != "exit")
                {
                    throw new Exception($"{wagerInput} is not a valid number.");
                }
                else
                {
                    wagerInput = "exit";
                    isQuit = true;
                }
            }
            else
            {
                wagerInput = "exit";
            }
        }

        public void Deal()
        {
            _playerCardTotal += r.Next(1, 10);
            _playerCardTotal += r.Next(1, 10);
            System.Console.WriteLine($"Player Total: {_playerCardTotal}");

            _dealerCardTotal += r.Next(1, 10);
            _dealerCardTotal += r.Next(1, 10);
            System.Console.WriteLine($"Dealer Total: {_dealerCardTotal}");
        }

        public void Hit(ref int CardTotal)
        {
            CardTotal += r.Next(1, 10);
        }

        public void GetResults()
        {          
            if( (_dealerCardTotal >= _playerCardTotal || _isPlayerBusted) && !_isDealerBusted)
            {
                System.Console.WriteLine($"Dealer wins ${wager}");
                _winnings -= wager;
            }
            else
            {
                System.Console.WriteLine($"Player wins ${wager}");
                _winnings += wager;
            }

            System.Console.WriteLine($"Total player winnings = ${winnings}");
            System.Console.WriteLine($"Total dealer winnings = ${winnings * -1}");

            Replay();            
        }
        
        public void Replay()
        {
            System.Console.WriteLine("Play again? Y N");
            replayInput = System.Console.ReadLine().ToLower();
            
            if(replayInput == "y")
            {
                System.Console.WriteLine("Good Luck!");
                _playerCardTotal = 0;
                _dealerCardTotal = 0;
                _isDealerBusted = false;
                _isPlayerBusted = false;
                replayInput = "exit";
                
            }
            else if(replayInput == "n" || replayInput == "exit")
            {
                isQuit = true;
            }
            else
            {
                throw new Exception($"{replayInput} is not a valid command");
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            do
            {
                do
                {
                    try
                    {
                        p.GetWager();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                while ( p.wagerInput != "exit");

                if (!p.isQuit)
                {
                    try
                    {
                        p.Deal();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }

                do
                {
                    if (!p.isQuit)
                    {
                        try
                        {
                            p.GetPlayerAction();
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        p.actionInput = "exit";
                    }

                }
                while (p.actionInput != "exit");

                do
                {
                    if (!p.isQuit)
                    {
                        try
                        {
                            p.GetResults();
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        p.replayInput = "exit";
                    }
                
                }
                while (p.replayInput != "exit");

                if (p.isQuit)
                {
                    System.Console.WriteLine("Thanks for playing!");
                }
            }
            while (!p.isQuit);
        }
    }
}
