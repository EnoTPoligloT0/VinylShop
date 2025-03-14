// VinylList.tsx
"use client";
import React, {useEffect, useState} from 'react';
import VinylCard from './VinylCard';
import {Vinyl, FilterVinylListProps} from '@/types/vinyl';
import {CartItem} from '@/types/cart';
import {getVinyls} from "@/utils/apiService";
import Popup from "@/components/Popup";

const VinylList: React.FC<FilterVinylListProps> = ({
                                                       genres = [],
                                                       years = [],
                                                       sortOption = ""
                                                   }) => {
    const [vinyls, setVinyls] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [cart, setCart] = useState<CartItem[]>([]);
    const [popupMessage, setPopupMessage] = useState<string>('');
    const [showPopup, setShowPopup] = useState<boolean>(false);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const fetchVinyls = async () => {
        try {
            setLoading(true);

            const filters = {
                genre: genres,
                decade: years.map(Number),
                sortOption,
            };

            const data = await getVinyls(page, pageSize, filters);
            setVinyls(data.vinyls);
        } catch (error) {
            setError('Failed to fetch vinyls. Please try again.');
            console.error("Fetch error:", error);
        } finally {
            setLoading(false);
        }
    };
    useEffect(() => {
        fetchVinyls();
    }, [genres, years, sortOption, page, pageSize]);

    const addToCart = (vinylId: string, price: number) => {
        const newCartItem: CartItem = {
            id: `cart-${vinylId}`,
            vinylId: vinylId,
            quantity: 1,
            unitPrice: price,
        };

        const updatedCart = [...cart, newCartItem];
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));

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

            {showPopup && <Popup message={popupMessage} onClose={closePopup}/>}
        </div>
    );
};

export default VinylList;
