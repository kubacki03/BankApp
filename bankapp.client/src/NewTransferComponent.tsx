/* eslint-disable @typescript-eslint/no-unused-vars */
import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";

type TransferFormInputs = {
    Amount: number;
    RecipientAccountNumber: string;
    Title: string; 
};

const validationSchema = Yup.object({
    Amount: Yup.number().required("Kwota jest wymagana"),
    RecipientAccountNumber: Yup.string()
        .min(24, "IBAN musi mieæ co najmniej 24 znaki")
        .required("IBAN jest wymagany"),
    Title: Yup.string().required("Tytul jest konieczny"),
});

function NewTransferComponent() {
    const [serverError, setServerError] = useState<string | undefined>(undefined);
    const [isSubmitting, setIsSubmitting] = useState(false); // Loading state
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TransferFormInputs>({
        resolver: yupResolver(validationSchema),
    });

    const onSubmit = async (data: TransferFormInputs) => {
        setIsSubmitting(true);
        try {
            const token = localStorage.getItem("token");
            await axios.post(
                'https://localhost:7263/Transfer/MakeTransfer',
                data,
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            alert("Przelew wykonany");
            navigate("/dashboard");
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setServerError(error.response?.data?.message || "Wyst¹pi³ b³¹d");
            } else {
                setServerError("Nie uda³o siê po³¹czyæ z serwerem");
            }
        } finally {
            setIsSubmitting(false);
        }
    };


    return (
        <div className="mx-30 flex flex-col items-center border-red-900">
            <form onSubmit={handleSubmit(onSubmit)} className="w-full px-4 md:w-1/2">
                <div>
                    <label htmlFor="Title" className="pr-4">Tytul</label>
                    <input
                        type="text"
                        {...register("Title")}
                        className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz tytul"
                    />
                    {errors.Title && (
                        <div className="my-1 text-sm text-red-600">{errors.Title.message}</div>
                    )}
                </div>

                <div>
                    <label htmlFor="Amount" className="pr-4">Kwota</label>
                    <input
                        type="number"
                        {...register("Amount")}
                        className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz kwotê"
                    />
                    {errors.Amount && (
                        <div className="my-1 text-sm text-red-600">{errors.Amount.message}</div>
                    )}
                </div>

                <div className="my-2">
                    <label htmlFor="RecipientAccountNumber" className="pr-4">IBAN odbiorcy</label>
                    <input
                        type="text"
                        {...register("RecipientAccountNumber")}
                        className="rounded-lg border border-amber-500 px-1 focus:border-yellow-300 focus:outline-none"
                        placeholder="Wpisz numer kontra odbiorcy"
                    />
                    {errors.RecipientAccountNumber && (
                        <div className="my-1 text-sm text-red-600">
                            {errors.RecipientAccountNumber.message}
                        </div>
                    )}
                </div>

                <div>
                    <button
                        type="submit"
                        disabled={isSubmitting}
                        className="cursor-pointer rounded-lg border border-amber-700 bg-amber-500 p-1 transition duration-300 ease-in-out hover:bg-amber-300 disabled:opacity-50"
                    >
                        {isSubmitting ? "Wysy³anie..." : "Wyslij przelew"}
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

export default NewTransferComponent;