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
        }
    }
}
