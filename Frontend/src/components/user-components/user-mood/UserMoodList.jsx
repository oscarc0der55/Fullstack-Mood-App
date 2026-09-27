import { useEffect, useMemo, useState } from 'react';
import {
    deleteUserMood,
    getMyUserMoods,
} from '../../../connection/user-mood-connection/UserMoodConnection';
import './UserMoodStyle.css';

function formatCreationDate(value) {
    const date = new Date(value);

    if (Number.isNaN(date.getTime())) {
        return 'Date unavailable';
    }

    return new Intl.DateTimeFormat(undefined, {
        dateStyle: 'medium',
        timeStyle: 'short',
    }).format(date);
}

export default function UserMoodList() {
    const [moods, setMoods] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    const [deletingId, setDeletingId] = useState(null);

    useEffect(() => {
        let active = true;

        getMyUserMoods()
            .then((entries) => {
                if (!active) return;
                setMoods(Array.isArray(entries) ? entries : []);
                setError('');
            })
            .catch((requestError) => {
                if (!active) return;
                setError('Mood entries could not be loaded. Please try again.');
                console.error('Error loading mood entries:', requestError);
            })
            .finally(() => {
                if (active) setIsLoading(false);
            });

        return () => {
            active = false;
        };
    }, []);

    const sortedMoods = useMemo(
        () => [...moods].sort(
            (first, second) => new Date(second.creationDate) - new Date(first.creationDate),
        ),
        [moods],
    );
    const averageScore = moods.length
        ? (moods.reduce((total, mood) => total + Number(mood.status || 0), 0) / moods.length).toFixed(1)
        : '--';
    const recentScores = sortedMoods.slice(0, 7).reverse();
    let moodHistory;

    if (isLoading) {
        moodHistory = <output className="mood-dashboard__message">Loading mood entries...</output>;
    } else if (sortedMoods.length === 0) {
        moodHistory = <p className="mood-dashboard__message">No mood entries yet. Your check-ins will appear here.</p>;
    } else {
        moodHistory = (
            <ul className="mood-history__list">
                {sortedMoods.map((mood) => (
                    <li className="mood-entry" key={mood.usersMoodId}>
                        <span className="mood-entry__score" aria-label={`Mood score ${mood.status} out of 10`}>
                            {mood.status}
                        </span>
                        <div className="mood-entry__content">
                            <div className="mood-entry__topline">
                                <strong>Mood score {mood.status}</strong>
                                <time dateTime={mood.creationDate}>{formatCreationDate(mood.creationDate)}</time>
                            </div>
                            <p>{mood.troubles || 'No notes added.'}</p>
                        </div>
                        <button
                            className="mood-entry__delete"
                            type="button"
                            onClick={() => handleDeleteMood(mood.usersMoodId)}
                            disabled={deletingId === mood.usersMoodId}
                            aria-label={`Delete mood entry from ${formatCreationDate(mood.creationDate)}`}
                        >
                            {deletingId === mood.usersMoodId ? 'Deleting...' : 'Delete'}
                        </button>
                    </li>
                ))}
            </ul>
        );
    }

    async function handleDeleteMood(usersMoodId) {
        setDeletingId(usersMoodId);
        setError('');

        try {
            await deleteUserMood(usersMoodId);
            setMoods((currentMoods) => currentMoods.filter(
                (mood) => mood.usersMoodId !== usersMoodId,
            ));
        } catch (requestError) {
            setError('This mood entry could not be deleted. Please try again.');
            console.error(`Error deleting mood entry ${usersMoodId}:`, requestError);
        } finally {
            setDeletingId(null);
        }
    }

    return (
        <section className="uml mood-dashboard" aria-labelledby="mood-dashboard-title">
            <div className="uml-container">
                <header className="mood-dashboard__header">
                    <div>
                        <p className="mood-dashboard__eyebrow">PERSONAL TRACKER</p>
                        <h1 id="mood-dashboard-title">Mood overview</h1>
                        <p className="mood-dashboard__intro">A clear view of your recent check-ins.</p>
                    </div>
                    <span className="mood-dashboard__count">{moods.length} entries</span>
                </header>

                <div className="mood-dashboard__metrics" aria-label="Mood summary">
                    <article className="mood-metric mood-metric--coral">
                        <span className="mood-metric__label">Total check-ins</span>
                        <strong>{moods.length}</strong>
                    </article>
                    <article className="mood-metric mood-metric--teal">
                        <span className="mood-metric__label">Average mood</span>
                        <strong>{averageScore}<small> / 10</small></strong>
                    </article>
                    <article className="mood-metric mood-metric--gold">
                        <span className="mood-metric__label">Most recent</span>
                        <strong className="mood-metric__date">
                            {sortedMoods.length ? formatCreationDate(sortedMoods[0].creationDate) : '--'}
                        </strong>
                    </article>
                </div>

                {recentScores.length > 0 && (
                    <section className="mood-trend" aria-labelledby="mood-trend-title">
                        <div className="mood-dashboard__section-heading">
                            <div>
                                <p className="mood-dashboard__eyebrow">LAST SEVEN CHECK-INS</p>
                                <h2 id="mood-trend-title">Mood trend</h2>
                            </div>
                            <span className="mood-trend__scale">1 low · 10 high</span>
                        </div>
                        <ol className="mood-trend__bars">
                            {recentScores.map((mood) => {
                                const score = Math.min(10, Math.max(0, Number(mood.status) || 0));

                                return (
                                    <li key={mood.usersMoodId} title={`${score} out of 10, ${formatCreationDate(mood.creationDate)}`}>
                                        <span className="mood-trend__score">{score}</span>
                                        <span
                                            className="mood-trend__bar"
                                            style={{ height: `${Math.max(score * 10, 8)}%` }}
                                        />
                                        <span className="mood-trend__date">
                                            {new Date(mood.creationDate).toLocaleDateString(undefined, { month: 'short', day: 'numeric' })}
                                        </span>
                                    </li>
                                );
                            })}
                        </ol>
                    </section>
                )}

                <section className="mood-history" aria-labelledby="mood-history-title">
                    <div className="mood-dashboard__section-heading">
                        <div>
                            <p className="mood-dashboard__eyebrow">YOUR JOURNAL</p>
                            <h2 id="mood-history-title">Recent entries</h2>
                        </div>
                    </div>

                    {error && <p className="mood-dashboard__message mood-dashboard__message--error" role="alert">{error}</p>}
                    {moodHistory}
                </section>
            </div>
        </section>
    );
}
