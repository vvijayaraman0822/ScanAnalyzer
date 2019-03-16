﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAnalyzer
{
    public partial class FindSampleGame : Form
    {
        ScanAnalyzer scan; // creating an instance of the Scanalyzer class      
        int numberSample = 0; // Setting sample to 0
        string appear = "cross";

        public FindSampleGame()
        {
            InitializeComponent();
        }



        private void FindSampleGame_Load(object sender, EventArgs e)
        {

            SubGuessButton.Enabled = false; // Guess button disabled after program loads 
            gameTextBox.Visible = true; //  Game text box visible 


        }

        private void NewGameButton_Click(object sender, EventArgs e) // Start a new game button click
        {
            try
            {

                int gridRow = int.Parse(gridsizeTextBoxRow.Text); // Gets the number of rows for the grid
                int gridColumn = int.Parse(gridsizeTextBoxColumn.Text); // Gets the number of columns for the grid

                NewGameButton.Visible = false; // New Game Button not visible once clicked
                SubGuessButton.Enabled = true; // Guess button enabled
                scan = new ScanAnalyzer(gridRow, gridColumn, this); // intantiate the scanner class with this
                gameTextBox.Visible = true;
                counterLabel.Text = Convert.ToString(scan.GuessCounter);
                counterLabel.Visible = true;
            }

            // throw and catch exception
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Check the fields again")) // If any textboxes are empty 

                    MessageBox.Show("Please make sure you have all the fields entered correctly"); // print message 
                else
                    MessageBox.Show(ex.ToString());

            }

        }
    
        private void SubGuessButton_Click(object sender, EventArgs e) // Guess Button click
        {
            scan.GuessCounter++;
            counterLabel.Text = Convert.ToString(scan.GuessCounter);
            try
            {
                int guessRows = int.Parse(guessTextBoxRows.Text); // guess Rows from the user
                int guessColumn = int.Parse(guessTextBoxColumn.Text); // guess Column from the user

                if (scan.EvaluateGuess(guessRows, guessColumn, numberSample, appear) == true)
                {
                    MessageBox.Show("You have discovered sample " + Convert.ToString(numberSample)); // if sample is discovered
                    numberSample++;

                }

                else
                {
                    appear = (appear == "cross" ? "down" : "cross");


                }


                if (numberSample >= 2)
                {

                    if (MessageBox.Show("You have guessed it correctly, do yo want to play a New Game?", "New Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        NewGameButton.Visible = true;
                        numberSample = 0;
                        gridsizeTextBoxRow.Text = "";
                        gameTextBox.Visible = false;
                        counterLabel.Visible = false;
                        SubGuessButton.Enabled = false;
                    }
                    else

                        this.Close();
                }
            }

            catch (Exception ex)
            {

                if (ex.ToString().Contains("Make sure you have entered everything in the right format")) // Exception thrown

                    MessageBox.Show("Please make sure to enter all the fields"); // show the messag

                else
                {
                    // if the guess is outside of the grid the user is prompted to enter a new guess
                    if (ex.ToString().Contains("Not within the range"))
                        MessageBox.Show("You have exited the range. Please guess within the range"); // show the message 

                    else
                        MessageBox.Show(ex.ToString());
                }
            }
        }
     
        public string AppearGrid
        {
            get
            {
                return gameTextBox.Text;
            }
            set
            {
                gameTextBox.Text = value;
            }


        }
    }
}
    
    
        
       