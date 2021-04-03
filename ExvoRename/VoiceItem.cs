using System;
using System.IO;
using System.Text;

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

        public string GetCurrentFilePath(string root, string ext = "wav")
        {
            string dirname = Path.Combine(root, FolderName);
            if (!Directory.Exists(dirname))
            {
                return null;
            }
            string path = Path.Combine(dirname, $"{VoiceId}.{ext}");
            if (File.Exists(path))
            {
                return path;
            }
            var files = Directory.GetFiles(dirname, $"{VoiceId}_*.{ext}");
            if (files.Length == 1)
            {
                return files[0];
            }
            string voiceIdZen = VoiceId.ConvertNumHanToZen();
            path = Path.Combine(dirname, $"{voiceIdZen}.{ext}");
            if (File.Exists(path))
            {
                return path;
            }
            files = Directory.GetFiles(dirname, $"{voiceIdZen}_*.{ext}");
            if (files.Length == 1)
            {
                return files[0];
            }
            return null;
        }

        public string GetNewFilePath(string root, FileNameStyle style, string ext = "wav")
        {
            switch (style)
            {
                case FileNameStyle.VoiceId:
                    return Path.Combine(root, FolderName, $"{VoiceId}.{ext}");
                case FileNameStyle.VoiceId_Line:
                    string filename = $"{VoiceId}_{Line}.{ext}";
                    filename = filename.ConvertInvalidFileNameChars();
                    return Path.Combine(root, FolderName, filename);
                default:
                    throw new ArgumentException($"スタイル{style}は不正です");
            }
        }

        public bool CreateTextFile(string root, FileNameStyle style, Encoding encoding, bool newline)
        {
            string filename = GetNewFilePath(root, style, "txt");
            using (var sw = new StreamWriter(filename, false, encoding))
            {
                if (newline)
                {
                    sw.WriteLine(Line);
                }
                else
                {
                    sw.Write(Line);
                }
            }
            return true;
        }
    }
}
