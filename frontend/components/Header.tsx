'use client';
import React, { useEffect, useState, useRef } from "react";
import Image from "next/image";
import Link from "next/link";
import { useRouter } from "next/navigation";
import SearchVinyl from "@/components/SearchVinyl";
import { FaUser } from "react-icons/fa";
import { FiChevronDown, FiChevronUp } from "react-icons/fi";
import { useCartContext } from "@/context/CartContext";
import { useAuthContext } from "@/context/AuthContext";

function Header() {
    const { cart, totalAmount, setCart } = useCartContext(); // Use CartContext for cart and totalAmount
    const { user, isLoggedIn, logout } = useAuthContext(); // Get auth context values
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const router = useRouter();
    const dropdownRef = useRef<HTMLDivElement>(null);

    const handleLogout = () => {
        logout(); // Use logout from AuthContext
        setDropdownOpen(false);
        setCart([]); // Clear cart
        localStorage.removeItem("cart");
        router.push("/"); // Redirect to homepage
    };

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                setDropdownOpen(false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <header className="bg-light-gray">
            <div className="hidden sm:grid container mx-auto grid-cols-12 items-center py-4">
                <div className="col-span-6 flex items-center">
                    <p className="text-gray-600 text-sm">
                        <i className="fa fa-map-marker"></i> Store Location: Kraków - ul. Floriańska 12, 31-021
                    </p>
                    <p className="text-gray-600 text-sm pl-5">
                        <i className="fa fa-phone"></i> (48) 578-320-405
                    </p>
                </div>
                <div className="col-span-6 flex justify-end space-x-4 text-black">
                    <div>
                        <select className="text-sm bg-light-gray">
                            <option>Eng</option>
                            <option>Other</option>
                        </select>
                    </div>
                    <div>
                        <select className="text-sm bg-light-gray">
                            <option>USD</option>
                            <option>EUR</option>
                        </select>
                    </div>
                    {isLoggedIn ? (
                        <div className="relative" ref={dropdownRef}>
                            <button
                                onClick={() => setDropdownOpen(!dropdownOpen)}
                                className="flex items-center space-x-2 text-sm">
                                <FaUser size={20} />
                                {dropdownOpen ? (
                                    <FiChevronUp size={20} />
                                ) : (
                                    <FiChevronDown size={20} />
                                )}
                            </button>
                            {dropdownOpen && (
                                <div
                                    className="absolute right-0 mt-2 w-48 bg-white border border-gray-200 rounded shadow-md"
                                    style={{ zIndex: 50 }}>
                                    <div className="px-4 py-2 text-sm text-gray-700">
                                        <p>User Info</p>
                                        <p>{user?.email}</p>
                                    </div>
                                    <button
                                        onClick={handleLogout}
                                        className="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">
                                        Logout
                                    </button>
                                </div>
                            )}
                        </div>
                    ) : (
                        <Link href="/login" className="text-sm">Sign In / Sign Up</Link>
                    )}
                </div>
            </div>

            <div className="container mx-auto grid grid-cols-12 items-center py-4">
                <div className="col-span-3 flex items-center">
                    <Link href="/" className="flex items-center">
                        <Image src="/vinyl-icon.svg" alt="Logo" width={30} height={30} />
                        <span className="ml-2 text-black font-semibold text-3xl">AmberVinyl Store</span>
                    </Link>
                </div>

                <div className="col-start-4 col-span-6">
                    <SearchVinyl />
                </div>

                <div className="col-start-11 col-span-2 flex justify-end items-center space-x-4">
                    <div>
                        <Link href="/wishlist">
                            <Image src="/heart.svg" alt="Heart" width={28} height={24} />
                        </Link>
                    </div>

                    <div>
                        <Image src="/divider.svg" alt="Divider" width={1} height={24} />
                    </div>

                    <div className="relative flex items-center">
                        <Link href="/cart" className="relative">
                            <Image src="/bag.svg" alt="Bag" width={26} height={26} />
                            <span
                                className="absolute -top-2 -right-2 bg-purple-600 text-white rounded-full px-2 py-1 text-xs flex items-center justify-center">
                                <p>{cart.length}</p>
                            </span>
                        </Link>
                        <p className="ml-2 hidden md:block">Total: ${totalAmount.toFixed(2)}</p>
                    </div>
                </div>
            </div>

            <div className="w-full bg-warning-yellow">
                <div className="container mx-auto grid grid-cols-12 items-center py-4">
                    <ul className="col-span-12 flex justify-start">
                        <li className="flex space-x-4">
                            <Link href="/"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105 pl-3">Home</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/shop"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105">Shop</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/delivery"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105">Delivery</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/about"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105">About Us</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/contact"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105 pr-3">Contact</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/condition"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105">
                                Vinyl condition grading system</Link>
                        </li>
                    </ul>
                </div>
            </div>
        </header>
    );
}

export default Header;
