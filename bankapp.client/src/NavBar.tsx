function NavBar() {
    return (
        <nav >
            <ul className="gap-6.5 flex flex-row">
                <li>
                    <a>Start</a>
                </li>
                <li>
                    <button>Płatnośći</button>
                </li>
                <li>
                    <button>Moje usługi</button>
                </li>
                <li>
                    <button>Moje sprawy</button>
                </li>
                <li>
                    <button>Alerty</button>
                </li>
                <li>
                    <button>Moje dane</button>
                </li>
                <li>
                    <button>Wyloguj</button>
                </li>
            </ul>
        </nav>
  );
}

export default NavBar;