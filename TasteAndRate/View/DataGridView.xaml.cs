using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasteAndRate.Models;
using TasteAndRate.ViewModel;

namespace TasteAndRate.View
{
    /// <summary>
    /// Lógica de interacción para DataGridView.xaml
    /// </summary>
    public partial class DataGridView : UserControl
    {
        public DataGridView()
        {
            InitializeComponent();
        }
 
        private async void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var dataGrid = (DataGrid)sender;
                var criterio = e.Row.Item as CriterioModel;

                if (criterio != null)
                {
                    var viewModel = (DataGridViewModel)DataContext;

                    
                    //await viewModel.ActualizarCriterioEnBD(criterio);
                }
            }
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox?.DataContext is CriterioModel criterio)
            {
            
                var binding = BindingOperations.GetBindingExpression(checkBox, CheckBox.IsCheckedProperty);
                binding?.UpdateSource();

                var viewModel = (DataGridViewModel)DataContext;
                //await viewModel.ActualizarCriterioEnBD(criterio);
            }
        }


    }
}
