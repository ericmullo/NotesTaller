using System.Collections.ObjectModel;
using System.IO;

namespace Notes.Models
{
    public class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public void LoadNotes()
        {
            Notes.Clear();
            string appDataPath = FileSystem.AppDataDirectory;

            foreach (var file in Directory.EnumerateFiles(appDataPath, "*.notes.txt"))
            {
                Notes.Add(new Note
                {
                    Filename = file,
                    Text = File.ReadAllText(file),
                    Date = File.GetCreationTime(file)
                });
            }
        }
    }
}
