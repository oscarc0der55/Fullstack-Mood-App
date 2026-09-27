import { useContext } from 'react';
import { WellnessContextObject } from './WellnessContextObject';

export function useWellness() {
    const context = useContext(WellnessContextObject);

    if (!context) {
        throw new Error('useWellness must be used inside WellnessProvider');
    }

    return context;
}
