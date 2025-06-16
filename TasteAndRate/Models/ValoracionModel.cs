using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TasteAndRate.DTOs;
using TasteAndRate.Utils;

namespace TasteAndRate.Models
{
    public class ValoracionModel
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }

        public string Descripcion { get; set; }
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public int EtiquetaId { get; set; }

        public string NombreEtiqueta { get; set; }
        public string Foto { get; set; } 
        //public DateTime FechaCreacion { get; set; }
        public double NotaFinal { get; set; }



        internal static ValoracionModel CreateModelFromDTO(ValoracionDTO valoracion,string nombreEtiqueta)
        {
            return new ValoracionModel
            {   
                Id = valoracion.Id,
                Nombre = valoracion.Gastro?.Nombre,          
                Direccion = valoracion.Gastro?.Direccion,    
                EtiquetaId = valoracion.EtiquetaId,
                NotaFinal = valoracion.NotaFinal,
                NombreEtiqueta= nombreEtiqueta,
                Foto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources",
                Constantes.IMAGEN_COMIDA)
            };
        }


    }
}
