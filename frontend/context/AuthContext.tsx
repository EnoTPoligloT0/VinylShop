'use client';

import React, { createContext, useContext, useEffect, useState } from 'react';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';

interface AuthContextType {
    user: { userId: string; email?: string; Admin?: string } | null;
    isLoggedIn: boolean;
    login: (token: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<AuthContextType['user']>(null);

    useEffect(() => {
        const token = Cookies.get('secretCookie');
        if (token) {
            try {
                const decoded: any = jwtDecode(token);
                setUser({ userId: decoded.userId, email: decoded.email, Admin: decoded.Admin });
            } catch {
                Cookies.remove('secretCookie');
            }
        }
    }, []);

    const login = (token: string) => {
        Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });
        const decoded: any = jwtDecode(token);
        setUser({ userId: decoded.userId, email: decoded.email, Admin: decoded.Admin });
    };

    const logout = () => {
        Cookies.remove('secretCookie');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, isLoggedIn: !!user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuthContext must be used within an AuthProvider');
    }
    return context;
};
