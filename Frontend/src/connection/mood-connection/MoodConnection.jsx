import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    withCredentials: true,
});

// Get moods belonging to the currently logged-in user
export const getMoods = async () => {
    try {
        const response = await api.get('/api/mood/mine');
        return response.data;
    } catch (error) {
        console.error('Error fetching my moods:', error);
        throw error;
    }
};

// Get a specific mood
export const getMoodById = async (moodId) => {
    try {
        const response = await api.get(`/api/mood/${moodId}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching mood with ID ${moodId}:`, error);
        throw error;
    }
};

// Create a mood
// userId is assigned by the backend from the authenticated user
export const createMood = async (mood) => {
    try {
        const response = await api.post('/api/mood', mood);
        return response.data;
    } catch (error) {
        console.error('Error creating mood:', error);
        throw error;
    }
};

// Update a mood
export const updateMood = async (moodId, updatedMood) => {
    try {
        const response = await api.put(
            `/api/mood/${moodId}`,
            updatedMood
        );

        return response.data;
    } catch (error) {
        console.error(`Error updating mood with ID ${moodId}:`, error);
        throw error;
    }
};

// Delete a mood
export const deleteMood = async (moodId) => {
    try {
        const response = await api.delete(`/api/mood/${moodId}`);
        return response.data;
    } catch (error) {
        console.error(`Error deleting mood with ID ${moodId}:`, error);
        throw error;
    }
};
