/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { ReactNode } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import HomeComponent from './HomeComponent';
import RegisterComponent from './RegisterComponent';
import SuccessRegisterComponent from './SuccessRegisterComponent';
import DashboardComponent from './DashboardComponent';
import BankBranchComponent from './BankBranchComponent';
import ApplicationAdComponent from './ApplicationAdComponent';

interface JwtPayload {
    exp: number;
}
interface PrivateRouteProps {
    element: ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }) => {
    const token = localStorage.getItem('token');

    if (!token) return <Navigate to="/" />;

    try {
        const decoded = jwtDecode<JwtPayload>(token);
        const currentTime = Date.now() / 1000;

        if (decoded.exp < currentTime) {
            localStorage.removeItem('token');
            return <Navigate to="/" />;
        }

        return <>{element}</>;

    } catch (error) {

        localStorage.removeItem('token');
        return <Navigate to="/" />;
    }
};

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomeComponent />} />
                <Route path="/register" element={<RegisterComponent />} />
                <Route path="/showLogin" element={<SuccessRegisterComponent />} />
                <Route path="/bankBranch" element={<BankBranchComponent />} />
                <Route path="/mobileApp" element={<ApplicationAdComponent />} />
                <Route path="/dashboard" element={<DashboardComponent/> } />

            </Routes>
        </Router>
    );
};

export default App;