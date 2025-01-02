"use client";
import React from 'react';
import Image from 'next/image';
import Link from 'next/link';
import { VinylCardProps } from '@/types/vinyl';
import { useRouter } from 'next/navigation';  // Corrected import
import { useCartContext } from "@/context/CartContext"; // Import CartContext

const VinylCard: React.FC<VinylCardProps> = ({
                                                 id,
                                                 imageBase64,
                                                 title,
                                                 price,
                                                 artist
                                             }) => {
    const { addToCart } = useCartContext();  // Use addToCart from CartContext

    const imageSrc = imageBase64 ? `data:image/jpeg;base64,${imageBase64}` : '/default-image.jpg';

    const handleAddToCart = (e: React.MouseEvent) => {
        e.preventDefault();
        if (id) {
            const cartItem = {
                vinylId: id,
                quantity: 1,
                unitPrice: price
            };
            addToCart(cartItem);
        }
    };

    return (
        <Link href={`/vinyl/${id}`} passHref>
            <div className="relative bg-white shadow-lg rounded-lg overflow-hidden w-full sm:w-64 transform transition-transform duration-300 hover:scale-105 hover:shadow-2xl cursor-pointer">
                <div className="relative w-full h-48 sm:h-64">
                    <Image src={imageSrc} alt={title} layout="fill" objectFit="cover" />
                </div>

                <div className="p-4 flex flex-col">
                    <h3 className="text-md sm:text-lg font-semibold text-gray-800 mb-1 truncate">{title}</h3>
                    <p className="text-gray-700 font-bold mb-1">${price.toFixed(2)}</p>
                    <p className="text-gray-600 text-sm">{artist}</p>
                </div>

                <div className="absolute bottom-4 right-4">
                    <button onClick={handleAddToCart}>
                        <Image src="/bag.svg" alt="Add to Cart" width={24} height={24} />
                    </button>
                </div>
            </div>
        </Link>
    );
};

export default VinylCard;
