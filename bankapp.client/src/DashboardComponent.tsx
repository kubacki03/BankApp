/* eslint-disable @typescript-eslint/no-unused-vars */
import { useState } from "react";
import AccountBalanceComponent from "./AccountBalanceComponent";
import LoanAdComponent from "./LoanAdComponent";
import NavBar from "./NavBar";
import ServicesComponent from "./ServicesComponent";
import TransfersHistoryComponent from "./TransfersHistoryComponent";

function DashboardComponent() {
    const [show, setShow] = useState<boolean>(false);

    function showHistory() {
        setShow(prev => !prev);
    }

    return (
        <main className="flex flex-col items-center border border-amber-300">
            <NavBar />
            <div className="border-b-gray-700300 mb-3 flex w-9/10 flex-row justify-center border">
                <LoanAdComponent />

                {/* Przekazujemy funkcję showHistory do AccountBalanceComponent */}
                <AccountBalanceComponent onHistoryClick={showHistory} />

                <div className="ml-30">
                    <ServicesComponent />
                </div>
            </div>

            {show && <TransfersHistoryComponent />}
        </main>
    );
}


export default DashboardComponent;