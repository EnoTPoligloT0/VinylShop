'use client';

import React, { useState } from 'react';
import { useAuthContext } from '@/context/AuthContext';
import AccessDenied from "@/components/AccessDenied";
import { getOrders } from '@/utils/apiService';
//todo add filters
interface OrdersPaginationProps {
    initialOrdersData: {
        orders: any[];
        pagination: {
            currentPage: number;
            totalPages: number;
        };
    };
}

//todo status style
const OrdersPagination: React.FC<OrdersPaginationProps> = ({ initialOrdersData }) => {
    const { user, isAdmin } = useAuthContext();
    const [ordersData, setOrdersData] = useState(initialOrdersData);
    const { orders, pagination } = ordersData;
    const [loading, setLoading] = useState(false);

    if (!isAdmin) {
        return <AccessDenied role={user?.Role || 'Guest'} />;
    }

    const fetchOrders = async (page: number) => {
        setLoading(true);
        try {
            const newOrdersData = await getOrders(page, 10);
            setOrdersData(newOrdersData);
        } catch (error) {
            console.error("Error fetching orders:", error);
        } finally {
            setLoading(false);
        }
    };

    const handleNextPage = () => {
        if (pagination.currentPage < pagination.totalPages) {
            fetchOrders(pagination.currentPage + 1);
        }
    };

    const handlePreviousPage = () => {
        if (pagination.currentPage > 1) {
            fetchOrders(pagination.currentPage - 1);
        }
    };

    return (
        <div className="p-6 bg-light-gray">
            <h2 className="text-2xl font-semibold text-deep-purple mb-6">Orders</h2>
            <ul className="space-y-4">
                {orders.map((order) => (
                    <li key={order.id} className="bg-white p-4 rounded-lg shadow-md border border-gray-200">
                        <p className="text-lg font-medium text-primary">
                            <span className="font-bold">Order ID:</span> {order.id}
                        </p>
                        <p className="text-md text-gray-600">
                            <span className="font-semibold">User ID:</span> {order.userId}
                        </p>
                        <p className="text-md text-gray-600">
                            <span className="font-semibold">Date:</span> {new Date(order.orderDate).toLocaleDateString()}
                        </p>
                        <p className="text-md text-golden-yellow font-bold">
                            <span className="font-semibold">Total Amount:</span> ${order.totalAmount}
                        </p>
                        <p className="text-md text-golden-yellow font-bold">
                            <span className="font-semibold">Status:</span> {order.status}
                        </p>
                    </li>
                ))}
            </ul>
            <div className="mt-6 flex justify-between items-center">
                <button
                    onClick={handlePreviousPage}
                    disabled={pagination.currentPage === 1 || loading}
                    className={`px-4 py-2 rounded-lg font-medium ${pagination.currentPage === 1 || loading
                        ? 'bg-gray-300 text-gray-500 cursor-not-allowed'
                        : 'bg-soft-primary text-white hover:bg-primary'}`}
                >
                    Previous
                </button>
                <span className="text-gray-600">
                    Page <span className="font-semibold text-deep-purple">{pagination.currentPage}</span> of{" "}
                    <span className="font-semibold text-deep-purple">{pagination.totalPages}</span>
                </span>
                <button
                    onClick={handleNextPage}
                    disabled={pagination.currentPage === pagination.totalPages || loading}
                    className={`px-4 py-2 rounded-lg font-medium ${pagination.currentPage === pagination.totalPages || loading
                        ? 'bg-gray-300 text-gray-500 cursor-not-allowed'
                        : 'bg-soft-primary text-white hover:bg-primary'}`}
                >
                    Next
                </button>
            </div>
        </div>
    );
};

export default OrdersPagination;
