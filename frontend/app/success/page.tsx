'use client';

import {useEffect, useState} from 'react';
import {useRouter, useSearchParams} from "next/navigation";
import axios from "axios";
import Cookies from "js-cookie";

const Success = () => {
    const router = useRouter();
    const searchParams = useSearchParams();
    const session_id = searchParams.get("session_id");
    const token = Cookies.get("secretCookie");
    const [orderId, setOrderId] = useState<string | null>(null);
    const [paymentProcessed, setPaymentProcessed] = useState(false);

    useEffect(() => {
        const verifyPayment = async () => {
            if (paymentProcessed) return;

            try {
                const storedOrderId = localStorage.getItem("orderId");
                if (storedOrderId) {
                    setOrderId(storedOrderId);
                } else {
                    console.error("Order ID is missing.");
                    return;
                }


                const paymentResponse = await fetch(
                    `https://localhost:44372/payments/verify-payment/${session_id}`,
                    {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${token}`,
                        },
                        body: JSON.stringify({orderId}),
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
                            headers: {Authorization: `Bearer ${token}`},
                            withCredentials: true,
                        }
                    );

                    setPaymentProcessed(true);
                    router.push("/order-complete");
                } else {
                    console.error("Payment verification failed:", data.message);
                }
            } catch (error) {
                console.error("Error processing payment:", error);
            }
        };

        verifyPayment();
    }, [session_id, orderId, router, token, paymentProcessed]);

    return <div>Processing your payment...</div>;
};

export default Success;
