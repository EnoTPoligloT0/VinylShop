import type { Metadata } from "next";
import { Header, Footer } from "@/components";
import "./globals.css";


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
        <Header/>
        <main>{children}</main>
        <Footer/>
      </body>
      
      </html>
  );
}
