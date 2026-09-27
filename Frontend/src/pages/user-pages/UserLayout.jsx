import UserTabs from './user-tab/UserTabs';
import { Outlet } from 'react-router-dom';

export default function UserLayout() {
  return (
    <>
      <UserTabs />
      <Outlet />
    </>
  );
}
