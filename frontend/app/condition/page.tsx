import React from 'react';

function Page() {
    return (
        <main className="bg-light-gray min-h-screen p-8">
            {/* Page Header */}
            <header className="text-center mb-12">
                <h1 className="text-4xl font-bold text-primary-deep-purple mb-4">The Vinyl Grading System</h1>
                <p className="text-lg text-royal-purple">Learn how we classify the quality of our vinyls, ensuring sound
                    perfection.</p>
            </header>

            {/* Grading System Section */}
            <section className="bg-light-gray p-6 rounded-lg shadow-md">
                <h2 className="text-3xl font-semibold text-primary-deep-purple mb-6">Vinyl Grading Categories</h2>

                {/* Grading Descriptions */}
                <div className="space-y-8">
                    {/* Mint (M) */}
                    <div className="p-6 bg-white rounded-lg shadow-md">
                        <h3 className="text-2xl font-semibold text-warning-yellow">Mint (M)</h3>
                        <p className="text-deep-purple mt-2">
                            Flawless and untouched, often sealed. The highest quality possible, perfect for serious
                            collectors and enthusiasts.
                        </p>
                    </div>

                    {/* Near Mint (NM) */}
                    <div className="p-6 bg-white rounded-lg shadow-md">
                        <h3 className="text-2xl font-semibold text-primary-deep-purple">Near Mint (NM)</h3>
                        <p className="text-deep-purple mt-2">
                            An almost perfect vinyl. Played a few times with minimal visible wear, offering pristine
                            sound and visual presentation.
                        </p>
                    </div>

                    {/* Very Good (VG) */}
                    <div className="p-6 bg-white rounded-lg shadow-md">
                        <h3 className="text-2xl font-semibold text-royal-purple">Very Good (VG)</h3>
                        <p className="text-deep-purple mt-2">
                            Light pops and clicks, with a few visible scratches. Overall enjoyable, but minor signs of
                            wear are noticeable.
                        </p>
                    </div>

                    {/* Good (G) */}
                    <div className="p-6 bg-white rounded-lg shadow-md">
                        <h3 className="text-2xl font-semibold text-primary-deep-purple">Good (G)</h3>
                        <p className="text-deep-purple mt-2">
                            Usable but with several flaws. Expect light scratches and audible distortions. Still worth a
                            listen for the casual collector.
                        </p>
                    </div>

                    {/* Poor or Fair (P/F) */}
                    <div className="p-6 bg-white rounded-lg shadow-md">
                        <h3 className="text-2xl font-semibold ">Poor (P) or Fair (F)</h3>
                        <p className="text-deep-purple mt-2">
                            Vinyls in poor condition. Expect major noise issues like skipping and scratching. Covers are
                            likely badly damaged or missing.
                        </p>
                    </div>
                </div>
            </section>

            {/* Footer */}
            <footer className="text-center mt-16">
                <p className="text-lg text-royal-purple">Explore our collection and find the perfect vinyl for your
                    collection.</p>
            </footer>
        </main>
    );
}

export default Page;
