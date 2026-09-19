import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
}); 

export const getMoods = async () => {
    try {
        const response = await api.get('/api/moods');
        return response.data;
    } catch (error) {
        console.error('Error fetching moods:', error);
        throw error;
    }
};

export const getMoodById = async (moodId) => {
    try {
        const response = await api.get(`/api/moods/${moodId}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching mood with ID ${moodId}:`, error);
        throw error;
    }
};

export const createMood = async (mood) => {
    try {
        const response = await api.post('/api/moods', mood);
        return response.data;
    } catch (error) {
        console.error('Error creating mood:', error);
        throw error;
    }
};

export const updateMood = async (moodId, updatedMood) => {
    try {
        const response = await api.put(`/api/moods/${moodId}`, updatedMood);
        return response.data;
    } catch (error) {
        console.error(`Error updating mood with ID ${moodId}:`, error);
        throw error;
    }
};

export const deleteMood = async (moodId) => {
    try {
       const response = await api.delete(`/api/moods/${moodId}`);
        return response.data;
    } catch (error) {
        console.error(`Error deleting mood with ID ${moodId}:`, error);
        throw error;
    }
};