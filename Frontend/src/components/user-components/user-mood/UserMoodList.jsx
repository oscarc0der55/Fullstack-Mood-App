import {deleteMood} from "../../../connection/mood-connection/MoodConnection";
import MoodContext from "../../../context/MoodContext";
import {useContext} from "react";

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
            <>
            <h1>List of Moods</h1>
            <ul>          
                {moods.map((mood) => (
                    <li key={mood.moodId}>
                        {mood.status} - {mood.troubles} 
                        <button onClick={() => handleDeleteMood(mood.moodId)}>Delete</button>
                    </li>
                ))}
            </ul>
        </>
    );
}
