import { useNavigate } from 'react-router-dom';
import './UserIntroStyle.css';

export default function UserIntro() {
    const navigate = useNavigate();

    return (
        <div className="ui">
        <div className="ui-container">
            <h1>Welcome to Mood App</h1>
            <p>Here you can track your mood and wellness over time. Use the navigation menu to access different features of the app.</p>
            <button onClick={() => navigate('/user-statistic')}>
                See your stats
            </button>
        </div>
        </div>
    );
}