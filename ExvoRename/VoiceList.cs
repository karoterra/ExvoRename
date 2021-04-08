using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExvoRename
{
    public class VoiceList
    {
        public readonly string Name;
        public readonly List<VoiceItem> Items;

        public VoiceList(string csvPath)
        {
            Name = Path.GetFileNameWithoutExtension(csvPath);
            Items = new List<VoiceItem>();
            using (var reader = new StreamReader(csvPath, Encoding.UTF8))
            {
                reader.ReadLine();  // skip header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    var values = line.Split(',');
                    if (values.Length != VoiceItem.FieldSize)
                    {
                        throw new InvalidDataException($"各行の列数は全て{VoiceItem.FieldSize}にしてください");
                    }

                    var item = new VoiceItem()
                    {
                        Id = int.Parse(values[0].ConvertNumZenToHan()),
                        FolderName = values[1],
                        VoiceId = values[2].ConvertNumZenToHan(),
                        Line = values[3],
                    };
                    Items.Add(item);
                }
            }

            Verify();
        }

        public void Verify()
        {
            var idDup = Items
                .GroupBy(item => item.Id)
                .Where(grp => grp.Count() >= 2)
                .Select(grp => grp.Key);
            if (idDup.Count() > 0)
            {
                string msg = $"ID {string.Join(", ", idDup)} は重複しています";
                throw new Exception(msg);
            }

            foreach (var folder in Items.GroupBy(item => item.FolderName))
            {
                var voiceIdDup = folder
                    .GroupBy(item => item.VoiceId)
                    .Where(grp => grp.Count() >= 2)
                    .Select(grp => grp.Key);
                if (voiceIdDup.Count() > 0)
                {
                    string msg = $"フォルダ\"{folder.Key}\"の音声No {string.Join(", ", voiceIdDup)} は重複しています";
                    throw new Exception(msg);
                }
            }
        }
    }
}
