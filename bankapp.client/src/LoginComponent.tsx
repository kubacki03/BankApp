import axios from "axios";
import { useState } from "react";

/* eslint-disable @typescript-eslint/no-unused-vars */
function LoginComponent() {

    const [serverError, setServerError] = useState<string | undefined>(undefined);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");


    const handleLogin = async (data: { email: string, password: string }) => {
        try {
            const response = await axios.post("url", data);

            localStorage.setItem("token", response.data.token);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "Błąd logowania");
            } else {
                setServerError("Nie udało się połączyć z serwerem");
            }
        }

    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault(); // zapobiega przeładowaniu strony
        handleLogin({ email, password });
    };

    return (
        <div className="border-amber-500 mx-50 mt-10 p-4 gap-50 flex rounded-lg border">

            <form onSubmit={handleSubmit} >
                <div >
                    <label htmlFor="login" className="pr-4">Login</label>
                    <input name="login" type="text" className="border-amber-500 px-1 rounded-lg border" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>
                <div className="my-2">
                    <label htmlFor="password" className="pr-4">Hasło</label>
                    <input name="password" type="password" className=" border-amber-500 px-1 rounded-lg border" placeholder=" Wpisz swoje hasło" value={password} onChange={(e) => setPassword(e.target.value)} ></input>
                </div>
                <div>
                    <button type="submit" className="border-amber-700 bg-amber-500 p-1 cursor-pointer rounded-lg border">Zaloguj się</button>
                </div>
                {serverError && (
                    <div className="text-red-600 my-2 font-semibold">
                        {serverError}
                    </div>
                )}
            </form>

            <div className="border-gray-300 pl-10 ml-50 border-l">
            <h1 className="my-4">Jeśli nie masz jeszcze konta?</h1>
                <a className="bg-red-400 p-2 cursor-pointer rounded-lg">Załóż konto</a>
            </div>
        </div>
    );
}

export default LoginComponent;