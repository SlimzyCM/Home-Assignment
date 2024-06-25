# Progi Bid Calculator UI

## Overview

This is the frontend application for the Progi Bid Calculator. It provides a user-friendly interface for calculating vehicle bid prices, interacting with the Progi Bid Calculator API.

## Problem Statement

The UI simplifies the process of calculating complex vehicle bid prices for users, providing an intuitive interface to input vehicle details and view calculated prices including various fees.

## Architecture

- Built with Vue 3 and TypeScript
- Uses Vite as the build tool
- Implements Composition API
- Utilizes Axios for API communication
- Employs Vue Router for navigation
- Uses Vitest for unit testing

## Project Structure

- `src/components`: Vue components
- `src/views`: Page components
- `src/router`: Route configurations
- `src/utils`: Utility functions and API client
- `tests`: Unit and integration tests

## Setup and Running

1. Ensure you have Node.js (v14+) and npm installed.
2. Clone the repository.
3. Navigate to the project directory.
4. Install dependencies:
   npm install
5. Start the development server:
   npm run dev
6. The application will be available at `http://localhost:5173/`

## Available Scripts

- `npm run dev`: Start development server
- `npm run build`: Build for production
- `npm run test`: Run unit tests
- `npm run lint`: Lint and fix files

## Testing

Run unit tests with:
`npm run test`

## API Integration

The UI communicates with the backend API. Ensure the API is running at `https://localhost:7118`. If the API URL changes, update it in `src/utils/api.ts`.

## Features

- Vehicle type selection (Common/Luxury)
- Base price input
- Real-time calculation of fees and total price
- Error handling and display

## Contributing

Please follow the Vue Style Guide and add unit tests for new components and features.

## Deployment

To build the project for production:
`npm run build`

The built files will be in the `dist` directory, ready for deployment.

## Note

Ensure you're using the correct Node.js version. This project was developed with Node.js v14+.
