using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TasteAndRate.ViewModel;

namespace TasteAndRate.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public LoginViewModel LoginViewModel { get; }
        public RegistrationViewModel RegistrationViewModel { get; }
        public StackPanelViewModel StackPanelViewModel { get; }

        public DataGridViewModel DataGridViewModel { get; }

        public OverviewViewModel OverviewViewModel { get; }

        [ObservableProperty]
        private bool _isLoggedIn = false;
       

        public MainViewModel(LoginViewModel loginViewModel, RegistrationViewModel registrationViewModel, StackPanelViewModel stackPanelViewModel, DataGridViewModel dataGridViewModel, OverviewViewModel overviewViewModel)
        {
            _selectedViewModel = loginViewModel;
            LoginViewModel = loginViewModel;
            RegistrationViewModel =registrationViewModel;
            StackPanelViewModel = stackPanelViewModel;
            DataGridViewModel = dataGridViewModel;
            OverviewViewModel = overviewViewModel;
        }
         
        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }


        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
        [RelayCommand]
        public async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
        public void SetLoginStatus(bool status)
        {
            IsLoggedIn = status;
        }
        [RelayCommand]
        private void Logout_Click()
        {
            SetLoginStatus(false);
            SelectViewModel(LoginViewModel);

        }



    }
}

