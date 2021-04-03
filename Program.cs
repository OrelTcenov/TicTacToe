using System;
using System.Linq;

namespace TicTacToe_C_
{
    class XOBoard
    {
        static string [,] boardChoice;
        static string status;
        static int player = 1;
        static int choice,boardSize,flag=0;
        static int rowLength;
        static int colLength;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe game let's play!");

            Console.WriteLine("Choose board size:");
            while(true){
                try{
                    boardSize = int.Parse(Console.ReadLine()); 
                    if(boardSize > 2){//Check if the chosen number greater than 2
                       break;
                    }
                    else{
                        throw new Exception("Please choose a number greater than 2");
                    }
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                }
                        
            }

            boardChoice = new string[boardSize,boardSize];

            int init = 1;
            rowLength = boardChoice.GetLength(0);
            colLength = boardChoice.GetLength(1);

            //initialize board
            for(int i=0;i<rowLength;i++){
                for(int j=0;j<colLength;j++){
                    boardChoice[i,j] = init.ToString();
                    init++;
                }
            }

            Game();//Start the game
            
            Console.Clear();//Clear Screen

            Display();//Display Board
            
            if(status == "X"){//Player 1 (X) won
                Console.WriteLine("Player 1 (X) has won");
            }
            else if(status == "O"){//Player 2 (O) won
                Console.WriteLine("Player 2 (O) has won");
            }
            else{//Draw
                Console.WriteLine("Draw");
            }
            
            Console.ReadLine();
        }

        public static void Game(){
            do{
                Console.Clear();
                Console.WriteLine("Player 1 is X and Player 2 is O\n");
                
                if(player % 2 == 0){
                    Console.WriteLine("Player 2 turn (O)\n");
                }
                else{
                    Console.WriteLine("Player 1 turn (X)\n");
                } 

                Display();//Display Board
                Console.WriteLine("\n");
                Put();
                
                status = Status();//Check game status

            }while (status == "None");
        }

        public static void Put(){
            Console.WriteLine("Please choose a number: ");
            //Choose a field to put the sign
            while(true){
                try{
                    choice = int.Parse(Console.ReadLine());
                    if(choice >=1 && choice <=boardSize*boardSize){//Check if the chosen number is in the range
                       break;
                    }
                    else{
                        throw new Exception("Please choose a number from the board:");
                    }
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }
            
            for(int i=0;i<rowLength;i++){
                for(int j=0;j<colLength;j++){
                    if(boardChoice[i,j]==choice.ToString()){
                        if(player % 2 == 0){    //Player 2 turn
                            boardChoice[i,j]="O";
                            flag=1;
                            player++;
                        }
                        else{                   ////Player 1 turn
                            boardChoice[i,j]="X";
                            flag=1;
                            player++;
                        }
                    }
                }
            }
            if(flag == 0){
                Console.WriteLine("This place is already marked");
                Console.WriteLine("Please wait, the board is loading...");
                System.Threading.Thread.Sleep(3000);
            }
            flag=0;
        }

        public static string Status(){
            //int flag = 0;
            bool row=false,col=false;
            string[] diagonalL = new string[rowLength];
            string[] diagonalR = new string[rowLength];
            int index = rowLength-1;

            for(int i=0;i<rowLength;i++){
                //Checking rows
                row = checkWinner(Enumerable.Range(0, rowLength)
                .Select(x => boardChoice[i, x])
                .ToArray());

                //Checking Columns    
                col = checkWinner(Enumerable.Range(0, colLength)
                .Select(x => boardChoice[x, i])
                .ToArray());

                if(col || row){
                    break;
                }

                //Diagonal check    
                diagonalL[i] = boardChoice[i,i];
                diagonalR[i] = boardChoice[i,index];
                index--;
            }

            //Diagonal check 
            var diagonal1 = checkWinner(diagonalL);
            var diagonal2 = checkWinner(diagonalR);

            if(row || col || diagonal1 || diagonal2 ){
                if(player % 2 == 0){
                    return "X";
                }
                else{
                    return "O";
                }
            }
            else if((player-1) == boardSize*boardSize){
                return "Draw";
            }
            else{
                return "None";
            }
        }

        //Check if the all values in the array is equal
        public static bool checkWinner(string[] StringArray){
            string firstItem = StringArray[0];
            bool allEqual = StringArray.Skip(1).All(s => string.Equals(firstItem, s, StringComparison.InvariantCultureIgnoreCase));  
            return allEqual;
        }

        //Add space between numbers for presentation
        public static string padding(string num,int chars){
            var charnum = num.Length;
            var dif = (chars - charnum)+2;
            string separator = " ";

            if(dif % 2 == 0){
                return String.Concat(Enumerable.Repeat(separator,dif/2))+num+String.Concat(Enumerable.Repeat(separator,dif/2));
            }
            else{
                return String.Concat(Enumerable.Repeat(separator,(dif-1)/2))+num+String.Concat(Enumerable.Repeat(separator,(dif+1)/2));
            }
        }

        public static void Display(){
            int chars = (boardSize*boardSize).ToString().Length; //Number of chers in the board
            //Create the separator line
            string space = " ";
            string separator = String.Concat(Enumerable.Repeat(space,chars+2))+"|";
            string separatorLine = String.Concat(Enumerable.Repeat(separator,boardSize-1));
            //Create the bottom separator line
            string bottom = "_";
            string bottomL = String.Concat(Enumerable.Repeat(bottom,chars+2))+"|";
            string bottomLine = String.Concat(Enumerable.Repeat(bottomL,boardSize-1))+String.Concat(Enumerable.Repeat(bottom,chars+2));
            
            for (int i = 0; i < rowLength; i++)
            {
                Console.WriteLine("{0}", separatorLine);
                for (int j = 0; j < colLength; j++)
                {
                    if(j==colLength-1){
                        Console.Write("{0}",padding(boardChoice[i,j],chars));
                    }
                    else{
                        Console.Write("{0}|",padding(boardChoice[i,j],chars));
                    }
                }
                Console.WriteLine("");
                if(i!=rowLength-1){
                    Console.WriteLine("{0}", bottomLine);
                }
                else{
                    Console.WriteLine("{0}", separatorLine);
                }
            }
        }
    }
}
