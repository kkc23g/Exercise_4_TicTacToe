using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Properties.Buttons = new Button[3, 3] {
                { Button1, Button2, Button3 },
                { Button4, Button5, Button6 },
                { Button7, Button8, Button9 }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            if (Properties.GameEnded)
            {
                return;
            }
            if (button.Content == null)
            {
                button.Content = "X";
                if (CheckEndCondition("X"))
                {
                    TextBox1.Text = "You won!";
                    Properties.GameEnded = true;
                    return;
                }
                ComputerMakeMove();
                if (CheckEndCondition("O"))
                {
                    TextBox1.Text = "You lost!";
                    Properties.GameEnded = true;
                    return;
                }
            }
        }
        private void Button_Restart_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var button = Properties.Buttons[i, j];
                    button.Content = null;
                    button.Background = Brushes.White;
                }
            }
            TextBox1.Text = "";
            Properties.GameEnded = false;
        }
        private static List<(int, int)> GetAvailbleSquare()
        {
            var available = new List<(int, int)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var button = Properties.Buttons[i, j];
                    if (button.Content == null)
                    {
                        available.Add((i, j));
                    }
                }
            }
            return available;
        }
        private static void ComputerMakeMove()
        {
            var available = GetAvailbleSquare();
            if (available.Count > 0)
            {
                Random rnd = new();
                int index = rnd.Next(available.Count);
                (int, int) grid = available[index];
                Button button = Properties.Buttons[grid.Item1, grid.Item2];
                button.Content = "O";
            }
        }
        private static bool CheckEndCondition(string textContent)
        {
            // Check horizontal match
            for (int i = 0; i < 3; i++)
            {
                if (MatchContent(Properties.Buttons, i, 0, textContent) &&
                    MatchContent(Properties.Buttons, i, 1, textContent) &&
                    MatchContent(Properties.Buttons, i, 2, textContent))
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Properties.Buttons[i, j].Background = Brushes.Orange;
                    }
                    return true;
                }
            }
            // Check vertical match
            for (int i = 0; i < 3; i++)
            {
                if (MatchContent(Properties.Buttons, 0, i, textContent) &&
                    MatchContent(Properties.Buttons, 1, i, textContent) &&
                    MatchContent(Properties.Buttons, 2, i, textContent))
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Properties.Buttons[j, i].Background = Brushes.Orange;
                    }
                    return true;
                }
            }
            // Check Diagonal match
            if (MatchContent(Properties.Buttons, 0, 0, textContent) &&
                MatchContent(Properties.Buttons, 1, 1, textContent) &&
                MatchContent(Properties.Buttons, 2, 2, textContent))
            {
                for (int i = 0; i < 3; i++)
                {
                    Properties.Buttons[i, i].Background = Brushes.Orange;
                }
                return true;
            }
            if (MatchContent(Properties.Buttons, 2, 0, textContent) &&
                MatchContent(Properties.Buttons, 1, 1, textContent) &&
                MatchContent(Properties.Buttons, 0, 2, textContent))
            {
                for (int i = 0; i < 3; i++)
                {
                    Properties.Buttons[i, 2 - i].Background = Brushes.Orange;
                }
                return true;
            }
            return false;
        }
        private static bool MatchContent(Button[,] buttons, int i, int j, string textContent)
        {
            var content = buttons[i, j].Content;
            if (content == null)
            {
                return false;
            }
            if ((string) content == textContent)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
    public static class Properties
    {
        public static Button[,] Buttons = new Button[3, 3];
        public static bool GameEnded = false;
    }
}
// Learn controltemplate wpf