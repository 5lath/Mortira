using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        Level_Select LS;
        double groundProportion = 5, fromCenterToMortiras = 250;
        public Game(Level_Select LS, string pathToLevelSettings)
        {
            InitializeComponent();
            this.LS = LS;
            PowerBar.Value = 10;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Line angleOX = new Line();
            angleOX.X1 = 0;
            angleOX.Y1 = AngleGraphImage.ActualHeight * 4.0 / 5.0;
            angleOX.X2 = AngleGraphImage.ActualWidth;
            angleOX.Y2 = angleOX.Y1 = AngleGraphImage.ActualHeight * 4.0 / 5.0;
            angleOX.StrokeThickness = 2;
            angleOX.Stroke = Brushes.Black;
            AngleGraphImage.Children.Add(angleOX);

            Line angleOY = new Line();
            angleOY.X1 = AngleGraphImage.ActualWidth / 5.0;
            angleOY.Y1 = 0;
            angleOY.X2 = AngleGraphImage.ActualWidth / 5.0;
            angleOY.Y2 = AngleGraphImage.ActualHeight;
            angleOY.StrokeThickness = 2;
            angleOY.Stroke = Brushes.Black;
            AngleGraphImage.Children.Add(angleOY);

            DrawGround(groundProportion);
            DrawMortiras(fromCenterToMortiras);
        }

        private void DrawGround(double heightProportion)
        {
            Rectangle ground = new Rectangle();
            ground.Width = GameField.ActualWidth;
            ground.Height = GameField.ActualHeight / heightProportion;
            ground.Fill = Brushes.Green;
            ground.Margin = new Thickness(0, ((heightProportion - 1.0) / heightProportion) * GameField.ActualHeight, 0, 0);
            GameField.Children.Add(ground);
        }

        private void DrawMortira(double distanceFromCenter, bool leftORright)
        {
            double mortiraSize = 30;//размер мортиры
            if (leftORright)//тут мы выбираем левую ли рисуем мортиру или правую
            {
                Rectangle mortira = new Rectangle();
                mortira.Width = mortiraSize;
                mortira.Height = mortiraSize;
                mortira.Fill = Brushes.Bisque;
                mortira.Margin = new Thickness(GameField.ActualWidth - (((GameField.ActualWidth / 2) + distanceFromCenter)), ((groundProportion - 1.0) / groundProportion) * GameField.ActualHeight - mortiraSize, 0, 0);
                GameField.Children.Add(mortira);
            }
            else
            {
                Rectangle mortira = new Rectangle();
                mortira.Width = mortiraSize;
                mortira.Height = mortiraSize;
                mortira.Fill = Brushes.Red;
                mortira.Margin = new Thickness(((GameField.ActualWidth / 2) + distanceFromCenter), ((groundProportion - 1.0) / groundProportion) * GameField.ActualHeight - mortiraSize, 0, 0);
                GameField.Children.Add(mortira);
            }

        }

        private void DrawMortiras(double distanceFromCenter)
        {
            DrawMortira(distanceFromCenter, true);
            DrawMortira(distanceFromCenter, false);
        }

        private void AngleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int result = 0;
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(AngleTextBox.Text, out result))
                {
                    if (result >= 0 && result < 90)
                    {
                        AngleTextBox.Background = new SolidColorBrush(Colors.Gold);

                        RotateTransform rotateTransform1 =
                            new RotateTransform(90 - result);
                        rotateTransform1.CenterX = AngleGraphImage.ActualWidth / 5.0;
                        rotateTransform1.CenterY = AngleGraphImage.ActualHeight * 4.0 / 5.0;

                        Line angleLine = new Line();
                        angleLine.X1 = AngleGraphImage.ActualWidth * 1.0 / 5.0;
                        angleLine.Y1 = 0;
                        angleLine.X2 = AngleGraphImage.ActualWidth * 1.0 / 5.0;
                        angleLine.Y2 = AngleGraphImage.ActualHeight;
                        angleLine.StrokeThickness = 2;
                        angleLine.Stroke = Brushes.Green;
                        if(AngleGraphImage.Children.Count > 2)
                            for(int i = 2; i < AngleGraphImage.Children.Count; i++)
                            {
                                AngleGraphImage.Children.RemoveAt(i);
                            }
                        AngleGraphImage.Children.Add(angleLine);

                        angleLine.RenderTransform = rotateTransform1;
                        angleLine.X2 = AngleGraphImage.ActualWidth / 5.0;
                        angleLine.Y2 = AngleGraphImage.ActualHeight * 4.0 / 5.0;
                    }
                    else
                    {
                        AngleTextBox.Background = new SolidColorBrush(Colors.White);
                        AngleTextBox.Text = "0";
                        MessageBox.Show("Угол должен быть >= 0 и < 90 градусов.");
                    }
                }
                else
                {
                    AngleTextBox.Background = new SolidColorBrush(Colors.White);
                    AngleTextBox.Text = "0";
                    MessageBox.Show("Некорректный ввод.");
                }
            }
        }

        private void PowerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int result = 10;
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(PowerTextBox.Text, out result))
                {
                    if (result >= 10 && result <= 100)
                    {
                        PowerTextBox.Background = new SolidColorBrush(Colors.Gold);
                        PowerBar.Value = result;
                    }
                    else
                    {
                        PowerTextBox.Background = new SolidColorBrush(Colors.White);
                        PowerBar.Value = 0;
                        MessageBox.Show("Мощность должна быть >= 10 и <= 100.");
                    }
                }
                else
                {
                    PowerTextBox.Background = new SolidColorBrush(Colors.White);
                    PowerBar.Value = 0;
                    MessageBox.Show("Некорректный ввод.");
                }
            }
        }

        private void AngleTriangleButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            if (int.TryParse(AngleTextBox.Text, out result))
            {
                if (result >= 0 && result < 90)
                {
                    if (result - 1 >= 0)
                    {
                        result -= 1;
                        AngleTextBox.Clear();
                        AngleTextBox.Text = $"{result}";

                        Line angleLine = AngleGraphImage.Children[AngleGraphImage.Children.Count - 1] as Line;
                        AngleGraphImage.Children.RemoveAt(AngleGraphImage.Children.Count - 1);
                        AngleGraphImage.Children.Add(angleLine);
                        RotateTransform rotateTransform1 =
                            new RotateTransform(90 - result);
                        rotateTransform1.CenterX = AngleGraphImage.ActualWidth / 5.0;
                        rotateTransform1.CenterY = AngleGraphImage.ActualHeight * 4.0 / 5.0;
                        angleLine.RenderTransform = rotateTransform1;
                        angleLine.X2 = AngleGraphImage.ActualWidth / 5.0;
                        angleLine.Y2 = AngleGraphImage.ActualHeight * 4.0 / 5.0;
                    }
                    else
                    { 
                        MessageBox.Show("Угол должен быть >= 0.");
                    }
                }
                else
                {
                    MessageBox.Show("Угол должен быть >= 0 и < 90 градусов.");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод.");
            }
        }

        private void AngleTriangleButtonRight_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            if (int.TryParse(AngleTextBox.Text, out result))
            {
                if (result >= 0 && result < 90)
                {
                    if (result + 1 < 90)
                    {
                        result += 1;
                        AngleTextBox.Clear();
                        AngleTextBox.Text = $"{result}";

                        Line angleLine = AngleGraphImage.Children[AngleGraphImage.Children.Count - 1] as Line;
                        AngleGraphImage.Children.RemoveAt(AngleGraphImage.Children.Count - 1);
                        AngleGraphImage.Children.Add(angleLine);
                        RotateTransform rotateTransform1 =
                            new RotateTransform(90 - result);
                        rotateTransform1.CenterX = AngleGraphImage.ActualWidth / 5.0;
                        rotateTransform1.CenterY = AngleGraphImage.ActualHeight * 4.0 / 5.0;
                        angleLine.RenderTransform = rotateTransform1;
                        angleLine.X2 = AngleGraphImage.ActualWidth / 5.0;
                        angleLine.Y2 = AngleGraphImage.ActualHeight * 4.0 / 5.0;
                    }
                    else
                        MessageBox.Show("Угол должен быть < 90.");
                }
                else
                {
                    MessageBox.Show("Угол должен быть >= 0 и < 90 градусов.");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод.");
            }
        }

        private void PowerTriangleButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            int result = 10;
            if (int.TryParse(PowerTextBox.Text, out result))
            {
                if (result >= 10 && result <= 100)
                {
                    if (result - 1 >= 10)
                    {
                        result -= 1;
                        PowerTextBox.Clear();
                        PowerTextBox.Text = $"{result}";
                        PowerBar.Value -= 1;
                    }
                    else
                        MessageBox.Show("Мощность должна быть >= 10.");
                }
                else
                {
                    MessageBox.Show("Мощность должна быть >= 10 и <= 100.");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод.");
            }
        }

        private void PowerTriangleButtonRight_Click(object sender, RoutedEventArgs e)
        {
            int result = 10;
            if (int.TryParse(PowerTextBox.Text, out result))
            {
                if (result >= 10 && result <= 100)
                {
                    if (result + 1 <= 100)
                    {
                        result += 1;
                        PowerTextBox.Clear();
                        PowerTextBox.Text = $"{result}";
                        PowerBar.Value += 1;
                    }
                    else
                        MessageBox.Show("Мощность должна быть <= 100.");
                }
                else
                {
                    MessageBox.Show("Мощность должна быть >= 10 и <= 100.");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод.");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LS.Show();
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            LS.Show();
            this.Close();
        }
    }
}


