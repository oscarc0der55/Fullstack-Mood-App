import { useState } from 'react';
import { useMood } from '../../../context/UseMood';
import { createMood } from '../../../connection/mood-connection/MoodConnection';
import './UserMoodStyle.css';

export default function UserMoodCreate() {
    const [status, setStatus] = useState('');
    const [troubles, setTroubles] = useState('');

    const { getMoodList } = useMood();

    async function handleSubmit(e) {
        e.preventDefault();

        try {
            const newMood = {
                status: Number(status),
                troubles,
            };

            await createMood(newMood);
            await getMoodList();

            setStatus('');
            setTroubles('');
        } catch (error) {
            console.error('Error creating mood:', error);
        }
    }

    return (
        <div className="umc">
            <div className="umc-container">
                <form className="umc-form" onSubmit={handleSubmit}>
                    <label htmlFor="status">Status:</label>

                    <input
                        id="status"
                        type="number"
                        min="1"
                        max="10"
                        value={status}
                        onChange={(e) => setStatus(e.target.value)}
                        required
                    />

                    <label htmlFor="troubles">Troubles:</label>

                    <input
                        id="troubles"
                        type="text"
                        value={troubles}
                        onChange={(e) => setTroubles(e.target.value)}
                    />

                    <button type="submit">
                        Create Mood
                    </button>
                </form>
            </div>
        </div>
    );
}
