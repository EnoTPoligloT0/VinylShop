"use client";

import { useState } from "react";
import Cookies from 'js-cookie';
import api from '../../utils/api';
import { useRouter } from 'next/navigation';
import { jwtDecode } from "jwt-decode";
import Link from "next/link";
import { GoogleLogin } from "@react-oauth/google";
import { CredentialResponse } from "@react-oauth/google";

//todo redundant code
const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState('');
    const router = useRouter();

    const handleGoogleLogin = async (response: CredentialResponse) => {
        try {
            const googleToken = response.credential;  // Explicitly typed as string
            const loginResponse = await api.post('/login/google', { googleToken });

            console.log("Google Login API response.");

            const token = loginResponse.data;
            if (token) {
                Cookies.remove('secretCookie');

                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });
                console.log('Token set in cookie.');

                const decodedToken: any = jwtDecode(token);
                console.log("Decoded Token.");
                if (decodedToken?.userId) {
                    router.push('/');
                    router.refresh();
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

            console.log("Login API response.");

            const token = loginResponse.data;
            if (token) {
                Cookies.remove('secretCookie');

                Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });
                console.log('Token set in cookie.');

                const decodedToken: any = jwtDecode(token);
                console.log("Decoded Token.");
                if (decodedToken?.userId) {
                    router.push('/');
                    router.refresh();
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

                    <div className="flex items-center justify-between mb-4">
                        <div className="flex items-center">
                            <input id="remember" type="checkbox" className="mr-2 leading-tight"/>
                            <label className="text-sm text-gray-600" htmlFor="remember">
                                Remember me
                            </label>
                        </div>
                        <Link href="#" className="text-sm text-gray-600 hover:underline">
                            Forgot Password?
                        </Link>
                    </div>

                    <button
                        type="submit"
                        className="mt-4 w-full bg-purple-600 text-white p-4 rounded-lg font-semibold hover:bg-deep-purple">
                        Login
                    </button>

                    <div className="mt-4 text-center">
                        Don’t have an account?{" "}
                        <Link href="/register" className="text-purple-600 hover:underline">
                            Register
                        </Link>
                    </div>
                </form>

                <div className="mt-6">
                    <GoogleLogin
                        onSuccess={handleGoogleLogin}
                        onError={() => setError('Google login failed')} // Correct type for error handler
                    />
                </div>
            </div>
        </main>
    );
};

export default LoginPage;
