using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversedTicTacToe
{
    public enum eBoardValue
    {
        X = 'X',
        O = 'O',
        Empty = ' '
    }

    class GameBoard
    {
        private int m_TotalRows;
        private int m_TotalCols;
        private eBoardValue[,] m_Board;
        private int m_FreeCells;

        public GameBoard(int i_BoardSize)
        {
            this.m_TotalRows = m_TotalCols = i_BoardSize;
            this.m_Board = new eBoardValue[m_TotalRows, m_TotalCols];
            cleanBoard();
        }

        public void cleanBoard()
        {
            this.m_FreeCells = m_TotalRows * m_TotalCols;
            for (int i = 0; i < m_TotalRows; i++)
            {
                for (int j = 0; j < m_TotalCols; j++)
                {
                    this.m_Board[i, j] = eBoardValue.Empty;
                }
            }
        }

        public int FreeCells
        {
            get 
            { 
                return m_FreeCells; 
            }

            set 
            {
                m_FreeCells = value;
            }
        }

        public int TotalRows
        {
            get 
            { 
                return m_TotalRows; 
            }

            set 
            { 
                m_TotalRows = value;
            }
        }

        public int TotalCols
        {
            get
            { 
                return m_TotalCols;
            }

            set 
            { 
                m_TotalCols = value;
            }
        }

        public eBoardValue[,] Board
        {
            get
            {
                return this.m_Board;
            }
        }

        public void SetPlayerSignInBoard(int i_X, int i_Y, ePlayerSign i_PlayerSign)
        {
            eBoardValue valueToSetInBoard;

            if(i_PlayerSign == ePlayerSign.X)
            {
                valueToSetInBoard = eBoardValue.X;
            }
            else
            {
                valueToSetInBoard = eBoardValue.O;
            }

            SetValueInBoard(i_X, i_Y, valueToSetInBoard);
        }

        public void SetValueInBoard(int i_X, int i_Y, eBoardValue i_Value)
        {
            this.m_Board[i_X, i_Y] = i_Value;
            this.m_FreeCells--;
        }

        public eBoardValue GetValueInPosition(int i_X, int i_Y)
        {
            return m_Board[i_X, i_Y];
        }
    }
}
