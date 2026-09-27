import { useContext } from 'react';
import { MoodContextObject } from './MoodContextObject';

export function useMood() {
    const context = useContext(MoodContextObject);

    if (!context) {
        throw new Error('useMood must be used inside MoodProvider');
    }

    return context;
}
