import "./UserTabStyle.css";
import { useNavigate, useLocation } from "react-router-dom";
import Logout from "../../../components/general-components/Logout";

export default function UserTabs() {
  const navigate = useNavigate();
  const location = useLocation();

  return (
    <section className="user-tabs">
      <div className="ut-container">
        <button
          className={location.pathname === "/user-home" ? "active" : ""}
          onClick={() => navigate("/user-home")}
        >
          Home
        </button>

        <button
          className={location.pathname === "/user-mood" ? "active" : ""}
          onClick={() => navigate("/user-mood")}
        >
          Mood
        </button>

        <button
          className={location.pathname === "/user-wellness" ? "active" : ""}
          onClick={() => navigate("/user-wellness")}
        >
          Wellness
        </button>

        <Logout />
      </div>
    </section>
  );
}
