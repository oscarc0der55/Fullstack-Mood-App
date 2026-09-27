import Login from "./pages/Login";
import AdminHome from "./pages/admin-pages/AdminHome";
import UserHome from "./pages/user-pages/UserHome";
import UserProfile from "./pages/user-pages/UserProfile";
import UserMoodPage from "./pages/user-pages/UserMoodPage";
import UserWellnessPage from "./pages/user-pages/UserWellnessPage";
import UserLayout from "./pages/user-pages/UserLayout";
import ProtectedRoute from "./Routes/ProtectedRoute";
import AdminRoute from "./routes/AdminRoute";
import { Routes, Route } from "react-router-dom";
import "./App.css";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      {/* Admin Routes */}
      <Route
        path="/admin-home"
        element={
          <AdminRoute>
            <AdminHome />
          </AdminRoute>
        }
      />

      {/* User Routes */}
      <Route
        element={
          <ProtectedRoute role="User">
            <UserLayout />
          </ProtectedRoute>
        }
      >
        <Route path="/user-home" element={<UserHome />} />
        <Route path="/user-profile" element={<UserProfile />} />
        <Route path="/user-mood" element={<UserMoodPage />} />
        <Route path="/user-wellness" element={<UserWellnessPage />} />
      </Route>
    </Routes>
  );
}

export default App;
