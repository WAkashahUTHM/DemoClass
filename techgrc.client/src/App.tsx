import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import ContractManagementPage from "./pages/ContractManagementPage"; 
import ContractDashboardPage from "./pages/ContractDashboardPage";
import GptDemoPage from "./pages/GptDemoPage";
import ContractFormPage from "./pages/ContractFormPage";

const theme = createTheme();

const App: React.FC = () => {
    const router = createBrowserRouter([
        {
            path: "/",
            element: (
                <ContractDashboardPage />
            ),
        },
        {
            path: "/contract",
            element: (
                <ContractManagementPage />
            ),
        },
        {
            path: "/contract/:id",
            element: (
                <ContractFormPage />
            ),
        },
        {
            path: "/gptdemo",
            element: (
                <GptDemoPage />
            ),
        }
    ]);
    
    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
                <RouterProvider router={router} />
            <ToastContainer aria-label="Notification container" />
        </ThemeProvider>
    );
    // return (
    //     <ThemeProvider theme={theme}>
    //         <CssBaseline />
    //         <ContractManagementPage />
    //     </ThemeProvider>
    // );
};

export default App;
