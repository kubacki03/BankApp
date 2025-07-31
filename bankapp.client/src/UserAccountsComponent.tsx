/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { useEffect, useState } from "react";

type Account = {
    name: string;
    balance: number;
    accountNumber: string;
};

function UserAccountsComponent() {
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchAccounts = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get("https://localhost:7263/AccountDetails/UserAccounts", {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                setAccounts(response.data);
            } catch (err: any) {
                setError("Wyst�pi� b��d podczas pobierania kont u�ytkownika.");
            }
        };

        fetchAccounts();
    }, []);

    return (
        <div className=" space-y-4 p-6">
            <h2 className="text-xl font-semibold">Lista kont użytkownika</h2>

            {error && <p className="text-red-500">{error}</p>}

            {accounts.length === 0 ? (
                <p>Brak dost�pnych kont.</p>
            ) : (
                <div className="grid grid-cols-1 gap-4">
                    {accounts.map((account, index) => (
                        <div
                            key={index}
                            className=" rounded-xl border border-gray-900 p-4 shadow-md transition hover:bg-amber-100"
                        >
                            <h3 className="text-lg font-bold">{account.name}</h3>
                            <p><strong>Numer konta:</strong> {account.accountNumber}</p>
                            <p><strong>Saldo:</strong> {account.balance.toFixed(2)} PLN</p>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}

export default UserAccountsComponent;
