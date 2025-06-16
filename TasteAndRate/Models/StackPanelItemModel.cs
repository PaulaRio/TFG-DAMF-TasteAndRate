using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;
using TasteAndRate.Utils;

namespace TasteAndRate.Models
{
    public class StackPanelItemModel
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string NombreEtiqueta { get; set; }

        public string Direccion { get; set; }

        public double NotaFinal { get; set; }

        public string Foto { get; set; }



        internal static StackPanelItemModel CreateModelFromDTO(ValoracionDTO objeto,string etiqueta)
        {
            return new StackPanelItemModel
            {
                
                Nombre = objeto.Gastro.Nombre,
                Descripcion = objeto.Descripcion,
                NombreEtiqueta = etiqueta,
                NotaFinal = objeto.NotaFinal,
                Direccion= objeto.Gastro.Direccion,

                Foto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", Constantes.IMAGEN_COMIDA)
               
            };
        }

    }
}
