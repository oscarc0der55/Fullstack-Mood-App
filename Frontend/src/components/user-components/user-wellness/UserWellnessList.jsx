import {deleteWellness} from '../../../connection/wellness-connection/WellnessConnection';
import WellnessContext from '../../../context/WellnessContext';
import {useContext} from 'react';
import './UserWellnessStyle.css';

export default function UserWellnessList() {
    const {getWellnessList, wellness} = useContext(WellnessContext);

    async function handleDeleteWellness(wellnessId) {
        try {
            await deleteWellness(wellnessId);
            getWellnessList();
        } catch (error) {
            console.error(`Error deleting wellness entry with ID ${wellnessId}:`, error);
        }
    }

    return (
        <div className="uwl">
            <div className="uwl-container">
                <h1>List of Wellness Entries</h1>
                <ul className="uwl-list">
                    {wellness.map((entry) => (
                        <li key={entry.wellnessId}>
                            Activity: {entry.activity} - Food: {entry.food} - Sleep Quality: {entry.sleepQuality}
                            <button onClick={() => handleDeleteWellness(entry.wellnessId)}>Delete</button>
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    )
}
