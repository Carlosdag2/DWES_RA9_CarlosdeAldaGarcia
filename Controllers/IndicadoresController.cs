using DWES_RA9_CarlosdeAldaGarcia.Models;
using DWES_RA9_CarlosdeAldaGarcia.Services;
using Microsoft.AspNetCore.Mvc;

namespace DWES_RA9_CarlosdeAldaGarcia.Controllers
{
    /// <summary>
    /// Controlador MVC para gestionar indicadores consumiendo la API REST
    /// </summary>
    public class IndicadoresController : Controller
    {
        private readonly IndicadorApiService _apiService;
        private readonly ILogger<IndicadoresController> _logger;

        public IndicadoresController(IndicadorApiService apiService, ILogger<IndicadoresController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        #region Listado y Consultas

        /// <summary>
        /// GET: /Indicadores
        /// Muestra el listado de indicadores con opción de filtrado
        /// </summary>
        public async Task<IActionResult> Index(string? filtroTipo, string? filtroCategoria, string? filtroAmbito)
        {
            try
            {
                List<IndicadorViewModel> indicadores;

                // Aplicar filtro según el criterio seleccionado
                if (!string.IsNullOrEmpty(filtroTipo))
                {
                    indicadores = await _apiService.GetByTipoAsync(filtroTipo);
                }
                else if (!string.IsNullOrEmpty(filtroCategoria))
                {
                    indicadores = await _apiService.GetByCategoriaAsync(filtroCategoria);
                }
                else if (!string.IsNullOrEmpty(filtroAmbito))
                {
                    indicadores = await _apiService.GetByAmbitoAsync(filtroAmbito);
                }
                else
                {
                    indicadores = await _apiService.GetAllAsync();
                }

                // Obtener listas únicas para los desplegables de filtros
                var todosIndicadores = await _apiService.GetAllAsync();
                
                var viewModel = new IndicadoresIndexViewModel
                {
                    Indicadores = indicadores,
                    FiltroTipo = filtroTipo,
                    FiltroCategoria = filtroCategoria,
                    FiltroAmbito = filtroAmbito,
                    TiposDisponibles = todosIndicadores.Select(i => i.Tipo).Distinct().OrderBy(t => t).ToList(),
                    CategoriasDisponibles = todosIndicadores.Select(i => i.Categoria).Distinct().OrderBy(c => c).ToList(),
                    AmbitosDisponibles = todosIndicadores.Select(i => i.Ambito).Distinct().OrderBy(a => a).ToList()
                };

                return View(viewModel);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al conectar con la API");
                TempData["Error"] = "No se pudo conectar con el servidor. Por favor, verifique que la API esté en ejecución.";
                return View(new IndicadoresIndexViewModel());
            }
        }

        /// <summary>
        /// GET: /Indicadores/Details/5
        /// Muestra los detalles de un indicador específico
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var indicador = await _apiService.GetByIdAsync(id);
                
                if (indicador == null)
                {
                    TempData["Error"] = "El indicador solicitado no existe.";
                    return RedirectToAction(nameof(Index));
                }

                return View(indicador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener detalles del indicador {Id}", id);
                TempData["Error"] = "Error al obtener los detalles del indicador.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: /Indicadores/Resumen
        /// Muestra estadísticas y resúmenes de los indicadores
        /// </summary>
        public async Task<IActionResult> Resumen()
        {
            try
            {
                var resumen = await _apiService.GetResumenCompletoAsync();
                return View(resumen);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener resumen");
                TempData["Error"] = "Error al obtener las estadísticas.";
                return View(new ResumenViewModel());
            }
        }

        #endregion

        #region Crear Indicador

        /// <summary>
        /// GET: /Indicadores/Create
        /// Muestra el formulario para crear un nuevo indicador
        /// </summary>
        public IActionResult Create()
        {
            return View(new IndicadorViewModel { Fecha = DateTime.Now });
        }

        /// <summary>
        /// POST: /Indicadores/Create
        /// Procesa la creación de un nuevo indicador
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndicadorViewModel indicador)
        {
            if (!ModelState.IsValid)
            {
                return View(indicador);
            }

            try
            {
                var resultado = await _apiService.CreateAsync(indicador);
                
                if (resultado)
                {
                    TempData["Success"] = "Indicador creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                
                TempData["Error"] = "No se pudo crear el indicador.";
                return View(indicador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al crear indicador");
                TempData["Error"] = "Error de conexión al crear el indicador.";
                return View(indicador);
            }
        }

        #endregion

        #region Editar Indicador

        /// <summary>
        /// GET: /Indicadores/Edit/5
        /// Muestra el formulario para editar un indicador existente
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var indicador = await _apiService.GetByIdAsync(id);
                
                if (indicador == null)
                {
                    TempData["Error"] = "El indicador solicitado no existe.";
                    return RedirectToAction(nameof(Index));
                }

                return View(indicador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al cargar indicador para edición {Id}", id);
                TempData["Error"] = "Error al cargar el indicador.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /Indicadores/Edit/5
        /// Procesa la actualización de un indicador
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IndicadorViewModel indicador)
        {
            if (id != indicador.Id)
            {
                TempData["Error"] = "ID de indicador no válido.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(indicador);
            }

            try
            {
                var resultado = await _apiService.UpdateAsync(id, indicador);
                
                if (resultado)
                {
                    TempData["Success"] = "Indicador actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                
                TempData["Error"] = "No se pudo actualizar el indicador.";
                return View(indicador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al actualizar indicador {Id}", id);
                TempData["Error"] = "Error de conexión al actualizar el indicador.";
                return View(indicador);
            }
        }

        #endregion

        #region Eliminar Indicador

        /// <summary>
        /// GET: /Indicadores/Delete/5
        /// Muestra la confirmación para eliminar un indicador
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var indicador = await _apiService.GetByIdAsync(id);
                
                if (indicador == null)
                {
                    TempData["Error"] = "El indicador solicitado no existe.";
                    return RedirectToAction(nameof(Index));
                }

                return View(indicador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al cargar indicador para eliminar {Id}", id);
                TempData["Error"] = "Error al cargar el indicador.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /Indicadores/Delete/5
        /// Procesa la eliminación de un indicador
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var resultado = await _apiService.DeleteAsync(id);
                
                if (resultado)
                {
                    TempData["Success"] = "Indicador eliminado correctamente.";
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el indicador.";
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al eliminar indicador {Id}", id);
                TempData["Error"] = "Error de conexión al eliminar el indicador.";
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion
    }
}
