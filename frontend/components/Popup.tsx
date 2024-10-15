// Popup.tsx
import React from 'react';

interface PopupProps {
    message: string;
    onClose: () => void;
}

const Popup: React.FC<PopupProps> = ({ message, onClose }) => {
    return (
        <div className="fixed top-0 left-0 w-full h-full flex items-center justify-center z-50">
            <div
                className="absolute inset-0 bg-black opacity-50"
                onClick={onClose} // Close popup on backdrop click
            />
            <div className="bg-white rounded-lg shadow-lg p-6 z-10">
                <h2 className="text-lg font-semibold text-gray-800">{message}</h2>
                <button
                    onClick={onClose}
                    className="mt-4 bg-purple-600 text-white rounded-lg px-4 py-2 hover:bg-purple-700 transition duration-200"
                >
                    Close
                </button>
            </div>
        </div>
    );
};

export default Popup;
