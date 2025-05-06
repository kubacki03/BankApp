import { useState } from "react";
import SavedPasswordPopUp from "./SavedPasswordPopUp";

function SuccessRegisterComponent() {
    const login = "842934934943390s";
    const [showPopUp, setShow] = useState<boolean>(false);

    const handlePopUpClose = () => {
        setShow(false);
    };

    return (
        <div>
            <div className="mx-3 mt-4 font-mono">
                <h1 className=" text-5xl font-bold">
                    <img src="src/assets/bank_logo.svg" alt="Ikona" width="50" className="mr-2 inline align-middle" />
                    Platforma iBPL
                </h1>
            </div>
            <div className="mx-4 mt-10 flex flex-col gap-10 rounded-lg border border-amber-500 p-4 md:flex-row md:mx-20">
                <h1 className="text-xl">
                    Twój osobisty login <span className="text-2xl font-bold">{login}</span>, Zapisz go, nie będzie można go zobaczyć ponownie
                </h1>
                <button
                    onClick={() => setShow(true)}
                    className="ml-auto self-start rounded-xl border border-amber-400 bg-amber-400 p-2 transition ease-in-out hover:bg-amber-300 md:self-center"
                >
                    Przejdź dalej
                </button>
            </div>

            {showPopUp && <SavedPasswordPopUp onClose={handlePopUpClose} />}
        </div>
    );
}

export default SuccessRegisterComponent;
