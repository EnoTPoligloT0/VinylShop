'use client';

import { useEffect, useState } from 'react';
import { useRouter, useSearchParams } from "next/navigation";
import axios from "axios";
import Cookies from "js-cookie";

const Success = () => {
    const router = useRouter();
    const searchParams = useSearchParams();
    const session_id = searchParams.get("session_id"); // Get session_id from query params
    const token = Cookies.get("secretCookie"); // Get token from cookies
    const [orderId, setOrderId] = useState<string | null>(null);

    useEffect(() => {
        const verifyPayment = async () => {
            try {
                const storedOrderId = localStorage.getItem("orderId"); // Retrieve the stored order ID from localStorage
                if (storedOrderId) {
                    setOrderId(storedOrderId);
                } else {
                    return;
                }

                if (!session_id) {
                    console.error("Session ID  is missing.");
                    return;
                }
                if (!orderId) {
                    console.error("Order ID is missing.");
                    return;
                }

                const paymentResponse = await fetch(
                    `https://localhost:44372/payments/verify-payment/${session_id}`, // Backend endpoint for payment verification
                    {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${token}`,
                        },
                        body: JSON.stringify({ orderId }),
                    }
                );

                if (!paymentResponse.ok) {
                    const errorDetails = await paymentResponse.text();
                    console.error("Error verifying payment:", errorDetails);
                    throw new Error("Payment verification failed");
                }

                const data = await paymentResponse.json();
                console.log("Payment verification response:", data);

                if (data.success) {
                    console.log("Payment verified successfully.");
                    await axios.post(
                        `https://localhost:44372/payments/${orderId}`,
                        {
                            paymentDate: new Date(),
                            amount: data.amount,
                            paymentMethod: "card",
                            stripePaymentId: session_id,
                        },
                        {
                            headers: { Authorization: `Bearer ${token}` },
                            withCredentials: true,
                        }
                    );

                    console.log("Payment recorded successfully!");
                    router.push("/order-complete");
                } else {
                    console.error("Payment verification failed:", data.message);
                }
            } catch (error) {
                console.error("Error processing payment:", error);
            }
        };

        verifyPayment();
    }, [session_id, orderId, router, token]);

    return <div>Processing your payment...</div>;
};

export default Success;
