import { useContext } from 'react';
import { AuthContextObject } from './AuthContextObject';

export function useAuth() {
  const context = useContext(AuthContextObject);

  if (!context) {
    throw new Error('useAuth must be used inside AuthProvider');
  }

  return context;
}