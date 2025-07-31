/* eslint-disable @typescript-eslint/no-unused-vars */
import { useState } from "react";
import AccountBalanceComponent from "./AccountBalanceComponent";
import LoanAdComponent from "./LoanAdComponent";
import NavBar from "./NavBar";
import ServicesComponent from "./ServicesComponent";
import TransfersHistoryComponent from "./TransfersHistoryComponent";
import React from "react";
import UserAccountsComponent from "./UserAccountsComponent";

function DashboardComponent() {
    const [show, setShow] = useState<boolean>(false);

    function showHistory() {
        setShow(prev => !prev);
    }

    return (
        <main className="flex flex-col items-center">
            <NavBar />
            <div className=" mt-3 mb-3 flex flex-row justify-center gap-[10%]">

                <aside className="w-[20%]">
                    <LoanAdComponent />
                    <UserAccountsComponent/>
                </aside>

                <AccountBalanceComponent onHistoryClick={showHistory} />

                <div className="">
                    <ServicesComponent />
                </div>
            </div>

            {show && <TransfersHistoryComponent />}
        </main>
    );
}


export default DashboardComponent;