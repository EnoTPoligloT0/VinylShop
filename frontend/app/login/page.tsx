"use client";

import { useState } from "react";
import Cookies from 'js-cookie';
import api from '../../utils/api';
import { useRouter } from 'next/navigation';
import { useAuthContext } from "@/context/AuthContext";  // Import AuthContext
import { jwtDecode } from "jwt-decode";
import Link from "next/link";
import { GoogleLogin } from "@react-oauth/google";
import { CredentialResponse } from "@react-oauth/google";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState('');
    const router = useRouter();
    const { login } = useAuthContext();  // Get login function from AuthContext

    const handleGoogleLogin = async (response: CredentialResponse) => {
        try {
            const googleToken = response.credential;  // Explicitly typed as string
            const loginResponse = await api.post('/login/google', { googleToken });

            const token = loginResponse.data;
            if (token) {
                Cookies.remove('secretCookie');
                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });

                const decodedToken: any = jwtDecode(token);
                if (decodedToken?.userId) {
                    login(token);  // Log the user in using the AuthContext login function
                    router.push('/');
                } else {
                    setError('Failed to retrieve userId from token');
                }
            } else {
                setError('Failed to retrieve token from the API response');
            }
        } catch (err) {
            console.error("Google login error:", err);
            setError('Failed to authenticate with Google');
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const loginResponse = await api.post('/login', { email, password });

            const token = loginResponse.data;
            if (token) {
                Cookies.remove('secretCookie');
                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });

                const decodedToken: any = jwtDecode(token);
                if (decodedToken?.userId) {
                    login(token);
                    router.push('/');
                } else {
                    setError('Failed to retrieve userId from token');
                }
            } else {
                setError('Failed to authenticate. Please check your credentials.');
            }
        } catch (err) {
            setError('An error occurred during login. Please try again later.');
        }
    };

    return (
        <main className="flex-grow flex items-center justify-center">
            <div className="bg-white p-10 rounded-lg shadow-md max-w-xl w-full my-20 mx-4">
                <h2 className="text-3xl font-bold text-center mb-6">Login</h2>
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

                    <button
                        type="submit"
                        className="mt-4 w-full bg-purple-600 text-white p-4 rounded-lg font-semibold hover:bg-purple-700">
                        Login
                    </button>

                    <div className="mt-4 text-center">
                        Don't have an account?{" "}
                        <Link href="/register" className="text-purple-600 hover:underline">Register</Link>
                    </div>

                    <GoogleLogin
                        onSuccess={handleGoogleLogin}
                        onError={() => setError("Google login failed.")}
                    />
                </form>
            </div>
        </main>
    );
};

export default LoginPage;
