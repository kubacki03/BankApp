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
        <div className="mx-30 border-red-900 flex flex-col items-center">


            <form onSubmit={handleSubmit} className="px-4 w-full md:w-1/2">

                <div >
                    <label htmlFor="login" className="pr-4">Login</label>
                    <input name="login" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>
                <div className="my-2">
                    <label htmlFor="password" className="pr-4">Hasło</label>
                    <input name="password" type="password" className=" border-amber-500 px-1 focus rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoje hasło" value={password} onChange={(e) => setPassword(e.target.value)} ></input>
                </div>
                <div>
                    <button type="submit" className="border-amber-700 bg-amber-500 p-1 cursor-pointer rounded-lg border transition duration-300 ease-in-out hover:bg-amber-300">Zaloguj się</button>
                </div>
                {serverError && (
                    <div className="text-red-600 my-2 font-semibold">
                        {serverError}
                    </div>
                )}
            </form>

            
        </div>
    );
}

export default LoginComponent;