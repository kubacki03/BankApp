interface Props {
    onClose: () => void;
}

function SavedPasswordPopUp({ onClose }: Props) {
    return (
        <div className="fixed inset-0 flex items-center justify-center bg-white/10 backdrop-blur-xs">

            <div className="rounded-lg bg-white p-6 shadow-lg">
                <h1 className="mb-4 text-xl font-bold">Czy na pewno zapisałeś/aś hasło?</h1>
                <div className="flex gap-4">
                    <a href="/dashboard" className="0.3s rounded bg-amber-500 px-4 py-2 text-white transition ease-in-out hover:bg-amber-600">
                        Tak, chcę przejść dalej
                    </a>
                    <button
                        onClick={onClose}
                        className="rounded bg-gray-400 px-4 py-2 text-white hover:bg-gray-500"
                    >
                        Nie, chcę zostać
                    </button>
                </div>
            </div>
        </div>
    );
}

export default SavedPasswordPopUp;
