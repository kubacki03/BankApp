/* eslint-disable @typescript-eslint/no-unused-vars */
import axios from "axios";
import { useState } from "react";

function RegisterComponent() {
    const [password, setPassword] = useState<string>();
    const [email, setEmail] = useState<string>();
    const [name, setName] = useState<string>();
    const [lastName, setLastName] = useState<string>();
    const [birthDay, setBirthDay] = useState<Date>();
    const [pesel, setPesel] = useState<string>();
    const [serverError, setServerError] = useState<string | undefined>(undefined);


    const [companyName, setCompanyName] = useState<string>();
    const [regon, setRegon] = useState<string>();
    const [nip, setNip] = useState<string>();

    const [isPrivateUser, setIsPrivateUser] = useState<boolean>(true);

    const handleRegisterPrivateUser = async (data: { Email: string, Password: string, Name: string, LastName:string, BirthDate:Date, Pesel:string  }) => {
        try {
            const response = await axios.post("url", data);

            localStorage.setItem("token", response.data.token);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "B��d logowania");
            } else {
                setServerError("Nie uda�o si� po��czy� z serwerem");
            }
        }

    };

    const handleRegisterCompanyUser = async (data: { Email: string, Password: string, Name: string, LastName: string, BirthDate: Date, Pesel: string, companyName:string, Regon: string, Nip: string }) => {
        try {
            const response = await axios.post("url", data);

            localStorage.setItem("token", response.data.token);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "B��d logowania");
            } else {
                setServerError("Nie uda�o si� po��czy� z serwerem");
            }
        }

    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault(); // zapobiega prze�adowaniu strony

        if (isPrivateUser) {
            //handleRegisterPrivateUser
        } else {
            //handleRegisterCompanyUser
        }
    };


    const setKindOfUser = () => {
      
        
        if (isPrivateUser) {
            setIsPrivateUser(false)
           
        } else {
            setIsPrivateUser(true);
           
        }
    };

    return (
        <div className="mx-30 border-red-900 flex flex-col items-center">

            <button className={ isPrivateUser ? 'bg-sky-500 rounded-lg p-3 mx-50' : 'bg-amber-500 rounded-lg p-3 mx-50' } onClick={setKindOfUser}>Zmien rodzaj konta</button>
            <p>Tworzenie konta dla {isPrivateUser && (<strong>Użytkownika Prywatnego</strong>)} {!isPrivateUser && (<strong>Użytkownika Firmowego</strong>)} </p>


            <form onSubmit={handleSubmit} className="space-y-4 max-w-md">

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
            <label htmlFor="email" className="pr-4">Email</label>
            <input name="email" type="email" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
            </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
            <label htmlFor="password" className="pr-4">Hasło</label>
            <input name="password" type="password" className=" border-amber-500 px-1 focus rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoje hasło" value={password} onChange={(e) => setPassword(e.target.value)} ></input>
            </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
            <label htmlFor="password2" className="pr-4">Hasło</label>
            <input name="password2" type="password2" className=" border-amber-500 px-1 focus rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoje hasło" value={password} onChange={(e) => setPassword(e.target.value)} ></input>
            </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center" >
                    <label htmlFor="name" className="pr-4">Imiona</label>
                    <input name="name" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
                    <label htmlFor="lastName" className="pr-4">Nazwisko</label>
                    <input name="lastName" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
                    <label htmlFor="birthdat" className="pr-4">Data urodzenia</label>
                    <input name="birthday" type="date" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
                    <label htmlFor="pesel" className="pr-4">Pesel</label>
                    <input name="pesel" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                {!isPrivateUser && (
                    <div>
                <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
                    <label htmlFor="companyName" className="pr-4">Nazwa firmy</label>
                    <input name="companyName" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                     <div className="grid-cols-[100px_1fr] gap-2 my-3 grid items-center">
                    <label htmlFor="Nip" className="pr-4">Nip</label>
                    <input name="Nip" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                </div>

                    <div className="grid-cols-[100px_1fr] gap-2 grid items-center">
                    <label htmlFor="Regon" className="pr-4">Regon</label>
                    <input name="Regon" type="text" className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none" placeholder=" Wpisz swoj login" value={email} onChange={(e) => setEmail(e.target.value)} ></input>
                        </div>
                    </div>
                )}
                


            <div>
                <button type="submit" className="border-amber-700 bg-amber-500 p-1 cursor-pointer rounded-lg border transition duration-300 ease-in-out hover:bg-amber-300">Załóż konto</button>
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

export default RegisterComponent;