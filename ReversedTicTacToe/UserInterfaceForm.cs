using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReversedTicTacToe
{
    public partial class UserInterfaceForm : Form
    {
        private ePlayerSign m_Player1Sign;
        private ePlayerSign m_Player2Sign;
        private bool m_IsPlayer2Human;

        public UserInterfaceForm()
        {
            InitializeComponent();
            m_IsPlayer2Human = false;
        }

        private void UserInterfaceForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = "";
                m_IsPlayer2Human = true;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
                m_IsPlayer2Human = false;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if(numericUpDownRows.Value != numericUpDownCols.Value
                || !(numericUpDownRows.Value >= 4 && numericUpDownRows.Value <= 10) 
                || !(numericUpDownCols.Value >= 4 && numericUpDownCols.Value <= 10))
            {
                MessageBox.Show("Invalid values for rows/cols entered. Please enter values once again.", "Invalid input");
            }
            else if(textBoxPlayer1.Text =="")
            {
                MessageBox.Show("Please enter a proper name for player 1", "Empty player name");
            }
            else if(textBoxPlayer2.Enabled == true && textBoxPlayer2.Text == "")
            {
                MessageBox.Show("Please enter a proper name for player 2", "Empty player name");
            }
            else if(OSignBox.Checked == false && XSignBox.Checked == false)
            {
                MessageBox.Show("Player 1, please choose your sign. X or O.", "Sign not filled");
            }
            else
            {
                int rowsEntered = (int)numericUpDownRows.Value;
                int colsEntered = (int)numericUpDownCols.Value;
                this.Hide();
                sendValuesToBoard(rowsEntered, colsEntered);
               
            }
        }

        private void sendValuesToBoard(int i_RowsEntered, int i_ColsEntered)
        {
            GameBoardForm gameBoard = new GameBoardForm(i_RowsEntered, i_ColsEntered, m_IsPlayer2Human, m_Player1Sign, m_Player2Sign);
            gameBoard.setAllButtons();
            gameBoard.ShowDialog();
            
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Value = numericUpDownRows.Value;
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Value = numericUpDownCols.Value;
        }

        private void XSignBox_CheckedChanged(object sender, EventArgs e)
        {
            if (XSignBox.Checked)
            {
                OSignBox.Checked = false;
                m_Player1Sign = ePlayerSign.X;
                m_Player2Sign = ePlayerSign.O;
            }
        }

        private void OSignBox_CheckedChanged(object sender, EventArgs e)
        {
            if (OSignBox.Checked)
            {
                XSignBox.Checked = false;
                m_Player1Sign = ePlayerSign.O;
                m_Player2Sign = ePlayerSign.X;
            }
        }
    }
}
