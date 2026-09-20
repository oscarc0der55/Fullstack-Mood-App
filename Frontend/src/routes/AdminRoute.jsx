import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { getCurrentUser } from '../connection/AuthConnection';

export default function AdminRoute({ children }) {
  const [status, setStatus] = useState('loading');

  useEffect(() => {
    let active = true;

    async function checkRole() {
      try {
        const user = await getCurrentUser();

        if (!active) return;

        if (user?.role === 'Admin') {
          setStatus('allowed');
        } else {
          setStatus('forbidden');
        }
      } catch {
        if (!active) return;
        setStatus('unauthorized');
      }
    }

    checkRole();

    return () => {
      active = false;
    };
  }, []);

  if (status === 'loading') {
    return <div>Loading...</div>;
  }

  if (status === 'unauthorized') {
    return <Navigate to="/login" replace />;
  }

  if (status === 'forbidden') {
    return <Navigate to="/user-home" replace />;
  }

  return children;
}