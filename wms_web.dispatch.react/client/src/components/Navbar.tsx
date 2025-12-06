import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Navbar: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  if (!user) return null;

  return (
    <nav className="bg-blue-600 text-white p-4">
      <div className="container mx-auto flex justify-between items-center">
        <div className="flex space-x-4">
          <Link to="/" className="hover:text-blue-200">
            Dashboard
          </Link>
          <Link to="/parcels" className="hover:text-blue-200">
            Parcels
          </Link>
          {user.role === 'branch_manager' && (
            <Link to="/dispatch" className="hover:text-blue-200">
              Dispatch
            </Link>
          )}
        </div>
        <div className="flex items-center space-x-4">
          <span>{user.username}</span>
          <button
            onClick={handleLogout}
            className="bg-blue-700 hover:bg-blue-800 px-4 py-2 rounded"
          >
            Logout
          </button>
        </div>
      </div>
    </nav>
  );
};

export default Navbar; 