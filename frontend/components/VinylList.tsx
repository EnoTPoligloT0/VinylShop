// VinylList.tsx
"use client";
import React, { useEffect, useState } from 'react';
import VinylCard from './VinylCard';
import { Vinyl } from '@/types/vinyl';
import { CartItem } from '@/types/cart'; // Import CartItem type
import Popup from "@/components/Popup";

const VinylList = () => {
    const [vinyls, setVinyls] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [cart, setCart] = useState<CartItem[]>([]);
    const [popupMessage, setPopupMessage] = useState<string>('');
    const [showPopup, setShowPopup] = useState<boolean>(false);

    useEffect(() => {
        const fetchVinyls = async () => {
            try {
                const response = await fetch('https://localhost:44372/vinyls/', {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                });

                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                const data = await response.json();
                setVinyls(data);
            } catch (error) {
                setError('Failed to fetch vinyls. Please try again.');
                console.error("Fetch error:", error);
            } finally {
                setLoading(false);
            }
        };

        // Load cart from local storage when the component mounts
        const loadCartFromLocalStorage = () => {
            const storedCart = localStorage.getItem('cart');
            if (storedCart) {
                setCart(JSON.parse(storedCart));
            }
        };

        loadCartFromLocalStorage();
        fetchVinyls();
    }, []);

    const addToCart = (vinylId: string, price: number) => {
        const newCartItem: CartItem = {
            id: `cart-${vinylId}`,
            vinylId: vinylId,
            quantity: 1,
            unitPrice: price,
        };

        // Update cart and save to local storage
        const updatedCart = [...cart, newCartItem];
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart)); // Save to local storage

        setPopupMessage(`Vinyl added to cart!`);
        setShowPopup(true);
        console.log(`Vinyl with ID ${vinylId} added to cart. Current cart:`, updatedCart);
    };

    const closePopup = () => {
        setShowPopup(false);
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p className="text-red-500">{error}</p>;

    return (
        <div className="flex flex-wrap gap-4">
            {vinyls.map((vinyl) => (
                <VinylCard
                    key={vinyl.id}
                    id={vinyl.id!}
                    imageBase64={vinyl.imageBase64}
                    title={vinyl.title}
                    price={vinyl.price}
                    artist={vinyl.artist}
                    addToCart={addToCart}
                />
            ))}

            {showPopup && <Popup message={popupMessage} onClose={closePopup} />}
        </div>
    );
};

export default VinylList;
