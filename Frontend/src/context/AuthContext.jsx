import { useCallback, useEffect, useState } from 'react';
import { AuthContextObject } from './AuthContextObject';
import { getCurrentUser, logoutFromCookie } from '../connection/AuthConnection';

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [status, setStatus] = useState('loading');

  const refreshUser = useCallback(async () => {
    setStatus('loading');

    try {
      const currentUser = await getCurrentUser();
      setUser(currentUser);
      setStatus('authenticated');
      return currentUser;
    } catch (error) {
      if (error.response?.status === 401) {
        setUser(null);
        setStatus('unauthenticated');
        return null;
      }

      setStatus('error');
      throw error;
    }
  }, []);

  useEffect(() => {
    refreshUser().catch(() => {});
  }, [refreshUser]);

  async function logout() {
    await logoutFromCookie();
    setUser(null);
    setStatus('unauthenticated');
  }

  return (
    <AuthContextObject.Provider
      value={{ user, status, refreshUser, logout }}
    >
      {children}
    </AuthContextObject.Provider>
  );
}
