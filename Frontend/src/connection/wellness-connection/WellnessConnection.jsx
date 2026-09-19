import axios from "axios";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

export const getWellness = async () => {
    try {
        const response = await api.get('/api/wellness');
        return response.data;
    } catch (error) {
        console.error('Error fetching wellness data:', error);
        throw error;
    }
};

export const getWellnessById = async (wellnessId) => {
    try {
        const response = await api.get(`/api/wellness/${wellnessId}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching wellness data with ID ${wellnessId}:`, error);
        throw error;
    }
};

export const createWellness = async (wellness) => {
    try {
        const response = await api.post('/api/wellness', wellness);
        return response.data;
    } catch (error) {
        console.error('Error creating wellness data:', error);
        throw error;
    }
};

export const updateWellness = async (wellnessId, updatedWellness) => {
    try {
        const response = await api.put(`/api/wellness/${wellnessId}`, updatedWellness);
        return response.data;
    } catch (error) {
        console.error(`Error updating wellness data with ID ${wellnessId}:`, error);
        throw error;
    }
};

export const deleteWellness = async (wellnessId) => {
    try {
        const response = await api.delete(`/api/wellness/${wellnessId}`);
        return response.data;
    } catch (error) {
        console.error(`Error deleting wellness data with ID ${wellnessId}:`, error);
        throw error;
    }
};