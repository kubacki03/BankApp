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
        <div>
            <form onSubmit={handleSubmit} >
                <div>
                    <label htmlFor="login">Login</label>
                    <input name="login" type="text" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>
                <div>
                    <label htmlFor="password">Hasło</label>
                    <input name="password" type="password" value={password} onChange={(e) => setPassword(e.target.value)}></input>
                </div>
                <div>
                    <button type="submit">Zaloguj się</button>
                </div>
            </form>

        </div>
    );
}

export default LoginComponent;