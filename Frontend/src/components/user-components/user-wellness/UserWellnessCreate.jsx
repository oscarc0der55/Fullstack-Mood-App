import { useState } from "react";
import { useWellness } from "../../../context/UseWellness";
import { createWellness } from "../../../connection/wellness-connection/WellnessConnection";
import "./UserWellnessStyle.css";

export default function UserWellnessCreate() {
  const [activity, setActivity] = useState("");
  const [food, setFood] = useState("");
  const [sleepQuality, setSleepQuality] = useState("");

  const { getWellnessList } = useWellness();

  async function handleSubmit(e) {
    e.preventDefault();

    try {
      const newWellness = {
        activity,
        food,
        sleepQuality,
      };

      await createWellness(newWellness);
      await getWellnessList();

      setActivity("");
      setFood("");
      setSleepQuality("");
    } catch (error) {
      console.error("Error creating wellness entry:", error);
    }
  }

  return (
    <div className="uwc">
      <div className="uwc-container">
        <form className="uwc-form" onSubmit={handleSubmit}>
          <label htmlFor="activity">Activity:</label>

          <input
            id="activity"
            type="text"
            value={activity}
            onChange={(e) => setActivity(e.target.value)}
            required
          />

          <label htmlFor="food">Food:</label>

          <input
            id="food"
            type="text"
            value={food}
            onChange={(e) => setFood(e.target.value)}
            required
          />

          <label htmlFor="sleepQuality">Sleep Quality:</label>

            <select
              value={sleepQuality}
              onChange={(e) => setSleepQuality(e.target.value)}>
              <option value="">Select sleep quality</option>
              <option value="Bad">Bad</option>
              <option value="Average">Average</option>
              <option value="Good">Good</option>
            </select>

          <button type="submit">Create Wellness Entry</button>
        </form>
      </div>
    </div>
  );
}
