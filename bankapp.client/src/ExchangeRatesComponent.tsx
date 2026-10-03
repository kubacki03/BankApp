import React, { useEffect, useState } from "react";
import NavBar from "./NavBar";

const ExchangeRatesComponent = () => {
    const [rates, setRates] = useState([]);
    const [loading, setLoading] = useState(true);
    const [date, setDate] = useState("");
    const [error, setError] = useState(null);
    const [filter, setFilter] = useState("");
    useEffect(() => {
        const fetchRates = async () => {
            try {
                const response = await fetch("https://api.nbp.pl/api/exchangerates/tables/a/?format=json");
                if (!response.ok) throw new Error("Nie uda�o si� pobra� danych");
                const data = await response.json();
                setRates(data[0].rates);
                setDate(data[0].effectiveDate);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchRates();
    }, []);
    const filteredRates = rates.filter((rate) =>
        rate.currency.toLowerCase().includes(filter.toLowerCase()) ||
        rate.code.toLowerCase().includes(filter.toLowerCase())
    );
    if (loading) return <p>�adowanie danych...</p>;
    if (error) return <p>B��d: {error}</p>;

    return (
        <div className="flex flex-col items-center">
            <NavBar />
            <input
                type="text"
                placeholder="Filtruj po nazwie waluty lub kodzie..."
                className="my-4 w-80 rounded border p-2"
                value={filter}
                onChange={(e) => setFilter(e.target.value)}
            />
            <h1 className="my-4 font-mono text-3xl">Kursy walut z dnia {date}</h1>
            <table>
                <thead>
                    <tr>
                        <th className="p-3">Waluta</th>
                        <th className="p-3">Kod</th>
                        <th className="p-3">Kurs (PLN)</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredRates.map((rate) => (
                        <tr key={rate.code}>
                            <td className="p-3">{rate.currency}</td>
                            <td className="p-3">{rate.code}</td>
                            <td className="p-3 font-bold">{rate.mid.toFixed(4)}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ExchangeRatesComponent;
