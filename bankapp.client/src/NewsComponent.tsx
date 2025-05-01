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
        <div className="flex-column gap-33 mx-10 flex">
           
            <div className="border-amber-500 px-10 pb-10 rounded-lg border shadow-xl hover:bg-amber-100">
                <article>
                    <h1 className="text-xl font-bold">{news[0].title}</h1>
                    <p className="text-xl">{news[0].content}</p>
                    <p>{news[0].date}</p>
                    <a href="#">Zobacz więcej</a>
            </article>
            </div>

                <div className="border-amber-500 px-10 pb-10 rounded-lg border shadow-xl hover:bg-amber-100">
                <article>
                    <h1 className="text-xl font-bold">{news[1].title}</h1>
                    <p className="text-xl">{news[1].content}</p>
                    <p>{news[1].date}</p>
                    <a href="#">Zobacz więcej</a>
                </article>
            </div>
         
            </div>
        </div>
    );

}

export default NewsComponent;