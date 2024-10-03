"use client";
import React from 'react';
import Image from 'next/image';
import Link from 'next/link';
import {Vinyl} from '@/app/types/vinyl';

const VinylCard: React.FC<Vinyl> = ({
                                        id,
                                        imageBase64,
                                        title,
                                        price,
                                        artist,
                                    }) => {
    const imageSrc = imageBase64 ? `data:image/jpeg;base64,${imageBase64}` : '/default-image.jpg';

    return (
        <Link href={`/vinyl/${id}`} passHref>
            <div
                
                className="relative bg-white shadow-lg rounded-lg overflow-hidden w-full sm:w-64 transform transition-transform duration-300 hover:scale-105 hover:shadow-2xl cursor-pointer"
            >
                <div className="relative w-full h-48 sm:h-64"> 
                    <Image src={imageSrc} alt={title} layout="fill" objectFit="cover"/>
                </div>

                <div className="p-4 flex flex-col">
                    <h3 className="text-md sm:text-lg font-semibold text-gray-800 mb-1 truncate">{title}</h3> {/* Truncated for long titles */}
                    <p className="text-gray-700 font-bold mb-1">${price.toFixed(2)}</p> 
                    <p className="text-gray-600 text-sm">{artist}</p> 
                </div>

                <div className="absolute bottom-4 right-4">
                    <Image src="/bag.svg" alt="Add to Cart" width={24} height={24}/>
                </div>
            </div>
        </Link>
    );
};

export default VinylCard;
