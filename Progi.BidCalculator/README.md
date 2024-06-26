# Progi Bid Calculator API

## Overview
The Progi Bid Calculator API is a .NET-based backend service that calculates vehicle bid prices based on various factors such as vehicle type and base price. It handles the complex fee calculations for vehicle auctions.

## Problem Statement
In vehicle auctions, calculating the final bid price involves various fees and factors. This API simplifies this process by providing a centralized calculation service that considers vehicle type, base price, and applies different fee structures accordingly.

## Architecture
- Built with .NET 8.0
- Follows Clean Architecture principles
- Uses Entity Framework Core for data access
- Implements IUnitOfWork pattern
- Utilizes AutoMapper for object mapping

### Project Structure
- `Progi.BidCalculator.API`: API controllers and configuration
- `Progi.BidCalculator.Core`: Domain entities and interfaces
- `Progi.BidCalculator.Application`: Application logic, DTOs
- `Progi.BidCalculator.Infrastructure`: Data access and external service implementations

## Setup and Running
1. Ensure you have .NET 8.0 SDK installed.
2. Clone the repository.
3. Navigate to the project directory.
4. Run the following commands:
`dotnet restore`
`dotnet build`
`dotnet run --project Progi.BidCalculator.API`
5. The API will start running on `https://localhost:7118`.

## API Endpoints
- POST `/api/v1/Calculator/CalculateBid`: Calculates the bid price
- Request body: `{ "price": number, "vehicleType": number }`
- Vehicle Type: 0 for Common, 1 for Luxury

## Testing
To run the tests:
`dotnet test`

## Database
The project uses Entity Framework Core with SQLite for development. Ensure you run migrations:
`dotnet ef database update`

## Configuration
Check `appsettings.json` for configuration options. Update the connection string if needed.

## Contributing
Please follow the existing code style and add unit tests for new features.