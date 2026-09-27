import { useEffect, useState } from 'react';
import { getMoods } from '../connection/mood-connection/MoodConnection';
import { MoodContextObject } from './MoodContextObject';

export function useMood({ children }) {
    const [moods, setMoods] = useState([]);

    useEffect(() => {
        let active = true;

        async function loadMoods() {
            try {
                const moodList = await getMoods();

                if (!active) return;

                setMoods(Array.isArray(moodList) ? moodList : []);
            } catch (error) {
                if (!active) return;

                console.error('Error fetching moods:', error);
            }
        }

        loadMoods();

        return () => {
            active = false;
        };
    }, []);

    return (
        <MoodContextObject.Provider
            value={{ moods, setMoods }}
        >
            {children}
        </MoodContextObject.Provider>
    );
}
