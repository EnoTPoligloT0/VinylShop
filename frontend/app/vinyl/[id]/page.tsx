    "use client";
    
    import React, {useEffect, useState} from 'react';
    import {useRouter} from 'next/navigation';
    import {Vinyl} from '@/types/vinyl';
    import Image from 'next/image';
    
    //todo list of tracks after the additional information
    const VinylDetail = ({params}: { params: { id: string } }) => {
        const [vinyl, setVinyl] = useState<Vinyl | null>(null);
        const [loading, setLoading] = useState(true);
        const [error, setError] = useState('');
    
        const id = params.id;
    
        useEffect(() => {
            if (!id) return;
    
            const fetchVinyl = async () => {
                try {
                    const response = await fetch(`https://localhost:44372/vinyls/${id}`, {
                        method: 'GET',
                        headers: {
                            'Accept': 'application/json',
                        },
                    });
    
                    if (!response.ok) {
                        throw new Error('Failed to fetch vinyl details');
                    }
    
                    const data = await response.json();
                    setVinyl(data);
                } catch (error) {
                    setError('Failed to fetch vinyl details. Please try again.');
                    console.error('Error fetching vinyl:', error);
                } finally {
                    setLoading(false);
                }
            };
    
            fetchVinyl();
        }, [id]);
    
        if (loading) return <p>Loading vinyl details...</p>;
        if (error) return <p className="text-red-500">{error}</p>;
        if (!vinyl) return null;
    
        return (
            <div className="container mx-auto p-8 bg-white rounded-xl shadow-lg mt-5 mb-10">
                <div className="flex flex-col sm:flex-row gap-10">
                    <div className="relative w-full sm:w-2/5 h-auto">
                        <Image
                            src={`data:image/jpeg;base64,${vinyl.imageBase64}`}
                            alt={vinyl.title}
                            layout="responsive"
                            width={800}
                            height={800}
                            objectFit="cover"
                            className="rounded-lg border-2 border-purple-300 shadow-xl transition-transform duration-500 hover:scale-105"
                        />
                    </div>
    
    
                    <div className="flex flex-col justify-between w-full sm:w-3/5 ">
                        <div>
                            <h1 className="text-5xl font-bold text-deep-purple mb-2">{vinyl.title}</h1>
                            <p className="text-xl text-gray-500 mb-4">By {vinyl.artist}</p>
                            <p className="text-6xl font-bold text-royal-purple mb-1">${vinyl.price.toFixed(2)}</p>
                            <p className="text-l font-bold text-primary-deep-purple mb-5">In
                                stock: {vinyl.stock || 'Out of Stock'}</p>
                        </div>

                        <div className="flex items-center gap-4 mb-6">
                            <div className="flex items-center border border-gray-300 rounded-3xl px-4 py-3 text-lg">
                                <button className="text-gray-600 hover:text-purple-600 transition">-</button>
                                <span className="mx-6">1</span>
                                <button className="text-gray-600 hover:text-purple-600 transition">+</button>
                            </div>

                            <button
                                className="w-1/3 bg-purple-600 text-white py-3 px-8 rounded-3xl text-lg font-medium shadow-md hover:bg-purple-700 transition duration-300 ease-in-out transform hover:scale-105">
                                Add to Cart
                            </button>
                            <div
                                className="p-1 rounded-3xl border border-gray-300 shadow-md hover:bg-purple-100 transition duration-300 ease-in-out transform hover:scale-105">
                                <Image
                                    src="/heart.svg"
                                    alt="Favourite"
                                    width={40}
                                    height={40}
                                    objectFit="cover"
                                    className="text-gray-600 hover:text-purple-600"
                                />
                            </div>
                        </div>


                        <div className="bg-gray-100 p-6 rounded-lg shadow-inner">
                            <h2 className="text-2xl font-semibold text-purple-900 mb-4">Additional Information</h2>
                            <p className="text-lg text-gray-700 mb-2">Genre: {vinyl.genre || 'N/A'}</p>
                            <p className="text-lg text-gray-700 mb-2">Release Year: {vinyl.releaseYear || 'N/A'}</p>
                        </div>
                    </div>
                </div>
    
    
                <div className="mt-10 bg-gray-50 p-6 rounded-lg shadow-md text-center">
                    <h2 className="text-3xl font-semibold text-purple-900 mb-4">Description</h2>
                    <p className="text-lg text-gray-700 leading-relaxed">{vinyl.description}</p>
                </div>
            </div>
    
        );
    };
    
    export default VinylDetail;
