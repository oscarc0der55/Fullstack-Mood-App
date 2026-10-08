import axios from 'axios';

const api = axios.create({
    baseURL: 'https://fullstackmoodbackend-e3ceetfzdhcabwc5.swedencentral-01.azurewebsites.net',
    withCredentials: true,
});

export async function loginWithCookie(email, password) {
    await api.post('/login?useCookies=true', {
        email,
        password,
    });
}

export async function getCurrentUser() {
    const response = await api.get('/manage/info');
    return response.data;
}

export async function logoutFromCookie() {
    api.post('/api/auth/logout');
}

export async function checkAuth() {
    try {
        await api.get('/manage/info');
        return true;
    } catch (error) {
        if (error.response?.status === 401) {
            return false;
        }

        throw error;
    }
}
