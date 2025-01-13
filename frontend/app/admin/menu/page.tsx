'use client';

import React from 'react';
import { useAuthContext } from '@/context/AuthContext';
import AccessDenied from '@/components/AccessDenied';
import {useRouter} from "next/navigation";
const AdminPage: React.FC = () => {
    const router = useRouter();
    const { user, isAdmin, loading } = useAuthContext();

    if (loading) {
        return (
            <div className="flex items-center justify-center h-screen bg-gray-100">
                <p className="text-xl text-gray-600">Loading...</p>
            </div>
        );
    }

    if (!isAdmin) {
        return <AccessDenied role={user?.Role || 'Guest'} />;
    }

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
            <div className="w-full max-w-4xl p-8 bg-white shadow-md rounded-lg">
                <h1 className="text-3xl font-bold text-purple-700">Admin Dashboard</h1>
                <p className="mt-4 text-gray-600">Welcome to the Admin-only area!</p>

                <div className="mt-8">
                    <h2 className="text-2xl font-semibold text-gray-700">Admin Tools</h2>
                    <button
                        onClick={() => router.push('/admin/vinyls-dashboard')}
                        className="mt-2 text-xl text-black bg-royal-purple rounded-xl mr-2 p-3">
                        Add Vinyls
                    </button>
                    <button
                        onClick={() => router.push('/admin/orders-dashboard')}
                        className="mt-2 text-xl text-black bg-royal-purple rounded-xl mr-2 p-3">
                        Orders
                    </button>
                </div>

                <div className="mt-8">
                    <h2 className="text-xl font-semibold text-gray-700">Recent Activity</h2>
                    <ul className="mt-2 space-y-2">
                        <li className="flex justify-between text-gray-600">
                            <span>User added</span>
                            <span>Just now</span>
                        </li>
                        <li className="flex justify-between text-gray-600">
                            <span>Vinyl sold</span>
                            <span>2 minutes ago</span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default AdminPage;
