import {useState, useContext} from 'react';
import {MoodContext} from '../../../context/user-context/MoodContext';
import {createMood} from '../../../connection/mood-connection/MoodConnection';

export default function UserMoodCreate() {
    const {status, setStatus} = useState('');
    const {troubles, setTroubles} = useState('');
    const {getMoodList} = useContext(MoodContext);

    async function handleSubmit(e) {
        e.preventDefault();
        try {
            const newMood = { status, troubles };
            await createMood(newMood);
            getMoodList();

            setStatus('');
            setTroubles('');
        }
        catch (error) {
            console.error('Error creating mood:', error);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <label htmlFor="status">Status:</label>
            <input type="number" min="1" max="10" value={status} onChange={(e) => setStatus(e.target.value)} required />
            <label htmlFor="troubles">Troubles:</label>
            <input type="text" value={troubles} onChange={(e) => setTroubles(e.target.value)} />
            <button type="submit">Create Mood</button>
            </form>
    )
}