using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversedTicTacToe
{
    class GameEngine
    {
        private Player m_Player1;
        private Player m_Player2;
        private GameBoard m_GameBoard;
        private const int k_NotFound = -1;
        private ePlayerType m_CurrentTurn;
        private int m_totalHumanPlayers;
        private string m_RoundWinner;
        Random m_Random;

        public string RoundWinner
        {
            get
            {
                return m_RoundWinner;
            }
        }

        public GameEngine(int i_boardSize, int i_totalHumanPlayers, ePlayerSign i_player1Sign, ePlayerSign i_player2Sign)
        {
            m_Player1 = new Player(ePlayerType.HumanPlayerA, i_player1Sign);
            ePlayerType player2Type;

            if (i_totalHumanPlayers == 2)
            {
                player2Type = ePlayerType.HumanPlayerB;
            }
            else
            {
                m_Random = new Random();
                player2Type = ePlayerType.Computer;
            }

            m_Player2 = new Player(player2Type, i_player2Sign);
            m_GameBoard = new GameBoard(i_boardSize);
            m_CurrentTurn = ePlayerType.HumanPlayerA;
            m_totalHumanPlayers = i_totalHumanPlayers;
        }

        public Tuple<int,int> GetScore()
        {
            Tuple<int, int> score = new Tuple<int, int>(m_Player1.Score, m_Player2.Score);

            return score;
        }

        public void ResetGame()
        {
            m_GameBoard.cleanBoard();
            CurrentTurn = ePlayerType.HumanPlayerA;
        }

        public void PlayerQuit()
        {
            if(m_CurrentTurn == ePlayerType.HumanPlayerA)
            {
                m_Player2.Score++;
            }
            else
            {
                m_Player1.Score++;
            }
        }

        public Tuple<int, int> PlayTurn()
        {
            Tuple<int, int> computerMove;

            if (m_CurrentTurn == ePlayerType.HumanPlayerA)
            {
                m_GameBoard.SetPlayerSignInBoard(m_Player1.ChosenMove.Item1,m_Player1.ChosenMove.Item2,m_Player1.ChosenSign);
                computerMove = new Tuple<int, int>(-1, -1);
            }
            else 
            {
                if(m_CurrentTurn == ePlayerType.Computer)
                {
                    int randX, randY;

                    do
                    {
                        randX = m_Random.Next(m_GameBoard.TotalRows); 
                        randY = m_Random.Next(m_GameBoard.TotalCols);
                    }
                    while (getBoardValueInCoordinates(randX, randY) != eBoardValue.Empty);

                    computerMove = m_Player2.ChosenMove = new Tuple<int, int>(randX, randY);
                }
                else
                {
                    computerMove = new Tuple<int, int>(-1, -1);
                }

                m_GameBoard.SetPlayerSignInBoard(m_Player2.ChosenMove.Item1,m_Player2.ChosenMove.Item2,m_Player2.ChosenSign);
            }

            return computerMove;
        }

        private int checkRows()
        {
            int result = k_NotFound;
            bool isCombinationFound;
            
            for (int i = 0; i < m_GameBoard.TotalRows; i++)
            {
                isCombinationFound = true;
                for (int j = 0; j < m_GameBoard.TotalCols - 1; j++)
                {
                    if(m_GameBoard.Board[i, j] == eBoardValue.Empty)
                    {
                        isCombinationFound = false;
                        break;
                        
                    }

                    if (m_GameBoard.Board[i, j] != m_GameBoard.Board[i, j + 1])
                    {
                        isCombinationFound = false;
                        break;
                    }
                }

                if (isCombinationFound)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public int GetTotalRows()
        {
            return m_GameBoard.TotalRows;
        }

        public int GetTotalCols()
        {
            return m_GameBoard.TotalCols;
        }

        public void SetCoordinateForCurrentPlayer(int i_X, int i_Y)
        {
            if(CurrentTurn == ePlayerType.HumanPlayerA)
            {
                m_Player1.ChosenMove = new Tuple<int, int>(i_X, i_Y);
            }
            else
            {
                m_Player2.ChosenMove = new Tuple<int, int>(i_X, i_Y);
            }
        }

        public ePlayerType CurrentTurn
        {
            get 
            { 
                return m_CurrentTurn;
            }

            set 
            { 
                m_CurrentTurn = value; 
            }
        }

        public void SwitchToNextPlayer()
        {
            ePlayerType opponent;

            if (this.CurrentTurn == ePlayerType.HumanPlayerA)
            {
                if (this.m_totalHumanPlayers > 1)
                {
                    opponent = ePlayerType.HumanPlayerB;
                }
                else
                {
                    opponent = ePlayerType.Computer;
                }

                this.CurrentTurn = opponent;
            }
            else
            {
                this.CurrentTurn = ePlayerType.HumanPlayerA;
            }
        }


        private int checkCols()
        {
            int result = k_NotFound;
            bool isCombinationFound;

            for (int j = 0; j < m_GameBoard.TotalRows; j++)
            {
                isCombinationFound = true;
                for (int i = 0; i < m_GameBoard.TotalCols - 1; i++)
                {
                    if (m_GameBoard.Board[i, j] == eBoardValue.Empty)
                    {
                        isCombinationFound = false;
                        break;
                    }

                    if (m_GameBoard.Board[i, j] != m_GameBoard.Board[i + 1, j])
                    {
                        isCombinationFound = false;
                        break;
                    }
                }

                if (isCombinationFound)
                {
                    result = j;
                    break;
                }
            }

            return result;
        }

        private int checkDiagonals()
        {
            int result = k_NotFound;
            bool isCombinationFound = true;

            for (int i = 0; i < m_GameBoard.TotalRows; i++)
            {
                isCombinationFound = true;
                if (m_GameBoard.Board[i, i] == eBoardValue.Empty)
                {
                    isCombinationFound = false;
                    break;
                }
            }
            if (isCombinationFound)
            {
                for (int i = 0; i < m_GameBoard.TotalRows - 1; i++)
                {
                    if (m_GameBoard.Board[i, i] != m_GameBoard.Board[i + 1, i + 1])
                    {
                        isCombinationFound = false;
                        break;
                    }
                }
            }

            if(isCombinationFound)
            {
                result = 0;
                //first loop is the condition of left diagonal.
                //starts with 0. ends with bottom right
            }
            else
            {
                for (int i = 0; i < m_GameBoard.TotalCols; i++)
                {
                    isCombinationFound = true;
                    if (m_GameBoard.Board[m_GameBoard.TotalRows - 1 - i, i] == eBoardValue.Empty)
                    {
                        isCombinationFound = false;
                        break;
                    }
                }
                if (isCombinationFound)
                {
                    for (int i = 0; i < m_GameBoard.TotalCols - 1; i++)
                    {
                        isCombinationFound = true;

                        if (m_GameBoard.Board[m_GameBoard.TotalRows - 1 - i, i]
                          != m_GameBoard.Board[m_GameBoard.TotalRows - i - 2, i + 1])
                        {
                            isCombinationFound = false;
                            break;
                        }
                    }
                }

                if (isCombinationFound)
                {
                    result = m_GameBoard.TotalCols - 1;
                    //second loop is the condition of right diagonal
                    //starts with column size -1, ends with 0
                }
            }

            return result;
        }

        public eBoardValue getBoardValueInCoordinates(int i_X, int i_Y)
        {
            return m_GameBoard.GetValueInPosition(i_X, i_Y);
        }

        public bool IsGameOver()
        {
            int losingCombinationRow, losingCombinationCol, losingCombinationDiagonal;
            bool gameOver = false;
            losingCombinationRow = checkRows();
            losingCombinationCol = checkCols();
            losingCombinationDiagonal = checkDiagonals();
            eBoardValue losingSign;
            ePlayerSign player1Sign = m_Player1.ChosenSign;

            if(losingCombinationRow != k_NotFound || losingCombinationCol != k_NotFound
                ||losingCombinationDiagonal != k_NotFound)
            {
                if(losingCombinationRow != k_NotFound)
                {
                    losingSign = m_GameBoard.Board[losingCombinationRow, 0];
                }
                else if(losingCombinationCol != k_NotFound)
                {
                    losingSign = m_GameBoard.Board[0, losingCombinationCol];
                }
                else
                {
                    losingSign = m_GameBoard.Board[0, losingCombinationDiagonal];
                }

                

                if ((char)losingSign == (char)player1Sign)
                {
                    m_RoundWinner = "player2";
                }
                else
                {
                    m_RoundWinner = "player1";
                }
                pointsAdding(m_RoundWinner);

                gameOver = true;
            }
            else if(m_GameBoard.FreeCells == 0) //tie
            {
                gameOver = true;
                m_RoundWinner = "tie";
            }

            return gameOver;
        }

        private void pointsAdding(string i_WhoWon)
        {
            if (i_WhoWon == "player1")
            {
                m_Player1.Score++;
            }
            else
            {
                m_Player2.Score++;
            }
        }
    }
}
