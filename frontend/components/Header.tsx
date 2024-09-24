import React from 'react';
import Image from "next/image";
import Link from "next/link";
//todo transfer sing up/in 1 section below, shopping cart one section below too  
//todo delete all comments
//todo change middle section phone responsive with icons and nav from left 
function Hero() {
    return (
        <header className="bg-light-gray">
            {/* Top Section: Location & Store Details */}
            <div
                className="hidden sm:grid container mx-auto  grid-cols-12 items-center py-4"> {/* Changed this to container */}
                <div className="col-span-6 flex items-center">
                    <p className="text-gray-600 text-sm">
                        <i className="fa fa-map-marker"></i> Store Location: Kraków - ul. Floriańska 12, 31-021
                    </p>
                    <p className="text-gray-600 text-sm pl-5">
                        <i className="fa fa-phone"></i> (48) 578-320-405
                    </p>
                </div>
                <div className="col-span-6 flex justify-end space-x-4">
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
                    <div>
                        <Link href="/signin" className="text-sm">Sign In / Sign Up</Link>
                    </div>
                </div>
            </div>

            {/* Middle Section: Search Bar */}
            <div className="container mx-auto grid grid-cols-12 items-center py-4"> {/* Container for layout */}
                {/* Logo Section */}
                <div className="col-span-2 flex items-center"> {/* Flex for vertical alignment */}
                    <Link href="/" className="flex items-center">
                        <Image src="/vinyl-icon.svg" alt="Logo" width={30} height={30}/>
                        <span className="ml-2 text-black font-semibold text-3xl">Vinyl Shop</span>
                    </Link>
                </div>

                {/* Search Bar Section */}
                <div
                    className="col-start-5 col-span-5 sm:col-start-4 sm:col-span-6"> {/* Adjusting for responsiveness */}
                    <div className="flex items-center">
                        <input
                            type="text"
                            placeholder="Search"
                            className="w-full border rounded-l-md px-4 py-2"
                        />
                        <button className="bg-purple-600 text-white rounded-r-md px-4 py-2 ">Search</button>
                    </div>
                </div>

                {/* Wishlist and Cart Section */}
                <div className="col-start-11 col-span-2 flex justify-end items-center space-x-4">
                    <div>
                        <Link href="/wishlist">
                            <Image src="/heart.svg" alt="Heart" width={28} height={24}/>
                        </Link>
                    </div>

                    <div>
                        <Image src="/divider.svg" alt="Divider" width={1} height={24}/>
                    </div>

                    <div className="relative flex items-center">
                        <Link href="/cart" className="relative">
                            <Image src="/bag.svg" alt="Bag" width={26} height={26}/>
                            <span
                                className="absolute -top-2 -right-2 bg-purple-600 text-white rounded-full px-2 py-1 text-xs flex items-center justify-center">
                    2
                </span>
                        </Link>
                        <p className="ml-2 hidden md:block">$57.00</p> {/* Hide price on smaller screens */}
                    </div>
                </div>
            </div>


            {/* Bottom Section: Navigation Menu & Contact Info */}
            <div className="w-full bg-warning-yellow"> {/* Ensure full width */}
                <div
                    className="container mx-auto grid grid-cols-12 items-center py-4"> {/* Center content using container */}
                    <ul className="col-span-12 flex justify-start "> {/* Use flex layout for items */}
                        <li className="flex space-x-4">
                            <Link href="/"
                                  className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105">Home</Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/shop"
                                  className="text-black block hover:text-royal-purple  transition duration-300 ease-in-out transform hover:scale-105">
                                Shop
                            </Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/delivery"
                                  className="text-black block hover:text-royal-purple  transition duration-300 ease-in-out transform hover:scale-105">
                                Delivery
                            </Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/about"
                                  className="text-black block hover:text-royal-purple  transition duration-300 ease-in-out transform hover:scale-105">
                                About Us
                            </Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/contact"
                                  className="text-black block hover:text-royal-purple  transition duration-300 ease-in-out transform hover:scale-105">
                                Contact Us
                            </Link>
                        </li>
                        <li className="flex-1 text-center">
                            <Link href="/condition"
                                  className="text-black block hover:text-royal-purple  transition duration-300 ease-in-out transform hover:scale-105">
                                Vinyl condition grading system
                            </Link>
                        </li>
                    </ul>
                </div>
            </div>

            {/*bottom-test2*/}
            {/*<div className="w-full bg-warning-yellow"> /!* Ensure full width *!/*/}
            {/*    <div className="container mx-auto py-4"> /!* Center content using container *!/*/}
            {/*        <ul className="flex justify-start space-x-4"> /!* Use flex layout for items *!/*/}
            {/*            {[*/}
            {/*                {href: "/", label: "Home"},*/}
            {/*                {href: "/shop", label: "Shop"},*/}
            {/*                {href: "/delivery", label: "Delivery"},*/}
            {/*                {href: "/about", label: "About Us"},*/}
            {/*                {href: "/contact", label: "Contact Us"},*/}
            {/*                {href: "/condition", label: "Vinyl Condition Grading System"},*/}
            {/*            ].map(({href, label}) => (*/}
            {/*                <li key={href}>*/}
            {/*                    <Link*/}
            {/*                        href={href}*/}
            {/*                        className="text-black block hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105 hover:shadow-lg rounded-md px-4 py-2"*/}
            {/*                    >*/}
            {/*                        {label}*/}
            {/*                    </Link>*/}
            {/*                </li>*/}
            {/*            ))}*/}
            {/*        </ul>*/}
            {/*    </div>*/}
            {/*</div>*/}


            {/*bottom-test*/}
            {/*<div className="w-full bg-warning-yellow shadow-md"> /!* Add shadow for depth *!/*/}
            {/*    <div className="container mx-auto py-4"> /!* Center content using container *!/*/}
            {/*        <ul className="flex justify-between"> /!* Use flex layout for items *!/*/}
            {/*            {["Home", "Shop", "Delivery", "About Us", "Contact Us", "Vinyl Condition Grading System"].map((item) => (*/}
            {/*                <li className="flex-1 text-center" key={item}>*/}
            {/*                    <Link*/}
            {/*                        href={`/${item.toLowerCase().replace(/\s+/g, '-')}`} // Create dynamic routes*/}
            {/*                        className="text-black block py-2 hover:text-royal-purple transition duration-300 ease-in-out transform hover:scale-105" // Add hover effect and transition*/}
            {/*                    >*/}
            {/*                        {item}*/}
            {/*                    </Link>*/}
            {/*                </li>*/}
            {/*            ))}*/}
            {/*        </ul>*/}
            {/*    </div>*/}
            {/*</div>*/}


        </header>
    );
}

export default Hero;
