// app/cart/page.tsx
"use client"
import React from 'react';
import Cart from '../../components/Cart';
import {CartProvider} from "@/context/CartContext";
import VinylList from "@/components/VinylList"; // Adjust the import path based on your project structure

const CartPage = () => {
    return (
        <div>
            <CartProvider>
             <Cart />
            </CartProvider>
        </div>
    );
};

export default CartPage;
