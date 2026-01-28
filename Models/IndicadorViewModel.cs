using System.ComponentModel.DataAnnotations;

namespace DWES_RA9_CarlosdeAldaGarcia.Models
{
    /// <summary>
    /// ViewModel para representar un Indicador con validaciones para la UI
    /// </summary>
    public class IndicadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [Display(Name = "Tipo")]
        [StringLength(50, ErrorMessage = "El tipo no puede exceder 50 caracteres")]
        public string Tipo { get; set; } = null!;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        [StringLength(100, ErrorMessage = "La categoría no puede exceder 100 caracteres")]
        public string Categoria { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El valor es obligatorio")]
        [Display(Name = "Valor")]
        public string Valor { get; set; } = null!;

        [Display(Name = "Unidad de medida")]
        [StringLength(50, ErrorMessage = "La unidad no puede exceder 50 caracteres")]
        public string? Unidad { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El ámbito es obligatorio")]
        [Display(Name = "Ámbito")]
        [StringLength(50, ErrorMessage = "El ámbito no puede exceder 50 caracteres")]
        public string Ambito { get; set; } = null!;
    }

    /// <summary>
    /// ViewModel para mostrar resúmenes estadísticos
    /// </summary>
    public class ResumenViewModel
    {
        public Dictionary<string, int> TotalPorTipo { get; set; } = new();
        public Dictionary<string, int> TotalPorCategoria { get; set; } = new();
        public Dictionary<string, int> TotalPorAmbito { get; set; } = new();
    }

    /// <summary>
    /// ViewModel para la página principal con listado y filtros
    /// </summary>
    public class IndicadoresIndexViewModel
    {
        public List<IndicadorViewModel> Indicadores { get; set; } = new();
        public string? FiltroTipo { get; set; }
        public string? FiltroCategoria { get; set; }
        public string? FiltroAmbito { get; set; }
        public List<string> TiposDisponibles { get; set; } = new();
        public List<string> CategoriasDisponibles { get; set; } = new();
        public List<string> AmbitosDisponibles { get; set; } = new();
    }
}
