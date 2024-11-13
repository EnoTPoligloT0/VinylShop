import type {Metadata} from "next";
import {Header, Footer} from "@/components";
import "./globals.css";
import {CartProvider} from "@/context/CartContext";
import {GoogleOAuthProvider} from "@react-oauth/google";


export const metadata: Metadata = {
    title: "VinylShop",
    description: "Your go-to vinyl shop",
};

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
            <CartProvider>
                <Header/>
                <main>{children}</main>
                <Footer/>
            </CartProvider>
        </GoogleOAuthProvider>
        </body>

        </html>
    );
}
