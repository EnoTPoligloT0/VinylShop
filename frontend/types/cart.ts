import {Vinyl} from "@/types/vinyl";

export interface OrderItem {
    vinyl: Vinyl;
    vinylid?: string;
    quantity: number;
    unitPrice: number;
}

export interface Order {
    userId: string;
    orderDate: string;
    totalAmount: number;
    items: OrderItem[];
}
export interface CartItem {
    id: string; 
    vinylId: string; 
    quantity: number; 
    unitPrice: number;
}