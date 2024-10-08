import React from 'react';
import Image from "next/image";
//todo add animations to buttons and make better responsive 
function Hero() {
    return (
        <div className="container mx-auto mt-4 ">
            <div className="grid grid-cols-12 gap-4 lg:max-h-[1000px]">
                
                <div className="col-span-12 lg:col-span-8 lg:pl-16 bg-gradient-to-r from-gray-900 to-soft-purple lg:relative rounded-lg">
                    <div className="flex flex-col lg:flex-row items-center lg:items-start ">

                        <div className="lg:h-72 lg:pt-36 py-4 px-4 sm:pt-10 z-20 relative">
                            <div >
                                <h1 className="md:w-2/3 text-5xl font-semibold text-white mb-7">
                                    Iconic & New Vinyls
                                </h1>
                                <p className="text-white text-regular py-1 mb-2 text-xl">
                                    Sale up to <span
                                    className="text-white bg-warning-yellow px-3 py-2 font-regular rounded">30% OFF</span>
                                </p>

                                <p className="text-white text-sm font-normal mb-4 opacity-75">Free shipping on all your
                                    orders.</p>
                            </div>
                            <button
                                className="bg-white w-2/5 h-2/5 text-royal-purple font-bold py-2 px-4 rounded-3xl mt-7 flex items-center justify-between">
                                <span className={"flex-grow"}>Shop now</span>
                                <Image src="/arrow.svg" alt="Arrow icon" width={15} height={12}/>
                            </button>
                        </div>

                        <div className="lg:w-1/2 lg:absolute lg:right-0 lg:top-0 lg:h-full overflow-hidden">
                            <img
                                src="/vinyl-man.png"
                                alt="Man with vinyl"
                                className="min-w-[420px] w-full h-full object-cover"
                            />
                        </div>

                    </div>

                </div>

                <div className="col-span-12 lg:col-span-4 flex flex-col h-[600px]">

                    <div className="flex flex-col h-full ">
                        <div className="h-1/2 mb-3 bg-[url('/vinyl-background.png')] bg-cover bg-center rounded-xl flex items-center justify-start">
                            <div className="w-1/3 h-1/2 flex flex-col items-center justify-start">
                                <p className="uppercase text-sm drop-shadow">Summer sale</p>
                                <h5 className="font-semibold text-black text-3xl drop-shadow-md mb-6">75% OFF</h5>
                                <button
                                    className="bg-transparent  text-royal-purple font-semibold py-2 px-4 rounded-3xl flex items-center justify-between">
                                    <span className={"flex-grow font-semibold  mr-1"}>Shop now</span>
                                    <Image src="/arrow.svg" alt="Arrow icon" width={15} height={12}/>
                                </button>
                            </div>
                        </div>

                        <div className="h-1/2 bg-warning-yellow rounded-xl flex items-center justify-center">
                            <div className="h-2/3 w-3/4 flex flex-col items-center justify-center">
                                <p className="uppercase text-gray-800 text-sm">Best Deal</p>
                                <h5 className="text-royal-purple font-semibold text-center text-3xl">Special Products Deal of the Month</h5>
                                <button
                                    className="bg-white w-2/3 text-royal-purple font-bold py-2 px-4 rounded-3xl mt-7 flex items-center justify-between">
                                    <span className={"flex-grow"}>Shop now</span>
                                    <Image src="/arrow.svg" alt="Arrow icon" width={15} height={12}/>
                                </button>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    );
}

export default Hero;