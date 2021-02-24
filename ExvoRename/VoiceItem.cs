using System;
using System.IO;

namespace ExvoRename
{
    public class VoiceItem
    {
        public const int FieldSize = 4;

        /// <summary>
        /// 通しNo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// フォルダ名
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// 音声No
        /// </summary>
        public string VoiceId { get; set; }

        /// <summary>
        /// セリフ
        /// </summary>
        public string Line { get; set; }

        public bool Exists(string root)
        {
            return GetCurrentFilePath(root) != null;
        }

        public string GetCurrentFilePath(string root)
        {
            string dirname = Path.Combine(root, FolderName);
            if (!Directory.Exists(dirname))
            {
                return null;
            }
            string path = Path.Combine(dirname, $"{VoiceId}.wav");
            if (File.Exists(path))
            {
                return path;
            }
            var files = Directory.GetFiles(dirname, $"{VoiceId}_*.wav");
            if (files.Length == 1)
            {
                return files[0];
            }
            string voiceIdZen = VoiceId.ConvertNumHanToZen();
            path = Path.Combine(dirname, $"{voiceIdZen}.wav");
            if (File.Exists(path))
            {
                return path;
            }
            files = Directory.GetFiles(dirname, $"{voiceIdZen}_*.wav");
            if (files.Length == 1)
            {
                return files[0];
            }
            return null;
        }

        public string GetNewFilePath(string root, FileNameStyle style)
        {
            switch (style)
            {
                case FileNameStyle.VoiceId:
                    return Path.Combine(root, FolderName, $"{VoiceId}.wav");
                case FileNameStyle.VoiceId_Line:
                    string filename = $"{VoiceId}_{Line}.wav";
                    filename = filename.ConvertInvalidFileNameChars();
                    return Path.Combine(root, FolderName, filename);
                default:
                    throw new ArgumentException($"スタイル{style}は不正です");
            }
        }
    }
}
