import {Vinyl} from "@/types/vinyl";
import {PaginationInfo} from "@/types/pagination";

export interface OrderItem {
    vinyl: Vinyl;
    vinylid?: string;
    quantity: number;
    unitPrice: number;
}

// export interface Order {
//     userId: string;
//     orderDate: string;
//     totalAmount: number;
//     items: OrderItem[];
// }

export interface Order{
    id: string;
    userId: string;
    orderDate: string;
    totalAmount: number;
}

export interface OrdersResponse {
    orders: Order[];
    pagination: PaginationInfo;
}
export interface OrdersPageProps {
    ordersData: OrdersResponse;
}
export interface CartItem {
    id?: string;
    vinylId: string; 
    quantity: number; 
    unitPrice: number;
}