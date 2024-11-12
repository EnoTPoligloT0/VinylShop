import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { CartItem } from "@/types/cart";
import { Vinyl } from "@/types/vinyl";
import Link from 'next/link';
import Cookies from "js-cookie";

//todo make axios use api.ts
const Cart = () => {
    const [cart, setCart] = useState<CartItem[]>([]);
    const [vinylDetails, setVinylDetails] = useState<Vinyl[]>([]);

    useEffect(() => {
        if (typeof window !== "undefined") {
            const storedCart = localStorage.getItem('cart');
            if (storedCart) {
                setCart(JSON.parse(storedCart));
            }
        }
    }, []);

    useEffect(() => {
        const token = Cookies.get('secretCookie');
        console.log("Token received");
        const fetchVinylDetails = async () => {
            const vinylIds = cart.map(item => item.vinylId);
            const promises = vinylIds.map(id => axios.get(`https://localhost:44372/vinyls/${id}`));
            const responses = await Promise.all(promises);
            const details = responses.map(response => response.data);
            setVinylDetails(details);
        };

        if (cart.length > 0) {
            fetchVinylDetails();
        } else {
            setVinylDetails([]);
        }
    }, [cart]);

    const handleRemoveFromCart = (vinylId: string) => {
        const updatedCart = cart.filter(item => item.vinylId !== vinylId);
        setCart(updatedCart);
        if (typeof window !== "undefined") {
            localStorage.setItem('cart', JSON.stringify(updatedCart));
        }

        const updatedVinylDetails = vinylDetails.filter(vinyl => vinyl.id !== vinylId);
        setVinylDetails(updatedVinylDetails);
    };

    const handleCheckout = async () => {
        try {
            const totalAmount = cart.reduce(
                (total, item) => total + item.unitPrice * item.quantity, 0
            );

            const token = Cookies.get('secretCookie');
            if (!token) {
                console.error("No token found!");
                return;
            }

            const orderResponse = await axios.post(
                'https://localhost:44372/orders',
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

            const orderId = orderResponse.data.id;

            for (const item of cart) {
                await axios.post(
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
            }

            setCart([]);
            if (typeof window !== "undefined") {
                localStorage.removeItem('cart');
            }
            setVinylDetails([]);
        } catch (error) {
            console.error('Checkout failed:', error);
        }
    };

    return (
        <main className="container mx-auto p-8 bg-gray-50 rounded-xl shadow-md my-5">
            <h1 className="text-4xl font-bold text-purple-700 mb-6">Your Cart</h1>
            <ul className="space-y-4">
                {vinylDetails.map((vinyl) => {
                    const cartItem = cart.find(item => item.vinylId === vinyl.id);
                    return (
                        <li key={vinyl.id}
                            className="bg-white rounded-lg shadow-lg p-4 flex justify-between items-center">
                            <div className="flex items-center">
                                {vinyl.imageBase64 && (
                                    <img
                                        src={`data:image/jpeg;base64,${vinyl.imageBase64}`}
                                        alt={vinyl.title}
                                        className="w-16 h-auto rounded-md mr-4" // Adjust the size as needed
                                    />
                                )}
                                <div>
                                    <h2 className="text-lg font-semibold text-purple-800">{vinyl.title}</h2>
                                    <p className="text-gray-600">Quantity: {cartItem?.quantity}</p>
                                    <p className="text-gray-700 font-bold">${vinyl.price.toFixed(2)}</p>
                                </div>
                            </div>
                            <button onClick={() => handleRemoveFromCart(vinyl.id)}>
                                Remove from Cart
                            </button>
                        </li>
                    );
                })}
            </ul>
            {cart.length === 0 && <p className="text-gray-600 text-center mt-4">Your cart is empty.</p>}

            {cart.length > 0 && (
                <div className="flex space-x-4 mt-6">
                    <button
                        className="bg-purple-700 text-white font-bold py-2 px-4 rounded"
                        onClick={handleCheckout}
                    >
                        Checkout
                    </button>

                    {/* Button to navigate to the checkout page */}
                    <Link href="/checkout">
                        <button className="bg-blue-500 text-white font-bold py-2 px-4 rounded">
                            Go to Checkout
                        </button>
                    </Link>
                </div>
            )}
        </main>
    );
};

export default Cart;
