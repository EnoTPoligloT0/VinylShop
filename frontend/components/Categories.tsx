import React from 'react';
import Image from "next/image";
import Link from "next/link";

function Categories() {
    return (
        <div className="container mx-auto">

            <div className="flex justify-between items-center mb-8">
                <h1 className="text-3xl font-semibold">Popular Categories</h1>
                <span className="flex items-center space-x-2">
                    <p className="font-medium text-royal-purple">View all</p>
                    <Image src="/arrow.svg" alt="Arrow" width={24} height={24}/>
                </span>
            </div>

            <div className="grid grid-cols-3 gap-1 md:gap-12">
                <div className="flex justify-start">
                    <Image className={"cursor-pointer"} src={"/rock-cat.png"} alt={"rock-alt"} height={424} width={424}/>
                </div>

                <div className="flex justify-center">
                    <Link href="/category/pop" passHref>
                        <Image className={"cursor-pointer"} src={"/pop-cat.png"} alt={"pop-alt"} height={424} width={424}/>
                    </Link>
                </div>

                <div className="flex justify-end">
                    <Link href="/category/jazz" passHref>
                        <Image className={"cursor-pointer"} src={"/jazz-cat.png"} alt={"jazz-alt"} height={424} width={424}/>
                    </Link>
                </div>
            </div>

        </div>
    );
}

export default Categories;