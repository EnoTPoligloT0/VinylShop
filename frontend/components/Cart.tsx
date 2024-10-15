import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { CartItem } from "@/types/cart";
import { Vinyl } from "@/types/vinyl";

const Cart = () => {
    const [cart, setCart] = useState<CartItem[]>(() => {
        const storedCart = localStorage.getItem('cart');
        return storedCart ? JSON.parse(storedCart) : [];
    });
    const [vinylDetails, setVinylDetails] = useState<Vinyl[]>([]);

    useEffect(() => {
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
            setVinylDetails([]); // Clear vinyl details when cart is empty
        }
    }, [cart]);

    const handleRemoveFromCart = (vinylId: string) => {
        const updatedCart = cart.filter(item => item.vinylId !== vinylId);
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart)); // Update local storage

        // Update vinyl details immediately after removing the item
        const updatedVinylDetails = vinylDetails.filter(vinyl => vinyl.id !== vinylId);
        setVinylDetails(updatedVinylDetails);
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
                            <button onClick={() => {
                                if (vinyl.id) { // Check if vinyl.id is defined
                                    handleRemoveFromCart(vinyl.id);
                                }
                            }}>
                                Remove from Cart
                            </button>
                        </li>
                    );
                })}
            </ul>
            {cart.length === 0 && <p className="text-gray-600 text-center mt-4">Your cart is empty.</p>}
        </main>
    );
};

export default Cart;
