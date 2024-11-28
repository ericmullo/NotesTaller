using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NotePage : ContentPage
    {
        private string _itemId;

        public string ItemId
        {
            get => _itemId;
            set
            {
                _itemId = value;
                LoadNote(GetFilePathFromItemId(_itemId));
            }
        }

        public NotePage()
        {
            InitializeComponent();
        }

        private string GetFilePathFromItemId(string itemId)
        {
            string appDataPath = FileSystem.AppDataDirectory;
            return Path.Combine(appDataPath, $"{itemId}.notes.txt");
        }

        private void LoadNote(string fileName)
        {
            Models.Note noteModel = new Models.Note
            {
                Filename = fileName
            };

            if (File.Exists(fileName))
            {
                noteModel.Text = File.ReadAllText(fileName);
                noteModel.Date = File.GetCreationTime(fileName);
            }

            BindingContext = noteModel;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note note)
            {
                File.WriteAllText(note.Filename, TextEditor.Text);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note note)
            {
                if (File.Exists(note.Filename))
                    File.Delete(note.Filename);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
