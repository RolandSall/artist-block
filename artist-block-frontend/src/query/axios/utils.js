import Cookies from 'js-cookie';

export const isAuthenticated = () => Cookies.get('Bearer');
