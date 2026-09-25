import {useState, useContext} from 'react';
import WellnessContext from '../../../context/WellnessContext';
import {createWellness} from '../../../connection/wellness-connection/WellnessConnection';
import './UserWellnessStyle.css';

export default function UserWellnessCreate() {
    const {activity, setActivity} = useState('');
    const {food, setFood} = useState('');
    const {sleepQuality, setSleepQuality} = useState('');
    const {getWellnessList} = useContext(WellnessContext);

    async function handleSubmit(e) {
        e.preventDefault();
        try {
            const newWellness = { activity, food, sleepQuality };
            await createWellness(newWellness);
            getWellnessList();
            setActivity('');
            setFood('');
            setSleepQuality('');
        } catch (error) {
            console.error('Error creating wellness entry:', error);
        }
    }

    return (
        <div className="uwc">
            <div className="uwc-container">
                <form className="uwc-form" onSubmit={handleSubmit}>
                    <label htmlFor="activity">Activity:</label>
                    <input type="text" value={activity} onChange={(e) => setActivity(e.target.value)} required />
                    <label htmlFor="food">Food:</label>
                    <input type="text" value={food} onChange={(e) => setFood(e.target.value)} required />
                    <label htmlFor="sleepQuality">Sleep Quality:</label>
                    <input type="text" value={sleepQuality} onChange={(e) => setSleepQuality(e.target.value)} required />
                    <button type="submit">Create Wellness Entry</button>
                </form>
            </div>
        </div>
    )
}
