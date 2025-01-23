"use client";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { Vinyl } from "@/types/vinyl";
import { loadStripe } from "@stripe/stripe-js";
import Cookies from "js-cookie";
import { useCartContext } from "@/context/CartContext";

const stripePromise = loadStripe("pk_test_51QKsJJHqGo0KeykHjiMci68gs5tv5Ym5wgt2WXb4zRHaID0V3AsQbjXSiuRJKD0FWBi9kH0LPtt6aZ37jac8azFa00OFZkimTs");
localStorage.removeItem("orderId");

const Cart = () => {
    const { cart, setCart, totalAmount } = useCartContext(); // Accessing cart and totalAmount from context
    const [vinylDetails, setVinylDetails] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(false);
    const [orderId, setOrderId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);

    useEffect(() => {
        const token = Cookies.get("secretCookie");
        if (token) {
            setToken(token);
        }
    }, []);

    useEffect(() => {
        const fetchVinylDetails = async () => {
            try {
                if (cart.length === 0) {
                    setVinylDetails([]);
                    return;
                }
                const vinylIds = cart.map((item) => item.vinylId);
                const vinylsData = await Promise.all(
                    vinylIds.map((id) =>
                        axios.get<Vinyl>(`https://localhost:44372/vinyls/${id}`)
                    )
                );
                const vinyls = vinylsData.map((response) => response.data);
                setVinylDetails(vinyls);
            } catch (error) {
                console.error("Error fetching vinyl details:", error);
            }
        };

        fetchVinylDetails();
    }, [cart]);

    const handleRemoveFromCart = (vinylId: string) => {
        const updatedCart = cart.filter((item) => item.vinylId !== vinylId);
        setCart(updatedCart);
        localStorage.setItem("cart", JSON.stringify(updatedCart));
    };

    const createOrder = async () => {
        try {
            if (!token) throw new Error("User not logged in.");

            const response = await axios.post(
                "https://localhost:44372/orders",
                {
                    orderDate: new Date(),
                    totalAmount,
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                    withCredentials: true,
                }
            );

            const orderId = response.data.id;
            localStorage.setItem("orderId", orderId);
            for (const item of cart) {
                await axios.post(
                    `https://localhost:44372/orderItems/orderItem/${orderId}`,
                    {
                        vinylId: item.vinylId,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice,
                    },
                    {
                        headers: { Authorization: `Bearer ${token}` },
                        withCredentials: true,
                    }
                );
            }

            setOrderId(orderId);
            return orderId;
        } catch (error) {
            console.error("Error creating order:", error);
            throw error;
        }
    };

    const handleCheckout = async () => {
        setLoading(true);
        try {
            const currentOrderId = orderId || (await createOrder());
            if (!currentOrderId) {
                throw new Error("Failed to create an order.");
            }

            const sessionResponse = await fetch("https://localhost:44372/payments/create-checkout-session", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    totalAmount: totalAmount,
                    orderId: currentOrderId,
                }),
            });

            if (!sessionResponse.ok) {
                throw new Error("Failed to create checkout session.");
            }

            const { sessionId } = await sessionResponse.json();

            const stripe = await stripePromise;
            if (!stripe) {
                throw new Error("Stripe.js failed to load.");
            }

            const { error } = await stripe.redirectToCheckout({ sessionId });
            if (error) {
                throw new Error(`Stripe Checkout error: ${error.message}`);
            }
        } catch (error) {
            console.error("Error during checkout:", error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <main className="container mx-auto p-8 bg-gray-50 rounded-xl shadow-md my-5">
            <h1 className="text-4xl font-bold text-purple-700 mb-6">Your Cart</h1>
            <ul className="space-y-4">
                {vinylDetails.map((vinyl) => {
                    const cartItem = cart.find((item) => item.vinylId === vinyl.id);
                    return (
                        <li
                            key={vinyl.id}
                            className="bg-white rounded-lg shadow-lg p-4 flex justify-between items-center">
                            <div className="flex items-center">
                                {vinyl.imageBase64 && (
                                    <img
                                        src={`data:image/jpeg;base64,${vinyl.imageBase64}`}
                                        alt={vinyl.title}
                                        className="w-16 h-auto rounded-md mr-4"
                                    />
                                )}
                                <div>
                                    <h2 className="text-lg font-semibold text-purple-800">{vinyl.title}</h2>
                                    <p className="text-gray-600">Quantity: {cartItem?.quantity}</p>
                                    <p className="text-gray-700 font-bold">${vinyl.price.toFixed(2)}</p>
                                </div>
                            </div>
                            <button
                                onClick={() => handleRemoveFromCart(vinyl.id)}
                                className="text-red-600 hover:underline">
                                Remove
                            </button>
                        </li>
                    );
                })}
            </ul>
            {cart.length === 0 && <p className="text-gray-600 text-center mt-4">Your cart is empty.</p>}
            {cart.length > 0 && (
                <div className="mt-6">
                    <div className="text-right text-lg font-semibold mb-4">
                        Total Amount: ${totalAmount.toFixed(2)}
                    </div>
                    <button
                        className="bg-purple-700 text-white font-bold py-2 px-4 rounded"
                        onClick={handleCheckout}
                        disabled={loading}>
                        {loading ? "Processing..." : "Proceed to Checkout"}
                    </button>
                </div>
            )}
        </main>
    );
};

export default Cart;
