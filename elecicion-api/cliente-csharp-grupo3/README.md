# Grupo 3 - Cliente C# Windows Forms que consume la API REST

API: esta misma carpeta (elecicion-api, Laravel) -> login con JWT y CRUD de candidatos.
Cliente: carpeta ClienteEleccion.

## 1. Levantar la API (en la carpeta elecicion-api)

    composer install
    copy .env.example .env
    php artisan key:generate

El .env.example ya trae JWT_SECRET con 32+ caracteres (con "secret" el login falla).
Crear la base eleccion_api en phpMyAdmin y luego:

    php artisan migrate:fresh --seed
    php artisan serve

La API queda en http://127.0.0.1:8000/api

## 2. Correr el cliente (en cliente-csharp-grupo3/ClienteEleccion)

    dotnet run

Admin: test@example.com / password
Usuario normal: test2@example.com / password (solo puede listar)
