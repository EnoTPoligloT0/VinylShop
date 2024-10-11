// lib/apiService.ts

import api from './api';

// User Authentication
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
export const fetchVinyls = async () => {
    return await api.get('/vinyls');
};

export const fetchVinylById = async (id: string) => {
    return await api.get(`/vinyls/${id}`);
};

export const searchVinyls = async (query: string) => {
    return await api.get(`/vinyls/search`, { params: { q: query } });
};

export const updateVinyl = async (id: string, vinylData: any) => {
    return await api.put(`/vinyls/${id}`, vinylData);
};

export const deleteVinyl = async (id: string) => {
    return await api.delete(`/vinyls/${id}`);
};

// Vinyl Order Items
export const fetchVinylOrderItemsByOrderItemId = async (orderItemId: string) => {
    return await api.get(`/vinyls/orderItems/${orderItemId}`);
};
