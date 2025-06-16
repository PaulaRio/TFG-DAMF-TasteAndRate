using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class RegistrationViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        [ObservableProperty]
        public string _Nombre;
        [ObservableProperty]
        public string _Email;
        [ObservableProperty]
        public string _Password;
        [ObservableProperty]
        public string _ConfirmPassword;


        IHttpsJsonClientProvider<RegisterDTO> _httpsJsonClientProvider;
        public RegistrationViewModel(IHttpsJsonClientProvider<RegisterDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }



        [RelayCommand]
        private void Login_Click()
        {
            _mainViewModel.SelectedViewModel = _mainViewModel.LoginViewModel;
        }

        [RelayCommand]
        private async Task Register_Click()
        {
            if (!ComprobacionEmail(Email))
            {
                Email = string.Empty;
            }
            if (!ComprobacionPassword(Password, ConfirmPassword))
            {
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
            else { 
            if(await GuardarRegistroAsync())
                {
                    var mainViewModel = App.Current.Services.GetService<MainViewModel>();
                    var LoginViewModel = App.Current.Services.GetService<LoginViewModel>();
                    mainViewModel.SelectViewModelCommand.Execute(LoginViewModel);
                 
                }
            }
        }

        private  bool ComprobacionPassword(string firstPass, string secondPass)
        {
            if (!firstPass.Equals(secondPass))
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, repítelas."); 

                return false;

            }
            if(firstPass.Length<8|| firstPass.Length > 20)
            {
                MessageBox.Show("La contraseña debe tener entre 8 y 20 caracteres.");
                return false;

            }
            if (!firstPass.Any(char.IsDigit))
            {
                MessageBox.Show("La contraseña debe contener al menos un número.");
                return false;
            }
            if (!firstPass.Any(char.IsLower))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra minúscula.");
                return false;
            }
            if (!firstPass.Any(char.IsUpper))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula.");
                return false;
            }
            var symbols = @"!"";#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
            if (!firstPass.Any(c => symbols.Contains(c)))
            {
                MessageBox.Show("La contraseña debe contener al menos un símbolo.");
                return false;
            }
            return true;



        }
        private bool ComprobacionEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("El correo electrónico no puede estar vacío.");
                return false;
            }

            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("El correo electrónico no es válido. Asegúrese de que tiene un formato correcto (ejemplo@dominio.com).");
                return false;
            }

            return true;
        }



        public override Task LoadAsync()
        {
            _mainViewModel = App.Current.Services.GetService<MainViewModel>();
            return Task.CompletedTask;
        }
        private async Task<bool> GuardarRegistroAsync()
        {
            try
            {
                

                    RegisterDTO user = new RegisterDTO
                {
                    Nombre = _Nombre,
                    Email = _Email,
                    Password = _Password,
                    Role ="Admin"
                };



                var resultado = await _httpsJsonClientProvider.RegisterPostAsync(Constantes.REGISTER_PATH, user);
               
                return resultado != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los datos del registro: {ex.Message}");
                return false;
            }
        }
    }
}

