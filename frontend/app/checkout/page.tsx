// app/checkout/page.tsx

"use client"; // Add this directive to indicate that this is a client component

import { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
import api from "@/utils/api";

const CheckoutPage = () => {
    const router = useRouter();
    const [cart, setCart] = useState<{ [id: string]: number }>({});
    const [totalPrice, setTotalPrice] = useState(0);
    const [user, setUser] = useState<any>(null); // Replace with your user type
    const [error, setError] = useState('');

    useEffect(() => {
        const savedCart = localStorage.getItem('cart');
        if (savedCart) {
            const parsedCart = JSON.parse(savedCart);
            setCart(parsedCart);

            // Calculate total price based on cart items
            const calculateTotalPrice = async () => {
                try {
                    const response = await api.get('/vinyls'); // Fetch all vinyls to get pricing
                    const vinyls = response.data;

                    const total = Object.keys(parsedCart).reduce((sum, id) => {
                        const vinyl = vinyls.find((v) => v.id === id);
                        return sum + (vinyl ? vinyl.price * parsedCart[id] : 0);
                    }, 0);
                    setTotalPrice(total);
                } catch (error) {
                    console.error('Failed to fetch vinyl prices:', error);
                }
            };

            calculateTotalPrice();
        }

        // Optionally fetch user info if needed
        const fetchUserInfo = async () => {
            try {
                const response = await api.get('/user'); // Adjust this endpoint as needed
                setUser(response.data);
            } catch (error) {
                console.error('Failed to fetch user info:', error);
            }
        };

        fetchUserInfo();
    }, []);

    const handleCheckout = async () => {
        if (!user) {
            setError('You need to be logged in to checkout.');
            return;
        }

        const orderData = {
            userId: user.id,
            totalAmount: totalPrice,
            orderItems: Object.keys(cart).map((id) => ({
                vinylId: id,
                quantity: cart[id],
            })),
        };

        try {
            await api.post('/orders', orderData);
            alert('Order placed successfully!');
            localStorage.removeItem('cart'); // Clear the cart after checkout
            router.push('/orders'); // Redirect to orders page or confirmation page
        } catch (err) {
            setError('Failed to place order');
        }
    };

    return (
        <div className="p-4">
            <h2 className="text-2xl font-bold mb-4">Checkout</h2>
            {error && <div className="text-red-500">{error}</div>}
            <h3 className="text-lg font-semibold">Total Price: ${totalPrice.toFixed(2)}</h3>
            <button onClick={handleCheckout} className="bg-green-500 text-white px-4 py-2 rounded">Place Order</button>
        </div>
    );
};

export default CheckoutPage;
