// src/components/CartItem.tsx
import React from 'react';
import { CartItem as CartItemType } from '../types/cart';

interface CartItemProps {
    item: CartItemType;
    removeFromCart: (itemId: string) => void;
}

const CartItem: React.FC<CartItemProps> = ({ item, removeFromCart }) => {
    return (
        <div>
            <h3>{item.vinylId} - {item.quantity} x ${item.unitPrice.toFixed(2)}</h3>
            <button onClick={() => removeFromCart(item.id)}>Remove</button>
        </div>
    );
};

export default CartItem;
