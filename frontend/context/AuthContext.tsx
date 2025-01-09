import React, { createContext, useContext, useEffect, useState } from 'react';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from "@/types/jwtPayload";

interface AuthContextType {
    user: { userId: string; email?: string; Role?: string } | null;
    isLoggedIn: boolean;
    isAdmin: boolean;
    loading: boolean; // Add loading state
    login: (token: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<AuthContextType['user']>(null);
    const [isAdmin, setIsAdmin] = useState(false);
    const [loading, setLoading] = useState(true); // Initialize loading state

    useEffect(() => {
        const token = Cookies.get('secretCookie');
        if (token) {
            try {
                const decoded: any = jwtDecode(token);
                setUser({ userId: decoded.userId, email: decoded.email, Role: decoded.Role });
                setIsAdmin(decoded.Role === 'Admin');
            } catch {
                Cookies.remove('secretCookie');
                setUser(null);
                setIsAdmin(false);
            }
        }
        setLoading(false); // Once the check is done, set loading to false
    }, []);

    const login = (token: string) => {
        Cookies.set('secretCookie', token, { expires: 1, path: '/', sameSite: 'Lax' });
        const decoded: any = jwtDecode(token);
        setUser({ userId: decoded.userId, email: decoded.email, Role: decoded.Role });
        setIsAdmin(decoded.Role === 'Admin');
    };

    const logout = () => {
        Cookies.remove('secretCookie');
        setUser(null);
        setIsAdmin(false);
    };

    return (
        <AuthContext.Provider value={{ user, isLoggedIn: !!user, isAdmin, loading, login, logout }}>
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
