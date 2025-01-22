"use client"

import React, { useState } from 'react';
import { useAuthContext } from '@/context/AuthContext';
import AccessDenied from '@/components/AccessDenied';
import api from "@/utils/api";
import { getSecretCookie } from "@/utils/cookies";

const VinylDashboard = () => {
    const [formData, setFormData] = useState({
        title: '',
        artist: '',
        genre: '',
        releaseYear: 0,
        price: 0,
        stock: 0,
        description: '',
        isAvailable: true,
        imageFile: null as File | null,
    });
    const { user, isAdmin, loading } = useAuthContext();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setFormData(prevState => ({
                ...prevState,
                imageFile: e.target.files[0],
            }));
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const form = new FormData();
        form.append('title', formData.title);
        form.append('artist', formData.artist);
        form.append('genre', formData.genre);
        form.append('releaseYear', String(formData.releaseYear));
        form.append('price', String(formData.price));
        form.append('stock', String(formData.stock));
        form.append('description', formData.description);
        form.append('isAvailable', String(formData.isAvailable));
        if (formData.imageFile) {
            form.append('ImageFile', formData.imageFile);
        }

        const token = getSecretCookie();
        try {
            const response = await api.post('/vinyls', form, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    'Authorization': `Bearer ${token}`,
                },
            });

            if (response.status === 200) {
                alert('Vinyl added successfully!');
                setFormData({
                    title: '',
                    artist: '',
                    genre: '',
                    releaseYear: 0,
                    price: 0,
                    stock: 0,
                    description: '',
                    isAvailable: true,
                    imageFile: null,
                });
            }
        } catch (error) {
            alert('Error adding vinyl');
        }
    };

    if (!isAdmin) {
        return <AccessDenied role={user?.Role || 'Guest'} />;
    }

    return (
        <div className="vinyl-dashboard max-w-4xl mx-auto p-6 bg-white rounded-lg shadow-lg">
            <h2 className="text-3xl font-semibold text-center text-deep-purple-700 mb-6">Add Vinyl</h2>
            <form onSubmit={handleSubmit} encType="multipart/form-data">
                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Title</label>
                    <input
                        type="text"
                        name="title"
                        value={formData.title}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                        required
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Artist</label>
                    <input
                        type="text"
                        name="artist"
                        value={formData.artist}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                        required
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Genre</label>
                    <input
                        type="text"
                        name="genre"
                        value={formData.genre}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Release Year</label>
                    <input
                        type="number"
                        name="releaseYear"
                        value={formData.releaseYear}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                        required
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Price</label>
                    <input
                        type="number"
                        name="price"
                        value={formData.price}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                        required
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Stock</label>
                    <input
                        type="number"
                        name="stock"
                        value={formData.stock}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Description</label>
                    <textarea
                        name="description"
                        value={formData.description}
                        onChange={handleInputChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                    ></textarea>
                </div>

                <div className="mb-6 flex items-center gap-4">
                    <label className="text-lg font-medium text-gray-700">Is Available</label>
                    <input
                        type="checkbox"
                        name="isAvailable"
                        checked={formData.isAvailable}
                        onChange={(e) =>
                            setFormData({
                                ...formData,
                                isAvailable: e.target.checked,
                            })
                        }
                        className="h-5 w-5 text-deep-purple-500 border-gray-300 rounded"
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-lg font-medium text-gray-700 mb-2">Image File</label>
                    <input
                        type="file"
                        name="imageFile"
                        accept="image/*"
                        onChange={handleFileChange}
                        className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-deep-purple-500"
                    />
                </div>

                <div className="flex gap-4 justify-center">
                    <button
                        type="submit"
                        className="bg-royal-purple text-white text-lg rounded-full px-8 py-3 hover:bg-deep-purple-700 transition-colors w-full sm:w-auto">
                        Add Vinyl
                    </button>
                    <button
                        type="reset"
                        onClick={() => setFormData({
                            title: '',
                            artist: '',
                            genre: '',
                            releaseYear: 0,
                            price: 0,
                            stock: 0,
                            description: '',
                            isAvailable: true,
                            imageFile: null,
                        })}
                        className="bg-red-500 text-white text-lg rounded-full px-8 py-3 hover:bg-red-700 transition-colors"
                    >
                        Reset
                    </button>
                </div>
            </form>
        </div>
    );
};

export default VinylDashboard;
