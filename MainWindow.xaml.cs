using System;
using System.IO;
using System.Windows;

namespace R6S_Server_Switcher
{
    public partial class MainWindow : Window
    {
        readonly string[] areas = { "default", "eastus", "centralus", "southcentralus", "westus", "brazilsouth", "northeurope", "westeurope", "southafricanorth", "eastasia", "southeastasia", "australiaeast", "australiasoutheast", "japanwest"};
        string url_document;
        string[] directories;


        public MainWindow()
        {
            InitializeComponent();
            url_document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Rainbow Six - Siege";

            if (!Directory.Exists(url_document))
            {
                MessageBox.Show("게임 폴더가 존재 하지 않습니다!");
                this.Close();
                Environment.Exit(Environment.ExitCode);
            }

            directories = Directory.GetDirectories(url_document);
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            switchArea(ComboBox_Server.SelectedIndex);
            MessageBox.Show("서버가 정상적으로 변경되었습니다.");
        }

        private void Site_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://prigic.flarebrick.com");
        }

        private void switchArea(object selectedIndex)
        {
            throw new NotImplementedException();
        }

        private void switchArea(int uid)
        {
            foreach (string dir in directories)
            {
                string url = dir + "\\GameSettings.ini";
                if (checkIfFileExist(url)) editGameSettings(url, uid);
            }
        }

        private Boolean checkIfFileExist(string url)
        {
            if (!File.Exists(url))
            {
                MessageBox.Show("GameSettings.ini 파일이 존재하지 않습니다!");
                return false;
            }
            return true;
        }

        private void editGameSettings(string url, int uid)
        {
            string[] contentInGameSetting;
            int length = 14;
            contentInGameSetting = File.ReadAllLines(url);

            for (byte i = 1; i < contentInGameSetting.Length; ++i)
            {
                if (contentInGameSetting[contentInGameSetting.Length - i].Length > 10)
                {
                    string lineString = contentInGameSetting[contentInGameSetting.Length - i].Substring(0, length);
                    if (lineString == "DataCenterHint")
                    {
                        contentInGameSetting[contentInGameSetting.Length - i] = "DataCenterHint=" + areas[uid];
                        File.WriteAllLines(url, contentInGameSetting);
                        break;
                    }
                }
            }
        }
    }
}
