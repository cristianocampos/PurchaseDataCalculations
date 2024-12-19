# PurchaseDataCalculations

This repository contains a .NET 8 WebAPI application that can be run using Docker Compose.

## Prerequisites
Before running the application, ensure you have the following installed:

1. **.NET SDK 8.0**
2. **Docker**
3. **Docker Compose** (Docker Compose typically comes bundled with Docker Desktop)

## Steps to Run the Application

### 1. Clone the Repository

Clone this repository to your local machine

### 2. Build and Run Using Docker Compose
The project contains a `docker-compose.yml` file to orchestrate the Docker container.

Run the following commands:

1. **Build the container**:
   ```bash
   docker compose build
   ```

2. **Start the container**:
   ```bash
   docker compose up
   ```

   By default, the application will be available at [http://localhost:5002](http://localhost:5002)

### 3. Stop the Application
To stop the application and remove container:

```bash
docker compose down
```

## Testing the API


Once the application is running, test the endpoints using a tool like:

- **Postman**
- **cURL**


Example using `cURL`:

```bash
curl http://localhost:5002/api/vatcalculations?NetAmount=1"&"VatRate=10
```