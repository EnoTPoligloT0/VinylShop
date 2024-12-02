import React from 'react';
import VinylList from "@/components/VinylList";
import Image from "next/image";
import {FilterVinylListProps} from "@/types/vinyl";


function Popular({ genres = [""], years = ["1960"], sortOption = "Price Low To High" }: FilterVinylListProps) {
    return (
        <div className="container mx-auto mt-16 mb-10">
            <div className="flex justify-between items-center mb-8">
                <h1 className="text-3xl font-semibold">Popular Products</h1>
                <span className="flex items-center space-x-2">
                    <p className="font-medium text-royal-purple">View all</p>
                    <Image src="/arrow.svg" alt="Arrow" width={24} height={24} />
                </span>
            </div>

            <div className="container mx-auto">
                <VinylList sortOption={sortOption} genres={genres} years={years} />
            </div>
        </div>
    );
}

export default Popular;
