using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TasteAndRate.Models
{
    public class CriterioNota: ObservableObject
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        private int _nota;
        public int Nota
        {
            get => _nota;
            set => SetProperty(ref _nota, value);
        }
    }
}
