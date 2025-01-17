// app/orders/page.tsx
import { getOrders } from '@/utils/apiService';
import OrdersPagination from './OrdersPagination';

import React from "react";


export default async function OrdersPage() {

    const page = 1;
    const pageSize = 10;
    const ordersData = await getOrders(page, pageSize);


    return (
        <div>
            <h1>Orders</h1>
            <OrdersPagination ordersData={ordersData} />
        </div>
    );
}
