/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { useEffect, useState } from "react";

/* eslint-disable @typescript-eslint/no-unused-vars */
function TransfersHistoryComponent() {

    type transfer = {
        Date: Date,
        PayeeName: string,
        Title: string
        Amount: number
    };

    const iban = "12342";

    const [history, setHistory] = useState<transfer[] | undefined>(undefined);

    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get('https://localhost:7263/News/news',
                    {
                        params: { iban: iban },
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
          {history?.map((item, index) => (
              <article key={index}>
                  <h1>{item.Title}</h1>
              </article>
          ))}
      </div>
  );
}

export default TransfersHistoryComponent;