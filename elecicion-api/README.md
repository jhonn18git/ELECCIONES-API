## habilitar apis 
php artisan install:api

## hacer correr las migraciones

php artisan migrate

## Crear modelo y migracion, seeder, factory y controller para api

php artisan make:model Candidato -msfc --api

## Luego de crear el modelo

### en la migracion colocar los campos

## CRear los seeders 
no olivdar enlazar los seeders al databaseseeder

## hacer correr las migraciones y los seeders

php artisan migrate:fresh --seed

## hacer correr la api
php artisan serve

## Poner la rua en api.php
Route::apiResource('candidatos', CandidatoController::class);   

## Instalar libreria
composer require firebase/php-jwt

# Escribir codigo en el archivo .env
JWT_SECRET=clave_secreta_sis258_eleccion_api_2026
JWT_ALGORITHM=HS256

ojo: la clave debe tener minimo 32 caracteres, con "secret" sale el error "Provided key is too short"

# Crear el midleware 
php artisan make:middleware JwtMiddleware

# Escribir codigo del midleware
 escribir elcodigo para leer el token  
# Proteger las rutasincluyendo a las que sean necesarias
->middleware(JwtMiddleware::class)
# crear un Controlador para el login
php artisan make:controller LoginController

# CRUD de clientes

## Crear modelo y migracion, seeder, factory y controller para api
php artisan make:model Cliente -msfc --api

## En la migracion colocar los campos
nombres, apellidos, ci, telefono, direccion

## Llenar el fillable del modelo, el factory y el seeder
no olvidar enlazar el ClienteSeeder al DatabaseSeeder

## Poner la ruta en api.php protegida con el middleware
Route::apiResource('clientes', ClienteController::class)->middleware(JwtMiddleware::class);

## Hacer correr las migraciones y los seeders
php artisan migrate:fresh --seed

## Rutas
GET    /api/clientes        listar
GET    /api/clientes/{id}   ver uno
POST   /api/clientes        crear (solo admin)
PUT    /api/clientes/{id}   actualizar (solo admin)
DELETE /api/clientes/{id}   eliminar (solo admin)

Todas llevan el header Authorization: Bearer <token> que se obtiene en POST /api/login
