import { useEffect, useState } from 'react';
import { getWellness } from '../connection/wellness-connection/WellnessConnection';
import { WellnessContextObject } from './WellnessContextObject';

export function useWellness({ children }) {
    const [wellness, setWellness] = useState([]);

    useEffect(() => {
        let active = true;

        async function loadWellness() {
            try {
                const wellnessList = await getWellness();

                if (!active) return;

                setWellness(
                    Array.isArray(wellnessList)
                        ? wellnessList
                        : []
                );
            } catch (error) {
                if (!active) return;

                console.error(
                    'Error fetching wellness data:',
                    error
                );
            }
        }

        loadWellness();

        return () => {
            active = false;
        };
    }, []);

    return (
        <WellnessContextObject.Provider
            value={{ wellness, setWellness }}
        >
            {children}
        </WellnessContextObject.Provider>
    );
}
