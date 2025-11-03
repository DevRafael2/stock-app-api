# StockApp - Backend

Este proyecto implementa una arquitectura robusta basada en principios de diseño y patrones de software que permiten escalabilidad, mantenibilidad y separación clara de responsabilidades.

## Tecnologías
- .Net 9.0
- PostgreSQL 18


## 🏛️ Arquitectura y Patrones Utilizados

- **CQRS (Command Query Responsibility Segregation)**
- **Repositorio (Repository Pattern)**
- **Unit of Work**
- **Mediator Pattern (MediatR)**
- **Builder Pattern**
- **Ports & Adapters (Arquitectura Hexagonal)**

## 📚 Librerías Principales

| Librería | Uso |
|---------|-----|
| **MediatR** | Manejo de comandos y consultas (Mediator Pattern) |
| **Mapster** | Mapeo entre DTOs y entidades |
| **Entity Framework Core** | ORM para acceso y gestión de base de datos |

## ⚙️ Configuración e Instalación

1. **Configurar la Cadena de Conexión**  
   Editar `appsettings.json` y actualizar la cadena de conexión:

```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=StockAppDev;Username=postgres;Password=tu_password"
}
```

2. **Crear la Base de Datos**
   Debe existir una base llamada **StockAppDev** en PostgreSQL.

3. **Ejecutar el Script de Inicialización**
   Ejecutar el archivo adjunto **query-postgree-sql.sql** para crear tablas e insertar datos iniciales.



## 🚀 Puesta en Marcha

Ejecutarla desde su IDE favorito usando el perfil **https** definido en `launchSettings.json`.

La API estará disponible en HTTPS para su comunicación con el frontend.
