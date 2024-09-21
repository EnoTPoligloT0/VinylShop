import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        'soft-purple': '#B983FF',  // Lavender Purple
        'primary-deep-purple': '#7A00CC',  // Deep Purple
        'deep-purple': '#5A189A', // Alternate Deep Purple
        'royal-purple': '#4B0082',  // Royal Purple 
        'warning-yellow': '#FFD700',       // Golden Yellow
        'light-gray': '#F5F5F5',    // Light Gray
      },
      fontFamily: {
        poppins: ['Poppins', 'sans-serif'],
      },
      fontWeight: {
        regular: '400',  
        medium: '500',
        semibold: '600',
      }
    },
  },
  plugins: [],
};
export default config;
