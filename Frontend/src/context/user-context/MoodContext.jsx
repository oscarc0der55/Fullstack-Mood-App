import {useState, useEffect, createContext} from 'react';
import {getMoods} from '../../../connection/mood-connection/MoodConnection';

export const MoodContext = createContext();

export function MoodProvider({children}) {
    const [moods, setMoods] = useState([]);

    async function getMoodList() {
        try {
            const moodList = await getMoods();
            setMoods(moodList);
        } catch (error) {
            console.error('Error fetching moods:', error);
        }
    }

    useEffect(() => {
        getMoodList();
    }, []);

    return (
        <MoodContext.Provider value={{ moods, setMoods }}>
            {children}
        </MoodContext.Provider>
    );
}