"use client";

import React from "react";
import { loadStripe, StripeError } from "@stripe/stripe-js";

const stripePromise = loadStripe("pk_test_51QKsJJHqGo0KeykHjiMci68gs5tv5Ym5wgt2WXb4zRHaID0V3AsQbjXSiuRJKD0FWBi9kH0LPtt6aZ37jac8azFa00OFZkimTs"); // Replace with your Stripe publishable key

const CheckoutButton = () => {
    const handleCheckout = async () => {
        try {
            const response = await fetch("https://localhost:44372/payments/create-checkout-session", {
                method: "POST",
            });

            if (!response.ok) {
                throw new Error("Failed to create checkout session");
            }

            const { sessionId } = await response.json();

            if (!sessionId) {
                throw new Error("No session ID returned from the backend");
            }

            const stripe = await stripePromise;

            if (!stripe) {
                throw new Error("Stripe.js failed to load.");
            }

            const { error } = await stripe.redirectToCheckout({ sessionId });

            if (error) {
                console.error("Error redirecting to Stripe Checkout:", error.message);
            }
        } catch (error) {
            console.error("Error during checkout:", error);
        }
    };

    return (
        <button
            onClick={handleCheckout}
            className="bg-purple-500 hover:bg-purple-700 text-white font-bold py-2 px-4 rounded"
        >
            Checkout
        </button>
    );
};

export default CheckoutButton;
