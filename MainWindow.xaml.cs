using System;
using System.Windows;
using System.Windows.Input;
using System.Media;
using CyberSecurityChatbot.Models;
using System.Windows.Input;
namespace CyberSecurityChatbot

{
    public partial class MainWindow : Window
    {
        ResponseManager bot = new ResponseManager();
        public MainWindow()
        {
            InitializeComponent();

            AppendMessage("Bot", "Hello! Welcome to the Cybersecurity Awareness.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessMessage();

        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessMessage();
            }
        }
        private void ProcessMessage()
        {
            string userMessage = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            AppendMessage("You", userMessage);
            string response = bot.GetResponse(userMessage);
            AppendMessage("Bot", response);
            UserInput.Clear();

        }

        

        
  

    private void AppendMessage(string sender, string message)
        {
            ChatDisplay.Text += $"{sender} {message}\n\n";
        }

        private void VoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Assets/greeting.wav");
            }
            catch
            {
                MessageBox.Show("Voice file not found.");
            }
        }
    }
}