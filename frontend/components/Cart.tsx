import React, { useEffect, useState } from "react";
import axios from "axios";
import { CartItem } from "@/types/cart";
import { Vinyl } from "@/types/vinyl";
import { loadStripe } from "@stripe/stripe-js";
import Cookies from "js-cookie";

//todo refactoring
const stripePromise = loadStripe("pk_test_51QKsJJHqGo0KeykHjiMci68gs5tv5Ym5wgt2WXb4zRHaID0V3AsQbjXSiuRJKD0FWBi9kH0LPtt6aZ37jac8azFa00OFZkimTs");

const Cart = () => {
    const [cart, setCart] = useState<CartItem[]>([]);
    const [vinylDetails, setVinylDetails] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(false);
    const [totalAmount, setTotalAmount] = useState(0);
    const [orderId, setOrderId] = useState<string | null>(null); // Order ID state
    const [token, setToken] = useState<string | null>(null); // User ID state

    useEffect(() => {
        const storedCart = typeof window !== "undefined" ? localStorage.getItem("cart") : null;
        if (storedCart) {
            setCart(JSON.parse(storedCart));
            console.log("Loaded cart from localStorage:", storedCart);
        }

        const token = Cookies.get("secretCookie");
        if (token) {
            setToken(token);
            console.log("Token received from cookies:", token);
        }
    }, []);

    useEffect(() => {
        const fetchVinylDetails = async () => {
            try {
                const vinylIds = cart.map((item) => item.vinylId);
                console.log("Fetching vinyl details for IDs:", vinylIds);

                const promises =
                    vinylIds.map((id) =>
                        axios.get<Vinyl>(`https://localhost:44372/vinyls/${id}`));

                const responses = await Promise.all(promises);
                console.log("Vinyl details response:", responses);

                const details = responses.map((res) => res.data);
                console.log("Vinyl details:", details);
                setVinylDetails(details);

                const total = cart.reduce((sum, item) => {
                    const vinyl = details.find((v) => v.id === item.vinylId);
                    return sum + (vinyl?.price || 0) * item.quantity;
                }, 0);
                console.log("Total amount calculated:", total);
                setTotalAmount(total);

            } catch (error) {
                console.error("Error fetching vinyl details:", error);
            }
        };

        if (cart.length > 0) {
            fetchVinylDetails();
        } else {
            setVinylDetails([]);
            setTotalAmount(0);
        }
    }, [cart]);

    // Remove item from cart
    const handleRemoveFromCart = (vinylId: string) => {
        console.log("Removing item from cart with vinylId:", vinylId);
        const updatedCart = cart.filter((item) => item.vinylId !== vinylId);
        setCart(updatedCart);
        localStorage.setItem("cart", JSON.stringify(updatedCart));
    };

    // Create an order and generate order ID
    const createOrder = async () => {
        try {
            if (!token) {
                throw new Error("User not logged in.");
            }

            // Call your API to create the order and get an order ID
            console.log("Creating order with total amount:", totalAmount);
            const response = await axios.post(
                "https://localhost:44372/orders",
                {
                    orderDate: new Date(),
                    totalAmount
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                    withCredentials: true,
                }
            );
            console.log("Order creation response:", response.data);
            const orderId = response.data.id;

            for (const item of cart) {
                const orderItemResponse = await axios.post(
                    `https://localhost:44372/orderItems/orderItem/${orderId}`,
                    {
                        vinylId: item.vinylId,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice,
                    },
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                        withCredentials: true,
                    }
                );
                console.log("Order item created:", orderItemResponse.data);
            }

            setOrderId(orderId);
            console.log("Order ID set:", orderId);

            return orderId;
        } catch (error) {
            console.error("Error creating order:", error);
            throw error;
        }
    };

    const handleCheckout = async () => {
        setLoading(true);
        try {
            if (!orderId) {
                console.log("No order ID found. Creating order...");
                const generatedOrderId = await createOrder(); // Create order if not exists
                if (!generatedOrderId) {
                    throw new Error("Failed to create order");
                }
                console.log("Order created with ID:", generatedOrderId);
            }

            console.log("Proceeding to checkout with orderId:", orderId);

            const response = await fetch("https://localhost:44372/payments/create-checkout-session", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    totalAmount: totalAmount, // Only send the total amount here
                }),
            });

            console.log("Checkout session response:", response);

            if (!response.ok) {
                throw new Error("Failed to create checkout session");
            }

            const { sessionId } = await response.json();
            console.log("Received session ID from response:", sessionId);

            const stripe = await stripePromise;

            if (!stripe) {
                throw new Error("Stripe.js failed to load.");
            }

            const { error } = await stripe.redirectToCheckout({ sessionId });
            if (error) {
                console.error("Error redirecting to Stripe Checkout:", error.message);
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
                            className="bg-white rounded-lg shadow-lg p-4 flex justify-between items-center"
                        >
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
                        disabled={loading}
                    >
                        {loading ? "Processing..." : "Proceed to Checkout"}
                    </button>
                </div>
            )}
        </main>
    );
};

export default Cart;
