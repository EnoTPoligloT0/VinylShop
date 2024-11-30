export interface Vinyl {
    id?: string; 
    title: string;
    artist: string;
    genre?: string;
    releaseYear?: number;
    price: number;
    stock?: number;
    description?: string;
    isAvailable?: boolean;
    imageBase64?: string;
    addToCart?: (id: string, price:number) => void;
}
export interface VinylCardProps extends Vinyl {
    addToCart?: (id: string, price: number) => void;
}
export interface FilterVinylListProps {
    genres: string[];
    years: string[];
    sortOption: string;
}
