import {deleteMood} from "../../../connection/mood-connection/MoodConnection";
import MoodContext from "../../../context/MoodContext";
import {useContext} from "react";
import './UserMoodStyle.css';

export default function UserMoodList(){
    const {getMoodList, moods} = useContext(MoodContext);

    async function handleDeleteMood(moodId) {
        try {
            await deleteMood(moodId);
            getMoodList();
        } catch (error) {
            console.error(`Error deleting mood with ID ${moodId}:`, error);
        }
    }
        return (
            <div className="uml">
                <div className="uml-container">
                    <h1>List of Moods</h1>
                    <ul className="uml-list">
                        {moods.map((mood) => (
                            <li key={mood.moodId}>
                                {mood.status} - {mood.troubles} 
                                <button onClick={() => handleDeleteMood(mood.moodId)}>Delete</button>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        );
}
