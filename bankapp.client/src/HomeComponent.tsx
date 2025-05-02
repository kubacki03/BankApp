/* eslint-disable @typescript-eslint/no-unused-vars */
import { useState } from "react";
import LoginComponent from "./LoginComponent";
import NewsComponent from "./NewsComponent";

function HomeComponent() {
    const [isVisible, setIsVisible] = useState(true);

    const hideAddBar = () => {
        setIsVisible(false);
    };

    return (
        <div>
            {isVisible && (
                <div id="AdBar" className="bg-orange-100 p-3 display:inline">
                    <h1 className="text-amber-500 text-xl font-bold">
                        <img src="src/assets/bank_logo.svg" alt="Ikona" width="24" height="24" className="mr-2 inline align-middle" />
                        Weź bank w swoje ręce i pobierz naszą aplikację!
                    </h1>

                    <a href="#" className="mr-2">Pobierz</a>
                    <button onClick={hideAddBar}>Ukryj</button>
                </div>
            )}
            <div className="mx-10">
            <div className="mt-4 mx-3 mb-15 font-mono">
                <h1 className=" text-5xl font-bold">
                    <img src="src/assets/bank_logo.svg" alt="Ikona" width="50"  className="mr-2 inline align-middle" />
                    Platforma iBPL
                </h1>
            </div>
            <LoginComponent />

                <div className="flex justify-center">
                    <hr className="my-10 w-3/4"></hr>
                </div>
            <NewsComponent/>
            </div>
           
        </div>
    );
}

export default HomeComponent;
