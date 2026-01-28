using DWES_RA9_CarlosdeAldaGarcia.Models;
using System.Text;
using System.Text.Json;

namespace DWES_RA9_CarlosdeAldaGarcia.Services
{
    /// <summary>
    /// Servicio para consumir la API REST de Indicadores
    /// Utiliza HttpClient para realizar las peticiones HTTP
    /// </summary>
    public class IndicadorApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndicadorApiService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public IndicadorApiService(HttpClient httpClient, ILogger<IndicadorApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            
            // Configuración de serialización JSON
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        #region Operaciones GET (Consulta)

        /// <summary>
        /// Obtiene todos los indicadores desde la API
        /// </summary>
        public async Task<List<IndicadorViewModel>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los indicadores desde la API");
                
                var response = await _httpClient.GetAsync("api/indicadores");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var indicadores = JsonSerializer.Deserialize<List<IndicadorViewModel>>(content, _jsonOptions);
                
                return indicadores ?? new List<IndicadorViewModel>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener indicadores desde la API");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un indicador por su ID
        /// </summary>
        public async Task<IndicadorViewModel?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo indicador con ID {Id}", id);
                
                // Obtenemos todos y filtramos por ID (la API no tiene endpoint específico por ID)
                var indicadores = await GetAllAsync();
                return indicadores.FirstOrDefault(i => i.Id == id);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener indicador {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Obtiene indicadores filtrados por tipo
        /// </summary>
        public async Task<List<IndicadorViewModel>> GetByTipoAsync(string tipo)
        {
            try
            {
                _logger.LogInformation("Obteniendo indicadores de tipo {Tipo}", tipo);
                
                var response = await _httpClient.GetAsync($"api/indicadores/tipo/{Uri.EscapeDataString(tipo)}");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var indicadores = JsonSerializer.Deserialize<List<IndicadorViewModel>>(content, _jsonOptions);
                
                return indicadores ?? new List<IndicadorViewModel>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener indicadores por tipo {Tipo}", tipo);
                throw;
            }
        }

        /// <summary>
        /// Obtiene indicadores filtrados por categoría
        /// </summary>
        public async Task<List<IndicadorViewModel>> GetByCategoriaAsync(string categoria)
        {
            try
            {
                _logger.LogInformation("Obteniendo indicadores de categoría {Categoria}", categoria);
                
                var response = await _httpClient.GetAsync($"api/indicadores/categoria/{Uri.EscapeDataString(categoria)}");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var indicadores = JsonSerializer.Deserialize<List<IndicadorViewModel>>(content, _jsonOptions);
                
                return indicadores ?? new List<IndicadorViewModel>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener indicadores por categoría {Categoria}", categoria);
                throw;
            }
        }

        /// <summary>
        /// Obtiene indicadores filtrados por ámbito
        /// </summary>
        public async Task<List<IndicadorViewModel>> GetByAmbitoAsync(string ambito)
        {
            try
            {
                _logger.LogInformation("Obteniendo indicadores de ámbito {Ambito}", ambito);
                
                var response = await _httpClient.GetAsync($"api/indicadores/ambito/{Uri.EscapeDataString(ambito)}");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var indicadores = JsonSerializer.Deserialize<List<IndicadorViewModel>>(content, _jsonOptions);
                
                return indicadores ?? new List<IndicadorViewModel>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener indicadores por ámbito {Ambito}", ambito);
                throw;
            }
        }

        #endregion

        #region Operaciones de Agregación (Resúmenes)

        /// <summary>
        /// Obtiene el total de indicadores agrupados por tipo
        /// </summary>
        public async Task<Dictionary<string, int>> GetTotalPorTipoAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo resumen por tipo");
                
                var response = await _httpClient.GetAsync("api/indicadores/total-por-tipo");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Dictionary<string, int>>(content, _jsonOptions);
                
                return resultado ?? new Dictionary<string, int>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener resumen por tipo");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el total de indicadores agrupados por categoría
        /// </summary>
        public async Task<Dictionary<string, int>> GetTotalPorCategoriaAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo resumen por categoría");
                
                var response = await _httpClient.GetAsync("api/indicadores/total-por-categoria");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Dictionary<string, int>>(content, _jsonOptions);
                
                return resultado ?? new Dictionary<string, int>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener resumen por categoría");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el total de indicadores agrupados por ámbito
        /// </summary>
        public async Task<Dictionary<string, int>> GetTotalPorAmbitoAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo resumen por ámbito");
                
                var response = await _httpClient.GetAsync("api/indicadores/total-por-ambito");
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Dictionary<string, int>>(content, _jsonOptions);
                
                return resultado ?? new Dictionary<string, int>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener resumen por ámbito");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los resúmenes en una sola llamada
        /// </summary>
        public async Task<ResumenViewModel> GetResumenCompletoAsync()
        {
            var resumen = new ResumenViewModel();
            
            // Ejecutamos las tres consultas en paralelo para mejor rendimiento
            var tareas = await Task.WhenAll(
                GetTotalPorTipoAsync(),
                GetTotalPorCategoriaAsync(),
                GetTotalPorAmbitoAsync()
            );
            
            resumen.TotalPorTipo = tareas[0];
            resumen.TotalPorCategoria = tareas[1];
            resumen.TotalPorAmbito = tareas[2];
            
            return resumen;
        }

        #endregion

        #region Operaciones CRUD (Crear, Actualizar, Eliminar)

        /// <summary>
        /// Crea un nuevo indicador en la API
        /// </summary>
        public async Task<bool> CreateAsync(IndicadorViewModel indicador)
        {
            try
            {
                _logger.LogInformation("Creando nuevo indicador: {Nombre}", indicador.Nombre);
                
                var json = JsonSerializer.Serialize(indicador, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/indicadores", content);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Indicador creado correctamente");
                    return true;
                }
                
                _logger.LogWarning("Error al crear indicador: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al crear indicador");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un indicador existente en la API
        /// </summary>
        public async Task<bool> UpdateAsync(int id, IndicadorViewModel indicador)
        {
            try
            {
                _logger.LogInformation("Actualizando indicador con ID {Id}", id);
                
                var json = JsonSerializer.Serialize(indicador, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"api/indicadores/{id}", content);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Indicador actualizado correctamente");
                    return true;
                }
                
                _logger.LogWarning("Error al actualizar indicador: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al actualizar indicador {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Elimina un indicador de la API
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Eliminando indicador con ID {Id}", id);
                
                var response = await _httpClient.DeleteAsync($"api/indicadores/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Indicador eliminado correctamente");
                    return true;
                }
                
                _logger.LogWarning("Error al eliminar indicador: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al eliminar indicador {Id}", id);
                throw;
            }
        }

        #endregion
    }
}
