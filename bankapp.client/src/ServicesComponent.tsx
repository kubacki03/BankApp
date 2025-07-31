function ServicesComponent() {
    return (
        <div className=" grid w-80 grid-cols-3 gap-2">
            <a href="/mobileApp" className="flex h-22 w-22 items-center rounded-full bg-amber-500 text-center text-white transition hover:bg-amber-600">Aplikacja BPL</a>
            <a href="#" className="flex h-22 w-22 items-center rounded-full bg-amber-500 text-center text-white transition hover:bg-amber-600">Ochrona danych</a>
            <a href="#" className="flex h-22 w-22 items-center justify-center rounded-full bg-amber-500 text-center text-white transition hover:bg-amber-600">Kantor</a>
            <a href="/bankBranch" className="flex h-22 w-22 items-center rounded-full bg-amber-500 text-center text-white transition hover:bg-amber-600">Nasze Oddzialy</a>
            <a href="#" className="flex h-22 w-22 items-center rounded-full bg-amber-500 text-center text-white transition hover:bg-amber-600">Zaplac pzniej</a>
      </div> 
  );
}

export default ServicesComponent;