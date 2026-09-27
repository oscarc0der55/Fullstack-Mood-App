import { useEffect, useMemo, useState } from 'react';
import {
    deleteUserWellness,
    getMyUserWellness,
} from '../../../connection/user-wellness-connection/UserWellnessConnection';
import './UserWellnessStyle.css';

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

export default function UserWellnessList() {
    const [wellness, setWellness] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    const [deletingId, setDeletingId] = useState(null);

    useEffect(() => {
        let active = true;

        getMyUserWellness()
            .then((entries) => {
                if (!active) return;
                setWellness(Array.isArray(entries) ? entries : []);
                setError('');
            })
            .catch((requestError) => {
                if (!active) return;
                setError('Wellness entries could not be loaded. Please try again.');
                console.error('Error loading wellness entries:', requestError);
            })
            .finally(() => {
                if (active) setIsLoading(false);
            });

        return () => {
            active = false;
        };
    }, []);

    const sortedWellness = useMemo(
        () => [...wellness].sort(
            (first, second) => new Date(second.creationDate) - new Date(first.creationDate),
        ),
        [wellness],
    );
    const latestEntry = sortedWellness[0];
    let wellnessHistory;

    if (isLoading) {
        wellnessHistory = <output className="wellness-dashboard__message">Loading wellness entries...</output>;
    } else if (sortedWellness.length === 0) {
        wellnessHistory = <p className="wellness-dashboard__message">No wellness entries yet. Your logs will appear here.</p>;
    } else {
        wellnessHistory = (
            <ul className="wellness-history__list">
                {sortedWellness.map((entry) => (
                    <li className="wellness-entry" key={entry.usersWellnessId}>
                        <div className="wellness-entry__content">
                            <div className="wellness-entry__topline">
                                <strong>{entry.activity}</strong>
                                <time dateTime={entry.creationDate}>{formatCreationDate(entry.creationDate)}</time>
                            </div>
                            <dl className="wellness-entry__details">
                                <div>
                                    <dt>Food</dt>
                                    <dd>{entry.food || 'Not recorded'}</dd>
                                </div>
                                <div>
                                    <dt>Sleep quality</dt>
                                    <dd>{entry.sleepQuality || 'Not recorded'}</dd>
                                </div>
                            </dl>
                        </div>
                        <button
                            className="wellness-entry__delete"
                            type="button"
                            onClick={() => handleDeleteWellness(entry.usersWellnessId)}
                            disabled={deletingId === entry.usersWellnessId}
                            aria-label={`Delete wellness entry from ${formatCreationDate(entry.creationDate)}`}
                        >
                            {deletingId === entry.usersWellnessId ? 'Deleting...' : 'Delete'}
                        </button>
                    </li>
                ))}
            </ul>
        );
    }

    async function handleDeleteWellness(usersWellnessId) {
        setDeletingId(usersWellnessId);
        setError('');

        try {
            await deleteUserWellness(usersWellnessId);
            setWellness((currentEntries) => currentEntries.filter(
                (entry) => entry.usersWellnessId !== usersWellnessId,
            ));
        } catch (requestError) {
            setError('This wellness entry could not be deleted. Please try again.');
            console.error(`Error deleting wellness entry ${usersWellnessId}:`, requestError);
        } finally {
            setDeletingId(null);
        }
    }

    return (
        <section className="uwl wellness-dashboard" aria-labelledby="wellness-dashboard-title">
            <div className="uwl-container">
                <header className="wellness-dashboard__header">
                    <div>
                        <p className="wellness-dashboard__eyebrow">PERSONAL TRACKER</p>
                        <h1 id="wellness-dashboard-title">Wellness overview</h1>
                        <p className="wellness-dashboard__intro">Your activity, food, and sleep in one view.</p>
                    </div>
                    <span className="wellness-dashboard__count">{wellness.length} entries</span>
                </header>

                <div className="wellness-dashboard__metrics" aria-label="Wellness summary">
                    <article className="wellness-metric wellness-metric--teal">
                        <span className="wellness-metric__label">Total entries</span>
                        <strong>{wellness.length}</strong>
                    </article>
                    <article className="wellness-metric wellness-metric--gold">
                        <span className="wellness-metric__label">Latest activity</span>
                        <strong className="wellness-metric__activity">{latestEntry?.activity || '--'}</strong>
                    </article>
                    <article className="wellness-metric wellness-metric--coral">
                        <span className="wellness-metric__label">Most recent</span>
                        <strong className="wellness-metric__date">
                            {latestEntry ? formatCreationDate(latestEntry.creationDate) : '--'}
                        </strong>
                    </article>
                </div>

                <section className="wellness-history" aria-labelledby="wellness-history-title">
                    <div className="wellness-dashboard__section-heading">
                        <div>
                            <p className="wellness-dashboard__eyebrow">YOUR JOURNAL</p>
                            <h2 id="wellness-history-title">Recent entries</h2>
                        </div>
                    </div>

                    {error && <p className="wellness-dashboard__message wellness-dashboard__message--error" role="alert">{error}</p>}
                    {wellnessHistory}
                </section>
            </div>
        </section>
    );
}
