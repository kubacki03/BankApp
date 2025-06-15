import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";

type LoginFormInputs = {
    email: string;
    password: string;
};

const validationSchema = Yup.object({
    email: Yup.string()
        
        .required("Email jest wymagany"),
    password: Yup.string()
        .min(6, "Hasło musi mieć co najmniej 6 znaków")
        .required("Hasło jest wymagane"),
});

function LoginComponent() {
    const [serverError, setServerError] = useState<string | undefined>(undefined);
    const navigate = useNavigate(); 
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginFormInputs>({
        resolver: yupResolver(validationSchema),
    });

    const onSubmit = async (data: LoginFormInputs) => {
        try {
            console.log(data);
            const response = await axios.post('https://localhost:7263/Auth/login', data);
            console.log("Token "+response.data);
            localStorage.setItem("token", response.data);
            navigate("/dashboard"); 
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "Błąd logowania");
            } else {
                setServerError("Nie udało się połączyć z serwerem");
            }
        }
    };

    return (
        <div className=" flex flex-col items-center border-red-900">
            <form onSubmit={handleSubmit(onSubmit)} className="w-full px-4 md:w-1/2">
                <div>
                    <label htmlFor="email" className="pr-4">Email</label>
                    <input
                        type="text"
                        {...register("email")}
                        className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz swój email"
                    />
                    {errors.email && <p className="text-sm text-red-600">{errors.email.message}</p>}
                </div>

                <div className="my-2">
                    <label htmlFor="password" className="pr-4">Hasło</label>
                    <input
                        type="password"
                        {...register("password")}
                        className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz swoje hasło"
                    />
                    {errors.password && <p className="text-sm text-red-600">{errors.password.message}</p>}
                </div>

                <div>
                    <button
                        type="submit"
                        className="cursor-pointer rounded-lg border border-amber-700 bg-amber-500 p-1 transition duration-300 ease-in-out hover:bg-amber-300"
                    >
                        Zaloguj się
                    </button>
                </div>

                {serverError && (
                    <div className="my-2 font-semibold text-red-600">
                        {serverError}
                    </div>
                )}
            </form>
        </div>
    );
}

export default LoginComponent;