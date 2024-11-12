import Cookies from 'js-cookie';

export const setSecretCookie = (token: string, expiresInDays: number = 1) => {
    Cookies.set('secretCookie', token, { expires: expiresInDays, path: '/', sameSite: 'Lax' });
};

export const getSecretCookie = () => {
    return Cookies.get('secretCookie');
};

export const removeSecretCookie = () => {
    Cookies.remove('secretCookie', { path: '/' });
};

export const isSecretCookieSet = () => {
    return !!Cookies.get('secretCookie');
};
