// /apiService.ts
"use server"
import api from './api';
import {cookies} from 'next/headers';
//todo ssl sertificates
export const getOrders = async (page: number, pageSize: number) => {
    const cookieStore = await cookies();
    const token = await cookieStore.get("secretCookie")?.value;
    if (!token) {
        throw new Error("Token is required to fetch orders");
    }
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

    const response = await api.get('/orders', {
        headers: {
            Authorization: `Bearer ${token}`,
        },
        params: {
            page,
            pageSize
        },
        withCredentials: true,
    });
    return response.data;
};

export const getVinyls = async (
    page: number,
    pageSize: number,
    filters?: {
        genre?: string[],
        decade?: number[],
        sortOption?: string
    }) => {

    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

    const params: any = {
        page,
        pageSize
    };

    if (filters?.genre?.length) {
        params.genre = filters.genre.join(',');
    }

    if (filters?.decade?.length) {
        params.decade = filters.decade.join(',');
    }

    if (filters?.sortOption) {
        params.sortOption = filters.sortOption;
    }

    try {
        const response = await api.get('/vinyls/filter', {params, withCredentials: true});
        return response.data;
    } catch (error) {
        console.error("Error fetching vinyls:", error);
        throw new Error("Failed to fetch vinyls");
    }
};

export const getVinylById = async (id: string) => {
    const response = await api.get(`/vinyls/${id}`,
        {
            headers: {
                'Accept': 'application/json',
            },
        });

    return response.data
};
export const deleteVinyl = async (id: string) => {
    const cookieStore = await cookies();
    const token = await cookieStore.get("secretCookie")?.value;

    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

    const respone = await api.delete(`/vinyls/${id}`,
        {
            headers:
                {'Authorization': `Bearer ${token}`},

        })
    return respone.data;

}
export const loginUser = async (credentials: { email: string; password: string }) => {
    return await api.post('/login', credentials);
};

export const registerUser = async (userData: any) => {
    return await api.post('/register', userData);
};

// Orders
export const fetchOrders = async () => {
    return await api.get('/orders');
};

export const fetchOrderById = async (orderId: string) => {
    return await api.get(`/orders/${orderId}`);
};

export const updateOrder = async (orderId: string, orderData: any) => {
    return await api.put(`/orders/${orderId}`, orderData);
};

export const deleteOrder = async (orderId: string) => {
    return await api.delete(`/orders/${orderId}`);
};

// Order Items
export const fetchOrderItemsByOrderId = async (orderId: string) => {
    return await api.get(`/orderItems/orderItem/order/${orderId}`);
};

export const fetchOrderItemById = async (id: string) => {
    return await api.get(`/orderItems/orderItem/${id}`);
};

export const updateOrderItem = async (id: string, orderItemData: any) => {
    return await api.put(`/orderItems/orderItem/${id}`, orderItemData);
};

export const deleteOrderItem = async (id: string) => {
    return await api.delete(`/orderItems/orderItem/${id}`);
};

// Payments
export const fetchPaymentsByOrderId = async (orderId: string) => {
    return await api.get(`/payments/order/${orderId}`);
};

export const fetchPaymentById = async (id: string) => {
    return await api.get(`/payments/${id}`);
};

export const updatePayment = async (id: string, paymentData: any) => {
    return await api.put(`/payments/${id}`, paymentData);
};

export const deletePayment = async (id: string) => {
    return await api.delete(`/payments/${id}`);
};

// Shipments
export const fetchShipmentsByOrderId = async (orderId: string) => {
    return await api.get(`/shipments/order/${orderId}`);
};

export const fetchShipmentById = async (id: string) => {
    return await api.get(`/shipments/${id}`);
};

export const updateShipment = async (id: string, shipmentData: any) => {
    return await api.put(`/shipments/${id}`, shipmentData);
};

export const deleteShipment = async (id: string) => {
    return await api.delete(`/shipments/${id}`);
};

// Users
export const fetchUsers = async () => {
    return await api.get('/users');
};

export const fetchUserById = async (id: string) => {
    return await api.get(`/users/${id}`);
};

export const fetchUserByEmail = async (email: string) => {
    return await api.get(`/users/email/${email}`);
};

// Vinyls

// Vinyl Order Items
export const fetchVinylOrderItemsByOrderItemId = async (orderItemId: string) => {
    return await api.get(`/vinyls/orderItems/${orderItemId}`);
};
