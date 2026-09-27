import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/UseAuth';

export default function Logout() {
  const { logout } = useAuth();
  const navigate = useNavigate();

  async function handleLogout() {
    await logout();
    navigate('/', { replace: true });
  }

  return (
    <button type="button" onClick={handleLogout}>
      Log out
    </button>
  );
}