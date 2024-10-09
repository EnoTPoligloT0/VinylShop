import React from 'react';

const About: React.FC = () => {
    return (
        <div className="bg-gray-100">

            <section className="container mx-auto py-12 px-4">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-10">
                    <div className="space-y-6">
                        <h2 className="text-2xl font-bold text-royal-purple">Our Story</h2>
                        <p className="text-gray-700">
                            At Vinyl Vibes, we believe that music is meant to be felt, not just heard. Established in 1990, our shop has been serving music lovers and vinyl enthusiasts for over three decades. Whether you're looking for rare finds, classic records, or the latest indie releases, we've got you covered.
                        </p>
                        <h2 className="text-2xl font-bold text-royal-purple">Why Vinyl?</h2>
                        <p className="text-gray-700">
                            There's something magical about the warm sound of vinyl. It captures the essence of music in a way digital formats simply can't replicate. Our passion is to preserve that analog experience and share it with you, one record at a time.
                        </p>
                    </div>

                    <div className="space-y-6">
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d81959.78199828413!2d19.879009723663337!3d50.063007132127765!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47165b11542ad5a1%3A0x9bcb8718d9e54158!2sWinylmojapasja.pl!5e0!3m2!1sru!2spl!4v1728472409483!5m2!1sru!2spl"
                            width="100%" height="400" loading="lazy"
                            referrerPolicy="no-referrer-when-downgrade"
                            className="rounded-lg"
                        ></iframe>
                    </div>
                </div>
            </section>

            <section className="bg-royal-purple text-white py-8 px-0 mx-0">
                <div className="container mx-auto text-center space-y-6">
                    <h2 className="text-2xl font-bold">Get in Touch</h2>
                    <p>
                        Have questions about records, want to request a specific album, or just want to chat about music? Reach out to us anytime!
                    </p>
                    <div className="flex justify-center space-x-4">
                        <a
                            href="mailto:proxy@gmail.com"
                            className="bg-white text-royal-purple px-6 py-2 rounded-full shadow-lg hover:bg-gray-200"
                        >
                            Email Us
                        </a>
                        <a
                            href="tel:2195550114"
                            className="bg-white text-royal-purple px-6 py-2 rounded-full shadow-lg hover:bg-gray-200"
                        >
                            Call Us
                        </a>
                    </div>
                </div>
            </section>

        </div>
    );
};

export default About;
