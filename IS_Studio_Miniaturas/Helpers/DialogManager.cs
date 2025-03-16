using System.Windows;

namespace IS_Studio_Miniaturas.Helpers
{
    public static class DialogManager
    {
        public enum DialogButtons
        {
            Ok = 0,
            OkCancel = 1,
            AbortTryIgnor = 2,
            YesNoCancel = 3,
            YesNo = 4,
            TryAgainCancel = 5,
            CancelTryAgainContinue = 6,
            None = 7
        }
        public enum DialogResult
        {
            Ok = 1,
            CancelClose = 2,
            Abort = 3,
            TryAgain = 4,
            Ignor = 5,
            Yes = 6,
            No = 7,
            TryAgainContinue = 10
        }

        /// <summary>
        /// Exibe uma mensagem de diálogo informativa.
        /// </summary>
        public static void ShowMessage(string message, string title = Constants.NomeAplicacao)
        {
            System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        /// <summary>
        /// Exibe uma mensagem de diálogo de aviso.
        /// </summary>
        public static void ShowWarning(string message, string title = Constants.NomeAplicacao)
        {
            System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        /// <summary>
        /// Exibe uma mensagem de diálogo de erro.
        /// </summary>
        public static void ShowError(string message, string title = Constants.NomeAplicacao)
        {
            System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowQuestion(string message, string title = Constants.NomeAplicacao)
        {
            return System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
        }
    }
}