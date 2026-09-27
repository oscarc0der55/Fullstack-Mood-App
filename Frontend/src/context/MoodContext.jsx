import { useCallback, useEffect, useState } from 'react';
import { getMoods } from '../connection/mood-connection/MoodConnection';
import { MoodContextObject } from './MoodContextObject';

export function MoodProvider({ children }) {
    const [moods, setMoods] = useState([]);

    const getMoodList = useCallback(async () => {
        try {
            const moodList = await getMoods();
            setMoods(moodList);
        } catch (error) {
            console.error('Error fetching moods:', error);
        }
    }, []);

    useEffect(() => {
        let active = true;

        async function loadMoods() {
            try {
                const moodList = await getMoods();

                if (active) {
                    setMoods(moodList);
                }
            } catch (error) {
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
            value={{
                moods,
                setMoods,
                getMoodList,
            }}
        >
            {children}
        </MoodContextObject.Provider>
    );
}
