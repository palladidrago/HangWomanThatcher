using HangWoman_Thatcher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangWoman_Thatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Image[] tempImg={ Resources.p1, Resources.p2, Resources.p3, Resources.p4, Resources.p5, Resources.p6, };
            m_Images = tempImg;
        }
        int letterAmount=26;

        private void setButtons(bool want)
        {
            //Enables/Disables all the buttons.
            Button curButton;
            for (int i = 1; i <= letterAmount; i++)
            {
                curButton = keyboard.Controls["button" + i] as Button;
                curButton.Enabled = want;
            }
        }
        private void Restart()
        {
            //Restart the game
            roniMan.Image = Resources.p0;
            
            for (int i = 1; i <= labels.Controls.Count; i++)
            {
                labels.Controls["label" + i].Text = "_";
            }
            setButtons(true);
            m_isFirst = true;
            m_CurrentLabel = 1; m_CountError = 0; m_RightCount = 0;
            m_WordToGuess = "";
            delButton.Visible = true;
            okeButton.Visible = true;
        }

        int m_CurrentLabel = 1, m_CountError = 0,m_RightCount=0;
        bool m_isFirst = true;
        Image[] m_Images;
        string m_WordToGuess;
       

        private void button_Letter_Click(object sender, EventArgs e)
        {
            //Handles keyboard button clicks.
            Button btn = sender as Button;
            string buttonText = btn.Text;
            if (m_isFirst) {
                //If first player, let input letters.
                labels.Controls["label" + m_CurrentLabel].Text = buttonText;
                m_CurrentLabel++;
                if (m_CurrentLabel == 10)
                {
                    okeButton.Enabled = true;
                    setButtons(false);
                }
            }
            else
            {
                //Else, if second player, check if letter pressed is in word, if so put it in the labels and increment
                //the right count, else, add to picture and increment the wrong count.

                if (m_WordToGuess.Contains(buttonText))
                {
                    for (int i = 0; i < m_WordToGuess.Length; i++)
                        if (m_WordToGuess[i].ToString() == buttonText)
                        {
                            labels.Controls["label" + (i + 1)].Text = buttonText;
                            m_RightCount++;
                        }
                }
                else
                {

                    //Todo=add losing
                    m_CountError++;

                    roniMan.Image = m_Images[m_CountError ];


                }
                //Disable the pressed button.
                (sender as Button).Enabled = false;
                //If won, restart.
                if (m_RightCount==letterAmount) { MessageBox.Show("YOU WON BRO !!!!!!!!!!!!!!");Restart(); }
                if (m_CountError == 5) { MessageBox.Show("YOU LOST BRO :("); Restart(); }

            }
        }

        private void buttonDel(object sender, EventArgs e)
        {
            if (m_CurrentLabel > 1)
            {
                labels.Controls["label" + (m_CurrentLabel - 1)].Text = "_";
                m_CurrentLabel--;
                okeButton.Enabled = false;
                setButtons(true);
            }

        }
        
        private void buttonOke(object sender, EventArgs e)
        {
            Label curLabel;
            for (int i = 1; i <= 9; i++)
            {
                curLabel = labels.Controls["label" + i] as Label;
                m_WordToGuess += curLabel.Text;
                curLabel.Text = "_";
            }
            setButtons(true);
            delButton.Visible = false;
            okeButton.Visible = false;
            m_isFirst = false;
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
