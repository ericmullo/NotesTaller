using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace Notes
{
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();

            // Obtener la ruta del directorio de datos de la aplicación
            string appDataPath = FileSystem.AppDataDirectory;

            // Generar un nombre de archivo aleatorio para la nota
            string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

            // Llamar al método LoadNote con la ruta completa
            LoadNote(Path.Combine(appDataPath, randomFileName));
        }

        // Método para cargar una nota desde un archivo
        private void LoadNote(string fileName)
        {
            // Crear un modelo de nota
            Models.Note noteModel = new Models.Note
            {
                Filename = fileName
            };

            // Verificar si el archivo existe
            if (File.Exists(fileName))
            {
                noteModel.Date = File.GetCreationTime(fileName);
                noteModel.Text = File.ReadAllText(fileName);
            }

            // Asignar el modelo como BindingContext para la página
            BindingContext = noteModel;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note noteModel)
            {
                // Guardar el contenido en el archivo especificado por el modelo
                File.WriteAllText(noteModel.Filename, noteModel.Text);
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Note noteModel)
            {
                // Eliminar el archivo si existe
                if (File.Exists(noteModel.Filename))
                    File.Delete(noteModel.Filename);

                // Limpiar el texto en el modelo
                noteModel.Text = string.Empty;
            }
        }
    }
}
