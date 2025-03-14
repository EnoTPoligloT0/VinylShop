
# AmberVinyl - e-commerce vinyl shop

AmberVinyl is a sleek e-commerce platform for vinyl enthusiasts, offering an intuitive interface to explore and purchase records, ideal for collectors and audiophiles.


## Screenshots

![App Screenshot](https://github.com/EnoTPoligloT0/VinylShop/blob/main/screenshot2.png?raw=true)


## Tech Stack

**Frontend:** Next.js, TypeScript, Tailwind CSS, Axios

**Backend:** .NET 8 Minimal API, Entity Framework Core

**Database:** PostgreSQL

**DevOps:** Docker, Docker Compose

**Other Tools:** Postman, Stripe, Google oAuth

## Features

- **Modern UI/UX Design**:  
    Clean, user-friendly interface built with **Next.js** and **Tailwind CSS**.

- **Authentication**:
  - **Custom Authentication**: Secure, role-based login system with cookie management and password hashing.
  - **Google Authentication**: Sign-in via Google account for a quick and easy login experience.

- **E-commerce Integration**:
  - **Stripe Payments**: Seamless payment processing with **Stripe** for secure transactions and checkout.
  - **Shopping Cart**: Users can add items to the cart and proceed to checkout with integrated payment options.



## Environment Variables

To run this project, you will need to add the following environment variables to your .env file

# Frontend:

NEXT_PUBLIC_API_URL=http://localhost/ 

NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY=your_stripe_publishable_key

NEXT_PUBLIC_GOOGLE_CLIENT_ID=your_google_client_id

# Backend
GOOGLE_CLIENT_ID=

GOOGLE_CLIENT_SECRET=
SENDCLOUD_PUBLIC_KEY=
SENDCLOUD_SECRET_KEY=
STRIPE_PUBLIC_KEY=pk_test_
STRIPE_SECRET_KEY=sk_test_

## Author

- [@EnoTPoligloT0](https://github.com/EnoTPoligloT0)

