/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { useEffect, useState } from "react";

/* eslint-disable @typescript-eslint/no-unused-vars */
function NewsComponent() {

    type News = {
        title: string,
        content: string,
        date: string
    }

    const [news, setNews] = useState<News[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchNews = async () => {
            try {
              
                const response = await axios.get('https://localhost:7263/News/news');
                console.log(response.data);
                setNews(response.data);
            } catch (error: any) {
               
                setError("Wystąpił błąd pobierania newsów");
            }
        };
        fetchNews();

        
    }, []);

    if (error) return <p>{error}</p>;

    if (news.length === 0) return <p>Brak newsów do wyświetlenia.</p>;

    console.log('Wszystko git')
    return (
        <div>
            <h1 className="mb-8 text-center text-2xl">Najnowsze wiadomości</h1>
            <div className="gap-6 mx-10 flex flex-col justify-center md:flex-row">
                {news.map((item, index) => (
                    <div
                        key={index}
                        className="border-amber-500 px-10 pb-10 m-4 w-full rounded-lg border shadow-xl hover:bg-amber-100 md:w-1/2 lg:w-1/4"
                    >
                        <article>
                            <h1 className="text-xl font-bold">{item.title}</h1>
                            <p className="text-xl">{item.content}</p>
                            <p>{item.date}</p>
                          
                        </article>
                    </div>
                ))}
            </div>
        </div>
    );

}

export default NewsComponent;