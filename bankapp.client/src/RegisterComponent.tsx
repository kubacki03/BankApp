/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { useState } from "react";
import { useForm, SubmitHandler, FieldErrors } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useNavigate } from "react-router-dom";

interface PrivateUserForm {
    email: string;
    password: string;
    password2: string;
    name: string;
    lastName: string;
    birthDate: string;
    pesel: string;
}

interface CompanyUserForm extends PrivateUserForm {
    companyName: string;
    nip: string;
    regon: string;
}

const privateSchema = yup.object({
    email: yup.string().email("Niepoprawny Email").required("Email jest wymagany"),
    password: yup.string().min(6, "Minimum 6 znaków").required("Hasło jest wymagane"),
    password2: yup
        .string()
        .oneOf([yup.ref("password")], "Hasła muszą być takie same")
        .required("Powtórz hasło"),
    name: yup.string().required("Imię jest wymagane"),
    lastName: yup.string().required("Nazwisko jest wymagane"),
    birthDate: yup.string().required("Data urodzenia jest wymagana"),
    pesel: yup.string().length(11, "PESEL musi mieć 11 cyfr").required("PESEL jest wymagany"),
});

const companySchema = privateSchema.shape({
    companyName: yup.string().required("Nazwa firmy jest wymagana"),
    nip: yup.string().length(10, "NIP musi mieć 10 cyfr").required("NIP jest wymagany"),
    regon: yup.string().length(9, "REGON musi mieć 9 cyfr").required("REGON jest wymagany"),
});

function RegisterComponent() {
    const navigate = useNavigate(); 
    const [isPrivateUser, setIsPrivateUser] = useState(true);
    const [serverError, setServerError] = useState<string | undefined>(undefined);

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<PrivateUserForm | CompanyUserForm>({
        resolver: yupResolver(isPrivateUser ? privateSchema : companySchema),
    });

    const onSubmit: SubmitHandler<PrivateUserForm | CompanyUserForm> = async (data) => {
        try {
            const response = await axios.post('https://localhost:7263/register', data);

            if (response.status === 200) {
                
                console.log("Rejestracja udana:", response.data);
                navigate("/showLogin"); 
            } 

        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "Błąd rejestrowania");
            } else {
                setServerError("Nie udało się połączyć z serwerem");
            }
        }
    };


    return (
        <div className="mx-30 flex flex-col items-center">
            <button
                className={`${isPrivateUser ? "border border-amber-500" : "bg-amber-500"
                    } rounded-lg p-3 mb-2`}
                onClick={() => setIsPrivateUser((prev) => !prev)}
                type="button"
            >
                Tworzenie konta dla{" "}
                <strong>{isPrivateUser ? "Użytkownika Prywatnego" : "Użytkownika Firmowego"}</strong>
            </button>

            <form onSubmit={handleSubmit(onSubmit)} className="max-w-md space-y-4">

                <InputField label="Email" type="email" name="email" register={register} error={errors.email?.message} />

                <InputField label="Hasło" type="password" name="password" register={register} error={errors.password?.message} />
                <InputField label="Powtórz Hasło" type="password" name="password2" register={register} error={errors.password2?.message} />
                <InputField label="Imię" name="name" register={register} error={errors.name?.message} />
                <InputField label="Nazwisko" name="lastName" register={register} error={errors.lastName?.message} />
                <InputField label="Data urodzenia" name="birthDate" type="date" register={register} error={errors.birthDate?.message} />
                <InputField label="PESEL" name="pesel" register={register} error={errors.pesel?.message} />

                {!isPrivateUser && (
                    <>
                        <InputField
                            label="Nazwa firmy"
                            name="companyName"
                            register={register}
                            error={(errors as FieldErrors<CompanyUserForm>).companyName?.message}
                        />
                        <InputField
                            label="NIP"
                            name="nip"
                            register={register}
                            error={(errors as FieldErrors<CompanyUserForm>).nip?.message}
                        />
                        <InputField
                            label="REGON"
                            name="regon"
                            register={register}
                            error={(errors as FieldErrors<CompanyUserForm>).regon?.message}
                        />
                    </>
                )}


                <button
                    type="submit"
                    className="cursor-pointer rounded-lg border border-amber-700 bg-amber-500 p-1 transition duration-300 ease-in-out hover:bg-amber-300"
                >
                    Załóż konto
                </button>

                {serverError && <div className="my-2 font-semibold text-red-600">{serverError}</div>}
            </form>
        </div>
    );
}

function InputField({
    label,
    name,
    type = "text",
    register,
    error,
}: {
    label: string;
    name: string;
    type?: string;
    register: any;
    error?: string;
}) {
    return (
        <div className="grid grid-cols-[100px_1fr] items-center gap-2">
            <label htmlFor={name} className="pr-4">
                {label}
            </label>
            <div className="flex flex-col">
                <input
                    {...register(name)}
                    type={type}
                    id={name}
                    name={name}
                    className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                />
                {error && <span className="text-sm text-red-600">{error}</span>}
            </div>
        </div>
    );
}

export default RegisterComponent;
