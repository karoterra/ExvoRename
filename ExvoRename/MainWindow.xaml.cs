using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ExvoRename
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<VoiceList> _Voices;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Voices = new List<VoiceList>();
            if (!Directory.Exists("data"))
            {
                ShowError("dataフォルダが見つかりません");
                return;
            }
            var csvFiles = Directory.GetFiles("data", "*.csv");
            foreach (var csvFilePath in csvFiles)
            {
                var voiceList = new VoiceList(csvFilePath);
                _Voices.Add(voiceList);
                Debug.Print($"{voiceList.Name}, {voiceList.Items.Count}");
            }
            Debug.Print($"{_Voices.Count}");

            exVoiceSelector.ItemsSource = _Voices.Select(x => x.Name);
        }

        private void browseExVoiceButton_Click(object sender, RoutedEventArgs e)
        {
            using (var cofd = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
            })
            {
                if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }

                exVoicePathTextBox.Text = cofd.FileName;
            }
        }

        private void verifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (Verify())
            {
                MessageBox.Show("チェック終了");
            }
        }

        private void renameWithVoiceIdButton_Click(object sender, RoutedEventArgs e)
        {
            Rename(FileNameStyle.VoiceId);
        }

        private void renameWithVoiceIdLineButton_Click(object sender, RoutedEventArgs e)
        {
            Rename(FileNameStyle.VoiceId_Line);
        }

        private bool CheckInput()
        {
            if (!Directory.Exists(exVoicePathTextBox.Text))
            {
                ShowError("指定されたディレクトリは存在しません");
                return false;
            }
            if (exVoiceSelector.SelectedIndex < 0)
            {
                ShowError("種類を選択してください");
                return false;
            }
            return true;
        }

        private bool Verify()
        {
            if (!CheckInput())
            {
                return false;
            }
            var voiceRoot = exVoicePathTextBox.Text;
            var voiceList = _Voices[exVoiceSelector.SelectedIndex];

            foreach (var item in voiceList.Items)
            {
                if (!item.Exists(voiceRoot))
                {
                    ShowError($"通しNo:{item.Id} フォルダ名:{item.FolderName} 音声No:{item.VoiceId} セリフ:{item.Line} が見つかりません!");
                    return false;
                }
            }

            return true;
        }

        private void Rename(FileNameStyle style)
        {
            if (!Verify())
            {
                return;
            }
            var voiceRoot = exVoicePathTextBox.Text;
            var voiceList = _Voices[exVoiceSelector.SelectedIndex];
            int renameCount = 0;

            foreach (var item in voiceList.Items)
            {
                var current = item.GetCurrentFilePath(voiceRoot);
                if (current != null)
                {
                    string newFilePath = item.GetNewFilePath(voiceRoot, style);
                    if (current == newFilePath)
                    {
                        continue;
                    }
                    if (File.Exists(newFilePath))
                    {
                        var result = MessageBox.Show(
                            $"{newFilePath}は既に存在します。\n{current}を{newFilePath}にリネームしますか？",
                            "確認",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning
                        );
                        if (result == MessageBoxResult.No)
                        {
                            continue;
                        }
                    }
                    File.Move(current, newFilePath);
                    renameCount++;
                }
            }

            MessageBox.Show($"{renameCount}個のファイルをリネームしました");
        }

        private MessageBoxResult ShowError(string text)
        {
            return MessageBox.Show(text, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
