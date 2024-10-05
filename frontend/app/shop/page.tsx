"use client";
import React, { useState } from 'react';
import VinylList from '@/components/VinylList'; // Adjust the import path based on your folder structure

const ShopPage = () => {
    const [filter, setFilter] = useState("Latest");

    const handleFilterChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setFilter(e.target.value);
    };

    return (
        <main className="container mx-auto">
            <div className="flex mt-8">
                {/* Sidebar */}
                <aside className="w-1/4 pr-6">
                    <div className="bg-white p-4 shadow-md rounded-lg">
                        <h2 className="text-lg font-semibold mb-4">All Categories</h2>
                        <ul>
                            <li className="mb-2">
                                <input type="checkbox" id="rock"/>
                                <label htmlFor="rock" className="ml-2">Rock (125)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="pop"/>
                                <label htmlFor="pop" className="ml-2">Pop (90)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="jazz"/>
                                <label htmlFor="jazz" className="ml-2">Jazz (60)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="classical"/>
                                <label htmlFor="classical" className="ml-2">Classical (45)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="hip-hop"/>
                                <label htmlFor="hip-hop" className="ml-2">Hip-Hop (80)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="electronic"/>
                                <label htmlFor="electronic" className="ml-2">Electronic (70)</label>
                            </li>
                            <li className="mb-2">
                                <input type="checkbox" id="country"/>
                                <label htmlFor="country" className="ml-2">Country (55)</label>
                            </li>
                        </ul>

                        <div className="mt-6">
                            <h3 className="text-lg font-semibold">Year of Release</h3>
                            <ul>
                                <li className="mb-2">
                                    <input type="checkbox" id="2020s"/>
                                    <label htmlFor="2020s" className="ml-2">2020s (30)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="2010s"/>
                                    <label htmlFor="2010s" className="ml-2">2010s (50)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="2000s"/>
                                    <label htmlFor="2000s" className="ml-2">2000s (65)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="1990s"/>
                                    <label htmlFor="1990s" className="ml-2">1990s (70)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="1980s"/>
                                    <label htmlFor="1980s" className="ml-2">1980s (85)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="1970s"/>
                                    <label htmlFor="1970s" className="ml-2">1970s (90)</label>
                                </li>
                                <li className="mb-2">
                                    <input type="checkbox" id="1960s"/>
                                    <label htmlFor="1960s" className="ml-2">1960s (100)</label>
                                </li>
                            </ul>
                        </div>

                        <div className="mt-6">
                            <h3 className="text-lg font-semibold">Price</h3>
                            <input type="range" min="1" max="150" className="w-full mt-2 cursor-pointer"/>
                        </div>
                    </div>
                </aside>
                {/* Main Content - Products */}
                <main className="w-3/4">
                    {/* Sort By */}
                    <div className="flex justify-between items-center mb-8">
                        <h1 className="text-3xl font-semibold">Products</h1>
                        <div>
                            <label htmlFor="sort" className="mr-2">Sort by:</label>
                            <select id="sort" value={filter} onChange={handleFilterChange}
                                    className="p-2 border border-gray-300 rounded">
                                <option value="Latest">Latest</option>
                                <option value="Popular">Popular</option>
                                <option value="Price: Low to High">Price: Low to High</option>
                                <option value="Price: High to Low">Price: High to Low</option>
                            </select>
                        </div>
                    </div>

                    <div className="grid mb-24">
                        <VinylList/>
                    </div>
                </main>
            </div>
        </main>
    );
};

export default ShopPage;
