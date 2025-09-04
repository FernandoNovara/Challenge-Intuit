# API REST — ABM de Clientes

Este proyecto implementa una **API REST** para gestionar una tabla de **Clientes** (ABM). Incluye definición de validaciones, ejemplos de requests y guía para ejecutar localmente.

---

## 1) Requisitos
- .NET 8
- SQL Server
- Postman para probar endpoints

---

### Datos de prueba
```sql
INSERT INTO dbo.Clients (FirstName, LastName, BirthDate, CUIT, Address, Cellphone, Email)
VALUES
('Ana',  'Pérez', '1990-05-12T00:00:00', '27123456783', 'Av. Demo 123',   '123456789', 'ana.perez@example.com'),
('Luis', 'Gómez', '1985-10-30T00:00:00', '20234567897', 'Calle Falsa 742', '123456789', 'luis.gomez@example.com');
```

---

## 2) Modelo de Datos en la API
```json
{
    "clientId": 1,
    "name": "Juan",
    "lastName": "Pérez",
    "birthDate": "1990-05-12T00:00:00",
    "cuit": "20-12345678-9",
    "address": "Av. Siempre Viva 742",
    "cellphone": "1123456789",
    "email": "juan.perez@email.com"
}
```

---

## 3) Validaciones
- **Obligatorios:** `name`, `LastName`, `CUIT`, `Cellphone`, `Email`.
- **Formatos:**
  - `BirthDate`: fecha válida (ISO 8601: YYYY-MM-DDTHH:mm:ss, ej.: 1990-05-12T00:00:00).
  - `Email`: formato de email válido.
  - `CUIT`: formato `##-########-#` (se puede incluir verificación de dígito verificador como mejora).
  - `Cellphone`: validación local simple.
- **Unicidad:** `clientId` (PK).



---

## 4) Endpoints

| Método | Ruta                        | Descripción                                      | 200 OK |
|-------:|-----------------------------|--------------------------------------------------|------------------|
| GET    | `/getClients`                         | **getClients** – Lista todos los clientes            | `[{...}, {...}]` |
| GET    | `/getClient/{id}`                     | **getClient** – Obtiene por ID                         | `{...}`          |
| GET    | `/getClientByName/{name}`                     | **getClientByName** – Obtiene por  el nombre                         | `{...}`          |
| POST   | `/addClient`                         | **addClient** – Crea un cliente                     | `{...}`          |
| PUT    | `/updateClient`                     | **updateClient** – Actualiza un cliente                | `{...}`          |
| DEL    | `/deleteClient/{id}`                     | **deleteClient** – Elimina un cliente                | `{...}`          |

> **Search (caracteres centrales):** debe hacer `LIKE '%{q}%'` en `FirstName` **o** `LastName` (case-insensitive, colación según BD).

### Ejemplos (curl)
```bash
# getClients
curl -X 'GET' \ 'https://localhost:7210/api/Clients/getClients'

# getClient
curl -X 'GET' \ 'https://localhost:7210/api/Clients/getClient/1'

# getClientByName
curl -X 'GET' \ 'https://localhost:7210/api/Clients/getClientByName/test'

# addClient
curl -X 'POST' \
  'https://localhost:7210/api/Clients/addClient' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
    "name": "Fernando",
    "lastName": "Novara",
    "birthDate": "1994-05-20T01:02:44.004",
    "cuit": "26-54919127-3",
    "address": "San Luis,Argentina",
    "cellphone": "123456789",
    "email": "user@example.com"
}'

# updateClient
curl -X 'PUT' \
  'https://localhost:7210/api/Clients/updateClient' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
    "clientId": 7,
    "name": "Fernando",
    "lastName": "Novara",
    "birthDate": "1994-05-20T01:02:44.004",
    "cuit": "26-54919127-3",
    "address": "San Luis,Argentina",
    "cellphone": "123456789",
    "email": "user@example.com"
}'

# deleteClient
curl -X 'DELETE' \
  'https://localhost:7210/api/Clients/deleteClient/7' \
```
