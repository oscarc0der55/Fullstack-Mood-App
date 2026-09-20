import {useState, useEffect} from 'react';
import {getMoods} from '../../../connection/mood-connection/MoodConnection';
import {MoodContextObject} from './MoodContextObject';

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
        <MoodContextObject.Provider value={{ moods, setMoods }}>
            {children}
        </MoodContextObject.Provider>
    );
}