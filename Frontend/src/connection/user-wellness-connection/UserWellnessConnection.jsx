import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

export const getMyUserWellness = async () => {
    const response = await api.get('/api/user-wellness/mine');
    return response.data;
};

export const getAllUserWellness = async () => {
    const response = await api.get('/api/user-wellness/all');
    return response.data;
};

export const getUserWellnessById = async (usersWellnessId) => {
    const response = await api.get(
        `/api/user-wellness/${usersWellnessId}`
    );

    return response.data;
};

export const createUserWellness = async (wellnessId) => {
    const response = await api.post(
        `/api/user-wellness?wellnessId=${wellnessId}`
    );

    return response.data;
};

export const deleteUserWellness = async (usersWellnessId) => {
    await api.delete(
        `/api/user-wellness/${usersWellnessId}`
    );
};