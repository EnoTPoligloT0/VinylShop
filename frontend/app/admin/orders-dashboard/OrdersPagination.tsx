'use client'

import React, { useState } from 'react';
import { useAuthContext } from '@/context/AuthContext';
import AccessDenied from "@/components/AccessDenied";
import {getSecretCookie} from "@/utils/cookies";

interface OrdersPaginationProps {
    ordersData: {
        orders: any[];
        pagination: {
            currentPage: number;
            totalPages: number;
        };
    };
}

const OrdersPagination: React.FC<OrdersPaginationProps> = ({ ordersData }) => {
    const { orders, pagination } = ordersData;
    const [page, setPage] = useState(pagination.currentPage);
    const { user, isAdmin, loading } = useAuthContext();
    // const {token} = getSecretCookie();
    //
    // console.log("Token in pagination:", token)
    if (!isAdmin) {
        return <AccessDenied role={user?.Role || 'Guest'} />;
    }
    const handleNextPage = () => {
        if (page < pagination.totalPages) {
            setPage(page + 1);
        }
    };

    const handlePreviousPage = () => {
        if (page > 1) {
            setPage(page - 1);
        }
    };

    return (
        <div>
            <ul>
                {orders.map((order) => (
                    <li key={order.id}>
                        <p>Order ID: {order.id}</p>
                        <p>User ID: {order.userId}</p>
                        <p>Date: {new Date(order.orderDate).toLocaleDateString()}</p>
                        <p>Total Amount: ${order.totalAmount}</p>
                    </li>
                ))}
            </ul>

            <div>
                <button onClick={handlePreviousPage} disabled={page === 1}>
                    Previous
                </button>
                <span>{`Page ${page} of ${pagination.totalPages}`}</span>
                <button onClick={handleNextPage} disabled={page === pagination.totalPages}>
                    Next
                </button>
            </div>
        </div>
    );
};

export default OrdersPagination;