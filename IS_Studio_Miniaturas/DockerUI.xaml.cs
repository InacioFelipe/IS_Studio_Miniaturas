using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using corel = Corel.Interop.VGCore;

namespace IS_Studio_Miniaturas
{
    public partial class DockerUI : UserControl
    {
        private corel.Application corelApp;

        public DockerUI(object app)
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ContainerAbas.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEFE00"));
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Uri destinationPage = null;

            // Lógica para lidar com a mudança de seleção da aba
            if (e.Source is System.Windows.Controls.TabControl tabControl)
            {
                if (tabControl.SelectedItem is System.Windows.Controls.TabItem selectedTab)
                {
                    // Aqui você pode atualizar o conteúdo com base na aba selecionada
                    if (selectedTab.Header.ToString() == "Principal")
                    {
                        destinationPage = new Uri("Views\\PrincipalView.xaml", UriKind.Relative);
                    }
                    else if (selectedTab.Header.ToString() == "Ajuda")
                    {
                        //destinationPage = new Uri("Views\\AjudaView.xaml", UriKind.Relative);
                    }
                    else if (selectedTab.Header.ToString() == "Sobre")
                    {
                        //destinationPage = new Uri("Views\\SobreView.xaml", UriKind.Relative);
                    }

                    if (destinationPage != null)
                    {
                        ContainerAbas.Source = destinationPage;
                        ContainerAbas.Navigate(ContainerAbas.Source, corelApp);
                    }
                }
            }


        }
    }
}
