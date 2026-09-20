import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

export const getMyUserMoods = async () => {
    const response = await api.get('/api/user-moods/mine');
    return response.data;
};

export const getAllUserMoods = async () => {
    const response = await api.get('/api/user-moods/all');
    return response.data;
};

export const getUserMoodById = async (usersMoodId) => {
    const response = await api.get(
        `/api/user-moods/${usersMoodId}`
    );

    return response.data;
};

export const createUserMood = async (moodId) => {
    const response = await api.post(
        `/api/user-moods?moodId=${moodId}`
    );

    return response.data;
};

export const deleteUserMood = async (usersMoodId) => {
    await api.delete(`/api/user-moods/${usersMoodId}`);
};