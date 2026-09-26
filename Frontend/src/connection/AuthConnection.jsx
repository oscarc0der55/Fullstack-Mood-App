import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

export async function loginWithCookie(email, password){
    await api.post("login?useCookies=true", {email, password});
}

export async function getCurrentUser() {
    const response = await api.get("manage/info");
    return response.data;
}

export async function logoutFromCookie() {
  await api.post('logout');
}

export async function checkAuth(){
    try {
        await api.get("manage/info")
        return true;
    } catch (error) {
        if(error.response.status === 401){
            return false;
        }
    }
}