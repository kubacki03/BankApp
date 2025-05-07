import AccountBalanceComponent from "./AccountBalanceComponent";
import TransfersHistoryComponent from "./TransfersHistoryComponent";

function DashboardComponent() {
  return (
      <>
          <AccountBalanceComponent />
          <TransfersHistoryComponent/>
      </>
  );
}

export default DashboardComponent;