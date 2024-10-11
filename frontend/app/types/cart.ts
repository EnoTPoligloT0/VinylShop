import {Vinyl} from "@/app/types/vinyl";

export interface CartItem {
    vinyl: Vinyl;
    quantity: number;
}

export interface Order {
    userId: string;
    orderDate: string;
    totalAmount: number;
    items: CartItem[];
}