/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import React from "react";
import { useEffect, useState } from "react";

/* eslint-disable @typescript-eslint/no-unused-vars */
function TransfersHistoryComponent() {
    type transfer = {
        id: number;
        date: string;
        payeeName: string;
        title: string;
        amount: number;
    };

    const [history, setHistory] = useState<transfer[]>([]);
    const [filteredHistory, setFilteredHistory] = useState<transfer[]>([]);
    const [error, setError] = useState<string | null>(null);

    // Filtry
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [searchPayee, setSearchPayee] = useState<string>("");
    const [sortOption, setSortOption] = useState<string>("dateDesc");

    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const token = localStorage.getItem("token");

                const response = await axios.get(
                    "https://localhost:7263/AccountDetails/lastTransfers",
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                );
                setHistory(response.data);
            } catch (error: any) {
                setError("Wystąpił błąd pobierania historii przelewów");
            }
        };
        fetchHistory();
    }, []);

    useEffect(() => {
        let filtered = [...history];

        if (searchTitle) {
            filtered = filtered.filter((item) =>
                item.title.toLowerCase().includes(searchTitle.toLowerCase())
            );
        }

        if (searchPayee) {
            filtered = filtered.filter((item) =>
                item.payeeName.toLowerCase().includes(searchPayee.toLowerCase())
            );
        }

        switch (sortOption) {
            case "dateAsc":
                filtered.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
                break;
            case "dateDesc":
                filtered.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
                break;
            case "amountAsc":
                filtered.sort((a, b) => a.amount - b.amount);
                break;
            case "amountDesc":
                filtered.sort((a, b) => b.amount - a.amount);
                break;
        }

        setFilteredHistory(filtered);
    }, [searchTitle, searchPayee, sortOption, history]);

    const download = async (id: number) => {
        try {
            const token = localStorage.getItem("token");

            const response = await axios.get(
                `https://localhost:7263/Transfer/confirmation/${id}`
,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                    responseType: "blob", // kluczowe!
                }
            );

            // Utwórz link i ściągnij plik
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement("a");
            link.href = url;
            link.setAttribute("download", `potwierdzenie_${id}.pdf`);
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

        } catch (error: any) {
            setError("Wystąpił błąd pobierania potwierdzenia przelewów");
        }
    };


    return (
        <div className="space-y-4 p-4">
            <div className="flex flex-col gap-4 md:flex-row">
                <input
                    type="text"
                    placeholder="Filtruj po tytule"
                    value={searchTitle}
                    onChange={(e) => setSearchTitle(e.target.value)}
                    className="border rounded-md p-2"
                />
                <input
                    type="text"
                    placeholder="Filtruj po odbiorcy"
                    value={searchPayee}
                    onChange={(e) => setSearchPayee(e.target.value)}
                    className="border rounded-md p-2"
                />
                <select
                    value={sortOption}
                    onChange={(e) => setSortOption(e.target.value)}
                    className="border rounded-md p-2"
                >
                    <option value="dateDesc">Sortuj: Data malejąco</option>
                    <option value="dateAsc">Sortuj: Data rosnąco</option>
                    <option value="amountDesc">Sortuj: Kwota malejąco</option>
                    <option value="amountAsc">Sortuj: Kwota rosnąco</option>
                </select>
            </div>

            {filteredHistory.length > 0 ? (
                <div className="rounded-lg border border-gray-300 p-3 shadow-2xl transition ease-in-out hover:bg-amber-200">
                    {filteredHistory.map((item, index) => (
                        <article key={index} className="mb-4">
                            <h1>
                                <strong>Tytuł:</strong> {item.title}{" "}
                                <strong>Data:</strong> {new Date(item.date).toLocaleDateString()}
                            </h1>
                            <h2>
                                <strong>Odbiorca:</strong> {item.payeeName}
                            </h2>
                            <h2>
                                <strong>Kwota:</strong> {item.amount.toFixed(2)} PLN
                            </h2>
                            <button onClick={() => download(item.id)} className="rounded-2xl border border-amber-600 p-2">
                                Potwierdzenie
                            </button>

                        </article>
                    ))}
                </div>
            ) : (
                <p>Brak wyników dla podanych filtrów</p>
            )}
        </div>
    );
}

export default TransfersHistoryComponent;
