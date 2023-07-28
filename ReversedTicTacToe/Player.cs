using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversedTicTacToe
{
    public enum ePlayerType
    {
        Computer,
        HumanPlayerA,
        HumanPlayerB
    }

    public enum ePlayerSign
    {
        X = 'X',
        O = 'O'
    }

    class Player
    {
        private ePlayerType m_PlayerId;
        private int m_Score;
        private ePlayerSign m_ChosenSign;
        private Tuple<int, int> m_ChosenMove;

        public Player(ePlayerType i_PlayerId, ePlayerSign i_ChosenSign)
        {
            this.m_PlayerId = i_PlayerId;
            this.m_Score = 0;
            this.m_ChosenSign = i_ChosenSign;
            this.m_ChosenMove = default;
        }

        public ePlayerType PlayerId
        {
            get 
            { 
                return m_PlayerId;
            }

            set 
            { 
                m_PlayerId = value;
            }
        }

        public Tuple<int, int> ChosenMove
        {
            get 
            { 
                return m_ChosenMove;
            }

            set 
            { 
                m_ChosenMove = value;
            }
        }

        public int Score
        {
            get 
            { 
                return m_Score;
            }

            set 
            { 
                m_Score = value; 
            }
        }

        public ePlayerSign ChosenSign
        {
            get 
            { 
                return m_ChosenSign;
            }

            set 
            { 
                m_ChosenSign = value;
            }
        }
    }
}
