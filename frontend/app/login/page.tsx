'use client';

import {useState} from "react";
import api from '../../utils/api'; // Adjust the path as needed
import {useRouter} from 'next/navigation';
import Link from "next/link";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState('');
    const router = useRouter();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await api.post('/login', {email, password});
            console.log(response.data); // Handle success response
            router.push('/dashboard'); // Redirect after successful login
        } catch (err) {
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
                        className="mt-4 w-full bg-purple-600 text-white p-4 rounded-lg font-semibold hover:bg-deep-purple"
                    >
                        Login
                    </button>

                    <div className="mt-4 text-center">
                        Don’t have an account?{" "}
                        <Link href="/register" className="text-purple-600 hover:underline">
                            Register
                        </Link>
                    </div>
                </form>
            </div>
        </main>

    );
};

export default LoginPage;
