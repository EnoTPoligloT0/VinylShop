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
}