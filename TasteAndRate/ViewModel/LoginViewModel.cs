using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TasteAndRate.ViewModel;
using TasteAndRate.Interfaces;
using TasteAndRate.DTO;
using TasteAndRate;
using TasteAndRate.Utils;


namespace TasteAndRate.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {


        private  MainViewModel _mainViewModel;
        [ObservableProperty]
        public string _Email;
        [ObservableProperty]
        public string _Password;

        IHttpsJsonClientProvider<UserDTO> _httpsJsonClientProvider;
        public LoginViewModel(IHttpsJsonClientProvider<UserDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }
     

        [RelayCommand]
        private void Register()
        {
            
            var mainViewModel = App.Current.Services.GetService<MainViewModel>();
            var RegistroViewModel = App.Current.Services.GetService<RegistrationViewModel>();
            mainViewModel.SelectViewModelCommand.Execute(RegistroViewModel);
            

        }

        [RelayCommand]
        private async void Data_Click()
        {
            if (await LoginAsync())
            {
                _mainViewModel.SetLoginStatus(true);
                var mainViewModel = App.Current.Services.GetService<MainViewModel>();
                var OverviewViewModel = App.Current.Services.GetService<OverviewViewModel>();
                mainViewModel.SelectViewModelCommand.Execute(OverviewViewModel);
            }
         

        }

        public override Task LoadAsync()
        {
            _mainViewModel = App.Current.Services.GetService<MainViewModel>();
            return Task.CompletedTask;
        }

        
        private async Task<bool> LoginAsync()
        {
            App.Current.Services.GetService<LoginDTO>().Email = Email;
            App.Current.Services.GetService<LoginDTO>().Password = Password;
            

            try
            {
                UserDTO user = await _httpsJsonClientProvider.LoginPostAsync($"{Constantes.LOGIN_PATH}/login", App.Current.Services.GetService<LoginDTO>());

                if (user != null && user.Result != null && !string.IsNullOrEmpty(user.Result.Token))
                {
                    LoginDTO loginDTO = App.Current.Services.GetService<LoginDTO>();
                    loginDTO.Token = user.Result.Token;
                    loginDTO.UserId = user.Result.User.Id;
                    App.Current.Services.GetService<LoginDTO>().Token = user.Result.Token;
                    return user.IsSuccess;
                }
                else
                {
                    MessageBox.Show("Error: Usuario o contraseña incorrectos.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        
    }
}
