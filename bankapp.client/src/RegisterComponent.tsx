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
                setServerError(error.response?.data?.message || "B³¹d logowania");
            } else {
                setServerError("Nie uda³o siê po³¹czyæ z serwerem");
            }
        }

    };

    const handleRegisterCompanyUser = async (data: { Email: string, Password: string, Name: string, LastName: string, BirthDate: Date, Pesel: string, companyName:string, Regon: string, Nip: string }) => {
        try {
            const response = await axios.post("url", data);

            localStorage.setItem("token", response.data.token);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "B³¹d logowania");
            } else {
                setServerError("Nie uda³o siê po³¹czyæ z serwerem");
            }
        }

    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault(); // zapobiega prze³adowaniu strony

        if (isPrivateUser) {
            //handleRegisterPrivateUser
        } else {
            //handleRegisterCompanyUser
        }
    };

    return (
       <p>Hello world</p>
    );
}

export default RegisterComponent;