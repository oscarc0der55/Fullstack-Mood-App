import UserWellnessList from "../../components/user-components/user-wellness/UserWellnessList";
import UserWellnessCreate from "../../components/user-components/user-wellness/UserWellnessCreate";

export default function UserWellnessPage() {
    return (
        <div>
            <UserWellnessCreate />
            <UserWellnessList />
        </div>
    );
}