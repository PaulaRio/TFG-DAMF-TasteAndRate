using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasteAndRate.Utils
{
    public static class Constantes
    {
        #region Math
        public const string Mas = "+";
        public const string Menos = "-";
        public const string Por = "x";
        public const string Division = "÷";
        public const string Resultado = "Resultado";
        public const string Pi = "π";
        #endregion

        #region WPF_Views
        public const int MAX_NUMBER_ITEMS_STACK_PANEL = 15;
        public const int MIN_NUMBER_ITEMS_STACK_PANEL = 5;
        public const string HALLOWEEN_URL_PATH = "/Resources/Halloween.png";
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        #endregion

        #region API Url
        internal const string BASE_URL = "https://localhost:7000/api/";
        internal const string LOGIN_PATH = "users";
        internal const string REGISTER_PATH = "users/register";
        internal const string IMAGES_EXTENSION = ".png";
        internal const string PATH_IMAGE_NOT_FOUND = "Not_found.png";
        internal const string GASTRO_URL = "Gastro/";
        internal const string VALORACION_URL = "Valoracion/";
        internal const string ETIQUETA_URL = "Etiqueta/";
        internal const string CRITERIO_URL = "Criterio/";
        internal const string LOGIN_URL = "Login/";
        internal const string VALORACIONCRITERIO_URL = "ValoracionCriterio/";
        internal const string IMAGEN_COMIDA = "IconoComida.png";
        #endregion
    }
}
