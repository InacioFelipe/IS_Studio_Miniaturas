using Microsoft.Win32;

namespace IS_Studio_Miniaturas.Services
{
    public interface IFileDialogService
    {
        string OpenFileDialog(string filter);
    }

    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog(string filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Selecione um arquivo",
                Filter = filter,
                RestoreDirectory = true
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }
    }
}