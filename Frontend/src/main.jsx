import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';

import App from './App';
import { AuthProvider } from './context/AuthContext';
import { MoodProvider } from './context/MoodContext';
import { WellnessProvider } from './context/WellnessContext';
import { GlobalErrorProvider } from './context/global-error/GlobalErrorContext';

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <BrowserRouter>
        <GlobalErrorProvider>
            <AuthProvider>
                <MoodProvider>
                    <WellnessProvider>
                        <App />
                    </WellnessProvider>
                </MoodProvider>
            </AuthProvider>
            </GlobalErrorProvider>
        </BrowserRouter>
    </React.StrictMode>
);
