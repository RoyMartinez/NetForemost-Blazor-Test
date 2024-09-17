# NetForemost Todo Application

Esta solución contiene tres proyectos principales: un cliente Blazor, una API y un conjunto de pruebas unitarias. A continuación se describe cada uno de los proyectos y cómo configurarlos y ejecutarlos.

## Proyectos

### 1. Cliente Blazor

El cliente Blazor es una aplicación web que proporciona una interfaz de usuario para interactuar con la API de Todo. Utiliza Blazor WebAssembly para construir una aplicación de una sola página (SPA).

#### Características

- Interfaz de usuario para gestionar tareas (crear, leer, actualizar, eliminar).
- Comunicación con la API de Todo para realizar operaciones CRUD.

#### Configuración y Ejecución

1. Navega al directorio del proyecto Blazor.
2. Ejecuta el siguiente comando para restaurar las dependencias y ejecutar la aplicación:

    ```sh
    dotnet run
    ```

3. Abre un navegador web y navega a `https://localhost:5001` para ver la aplicación en acción.

### 2. API de Todo

La API de Todo es una aplicación ASP.NET Core que proporciona endpoints para gestionar tareas. Utiliza Entity Framework Core para interactuar con una base de datos.

#### Características

- Endpoints para operaciones CRUD en tareas.
- Autenticación y autorización.
- Validación de datos.

#### Configuración y Ejecución

1. Navega al directorio del proyecto API.
2. Ejecuta el siguiente comando para restaurar las dependencias y ejecutar la aplicación:

    ```sh
    dotnet run
    ```

3. La API estará disponible en `https://localhost:5001/api/todo`.

### 3. Pruebas Unitarias

El proyecto de pruebas unitarias contiene pruebas para verificar la funcionalidad de la API de Todo. Utiliza xUnit y Moq para las pruebas.

#### Características

- Pruebas unitarias para los servicios de la API.
- Mocking de dependencias para pruebas aisladas.

#### Ejecución de Pruebas

1. Navega al directorio del proyecto de pruebas unitarias.
2. Ejecuta el siguiente comando para ejecutar todas las pruebas:

    ```sh
    dotnet test
    ```

## Estructura del Proyecto

La estructura del proyecto es la siguiente:
## Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/)

## Configuración Inicial

1. Clona el repositorio:

    ```sh
    git clone https://github.com/tu-usuario/NetForemost-Todo.git
    ```

2. Navega al directorio del proyecto:

    ```sh
    cd NetForemost-Todo
    ```

3. Restaura las dependencias para todos los proyectos:

    ```sh
    dotnet restore
    ```
