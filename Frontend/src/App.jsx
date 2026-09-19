import Login from './pages/Login';
import AdminHome from './pages/admin-pages/AdminHome';
import UserHome from './pages/user-pages/UserHome';
import ProtectedRoute from './Routes/ProtectedRoute';
import {Routes, Route} from 'react-router-dom';
import './App.css'

function App() {

  return (
    <Routes>
      <Route path="/" element={<Login />} />
      {/* Admin Routes */}
      <Route path="/admin-home" element={<ProtectedRoute role="Admin" component={AdminHome} />} />

      {/* User Routes */}
      <Route path="/user-home" element={<ProtectedRoute role="User" component={UserHome} />} />
    </Routes>
  )
}

export default App
