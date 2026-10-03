import React from "react";
import NavBar from "./NavBar";

function BankBranchComponent() {
    return (
        <div className="flex flex-col items-center gap-10">
            <NavBar />
            <div className="flex w-[100%] flex-col items-center">
                <h1 className="pb-3 text-2xl font-bold">Oddziały w twojej okolicy</h1>
            <iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d19158.006020026227!2d23.136859193907153!3d53.11466756179512!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sPKO%20Bank%20Polski!5e0!3m2!1spl!2spl!4v1752709453932!5m2!1spl!2spl" width="95%" height="600" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
            </div>
                <div className="flex flex-col items-center">
                <h1 className="font-mono text-3xl font-bold">Główny oddział</h1>
                <h1>Adres: Wiejska 5, Warszawa 21-370</h1>
            <h2>Godziny otwarcia</h2>
            <p>pon-pt: 8:00 - 19:00</p>
                <p>sb: 10:00 - 15:00</p>
            </div>
        </div>
  );
}

export default BankBranchComponent;