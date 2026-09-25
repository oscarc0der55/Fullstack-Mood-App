import UserMoodList from "../../components/user-components/user-mood/UserMoodList";
import UserMoodCreate from "../../components/user-components/user-mood/UserMoodCreate";

export default function UserMoodPage() {
    return (
        <div>
            <UserMoodCreate />
            <UserMoodList />
        </div>
    );
}