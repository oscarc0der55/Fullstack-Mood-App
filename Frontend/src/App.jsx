import Login from './pages/Login';
import AdminHome from './pages/admin-pages/AdminHome';
import UserHome from './pages/user-pages/UserHome';
import UserProfile from './pages/user-pages/UserProfile';
import ProtectedRoute from './Routes/ProtectedRoute';
import AdminRoute from './routes/AdminRoute';
import {Routes, Route} from 'react-router-dom';
import './App.css'

function App() {

  return (
    <Routes>
      <Route path="/" element={<Login />} />
      {/* Admin Routes */}
      <Route path="/admin-home" element={<AdminRoute><AdminHome /></AdminRoute>} />

      {/* User Routes */}
      <Route path="/user-home" element={<ProtectedRoute role="User" component={UserHome} />} />
      <Route path="/user-profile" element={<ProtectedRoute role="User" component={UserProfile} />} />
    </Routes>
  )
}

export default App
