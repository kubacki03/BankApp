/* eslint-disable @typescript-eslint/no-unused-vars */
import { useState } from "react";
import LoginComponent from "./LoginComponent";
import NewsComponent from "./NewsComponent";
import RegisterComponent from "./RegisterComponent";

function HomeComponent() {
    const [isVisible, setIsVisible] = useState(true);

    const hideAddBar = () => {
        setIsVisible(false);
    };

    const [showLogin, setShowLogin] = useState<boolean>(true); 

    const changeView = () => {

        if (showLogin) {
            setShowLogin(false);
        } else {
            setShowLogin(true);
        }
    }

    return (
        <div>
            {isVisible && (
                <div id="AdBar" className="bg-orange-100 p-3 display:inline">
                    <h1 className="text-xl font-bold text-amber-500">
                        <img src="src/assets/bank_logo.svg" alt="Ikona" width="24" height="24" className="mr-2 inline align-middle" />
                        Weź bank w swoje ręce i pobierz naszą aplikację!
                    </h1>

                    <a href="#" className="mr-2">Pobierz</a>
                    <button onClick={hideAddBar}>Ukryj</button>
                </div>
            )}
            <div className="mx-10">
            <div className="mx-3 mt-4 mb-15 font-mono">
                <h1 className=" text-5xl font-bold">
                    <img src="src/assets/bank_logo.svg" alt="Ikona" width="50"  className="mr-2 inline align-middle" />
                    Platforma iBPL
                </h1>
                </div>

                <div className="mx-4 mt-10 flex flex-col gap-10 rounded-lg border border-amber-500 p-4 md:flex-row md:mx-20">

                    {showLogin && (<LoginComponent />)}
                    {!showLogin && (<RegisterComponent />)}

                    {showLogin && (<div className="mt-6 w-full border-l border-gray-300 pl-6 md:w-1/2 md:mt-0">

                        <h1 className="my-4">Jeśli nie masz jeszcze konta?</h1>
                        <button className="rounded-lg border border-amber-500 p-2 transition duration-300 ease-in-out hover:bg-amber-300" onClick={changeView}>Załóż konto</button>
                    </div>)}

                    {!showLogin && (<div className="mt-6 w-full border-l border-gray-300 pl-6 md:w-1/2 md:mt-0">

                        <h1 className="my-4">A może już masz moje m-Konto?</h1>
                        <button className="rounded-lg border border-amber-500 p-2 transition duration-300 ease-in-out hover:bg-amber-300" onClick={changeView}>Pokaż logowanie</button>
                    </div>)}
                   
                </div>
           

                <div className="flex justify-center">
                    <hr className="my-10 w-3/4"></hr>
                </div>
            <NewsComponent/>
            </div>
           
        </div>
    );
}

export default HomeComponent;
