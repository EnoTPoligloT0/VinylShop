import React from "react";
import CheckoutButton from "@/components/CheckoutButton";


const CheckoutPage = () => {
    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <h1 className="text-2xl font-bold mb-4">Ready to Checkout?</h1>
            <CheckoutButton />
        </div>
    );
};

export default CheckoutPage;
