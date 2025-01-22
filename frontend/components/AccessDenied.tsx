'use client';

import React from 'react';

interface AccessDeniedProps {
    role: string;
}

const AccessDenied: React.FC<AccessDeniedProps> = ({ role }) => {
    return (
        <div className="flex items-center justify-center h-screen bg-gray-100">
            <div className="w-full max-w-lg p-8 bg-white shadow-md rounded-lg text-center">
                <h1 className="text-3xl font-bold text-red-500">Access Denied</h1>
                <p className="mt-4 text-gray-600">You do not have access to this page.</p>
                <p className="mt-4 text-lg">
                    Your Role: <strong>{role}</strong>
                </p>
                <p className="mt-4 text-gray-400">
                    Please contact an administrator if you believe this is an error.
                </p>
            </div>
        </div>
    );
};

export default AccessDenied;
