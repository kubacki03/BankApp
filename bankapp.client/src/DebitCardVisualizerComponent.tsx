import React, { useEffect, useState } from 'react';
import axios from 'axios';
import NavBar from './NavBar';

interface DebitCard {
    cardNumber: string;
    expirationDate: string;
    cvv: string;
    lifetime: string;
}

const DebitCardVisibilityComponent: React.FC = () => {
    const [card, setCard] = useState<DebitCard | null>(null);
    const [remainingTime, setRemainingTime] = useState<number | null>();
    const [showCard, setShowCard] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchCard = async () => {
            try {
                const token = localStorage.getItem('token');

                const response = await axios.get('https://localhost:7263/Card/GetCard', {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                const cardData: DebitCard = response.data;
                setCard(cardData);

                const lifetimeDate = new Date(cardData.lifetime).getTime();
                const now = new Date().getTime();
                const diff = Math.max(0, Math.floor((lifetimeDate - now) / 1000));
                setRemainingTime(diff);
            } catch (err) {
                setError('Nie udało się pobrać danych karty.');
            } finally {
                setLoading(false);
            }
        };

        fetchCard();
    }, []);

    useEffect(() => {
        if (remainingTime === null) return;

        const interval = setInterval(() => {
            setRemainingTime((prev) => {
                if (prev !== null && prev > 0) {
                    return prev - 1;
                } else {
                    clearInterval(interval);
                    return 0;
                }
            });
        }, 1000);

        return () => clearInterval(interval);
    }, [remainingTime]);

    const formatTime = (seconds: number) => {
        const min = Math.floor(seconds / 60);
        const sec = seconds % 60;
        return `${min.toString().padStart(2, '0')}:${sec.toString().padStart(2, '0')}`;
    };

    if (loading) return <p>Ładowanie...</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;
    if (!card) return <p>Brak karty.</p>;
    if (remainingTime !== null && remainingTime <= 0) {
        return <p style={{ color: 'orange' }}>Karta wygasła.</p>;
    }

    return (<div className="flex flex-col items-center">
        <NavBar/>
        <div style={styles.cardContainer}>
            <h2>Tymczasowa karta debetowa</h2>
            <p><strong>Ważna do:</strong> {new Date(card.lifetime).toLocaleString()}</p>
            <p><strong>Pozostały czas:</strong> {remainingTime !== null ? formatTime(remainingTime) : '—'}</p>

            <button style={styles.button} onClick={() => setShowCard(!showCard)}>
                {showCard ? 'Ukryj dane karty' : 'Pokaż dane karty'}
            </button>

            {showCard && (
                <div style={styles.cardDetails}>
                    <p><strong>Numer karty:</strong> {card.cardNumber}</p>
                    <p><strong>Data ważności:</strong> {card.expirationDate}</p>
                    <p><strong>CVV:</strong> {card.cvv}</p>
                </div>
            )}
        </div></div>
    );
};

const styles = {
    cardContainer: {
        border: '1px solid #ccc',
        padding: '1rem',
        borderRadius: '8px',
        backgroundColor: '#f7f7f7',
        maxWidth: '400px',
        margin: '1rem auto',
        boxShadow: '0 2px 6px rgba(0,0,0,0.1)',
    },
    button: {
        marginTop: '1rem',
        padding: '0.5rem 1rem',
        fontSize: '1rem',
        cursor: 'pointer',
        backgroundColor: '#007bff',
        color: '#fff',
        border: 'none',
        borderRadius: '6px',
    },
    cardDetails: {
        marginTop: '1rem',
        backgroundColor: '#fff',
        padding: '0.75rem',
        borderRadius: '6px',
        border: '1px solid #ddd',
    },
};



export default DebitCardVisibilityComponent;
