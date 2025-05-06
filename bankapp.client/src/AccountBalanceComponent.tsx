function AccountBalanceComponent() {

    const kindOfAccount = "Prywatne";
    const iban = "123";
    const balance = 123;
  return (
      <>
          <div>
          <p>BPL Bank Polski</p>
          <h1>BPL KONTO {kindOfAccount}</h1>
          <p>{iban}</p>
          </div>

          <div>
              <h1>Dostêpne œrodki</h1>
              <h2>{balance} PLN</h2>
              <div className="flex flex-row">
                  <a>Historia</a>
                  <a>Przelew</a>
              </div>

          </div>

      </>
  );
}

export default AccountBalanceComponent;