import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

// Get wellness entries belonging to the currently logged-in user
export const getWellness = async () => {
    try {
        const response = await api.get('/api/wellness/mine');
        return response.data;
    } catch (error) {
        console.error('Error fetching my wellness data:', error);
        throw error;
    }
};

// Get a specific wellness entry
export const getWellnessById = async (wellnessId) => {
    try {
        const response = await api.get(`/api/wellness/${wellnessId}`);
        return response.data;
    } catch (error) {
        console.error(
            `Error fetching wellness data with ID ${wellnessId}:`,
            error
        );
        throw error;
    }
};

// Create a wellness entry
// userId is assigned by the backend from the authenticated user
export const createWellness = async (wellness) => {
    try {
        const response = await api.post('/api/wellness', wellness);
        return response.data;
    } catch (error) {
        console.error('Error creating wellness data:', error);
        throw error;
    }
};

// Update a wellness entry
export const updateWellness = async (wellnessId, updatedWellness) => {
    try {
        const response = await api.put(
            `/api/wellness/${wellnessId}`,
            updatedWellness
        );

        return response.data;
    } catch (error) {
        console.error(
            `Error updating wellness data with ID ${wellnessId}:`,
            error
        );
        throw error;
    }
};

// Delete a wellness entry
export const deleteWellness = async (wellnessId) => {
    try {
        const response = await api.delete(
            `/api/wellness/${wellnessId}`
        );

        return response.data;
    } catch (error) {
        console.error(
            `Error deleting wellness data with ID ${wellnessId}:`,
            error
        );
        throw error;
    }
};
