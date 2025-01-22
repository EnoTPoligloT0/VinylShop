"use client";
import { createContext, useContext, useState, useEffect, ReactNode } from "react";
import { CartItem } from "@/types/cart";

interface CartContextType {
    cart: CartItem[];
    addToCart: (item: CartItem) => void;
    removeFromCart: (vinylId: string) => void;
    clearCart: () => void;
    totalAmount: number;
    setCart: React.Dispatch<React.SetStateAction<CartItem[]>>;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export const CartProvider = ({ children }: { children: ReactNode }) => {
    const [cart, setCart] = useState<CartItem[]>(() => {
        const savedCart = localStorage.getItem('cart') ;
        return savedCart ? JSON.parse(savedCart) : [];
    });

    const [totalAmount, setTotalAmount] = useState(0);

    useEffect(() => {
        const calculateTotalAmount = () => {
            const total = cart.reduce((sum, item) => sum + item.quantity * item.unitPrice, 0);
            setTotalAmount(total);
        };
        calculateTotalAmount();
    }, [cart]);

    const syncCartWithLocalStorage = (updatedCart: CartItem[]) => {
        localStorage.setItem("cart", JSON.stringify(updatedCart));
    };

    const addToCart = (item: CartItem) => {
        setCart((prevCart) => {
            const existingItem = prevCart.find((cartItem) => cartItem.vinylId === item.vinylId);
            const updatedCart = existingItem
                ? prevCart.map((cartItem) =>
                    cartItem.vinylId === item.vinylId
                        ? { ...cartItem, quantity: cartItem.quantity + item.quantity }
                        : cartItem
                )
                : [...prevCart, item];
            syncCartWithLocalStorage(updatedCart);
            return updatedCart;
        });
    };

    const removeFromCart = (vinylId: string) => {
        setCart((prevCart) => {
            const updatedCart = prevCart.filter((item) => item.vinylId !== vinylId);
            syncCartWithLocalStorage(updatedCart);
            return updatedCart;
        });
    };

    const clearCart = () => {
        setCart([]);
        localStorage.removeItem("cart");
    };

    return (
        <CartContext.Provider value={{ cart, addToCart, removeFromCart, clearCart, totalAmount, setCart }}>
            {children}
        </CartContext.Provider>
    );
};

export const useCartContext = () => {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error("useCart must be used within a CartProvider");
    }
    return context;
};
