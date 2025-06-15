/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { useEffect, useState } from "react";

/* eslint-disable @typescript-eslint/no-unused-vars */
function TransfersHistoryComponent() {

    type transfer = {
        date: string,
        payeeName: string,
        title: string
        amount: number
    };

    const iban = "12342";

    const [history, setHistory] = useState<transfer[] | undefined>(undefined);

    const [error, setError] = useState<string | null>(null);
   

    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const token = localStorage.getItem("token");
             
                const response = await axios.get('https://localhost:7263/AccountDetails/lastTransfers',
                    {
                       
                        headers: {
                            Authorization: `Bearer ${token}`
                        }
                    }
                );
                console.log(response.data);
                setHistory(response.data);
            } catch (error: any) {

                setError("Wyst�pi� b��d pobierania news�w");
            }
        };
        fetchHistory();


    }, []);


    return (
        <div>
            
            {history && (
                <div className=" rounded-lg border border-gray-300 p-3 shadow-2xl transition ease-in-out hover:bg-amber-200">

                    {history?.map((item, index) => (
                        <article key={index}>
                            <h1>Tytu�: {item.title} Data: {item.date}</h1>
                            <h2>Odbiorca: {item.payeeName}</h2>
                            <h2>Kwota: {item.amount}</h2>
                        </article>
                    ))}

                </div>
            )}
            {history?.length==0 && (
                <div>
                    <p>Brak historii przelew�w</p>
                </div>
            ) }
        </div>
  );
}

export default TransfersHistoryComponent;