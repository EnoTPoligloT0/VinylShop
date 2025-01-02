"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { useAuthContext } from "@/context/AuthContext";  // Import AuthContext
import api from "../../utils/api";
import Cookies from "js-cookie";
import { jwtDecode } from "jwt-decode";
import { AxiosError } from "axios";

const RegisterPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();
    const { login } = useAuthContext();  // Get login function from AuthContext

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (password !== confirmPassword) {
            setError('Passwords do not match.');
            return;
        }

        try {
            await api.post('/register', { email, password });

            const loginResponse = await api.post('/login', { email, password });

            const token = loginResponse.data;
            if (token) {
                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });

                const decodedToken: any = jwtDecode(token);
                if (decodedToken?.userId) {
                    login(token);
                    router.replace('/');
                } else {
                    setError('Failed to retrieve userId from token');
                }
            } else {
                setError('No token received. Login failed.');
            }
        } catch (error) {
            if (error instanceof AxiosError) {
                setError('Registration or login failed. Please try again.');
            } else {
                setError('An unexpected error occurred. Please try again later.');
            }
        }
    };

    return (
        <main className="flex-grow flex items-center justify-center">
            <div className="bg-white p-10 rounded-lg shadow-md max-w-xl w-full my-20 mx-4">
                <h2 className="text-3xl font-bold text-center mb-6">Register</h2>
                {error && <p className="text-red-500 text-center mb-4">{error}</p>}
                <form onSubmit={handleSubmit}>
                    <div className="mb-4">
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="email">Email</label>
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
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="password">Password</label>
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

                    <div className="mb-4">
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="confirm-password">Confirm Password</label>
                        <input
                            id="confirm-password"
                            type="password"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            className="w-full p-4 border rounded-lg focus:outline-none focus:border-purple-600"
                            placeholder="Confirm Password"
                            required
                        />
                    </div>

                    <button
                        type="submit"
                        className="mt-4 w-full bg-purple-600 text-white p-4 rounded-lg font-semibold hover:bg-purple-700"
                    >
                        Register
                    </button>

                    <div className="mt-4 text-center">
                        Already have an account?{" "}
                        <Link href="/login" className="text-purple-600 hover:underline">Sign In</Link>
                    </div>
                </form>
            </div>
        </main>
    );
};

export default RegisterPage;
