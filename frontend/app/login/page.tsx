'use client';

import { useState } from "react";
import Cookies from 'js-cookie';
import api from '../../utils/api'; // Adjust the path as needed
import { useRouter } from 'next/navigation';
import {jwtDecode} from "jwt-decode";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState('');
    const router = useRouter();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await api.post('/login', { email, password });

            console.log("Login API response:", response);

            const token = response.data;
            if (token) {
                Cookies.remove('secretCookie');

                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });
                console.log('Token set in cookie:', token);

                const decodedToken: any = jwtDecode(token);
                console.log("Decoded Token:", decodedToken);
                if (decodedToken?.userId) {
                    router.push('/');
                } else {
                    setError('Failed to retrieve userId from token');
                }
            } else {
                setError('Failed to retrieve token from the API response');
            }
        } catch (err) {
            console.error("Login error:", err);
            setError('Invalid email or password');
        }
    };

    return (
        <main className="flex-grow flex items-center justify-center">
            <div className="bg-white p-10 rounded-lg shadow-md max-w-xl w-full my-20 mx-4">
                <h2 className="text-3xl font-bold text-center mb-6">Sign In</h2>
                {error && <p className="text-red-500 text-center">{error}</p>}
                <form onSubmit={handleSubmit}>
                    <div className="mb-4">
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="email">
                            Email
                        </label>
                        <input
                            id="email"
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className="w-full p-4 border rounded-lg focus:outline-none focus:border-purple-600"
                            placeholder="Email"
                            required
                        />
                    </div>
                    <div className="mb-4">
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="password">
                            Password
                        </label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="w-full p-4 border rounded-lg focus:outline-none focus:border-purple-600"
                            placeholder="Password"
                            required
                        />
                    </div>
                    <button
                        type="submit"
                        className="mt-4 w-full bg-purple-600 text-white p-4 rounded-lg font-semibold hover:bg-deep-purple"
                    >
                        Login
                    </button>
                </form>
            </div>
        </main>
    );
};

export default LoginPage;
