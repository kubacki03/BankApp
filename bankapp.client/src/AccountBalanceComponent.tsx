/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */

import axios from "axios";
import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import NewTransferComponent from "./NewTransferComponent";

type AccountData = {
    accountNumber: string;
    balance: number;
};


type AccountBalanceProps = {
    onHistoryClick: () => void;
};

function AccountBalanceComponent({ onHistoryClick }: AccountBalanceProps) {
    const [account, setData] = useState<AccountData>();
    const [errorOccured, setError] = useState<boolean>(false);
    const [showTransfer, setShowTransfer] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get('https://localhost:7263/AccountDetails/account', {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                setData(response.data);
            } catch (error: any) {
                setError(true);
            }
        };

        fetchData();
    }, []);

    if (errorOccured) {
        alert("Czas sesji minał")
        return <Navigate to="/" />;
        
    }

    return (
        <div className=" grid max-h-45 grid-cols-2 grid-rows-2 rounded-md shadow-2xl">
            <div className="col-start-1 row-start-1">
                <p>BPL Bank Polski</p>
                <h1 className="block text-xl">BPL KONTO PRYWATNE</h1>
                <p>{account?.accountNumber}</p>
            </div>

            <div className="col-2 row-2 flex flex-col items-end">
                <h1 className="text-gray-400">Dostępne środki</h1>
                <h2 className="text-xl font-bold">{account?.balance} PLN</h2>
                <div className="flex flex-row items-center justify-center gap-[3%]">
                    
                    <button onClick={onHistoryClick} className="cursor-pointer">
                        Historia
                    </button>

                    <button
                        onClick={() => setShowTransfer(true)}
                        className="time rounded-lg bg-amber-300 p-1 transition ease-linear hover:bg-amber-500"
                    >
                        Przelew
                    </button>
                </div>
            </div>

            {showTransfer && (
                <div className="fixed inset-0 z-50 flex items-center justify-center bg-white/30 backdrop-blur-sm">
                    <div className="w-full max-w-xl rounded-lg bg-white p-4 shadow-lg">
                        <div className="flex justify-end">
                            <button onClick={() => setShowTransfer(false)} className="text-red-500 text-xl">
                                ✕
                            </button>
                        </div>
                        <NewTransferComponent />
                    </div>
                </div>
            )}
        </div>
    );
}


export default AccountBalanceComponent;
