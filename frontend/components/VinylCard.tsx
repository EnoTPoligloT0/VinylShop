// VinylCard.tsx
"use client";
import React from 'react';
import Image from 'next/image';
import { Vinyl } from '@/app/types/vinyl';

const VinylCard: React.FC<Vinyl> = ({
                                        imageBase64,
                                        title,
                                        price,
                                        artist,
                                    }) => {
    const imageSrc = imageBase64 ? `data:image/jpeg;base64,${imageBase64}` : '/default-image.jpg';

    return (
        <div className="relative bg-white shadow-lg rounded-lg overflow-hidden w-64">
            <div className="relative w-full h-64">
                <Image src={imageSrc} alt={title} layout="fill" objectFit="cover" />
            </div>

            <div className="p-4">
                <h3 className="text-lg font-semibold">{title}</h3>
                <p className="text-gray-700">${price.toFixed(2)}</p>
                <p className="text-gray-600">{artist}</p>
            </div>

            <div className="absolute bottom-4 right-4">
                <Image src="/bag.svg" alt="Locked" width={24} height={24} />
            </div>
        </div>
    );
};

export default VinylCard;
