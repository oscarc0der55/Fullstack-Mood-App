import { useEffect, useMemo, useState } from "react";
import {
  deleteWellness,
  getWellness,
  updateWellness,
} from "../../../connection/wellness-connection/WellnessConnection";
import "./UserWellnessStyle.css";

function formatCreationDate(value) {
  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return "Date unavailable";
  }

  return new Intl.DateTimeFormat(undefined, {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
}

export default function UserWellnessList() {
  const [wellness, setWellness] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  const [deletingId, setDeletingId] = useState(null);
  const [editingId, setEditingId] = useState(null);
  const [savingId, setSavingId] = useState(null);

  const [editActivity, setEditActivity] = useState("");
  const [editFood, setEditFood] = useState("");
  const [editSleepQuality, setEditSleepQuality] = useState("");

  useEffect(() => {
    let active = true;

    getWellness()
      .then((entries) => {
        if (!active) return;

        setWellness(Array.isArray(entries) ? entries : []);
        setError("");
      })
      .catch((requestError) => {
        if (!active) return;

        setError("Wellness entries could not be loaded. Please try again.");

        console.error("Error loading wellness entries:", requestError);
      })
      .finally(() => {
        if (active) {
          setIsLoading(false);
        }
      });

    return () => {
      active = false;
    };
  }, []);

  const sortedWellness = useMemo(
    () =>
      [...wellness].sort(
        (first, second) =>
          new Date(second.creationDate) - new Date(first.creationDate),
      ),
    [wellness],
  );

  const latestEntry = sortedWellness[0];
  const recentWellness = sortedWellness.slice(0, 7).reverse();

  const sleepQualityScore = {
    Bad: 3,
    Average: 6,
    Good: 9,
  };

  function handleEditWellness(entry) {
    setEditingId(entry.wellnessId);

    setEditActivity(entry.activity || "");
    setEditFood(entry.food || "");
    setEditSleepQuality(entry.sleepQuality || "");

    setError("");
  }

  function handleCancelEdit() {
    setEditingId(null);
    setEditActivity("");
    setEditFood("");
    setEditSleepQuality("");
  }

  async function handleUpdateWellness(wellnessId) {
    setSavingId(wellnessId);
    setError("");

    try {
      const updatedWellness = {
        activity: editActivity,
        food: editFood,
        sleepQuality: editSleepQuality,
      };

      await updateWellness(wellnessId, updatedWellness);

      setWellness((currentEntries) =>
        currentEntries.map((entry) =>
          entry.wellnessId === wellnessId
            ? {
                ...entry,
                activity: updatedWellness.activity,
                food: updatedWellness.food,
                sleepQuality: updatedWellness.sleepQuality,
              }
            : entry,
        ),
      );

      handleCancelEdit();
    } catch (requestError) {
      setError("This wellness entry could not be updated. Please try again.");

      console.error(
        `Error updating wellness entry ${wellnessId}:`,
        requestError,
      );
    } finally {
      setSavingId(null);
    }
  }

  async function handleDeleteWellness(wellnessId) {
    setDeletingId(wellnessId);
    setError("");

    try {
      await deleteWellness(wellnessId);

      setWellness((currentEntries) =>
        currentEntries.filter((entry) => entry.wellnessId !== wellnessId),
      );
    } catch (requestError) {
      setError("This wellness entry could not be deleted. Please try again.");

      console.error(
        `Error deleting wellness entry ${wellnessId}:`,
        requestError,
      );
    } finally {
      setDeletingId(null);
    }
  }

  let wellnessHistory;

  if (isLoading) {
    wellnessHistory = (
      <output className="wellness-dashboard__message">
        Loading wellness entries...
      </output>
    );
  } else if (sortedWellness.length === 0) {
    wellnessHistory = (
      <p className="wellness-dashboard__message">
        No wellness entries yet. Your logs will appear here.
      </p>
    );
  } else {
    wellnessHistory = (
      <ul className="wellness-history__list">
        {sortedWellness.map((entry) => {
          const isEditing = editingId === entry.wellnessId;
          const isSaving = savingId === entry.wellnessId;
          const isDeleting = deletingId === entry.wellnessId;

          return (
            <li className="wellness-entry" key={entry.wellnessId}>
              <div className="wellness-entry__content">
                {isEditing ? (
                  <>
                    <div className="wellness-entry__topline">
                      <span className="wellness-entry__title">
                        Edit wellness entry
                      </span>

                      <time dateTime={entry.creationDate}>
                        {formatCreationDate(entry.creationDate)}
                      </time>
                    </div>

                    <div className="wellness-entry__edit-fields">
                      <label>
                        Activity
                        <input
                          type="text"
                          value={editActivity}
                          onChange={(e) => setEditActivity(e.target.value)}
                        />
                      </label>

                      <label>
                        Food
                        <input
                          type="text"
                          value={editFood}
                          onChange={(e) => setEditFood(e.target.value)}
                        />
                      </label>

                      <label>
                        Sleep quality
                        <select
                          value={editSleepQuality}
                          onChange={(e) => setEditSleepQuality(e.target.value)}
                        >
                          <option value="">Select sleep quality</option>
                          <option value="Bad">Bad</option>
                          <option value="Average">Average</option>
                          <option value="Good">Good</option>
                        </select>
                      </label>
                    </div>
                  </>
                ) : (
                  <>
                    <div className="wellness-entry__topline">
                      <span className="wellness-entry__title">
                        {entry.activity || "No activity recorded"}
                      </span>

                      <time dateTime={entry.creationDate}>
                        {formatCreationDate(entry.creationDate)}
                      </time>
                    </div>

                    <dl className="wellness-entry__details">
                      <div>
                        <dt>Food</dt>
                        <dd>{entry.food || "Not recorded"}</dd>
                      </div>

                      <div>
                        <dt>Sleep quality</dt>
                        <dd>{entry.sleepQuality || "Not recorded"}</dd>
                      </div>
                    </dl>
                  </>
                )}
              </div>

              <div className="wellness-entry__actions">
                {isEditing ? (
                  <>
                    <button
                      className="wellness-entry__save"
                      type="button"
                      onClick={() => handleUpdateWellness(entry.wellnessId)}
                      disabled={isSaving}
                    >
                      {isSaving ? "Saving..." : "Save"}
                    </button>

                    <button
                      className="wellness-entry__cancel"
                      type="button"
                      onClick={handleCancelEdit}
                      disabled={isSaving}
                    >
                      Cancel
                    </button>
                  </>
                ) : (
                  <>
                    <button
                      className="wellness-entry__edit"
                      type="button"
                      onClick={() => handleEditWellness(entry)}
                      disabled={isDeleting || editingId !== null}
                    >
                      Edit
                    </button>

                    <button
                      className="wellness-entry__delete"
                      type="button"
                      onClick={() => handleDeleteWellness(entry.wellnessId)}
                      disabled={isDeleting || editingId !== null}
                      aria-label={`Delete wellness entry from ${formatCreationDate(
                        entry.creationDate,
                      )}`}
                    >
                      {isDeleting ? "Deleting..." : "Delete"}
                    </button>
                  </>
                )}
              </div>
            </li>
          );
        })}
      </ul>
    );
  }

  return (
    <section
      className="uwl-container"
    >
      <div className="uwl wellness-dashboard"
      aria-labelledby="wellness-dashboard-title">
        <span className="wellness-dashboard__count">
          {wellness.length} entries
        </span>

        <div className="wellness-dashboard__metrics">
          <article className="wellness-metric wellness-metric--teal">
            <span className="wellness-metric__label">Total entries</span>

            <span className="wellness-metric__value">{wellness.length}</span>
          </article>

          <article className="wellness-metric wellness-metric--gold">
            <span className="wellness-metric__label">Latest activity</span>

            <span className="wellness-metric__value wellness-metric__activity">
              {latestEntry?.activity || "--"}
            </span>
          </article>

          <article className="wellness-metric wellness-metric--coral">
            <span className="wellness-metric__label">Most recent</span>

            <span className="wellness-metric__date">
              {latestEntry
                ? formatCreationDate(latestEntry.creationDate)
                : "--"}
            </span>
          </article>
        </div>

        {recentWellness.length > 0 && (
          <section
            className="wellness-trend"
            aria-labelledby="wellness-trend-title"
          >
            <div className="wellness-dashboard__section-heading">
              <div>
                <p className="wellness-dashboard__eyebrow">
                  LAST SEVEN CHECK-INS
                </p>

                <h2 id="wellness-trend-title">Wellness trend</h2>
              </div>

              <span className="wellness-trend__scale">
                Bad · Average · Good
              </span>
            </div>

            <ol className="wellness-trend__bars">
              {recentWellness.map((entry) => {
                const score = sleepQualityScore[entry.sleepQuality] || 0;

                return (
                  <li
                    key={entry.wellnessId}
                    title={`${entry.sleepQuality || "Not recorded"}, ${formatCreationDate(
                      entry.creationDate,
                    )}`}
                  >
                    <span className="wellness-trend__score">
                      {entry.sleepQuality || "-"}
                    </span>

                    <span
                      className="wellness-trend__bar"
                      style={{
                        height: `${Math.max(score * 10, 8)}%`,
                      }}
                    />

                    <span className="wellness-trend__date">
                      {new Date(entry.creationDate).toLocaleDateString(
                        undefined,
                        {
                          month: "short",
                          day: "numeric",
                        },
                      )}
                    </span>
                  </li>
                );
              })}
            </ol>
          </section>
        )}

        <section
          className="wellness-history"
          aria-labelledby="wellness-history-title"
        >
          <div className="wellness-dashboard__section-heading">
            <div>
              <p className="wellness-dashboard__eyebrow">YOUR JOURNAL</p>

              <h2 id="wellness-history-title">Recent entries</h2>
            </div>
          </div>

          {error && (
            <p
              className="wellness-dashboard__message wellness-dashboard__message--error"
              role="alert"
            >
              {error}
            </p>
          )}

          {wellnessHistory}
        </section>
      </div>
    </section>
  );
}
