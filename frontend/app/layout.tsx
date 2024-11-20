"use client";

import {Footer, Header} from "@/components";
import "./globals.css";
import {CartProvider} from "@/context/CartContext";
import {GoogleOAuthProvider} from "@react-oauth/google";
import {Elements} from "@stripe/react-stripe-js";
import {loadStripe} from "@stripe/stripe-js";

const stripePromise = loadStripe("pk_test_51QKsJJHqGo0KeykHjiMci68gs5tv5Ym5wgt2WXb4zRHaID0V3AsQbjXSiuRJKD0FWBi9kH0LPtt6aZ37jac8azFa00OFZkimTs");

export default function RootLayout({
                                       children,
                                   }: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
        <head>
            <link rel="preconnect" href="https://fonts.googleapis.com"/>
            <link rel="preconnect" href="https://fonts.gstatic.com"/>
            <link
                href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap"
                rel="stylesheet"
            />
            <title>VinylShop</title>
        </head>

        <body className="font-poppins antialiased">

        <GoogleOAuthProvider clientId={process.env.NEXT_PUBLIC_GOOGLE_CLIENT_ID || ""}>
            <Elements stripe={stripePromise}>
                <CartProvider>
                    <Header/>
                    <main>{children}</main>
                    <Footer/>
                </CartProvider>
            </Elements>
        </GoogleOAuthProvider>
        </body>

        </html>
    );
}
