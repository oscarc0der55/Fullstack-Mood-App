import { useEffect, useState} from "react";
import { GlobalErrorContext } from "./GlobalErrorContext.js";

export const GlobalErrorProvider = ({children}) => {
    const [globalError, setGlobalError] = useState(sessionStorage.getItem('globalErrorMessage') || '');

     useEffect(() => {
        const handleGlobalError = (event) => {
            setGlobalError(event.detail);
        };

        window.addEventListener('global-error', handleGlobalError);

        return () => {
            window.removeEventListener('global-error', handleGlobalError);
        };
    }, []);

    const clearGlobalError = () => {
        setGlobalError('');
        sessionStorage.removeItem('globalErrorMessage');
    }


    return (
        <GlobalErrorContext.Provider value={{ globalError, clearGlobalError, setGlobalError }}>
            {children}
        </GlobalErrorContext.Provider>
    );
}