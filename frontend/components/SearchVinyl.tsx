"use client";
import React, { useState } from 'react';
import { Vinyl } from '@/types/vinyl';
import Link from 'next/link';

const SearchVinyl = () => {
    const [searchQuery, setSearchQuery] = useState('');
    const [searchResults, setSearchResults] = useState<Vinyl[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleSearch = async () => {
        if (!searchQuery) return;

        setLoading(true);
        setError('');

        try {
            const response = await fetch(`https://localhost:44372/vinyls/search?searchTerm=${encodeURIComponent(searchQuery)}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                },
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            setSearchResults(data);
        } catch (error) {
            setError('Search failed. Please try again.');
            console.error("Search failed:", error);
        } finally {
            setLoading(false);
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(e.target.value);
    };

    return (
        <div className="flex flex-col items-center w-full">
            <div className="flex items-center w-full"> {/* Removed mb-4 to keep search bar aligned */}
                <input
                    type="text"
                    value={searchQuery}
                    onChange={handleInputChange}
                    placeholder="Search Vinyl"
                    className="flex-grow border rounded-l-md px-4 py-2" // Input takes full space
                />
                <button
                    className="bg-purple-600 text-white rounded-r-md px-4 py-2 w-32" // Fixed width for the button
                    onClick={handleSearch}
                >
                    Search
                </button>
            </div>

            {loading && <p>Loading...</p>}
            {error && <p className="text-red-500">{error}</p>}

            {searchResults.length > 0 && (
                <ul className="mt-4 w-full max-w-md"> {/* Optional: Add max width for results */}
                    {searchResults.map((result) => (
                        <li key={result.id} className="py-1">
                            <Link href={`/vinyls/${result.id}`}>
                                {result.title} by {result.artist}
                            </Link>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default SearchVinyl;
