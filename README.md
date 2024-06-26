# Vehicle Auction Bid Calculator

## Project Overview

The Progi Bid Calculator is a comprehensive solution designed to streamline the process of calculating vehicle bid prices in auction scenarios. This project consists of two main components:

1. A robust backend API built with .NET 8.0
2. A user-friendly frontend interface developed using Vue 3 and TypeScript

Our system takes into account various factors such as vehicle type and base price to compute different fees and determine the total bid price. It's designed to be scalable, maintainable, and easy to use for both end-users and developers.

## Key Features

- Dynamic vehicle type selection (Common/Luxury)
- Real-time base price input and calculation
- Automatic computation of multiple fees:
  - Basic user fee
  - Seller's special fee
  - Association fee
  - Storage fee
- Total price calculation and display
- Responsive design for various devices
- Robust error handling and user feedback

## Project Structure

The project is organized into two main directories:
progi-bid-calculator/
├── progi-bid-calculator-ui/    # Frontend Vue application
├── Progi.BidCalculator/        # Backend .NET application
└── README.md                   # This file

## Detailed Documentation

For more detailed information about each component, please refer to their respective README files:

- [Frontend (Vue UI) Documentation](./progi-bid-calculator-ui/README.md)
- [Backend (API) Documentation](./Progi.BidCalculator/README.md)

These README files contain specific instructions for setup, running the applications, available scripts, testing procedures, and contribution guidelines for each component.

## Getting Started

To get started with the Progi Bid Calculator:

1. Clone this repository
2. Follow the setup instructions in the Frontend README to set up and run the Vue UI
3. Follow the setup instructions in the Backend README to set up and run the .NET API

Ensure both the frontend and backend are running simultaneously for full functionality.

## Support

If you encounter any issues or have questions, please file an issue on the GitHub repository or contact me on GitHub or send a [mail](chidimicheal17@gmail.com)

----------------------------------------------------------------------


## Objective

The purpose of this exercise is to assess a programmer's ability to develop a minimum viable product. Note that this exercise does not simulate a real work situation. The objective is to develop a simple application using good programming practices.

**Important:** The programming language to use is the one specified in the job description. If possible, also implement a graphical web interface (UI) using an appropriate framework (Vue.js, AngularJs, etc.).

## Evaluation Criteria

The final solution will be evaluated according to the following criteria:
- Code clarity
- Algorithm and calculation result
- Use of Object-Oriented Programming principles
- Implementation of good software architecture practices (Clean Code, SOLID, KISS, DRY, YAGNI, etc.)
- Proper use of frameworks, tools, and libraries related to the programming language used

## Task Description

Develop an application that will allow a buyer to calculate the total price of a vehicle at a car auction. The software must consider several costs in the calculation. The buyer must pay various fees for the transaction, all of which are calculated on the base price amount. Fees must be dynamically computed.

### Requirements

- There is a field to enter the vehicle base price.
- There is a field to specify the vehicle type (Common or Luxury).
- The list of fees and their amount are displayed.
- The total cost is automatically computed and displayed every time the price or type changes.

### List of Fixed and Variable Costs

- **Basic user fee:** 10% of the price of the vehicle
  - Common car: minimum $10 and maximum $50
  - Luxury car: minimum $25 and maximum $200
- **The seller's special fee:**
  - Common car: 2% of the vehicle price
  - Luxury car: 4% of the vehicle price
- **The added costs for the association based on the price of the vehicle:**
  - $5 for an amount between $1 and $500
  - $10 for an amount greater than $500 up to $1000
  - $15 for an amount greater than $1000 up to $3000
  - $20 for an amount over $3000
- **A fixed storage fee:** $100

### Calculation Example

- **Vehicle Price (Common):** $1,000
  - Basic fee: $50 (10%, min: $10, max: $50)
  - Special fee: $20 (2%)
  - Association fee: $10
  - Storage fee: $100
  - **Total:** $1,180 = $1,000 + $50 + $20 + $10 + $100

## Test Cases

| Vehicle Price ($) | Vehicle Type | Basic Fee ($) | Special Fee ($) | Association Fee ($) | Storage Fee ($) | Total ($)     |
|-------------------|--------------|---------------|------------------|----------------------|-----------------|---------------|
| 398.00            | Common       | 39.80         | 7.96             | 5.00                 | 100.00          | 550.76        |
| 501.00            | Common       | 50.00         | 10.02            | 10.00                | 100.00          | 671.02        |
| 57.00             | Common       | 10.00         | 1.14             | 5.00                 | 100.00          | 173.14        |
| 1,800.00          | Luxury       | 180.00        | 72.00            | 15.00                | 100.00          | 2,167.00      |
| 1,100.00          | Common       | 50.00         | 22.00            | 15.00                | 100.00          | 1,287.00      |
| 1,000,000.00      | Luxury       | 200.00        | 40,000.00        | 20.00                | 100.00          | 1,040,320.00  |

---
