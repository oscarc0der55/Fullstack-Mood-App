import UserMoodPage from './user-content-pages/UserMoodPage';
import UserWellnessPage from './user-content-pages/UserWellnessPage';

export default function UserStatistic() {
    return (
        <div>
            <UserMoodPage />
            <UserWellnessPage />
        </div>
    );
}