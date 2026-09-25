import {useState, useEffect} from 'react';
import {getWellness} from '../connection/WellnessConnection';
import {WellnessContextObject} from './WellnessContextObject';

export function WellnessProvider({children}) {
    const [wellness, setWellness] = useState([]);

    async function getWellnessList() {
        try {
            const wellnessList = await getWellness();
            setWellness(wellnessList);
        } catch (error) {
            console.error('Error fetching wellness data:', error);
        }
    }

    useEffect(() => {
        getWellnessList();
    }, []);

    return (
        <WellnessContextObject.Provider value={{ wellness, setWellness }}>
            {children}
        </WellnessContextObject.Provider>
    );
}
