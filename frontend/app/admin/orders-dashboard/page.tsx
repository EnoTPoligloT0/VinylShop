// /admin/orders-dashboard/page.tsx

import { getOrders } from '@/utils/apiService';
import OrdersPagination from './OrdersPagination';

export default async function OrdersPage() {
    const page = 1;
    const pageSize = 10;

    const initialOrdersData = await getOrders(page, pageSize);

    return (
        <div className="min-h-screen bg-light-gray p-8">
            <OrdersPagination initialOrdersData={initialOrdersData} />
        </div>
    );
}
