import React from "react";
import Image from "next/image";
import Link from "next/link";

function Footer() {
    return (
        <footer className="bg-light-gray">
            <div className=" mx-auto grid grid-cols-12 py-8 container">

                <div className="col-span-3">
                    <Link href="/" className="flex items-center">
                        <Image src="/vinyl-icon.svg" alt="Logo" width={30} height={30} />
                        <span className="ml-2  font-semibold text-3xl">Vinyl Shop</span>
                    </Link>
                    <p className="mt-4 text-sm">
                        Discover a world of vinyl treasures. Explore our wide collection and find the perfect record to complete your collection.
                    </p>
                    <p className="mt-2">
                        <i className="fa fa-phone"></i> (48) 578-320-405
                    </p>
                    <p className="mt-1">
                        <i className="fa fa-envelope"></i> support@vinylshop.com
                    </p>
                </div>

                <div className="col-span-2 pl-6">
                    <h3 className="font-semibold mb-3">My Account</h3>
                    <ul>
                        <li className="mb-2">
                            <Link href="/account">My Account</Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/orders">Order History</Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/wishlist">Wishlist</Link>
                        </li>
                        <li>
                            <Link href="/cart">Shopping Cart</Link>
                        </li>
                    </ul>
                </div>

                <div className="col-span-2">
                    <h3 className="font-semibold mb-3">Help</h3>
                    <ul>
                        <li className="mb-2">
                            <Link href="/contact">Contact Us</Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/faqs">FAQs</Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/terms">Terms & Conditions</Link>
                        </li>
                        <li>
                            <Link href="/privacy">Privacy Policy</Link>
                        </li>
                    </ul>
                </div>

                <div className="col-span-2">
                    <h3 className="font-semibold mb-3">Categories</h3>
                    <ul>
                        <li className="mb-2">
                            <Link href="/categories/classic">Classic</Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/categories/rock">Rock </Link>
                        </li>
                        <li className="mb-2">
                            <Link href="/categories/jazz">Jazz </Link>
                        </li>
                        <li>
                            <Link href="/categories/pop">Pop </Link>
                        </li>
                    </ul>
                </div>
                //todo add blik
                <div className="col-span-3 text-center">
                    <h3 className="font-semibold mb-3">Secure Payment</h3>
                    <div className="flex justify-center space-x-4">
                        <Image src="/apple-pay.svg" alt="Apple Pay" width={40} height={24} />
                        <Image src="/visa.svg" alt="Visa" width={40} height={24} />
                        <Image src="/mastercard.svg" alt="Mastercard" width={40} height={24} />
                        <Image src="/discover.svg" alt="Discover" width={40} height={24} />
                    </div>
                </div>
            </div>

            <div className="text-center py-4 bg-warning-yellow text-xs">
                <p>Vinyl Shop eCommerce © 2024. All Rights Reserved.</p>
            </div>
        </footer>
    );
}

export default Footer;
