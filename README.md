# Actividad Evaluable RA9 - Carlos de Alda García

**Alumno:** Carlos de Alda García

**Proyecto:** Actividad Evaluable RA9

**Curso:** DAW 

**Fecha:** 31/01/2026

---

## Índice

1. [Introducción](#introducción)
2. [Descripción del Proyecto](#descripción-del-proyecto)
3. [Arquitectura del Sistema](#arquitectura-del-sistema)
4. [Tecnologías Utilizadas](#tecnologías-utilizadas)
5. [Estructura del Proyecto](#estructura-del-proyecto)
6. [Integración del API REST](#integración-del-api-rest)
7. [Decisiones de Diseño](#decisiones-de-diseño)
8. [Vistas Implementadas](#vistas-implementadas)
9. [Funcionalidades](#funcionalidades)

---

## Introducción

Este proyecto consiste en la creación de una **aplicación web MVC** que consume un **API REST** previamente desarrollado. El objetivo principal es demostrar la capacidad de integrar dos aplicaciones .NET independientes, donde una actúa como **backend (API REST)** y otra como **frontend (MVC)**, permitiendo gestionar indicadores de sostenibilidad y digitalización de forma visual e interactiva.

La idea es separar la lógica de negocio y acceso a datos (API) de la presentación y experiencia de usuario (MVC), siguiendo el patrón arquitectónico **cliente-servidor**.

---

## Descripción del Proyecto

### Contexto

EcoData Solutions S.L. es una empresa ficticia comprometida con la transformación digital sostenible. El proyecto "DWES + Digitalización + Sostenibilidad" busca centralizar la gestión de **indicadores ambientales, sociales y económicos** a través de una plataforma digital moderna.

### Objetivos

1. **Desarrollar un API REST** que permita realizar operaciones CRUD sobre indicadores de sostenibilidad
2. **Crear una aplicación MVC** que consuma dicho API y ofrezca una interfaz amigable para los usuarios
3. **Implementar filtros y estadísticas** para facilitar el análisis de datos
4. **Diseñar una landing page informativa** que explique el contexto del proyecto y la empresa

### ¿Qué son los Indicadores?

Los indicadores son **métricas cuantificables** que nos permiten medir el impacto en diferentes ámbitos:

- **Ambiental**: Emisiones de CO2, consumo energético, gestión de residuos
- **Social**: Formación de empleados, igualdad de género, condiciones laborales
- **Económico**: Inversión en I+D, proveedores locales, crecimiento sostenible

Cada indicador tiene:
- **Tipo**: Sostenibilidad o Digitalización
- **Categoría**: Ambiental, Social, Económico, TIC, o Automatización
- **Nombre y descripción**
- **Valor numérico y unidad de medida**
- **Fecha de registro**
- **Ámbito**: Local, Regional, Nacional, Internacional

---

## Arquitectura del Sistema

### Patrón Cliente-Servidor

El proyecto utiliza una **arquitectura distribuida** con dos aplicaciones separadas:

```
┌─────────────────────┐          HTTP/JSON          ┌─────────────────────┐
│                     │   ─────────────────────>    │                     │
│   Frontend MVC      │                             │   Backend API       │
│  (Cliente Web)      │   <─────────────────────    │  (Servidor REST)    │
│                     │                             │                     │
└─────────────────────┘                             └─────────────────────┘
         │                                                     │
         │                                                     │
         v                                                     v
   Vistas Razor                                        Base de Datos
   Bootstrap UI                                         (SQL Server)
```

### ¿Por qué separar en dos aplicaciones?

**Ventajas:**

1. **Separación de responsabilidades**: El API se encarga solo de la lógica de negocio, el MVC solo de la presentación
2. **Reutilización**: El mismo API puede ser consumido por múltiples clientes (MVC, móvil, SPA)
3. **Escalabilidad**: Podemos escalar el backend y frontend de forma independiente
4. **Mantenimiento**: Es más fácil mantener y actualizar cada parte por separado
5. **Testing**: Podemos probar el API de forma aislada sin depender de la UI

**Desventajas:**

1. Mayor complejidad inicial
2. Necesidad de ejecutar dos aplicaciones
3. Gestión de errores de red
4. Posibles problemas de CORS (Cross-Origin Resource Sharing)

### Flujo de Comunicación

1. **Usuario** accede a la aplicación MVC a través del navegador
2. **MVC** solicita datos al API REST mediante HTTP (GET, POST, PUT, DELETE)
3. **API REST** procesa la petición, consulta/modifica la base de datos
4. **API REST** devuelve una respuesta en formato JSON
5. **MVC** deserializa el JSON y lo convierte en objetos C#
6. **MVC** pasa los datos a las vistas Razor
7. **Vista Razor** renderiza HTML con los datos
8. **Usuario** visualiza el resultado en el navegador

---

## Tecnologías Utilizadas

### Backend (API REST)

| Tecnología | Versión | Uso |
|------------|---------|-----|
| .NET | 8.0 | Framework principal |
| ASP.NET Core Web API | 8.0 | Creación del API REST |
| Entity Framework Core | 8.0 | ORM para acceso a datos |
| SQL Server | - | Base de datos |
| C# | 12.0 | Lenguaje de programación |
| Swagger | - | Documentación del API |

### Frontend (MVC)

| Tecnología | Versión | Uso |
|------------|---------|-----|
| .NET | 8.0 | Framework principal |
| ASP.NET Core MVC | 8.0 | Patrón MVC |
| C# | 12.0 | Lenguaje de programación |
| Razor | 8.0 | Motor de vistas |
| Bootstrap | 5.3 | Framework CSS |
| Bootstrap Icons | 1.11 | Iconografía |
| HttpClient | - | Consumo del API REST |
| System.Text.Json | - | Serialización JSON |

### Herramientas de Desarrollo

- **Visual Studio 2026**: IDE principal
- **Git**: Control de versiones
- **GitHub**: Repositorio remoto

---

## Estructura del Proyecto

### Proyecto MVC: `DWES_RA9_CarlosdeAldaGarcia`

```
DWES_RA9_CarlosdeAldaGarcia/
│
├── Controllers/
│   ├── HomeController.cs             # Controlador de páginas estáticas
│   └── IndicadoresController.cs      # Controlador de indicadores
│
├── Models/
│   └── IndicadorViewModel.cs         # ViewModels para las vistas
│
├── Services/
│   └── IndicadorApiService.cs        # Servicio para consumir el API
│
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml              # Página de inicio
│   │   └── Privacy.cshtml            # Acerca de
│   ├── Indicadores/
│   │   ├── Index.cshtml              # Listado de indicadores
│   │   ├── Create.cshtml             # Crear indicador
│   │   ├── Edit.cshtml               # Editar indicador
│   │   ├── Delete.cshtml             # Eliminar indicador
│   │   ├── Details.cshtml            # Detalles de indicador
│   │   └── Resumen.cshtml            # Estadísticas
│   └── Shared/
│       └── _Layout.cshtml            # Plantilla maestra
│
├── wwwroot/
│   └── css/
│       └── site.css                  # Estilos personalizados
│
├── appsettings.json                  # Configuración (URL del API)
└── Program.cs                        # Configuración de la aplicación
```

---

## Integración del API REST

### 1. Configuración en `appsettings.json`

Primero, he configurado la **URL base del API** en el archivo de configuración:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7097"
  }
}
```

**Razonamiento**: Usar el archivo de configuración permite cambiar fácilmente la URL sin modificar el código (por ejemplo, cuando pasamos a producción).

### 2. Creación del Servicio `IndicadorApiService`

He creado una clase de servicio que **encapsula toda la comunicación con el API**:

```csharp
public class IndicadorApiService
{
    private readonly HttpClient _httpClient;

    public IndicadorApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Métodos para consumir el API
}
```

**¿Por qué usar HttpClient?**

- Es la clase estándar de .NET para realizar peticiones HTTP
- Soporta operaciones asíncronas (async/await)
- Permite enviar y recibir JSON fácilmente
- Se integra con el sistema de inyección de dependencias

### 3. Registro del Servicio en `Program.cs`

He configurado la **inyección de dependencias** para el `HttpClient`:

```csharp
builder.Services.AddHttpClient<IndicadorApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});
```

**Ventajas de esta configuración:**

1. **Inyección de dependencias**: No creamos instancias manualmente
2. **Configuración centralizada**: La URL base se lee del `appsettings.json`
3. **Headers por defecto**: Indicamos que aceptamos JSON
4. **Gestión del ciclo de vida**: .NET maneja la creación/destrucción del HttpClient

### 4. Métodos Implementados

#### GET - Obtener todos los indicadores

```csharp
public async Task<List<IndicadorViewModel>> GetAllAsync()
{
    var response = await _httpClient.GetAsync("api/indicadores");
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<List<IndicadorViewModel>>(content, _jsonOptions) 
        ?? new List<IndicadorViewModel>();
}
```

**Explicación paso a paso:**

1. `GetAsync()`: Realiza una petición GET al endpoint
2. `EnsureSuccessStatusCode()`: Lanza excepción si el código no es 2xx
3. `ReadAsStringAsync()`: Lee el contenido de la respuesta como string
4. `Deserialize()`: Convierte el JSON en objetos C#
5. Operador `??`: Si es null, devuelve lista vacía

#### GET con filtros

He implementado varios métodos de filtrado:

```csharp
public async Task<List<IndicadorViewModel>> GetByTipoAsync(string tipo)
{
    var response = await _httpClient.GetAsync($"api/indicadores/tipo/{tipo}");
    // ... resto del código
}

public async Task<List<IndicadorViewModel>> GetByCategoriaAsync(string categoria)
{
    var response = await _httpClient.GetAsync($"api/indicadores/categoria/{categoria}");
    // ... resto del código
}
```

**Nota importante**: Uso **interpolación de strings** (`$"..."`) para construir la URL con parámetros.

#### POST - Crear indicador

```csharp
public async Task<IndicadorViewModel?> CreateAsync(IndicadorViewModel indicador)
{
    var json = JsonSerializer.Serialize(indicador, _jsonOptions);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    
    var response = await _httpClient.PostAsync("api/indicadores", content);
    response.EnsureSuccessStatusCode();
    
    var responseContent = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<IndicadorViewModel>(responseContent, _jsonOptions);
}
```

**Proceso:**

1. Serializar el objeto a JSON
2. Crear un `StringContent` con el JSON (especificando UTF-8 y tipo de contenido)
3. Enviar POST al API
4. Deserializar la respuesta (el API devuelve el objeto creado)

#### PUT - Actualizar indicador

```csharp
public async Task<bool> UpdateAsync(int id, IndicadorViewModel indicador)
{
    var json = JsonSerializer.Serialize(indicador, _jsonOptions);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    
    var response = await _httpClient.PutAsync($"api/indicadores/{id}", content);
    return response.IsSuccessStatusCode;
}
```

**Diferencia con POST**: 

- PUT requiere el ID en la URL
- Devuelvo un `bool` para indicar éxito/fracaso

#### DELETE - Eliminar indicador

```csharp
public async Task<bool> DeleteAsync(int id)
{
    var response = await _httpClient.DeleteAsync($"api/indicadores/{id}");
    return response.IsSuccessStatusCode;
}
```

**El más simple**: Solo necesita el ID y devuelve si tuvo éxito.

### 5. Gestión de Errores

He implementado un manejo básico de excepciones en el controlador:

```csharp
public async Task<IActionResult> Index(string? tipo, string? categoria, string? ambito)
{
    try
    {
        // Lógica de filtrado
    }
    catch (HttpRequestException ex)
    {
        TempData["Error"] = "Error al conectar con el servidor: " + ex.Message;
        return View(new IndicadoresIndexViewModel());
    }
}
```

**Buenas prácticas:**

- Capturo `HttpRequestException` específicamente (errores de red/HTTP)
- Uso `TempData` para mostrar mensajes de error en la vista
- Devuelvo un ViewModel vacío en caso de error para evitar que la vista falle

---

## Decisiones de Diseño

### 1. Uso de ViewModels

**Decisión**: Crear clases `ViewModel` separadas de los DTOs del API.

**Razonamiento:**

- **Separación de responsabilidades**: Los ViewModels tienen validaciones específicas de la UI
- **Flexibilidad**: Puedo agregar propiedades que no existen en el API (como listas de selección)
- **Validaciones**: Uso `DataAnnotations` específicas para formularios

```csharp
public class IndicadorViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El tipo es obligatorio")]
    public string Tipo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    // ... más propiedades con validaciones
}
```

### 2. Sistema de Filtros

**Decisión**: Implementar filtros **acumulativos** en la vista Index.

**Implementación:**

```csharp
public async Task<IActionResult> Index(string? tipo, string? categoria, string? ambito)
{
    List<IndicadorViewModel> indicadores;
    
    // Filtrar por los parámetros proporcionados
    if (!string.IsNullOrEmpty(tipo))
        indicadores = await _apiService.GetByTipoAsync(tipo);
    else if (!string.IsNullOrEmpty(categoria))
        indicadores = await _apiService.GetByCategoriaAsync(categoria);
    else if (!string.IsNullOrEmpty(ambito))
        indicadores = await _apiService.GetByAmbitoAsync(ambito);
    else
        indicadores = await _apiService.GetAllAsync();
    
    // ... resto del código
}
```

**Razonamiento:**

- **Prioridad**: Tipo > Categoría > Ámbito
- **Eficiencia**: Evito múltiples llamadas al API
- **UX**: El usuario ve inmediatamente los resultados filtrados
- **Query strings**: Los filtros se reflejan en la URL (`?tipo=Sostenibilidad`)

### 3. Vista de Resumen/Estadísticas

**Decisión**: Crear una vista separada para mostrar estadísticas agregadas.

**Implementación:**

```csharp
public async Task<IActionResult> Resumen()
{
    var totalPorTipo = await _apiService.GetTotalPorTipoAsync();
    var totalPorCategoria = await _apiService.GetTotalPorCategoriaAsync();
    var totalPorAmbito = await _apiService.GetTotalPorAmbitoAsync();
    
    var viewModel = new ResumenViewModel
    {
        TotalPorTipo = totalPorTipo,
        TotalPorCategoria = totalPorCategoria,
        TotalPorAmbito = totalPorAmbito
    };
    
    return View(viewModel);
}
```

**Características:**

- Uso de **múltiples llamadas asíncronas**
- Creación de un `ResumenViewModel` específico
- Visualización con gráficos de progreso (progress bars)

### 4. Diseño Responsive

**Decisión**: Usar Bootstrap 5 para garantizar que la aplicación funcione en todos los dispositivos.

**Implementación:**

```html
<div class="row g-4">
    <div class="col-md-6 col-lg-4">
        <!-- Card responsive: 
             - 1 columna en móvil
             - 2 columnas en tablet (md)
             - 3 columnas en desktop (lg)
        -->
    </div>
</div>
```

**Clases Bootstrap utilizadas:**

- `container`: Centra el contenido
- `row`: Fila del grid
- `col-*`: Columnas responsivas
- `g-4`: Espaciado entre elementos
- `mb-*`, `mt-*`, `py-*`: Márgenes y padding

### 5. Landing Page Informativa

**Decisión**: Crear una página de inicio atractiva que explique el proyecto.

**Secciones implementadas:**

1. **Hero Section**: Presentación principal con estadísticas
2. **Accesos Rápidos**: Cards clickables para navegación
3. **Sobre la Empresa**: Información de EcoData Solutions
4. **Los Tres Pilares**: Explicación de sostenibilidad
5. **Digitalización**: Ventajas de la transformación digital
6. **ODS**: Los 17 Objetivos de Desarrollo Sostenible
7. **Arquitectura**: Diagrama del sistema

**Estilo visual:**

```css
/* Hero con gradiente verde */
.hero-section {
    background: linear-gradient(135deg, #10b981 0%, #059669 100%);
}

/* Cards con hover effect */
.nav-card {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.nav-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15) !important;
}
```

---

## Vistas Implementadas

### 1. Home/Index.cshtml - Página de Inicio

**Propósito**: Presentar el proyecto y la empresa, facilitar la navegación.

**Secciones:**

- **Hero**: Título principal y botones de acción
- **Accesos Rápidos**: 3 cards para Ver/Crear/Estadísticas
- **Empresa**: Descripción de EcoData Solutions
- **Pilares**: Ambiental, Social, Económico
- **Digitalización**: 4 beneficios clave
- **ODS**: 17 objetivos con colores oficiales
- **Arquitectura**: Diagrama cliente-servidor

**Decisiones de diseño:**

- Colores corporativos: Verde (#10b981) para sostenibilidad
- Icons de Bootstrap Icons para mejor UX
- Cards interactivos con hover effects
- Responsive en todos los dispositivos

### 2. Indicadores/Index.cshtml - Listado

**Propósito**: Mostrar todos los indicadores con opciones de filtrado.

**Características:**

```html
<!-- Formulario de filtros -->
<form method="get" class="row g-3 mb-4">
    <div class="col-md-4">
        <select name="tipo" class="form-select">
            <option value="">-- Todos los tipos --</option>
            <option value="Sostenibilidad">Sostenibilidad</option>
            <option value="Digitalización">Digitalización</option>
        </select>
    </div>
    <!-- ... más filtros -->
</form>

<!-- Tabla de resultados -->
<table class="table table-hover">
    @foreach (var item in Model.Indicadores)
    {
        <tr>
            <td>@item.Nombre</td>
            <!-- ... más columnas -->
        </tr>
    }
</table>
```

**Funcionalidades:**

- Filtros por Tipo, Categoría y Ámbito
- Tabla responsive con scroll horizontal en móvil
- Badges de colores para tipos y categorías
- Botones de acción (Ver, Editar, Eliminar)
- Formateo de fechas y números

### 3. Indicadores/Create.cshtml - Crear

**Propósito**: Formulario para añadir nuevos indicadores.

**Validaciones implementadas:**

```html
<div class="mb-3">
    <label asp-for="Nombre" class="form-label"></label>
    <input asp-for="Nombre" class="form-control" />
    <span asp-validation-for="Nombre" class="text-danger"></span>
</div>
```

**Tag Helpers utilizados:**

- `asp-for`: Enlaza con la propiedad del modelo
- `asp-validation-for`: Muestra errores de validación
- `asp-items`: Carga opciones de select desde el ViewBag

**Flujo:**

1. Usuario rellena el formulario
2. Cliente valida con JavaScript (validación del lado del cliente)
3. Al enviar, se valida en el servidor (ModelState)
4. Si es válido, se llama al API con POST
5. Se redirige al Index con mensaje de éxito

### 4. Indicadores/Edit.cshtml - Editar

**Propósito**: Modificar un indicador existente.

**Diferencias con Create:**

```html
<!-- Campo oculto con el ID -->
<input type="hidden" asp-for="Id" />

<!-- Campos pre-poblados -->
<input asp-for="Nombre" class="form-control" value="@Model.Nombre" />
```

**Flujo:**

1. GET: Obtener indicador del API por ID
2. Mostrar formulario pre-poblado
3. Usuario modifica campos
4. POST: Validar y enviar al API con PUT
5. Redirigir con mensaje de confirmación

### 5. Indicadores/Delete.cshtml - Eliminar

**Propósito**: Confirmación antes de eliminar.

**Diseño:**

```html
<div class="alert alert-warning">
    <i class="bi bi-exclamation-triangle"></i>
    ¿Está seguro de que desea eliminar este indicador?
</div>

<dl class="row">
    <dt class="col-sm-3">Nombre</dt>
    <dd class="col-sm-9">@Model.Nombre</dd>
    <!-- ... más detalles -->
</dl>

<form method="post">
    <button type="submit" class="btn btn-danger">Eliminar</button>
    <a asp-action="Index" class="btn btn-secondary">Cancelar</a>
</form>
```

**Seguridad:**

- Requiere POST para eliminar (no se puede eliminar con un simple link)
- Muestra todos los detalles del indicador
- Botón de cancelar siempre visible

### 6. Indicadores/Details.cshtml - Detalles

**Propósito**: Mostrar información completa de un indicador.

**Presentación:**

```html
<div class="card">
    <div class="card-header">
        <h5>@Model.Nombre</h5>
    </div>
    <div class="card-body">
        <dl class="row">
            <dt class="col-sm-3">Tipo</dt>
            <dd class="col-sm-9">
                <span class="badge bg-@(Model.Tipo == "Sostenibilidad" ? "success" : "info")">
                    @Model.Tipo
                </span>
            </dd>
            <!-- ... más campos -->
        </dl>
    </div>
</div>
```

**Características:**

- Solo lectura
- Formato amigable de fechas
- Badges de colores
- Botones para Editar/Eliminar/Volver

### 7. Indicadores/Resumen.cshtml - Estadísticas

**Propósito**: Visualizar métricas agregadas.

**Gráficos con Progress Bars:**

```html
<div class="mb-4">
    <h5>Por Tipo</h5>
    @foreach (var item in Model.TotalPorTipo)
    {
        <div class="mb-2">
            <div class="d-flex justify-content-between mb-1">
                <span>@item.Key</span>
                <span class="badge bg-primary">@item.Value</span>
            </div>
            <div class="progress">
                <div class="progress-bar" style="width: @((item.Value * 100 / total))%"></div>
            </div>
        </div>
    }
</div>
```

**Secciones:**

1. Total por Tipo (Sostenibilidad vs Digitalización)
2. Total por Categoría (Ambiental, Social, Económico, TIC, Automatización)
3. Total por Ámbito (Local, Regional, Nacional, etc.)

**Cálculos:**

- Porcentajes dinámicos basados en el total
- Colores diferentes para cada categoría
- Ordenamiento por cantidad (mayor a menor)

---

## Funcionalidades

### CRUD Completo

| Operación | Método HTTP | Endpoint | Vista |
|-----------|-------------|----------|-------|
| **Create** (Crear) | POST | `/api/indicadores` | Create.cshtml |
| **Read** (Leer) | GET | `/api/indicadores` | Index.cshtml |
| **Read One** (Leer uno) | GET | `/api/indicadores/{id}` | Details.cshtml |
| **Update** (Actualizar) | PUT | `/api/indicadores/{id}` | Edit.cshtml |
| **Delete** (Eliminar) | DELETE | `/api/indicadores/{id}` | Delete.cshtml |

### Filtros Avanzados

```csharp
// Filtro por Tipo
GET /api/indicadores/tipo/Sostenibilidad

// Filtro por Categoría
GET /api/indicadores/categoria/Ambiental

// Filtro por Ámbito
GET /api/indicadores/ambito/Nacional
```

**Implementación en la UI:**

- Dropdowns en la vista Index
- Submit automático del formulario
- URL con query strings para compartir filtros

### Estadísticas

```csharp
// Total por Tipo
GET /api/indicadores/total-por-tipo
// Respuesta: { "Sostenibilidad": 15, "Digitalización": 8 }

// Total por Categoría
GET /api/indicadores/total-por-categoria
// Respuesta: { "Ambiental": 10, "Social": 5, "Económico": 3, ... }

// Total por Ámbito
GET /api/indicadores/total-por-ambito
// Respuesta: { "Local": 7, "Regional": 5, "Nacional": 8, ... }
```

**Visualización:**

- Progress bars con porcentajes
- Badges con números
- Gráficos responsivos

### Validaciones

**Del lado del cliente (JavaScript):**

- Campos obligatorios marcados con `*`
- Validación en tiempo real
- Mensajes de error bajo cada campo

**Del lado del servidor (C#):**

```csharp
[Required(ErrorMessage = "El nombre es obligatorio")]
[StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
public string Nombre { get; set; }

[Range(0, double.MaxValue, ErrorMessage = "El valor debe ser positivo")]
public decimal Valor { get; set; }
```

### Mensajes de Feedback

**Uso de TempData:**

```csharp
// En el controlador
TempData["Success"] = "Indicador creado correctamente";
TempData["Error"] = "Error al crear el indicador";

// En la vista
@if (TempData["Success"] != null)
{
    <div class="alert alert-success">@TempData["Success"]</div>
}
```

---

## Información del Autor

**Nombre:** Carlos de Alda García  
**Proyecto:** DWES_RA9 - Integración API REST con MVC  
**Curso:** Desarrollo Web en Entorno Servidor  
**Año:** 2026

**Repositorios:**
- API REST: [https://github.com/Carlosdag2/DigitalizacionSostenibilidad](https://github.com/Carlosdag2/DigitalizacionSostenibilidad)
- Cliente MVC: [https://github.com/Carlosdag2/DWES_RA9_CarlosdeAldaGarcia](https://github.com/Carlosdag2/DWES_RA9_CarlosdeAldaGarcia)

---