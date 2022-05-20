using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BigGun
{
    /// <summary>
    /// Логика взаимодействия для Level_Select.xaml
    /// </summary>
    public partial class Level_Select : Window
    {
        public MainWindow menu;
        bool isDifficultSelected = false;
        int numberOfSelectedDiff = -1;
        string path;
        public Level_Select(MainWindow menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            menu.Show();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DifficultEasy_Click(object sender, RoutedEventArgs e)
        {
            DifficultEasy.BorderBrush = new SolidColorBrush(Colors.Yellow);
            DifficultEasy.BorderThickness = new Thickness(3);

            if (isDifficultSelected)
            {
                if (numberOfSelectedDiff == 2)
                {
                    DifficultMedium.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else if (numberOfSelectedDiff == 3)
                {
                    DifficultHard.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else
                {
                    DifficultEasy.BorderThickness = new Thickness(0);
                    isDifficultSelected = false;
                }
            }
            else
                isDifficultSelected = true;

            numberOfSelectedDiff = 1;
        }

        private void DifficultMedium_Click(object sender, RoutedEventArgs e)
        {
            DifficultMedium.BorderBrush = new SolidColorBrush(Colors.Yellow);
            DifficultMedium.BorderThickness = new Thickness(3);

            if (isDifficultSelected)
            {
                if (numberOfSelectedDiff == 1)
                {
                    DifficultEasy.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else if (numberOfSelectedDiff == 3)
                {
                    DifficultHard.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else
                {
                    DifficultMedium.BorderThickness = new Thickness(0);
                    isDifficultSelected = false;
                }
            }
            else
                isDifficultSelected = true;

            numberOfSelectedDiff = 2;
        }

        private void DifficultHard_Click(object sender, RoutedEventArgs e)
        {
            DifficultHard.BorderBrush = new SolidColorBrush(Colors.Yellow);
            DifficultHard.BorderThickness = new Thickness(3);

            if (isDifficultSelected)
            {
                if (numberOfSelectedDiff == 1)
                {
                    DifficultEasy.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else if (numberOfSelectedDiff == 2)
                {
                    DifficultMedium.BorderThickness = new Thickness(0);
                    isDifficultSelected = true;
                }
                else
                {
                    DifficultHard.BorderThickness = new Thickness(0);
                    isDifficultSelected = false;
                }
            }
            else
                isDifficultSelected = true;

            numberOfSelectedDiff = 3;
        }
        private void btnLevel1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnLevel2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Game gameWindow = new Game(this, path);
            gameWindow.Show();
            this.Hide();
        }
    }
}
