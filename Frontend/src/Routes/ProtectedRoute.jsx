import {useState, useEffect} from 'react';
import {checkAuth} from '../connection/AuthConnection';
import {Navigate} from 'react-router-dom';

export default function ProtectedRoute({children}) {
    const [isAuthenticated, setIsAuthenticated] = useState(null);
    useEffect(() => {
        
        async function verifyAuth(){
            const auth = await checkAuth();
            setIsAuthenticated(auth);
        }

        verifyAuth();
    }, []);

    if (isAuthenticated === null) {
        return <div>Loading...</div>; // or a spinner
    }

    if (isAuthenticated === false){
        return <Navigate to="/login" />;
    }

    return children;
}