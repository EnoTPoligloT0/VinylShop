"use client";

import React from 'react';
import Cart from '../../components/Cart';
import { CartProvider } from "@/context/CartContext";

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
