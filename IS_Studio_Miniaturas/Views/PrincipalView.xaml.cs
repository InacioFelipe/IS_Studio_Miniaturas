using Corel.Interop.VGCore;
using IS_Studio_Miniaturas.Helpers;
using IS_Studio_Miniaturas.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace IS_Studio_Miniaturas.Views
{
    public partial class PrincipalView : UserControl
    {
        private readonly CorelHelper _corelHelper;

        // Construtor
        public PrincipalView()
        {
            InitializeComponent();

            _corelHelper = new CorelHelper();

            PopulateCbxFormatoPagina();
            PopulateCbxColunas();

            // Associa o evento SelectionChanged do ComboBox
            cbxFormatoPagina.SelectionChanged += CbxFormatoPagina_SelectionChanged;
        }

        private void btnImportarDXF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Chama o método para abrir a caixa de diálogo e importar o arquivo DXF
            string filePath = OpenDxfFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                // Importa o arquivo DXF no CorelDRAW
                _corelHelper.ImportDxfFile(filePath);

                // Atualiza o nome do arquivo na interface do usuário
                txtNomeArquivo.Text = $"Arquivo Importado:\n" +
                    $"{System.IO.Path.GetFileName(filePath)}";
            }
        }

        private void btnExecutar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_corelHelper.DocExists())
            {
                DialogManager.ShowWarning("Nenhum documento aberto no CorelDRAW.");
                return;
            }


            (double width, double height) = _corelHelper.GetPageSize();

            int col = Convert.ToInt32(cbxNumeroColunas.SelectedItem);

            // obter o numero de pecas na pagina (lista de pecas) e então calcular o numero de linhas = (Nº de peças / Por Col)
            int lin = 5;

            int margemImpressao = 8;

            _corelHelper.CreateMatrix(col, lin, margemImpressao);
        }


        // Metodos auxiliares
        private string OpenDxfFileDialog()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Selecione um arquivo DXF",
                Filter = "Arquivos DXF (*.dxf)|*.dxf|Todos os arquivos (*.*)|*.*",
                RestoreDirectory = true
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }

        private void PopulateCbxFormatoPagina()
        {
            // Dicionário com os formatos de página e suas dimensões
            Dictionary<string, string> formatosPagina = new Dictionary<string, string>
            {
                { "A4", "210 x 297 mm" },
                { "A3", "297 x 420 mm" }
            };

            // Define os itens do ComboBox
            cbxFormatoPagina.ItemsSource = formatosPagina.Keys;

            // Define o valor padrão (opcional)
            cbxFormatoPagina.SelectedIndex = 0; // Seleciona "A4" como padrão
        }

        private void PopulateCbxColunas()
        {
            List<string> colunas = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10"
            };
            cbxNumeroColunas.ItemsSource = colunas;
            cbxNumeroColunas.SelectedIndex = 2; // Seleciona "3" como padrão
        }

        private void ApllySizesInPageBasedOnSelection()
        {
            string selectedFormat = cbxFormatoPagina.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedFormat))
            {
                throw new InvalidOperationException("Nenhum formato de página selecionado.");
            }

            double largura = 0;
            double altura = 0;

            switch (selectedFormat)
            {
                case "A4":
                    largura = 210;
                    altura = 297;
                    break;

                case "A3":
                    largura = 297;
                    altura = 420;
                    break;

                default:
                    throw new InvalidOperationException("Formato de página inválido.");
            }

            // Aplica tamanhos a página no CorelDRAW usando o CorelHelper
            if (_corelHelper.DocExists())
            {
                _corelHelper.ApplySizesToExistingPage(largura, altura);
            }
        }


        // Metodos reativos a eventos #####################################################################################

        private void CbxFormatoPagina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ApllySizesInPageBasedOnSelection();
            }
            catch (Exception ex)
            {
                DialogManager.ShowWarning($"Erro ao aplicar medidas à página: {ex.Message}");
            }
        }


    }
}