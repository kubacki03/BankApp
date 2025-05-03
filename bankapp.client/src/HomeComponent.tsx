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

                <div className="border-amber-500 mt-10 p-4 gap-10 mx-4 flex flex-col rounded-lg border md:flex-row md:mx-20">

                    {showLogin && (<LoginComponent />)}
                    {!showLogin && (<RegisterComponent />)}

                    {showLogin && (<div className="pl-6 mt-6 border-gray-300 w-full border-l md:w-1/2 md:mt-0">

                        <h1 className="my-4">Jeśli nie masz jeszcze konta?</h1>
                        <button className="bg-purple-400 p-2 rounded-lg transition duration-300 ease-in-out hover:bg-red-300" onClick={changeView}>Załóż konto</button>
                    </div>)}

                    {!showLogin && (<div className="pl-6 mt-6 border-gray-300 w-full border-l md:w-1/2 md:mt-0">

                        <h1 className="my-4">A może już masz moje m-Konto?</h1>
                        <button className="bg-purple-400 p-2 rounded-lg transition duration-300 ease-in-out hover:bg-red-300" onClick={changeView}>Pokaż logowanie</button>
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
