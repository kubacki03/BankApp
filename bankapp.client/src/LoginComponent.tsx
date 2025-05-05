import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";

type LoginFormInputs = {
    email: string;
    password: string;
};

const validationSchema = Yup.object({
    email: Yup.string()
        .email("Nieprawidłowy adres email")
        .required("Email jest wymagany"),
    password: Yup.string()
        .min(6, "Hasło musi mieć co najmniej 6 znaków")
        .required("Hasło jest wymagane"),
});

function LoginComponent() {
    const [serverError, setServerError] = useState<string | undefined>(undefined);

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginFormInputs>({
        resolver: yupResolver(validationSchema),
    });

    const onSubmit = async (data: LoginFormInputs) => {
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

    return (
        <div className="mx-30 border-red-900 flex flex-col items-center">
            <form onSubmit={handleSubmit(onSubmit)} className="px-4 w-full md:w-1/2">
                <div>
                    <label htmlFor="email" className="pr-4">Email</label>
                    <input
                        type="text"
                        {...register("email")}
                        className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz swój email"
                    />
                    {errors.email && <p className="text-red-600 text-sm">{errors.email.message}</p>}
                </div>

                <div className="my-2">
                    <label htmlFor="password" className="pr-4">Hasło</label>
                    <input
                        type="password"
                        {...register("password")}
                        className="border-amber-500 px-1 rounded-lg border focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz swoje hasło"
                    />
                    {errors.password && <p className="text-red-600 text-sm">{errors.password.message}</p>}
                </div>

                <div>
                    <button
                        type="submit"
                        className="border-amber-700 bg-amber-500 p-1 cursor-pointer rounded-lg border transition duration-300 ease-in-out hover:bg-amber-300"
                    >
                        Zaloguj się
                    </button>
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
