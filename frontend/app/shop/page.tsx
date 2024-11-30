"use client";
import React, { useState } from 'react';
import VinylList from '@/components/VinylList';

const ShopPage = () => {
    const [sortOption, setSortOption] = useState("Latest");
    const [genres, setGenres] = useState<string[]>([]);
    const [years, setYears] = useState<string[]>([]);

    const handleSortOptionChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSortOption(e.target.value);
    };

    const handleGenreChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const genre = e.target.id;
        setGenres(prevGenres =>
            e.target.checked ? [...prevGenres, genre] : prevGenres.filter(g => g !== genre)
        );
    };

    const handleYearChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const year = e.target.id;
        setYears(prevYears =>
            e.target.checked ? [...prevYears, year] : prevYears.filter(y => y !== year)
        );
    };

    return (
        <main className="container mx-auto">
            <div className="flex mt-8">
                <aside className="w-1/4 pr-6">
                    <div className="bg-white p-4 shadow-md rounded-lg">
                        <h2 className="text-lg font-semibold mb-4">All Categories</h2>
                        <ul>
                            {['rock', 'pop', 'jazz', 'classical', 'hip-hop', 'electronic', 'country'].map((genre) => (
                                <li key={genre} className="mb-2">
                                    <input
                                        type="checkbox"
                                        id={genre}
                                        onChange={handleGenreChange}
                                    />
                                    <label htmlFor={genre} className="ml-2">{genre.charAt(0).toUpperCase() + genre.slice(1)}</label>
                                </li>
                            ))}
                        </ul>

                        <div className="mt-6">
                            <h3 className="text-lg font-semibold">Year of Release</h3>
                            <ul>
                                {['2020', '2010', '2000', '1990', '1980', '1970', '1960'].map((year) => (
                                    <li key={year} className="mb-2">
                                        <input
                                            type="checkbox"
                                            id={year}
                                            onChange={handleYearChange}
                                        />
                                        <label htmlFor={year} className="ml-2">{year}</label>
                                    </li>
                                ))}
                            </ul>
                        </div>

                        <div className="mt-6">
                            <h3 className="text-lg font-semibold">Price</h3>
                            <input type="range" min="1" max="150" className="w-full mt-2 cursor-pointer"/>
                        </div>
                    </div>
                </aside>

                <main className="w-3/4">
                    <div className="flex justify-between items-center mb-8">
                        <h1 className="text-3xl font-semibold">Products</h1>
                        <div>
                            <label htmlFor="sort" className="mr-2">Sort by:</label>
                            <select id="sort" value={sortOption} onChange={handleSortOptionChange} className="p-2 border border-gray-300 rounded">
                                <option value="Latest">Latest</option>
                                <option value="Popular">Popular</option>
                                <option value="Price: Low to High">Price: Low to High</option>
                                <option value="Price: High to Low">Price: High to Low</option>
                            </select>
                        </div>
                    </div>

                    <div className="grid mb-24">
                        <VinylList genres={genres} years={years} sortOption={sortOption} />
                    </div>
                </main>
            </div>
        </main>
    );
};

export default ShopPage;
