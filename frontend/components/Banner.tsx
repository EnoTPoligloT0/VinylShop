import Image from "next/image"; 

function Banner() {
    return (
        <div className="container mx-auto py-6">
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4 bg-light-gray p-10 rounded-xl">

                <div className="flex items-center space-x-4 h-12">
                    <Image src="/track.svg" alt="Free Shipping" width={40} height={40} />
                    <div className="flex flex-col">
                        <h5 className="font-semibold">Free Shipping</h5>
                        <p>Free shipping on all your orders</p>
                    </div>
                </div>
                
                <div className="flex items-center space-x-4 h-12">
                    <Image src="/headset.svg" alt="Customer Support" width={40} height={40} />
                    <div className="flex flex-col">
                        <h5 className="font-semibold">Customer Support 24/7</h5>
                        <p>Instant access to Support</p>
                    </div>
                </div>

                <div className="flex items-center space-x-4 h-12">
                    <Image src="/approved-bag.svg" alt="Secure Payment" width={40} height={40} />
                    <div className="flex flex-col">
                        <h5 className="font-semibold">100% Secure Payment</h5>
                        <p>We ensure your money is safe</p>
                    </div>
                </div>

                <div className="flex items-center space-x-4 h-12">
                    <Image src="/box.svg" alt="Money-Back Guarantee" width={40} height={40} />
                    <div className="flex flex-col">
                        <h5 className="font-semibold">Money-Back Guarantee</h5>
                        <p>30 Days Money-Back Guarantee</p>
                    </div>
                </div>

            </div>
        </div>
    );
}

export default Banner;
