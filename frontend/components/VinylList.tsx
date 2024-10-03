// VinylList.tsx
"use client";
import React, { useEffect, useState } from 'react';
import VinylCard from './VinylCard'; // Adjust the import based on your folder structure
import { Vinyl } from '@/app/types/vinyl';

const VinylList = () => {
    const [vinyls, setVinyls] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

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

        fetchVinyls();
    }, []); // Empty dependency array to run on component mount

    if (loading) return <p>Loading...</p>;
    if (error) return <p className="text-red-500">{error}</p>;

    return (
    <div className="flex flex-wrap gap-4">
            {vinyls.map((vinyl) => (
                <VinylCard
                    key={vinyl.id}
                    id={vinyl.id}
                    imageBase64={vinyl.imageBase64}
                    title={vinyl.title}
                    price={vinyl.price}
                    artist={vinyl.artist} // Ensure artist is passed here
                />
            ))}
        </div>
    );
};
//grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4 justify-items-center
export default VinylList;
