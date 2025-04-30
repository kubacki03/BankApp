import { useState } from "react";
import LoginComponent from "./LoginComponent";

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
                        Weź bank w swoje ręce i pobierz naszą aplikację!
                    </h1>
                    <a href="#" className="mr-2">Pobierz</a>
                    <button onClick={hideAddBar}>Ukryj</button>
                </div>
            )}

            <LoginComponent />
        </div>
    );
}

export default HomeComponent;
