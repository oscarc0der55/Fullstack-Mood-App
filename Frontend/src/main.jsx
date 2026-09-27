import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';

import App from './App';
import { AuthProvider } from './context/AuthContext';
import { MoodProvider } from './context/MoodContext';
import { WellnessProvider } from './context/WellnessContext';

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <BrowserRouter>
            <AuthProvider>
                <MoodProvider>
                    <WellnessProvider>
                        <App />
                    </WellnessProvider>
                </MoodProvider>
            </AuthProvider>
        </BrowserRouter>
    </React.StrictMode>
);
