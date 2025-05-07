function AccountBalanceComponent() {

    const kindOfAccount = "Prywatne";
    const iban = "13 20001 10001 00001234567 89";
    const balance = 123.25;
    return (
        <div className="gap-0.5 grid w-[30%] grid-cols-2 grid-rows-2 rounded-md border px-[1%] pb-[1%] shadow-2xl">
            <div className="col-start-1 row-start-1">
                <p>BPL Bank Polski</p>
                <h1 className="block text-xl">BPL KONTO {kindOfAccount}</h1>
          <p>{iban}</p>
          </div>

            <div className="rig col-2 row-2 flex flex-col items-end">
                <h1 className="text-gray-400">Dostępne środki</h1>
                <h2 className="text-xl font-bold">{balance} PLN</h2>
                <div className="flex flex-row items-center justify-center gap-[3%]">
                    <a href="#">Historia</a>
                    <a href="#" className="time rounded-lg bg-amber-300 p-1 transition ease-linear hover:bg-amber-500">Przelew</a>
                </div>


          </div>


           
      </div>
  );
}

export default AccountBalanceComponent;