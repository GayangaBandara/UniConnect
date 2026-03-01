'use client';

import ProtectedRoute from '@/components/ProtectedRoute';
import { useAuth } from '@/contexts/AuthContext';

export default function DashboardPage() {
  const { user } = useAuth();

  return (
    <ProtectedRoute>
      <div className="container mx-auto px-4 py-8">
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-gray-800 mb-2">
            Welcome, {user?.firstName}!
          </h1>
          <p className="text-gray-600">Here's your dashboard overview</p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {/* Stats Cards */}
          <div className="bg-white rounded-lg shadow p-6">
            <h3 className="text-gray-500 text-sm font-semibold uppercase tracking-wide">
              Your Requests
            </h3>
            <p className="mt-2 text-3xl font-extrabold text-gray-900">0</p>
            <p className="mt-2 text-sm text-gray-600">Active guidance requests</p>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <h3 className="text-gray-500 text-sm font-semibold uppercase tracking-wide">
              Profile Status
            </h3>
            <p className="mt-2 text-3xl font-extrabold text-blue-600">Complete</p>
            <p className="mt-2 text-sm text-gray-600">Your profile information</p>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <h3 className="text-gray-500 text-sm font-semibold uppercase tracking-wide">
              User Role
            </h3>
            <p className="mt-2 text-3xl font-extrabold text-gray-900">{user?.role}</p>
            <p className="mt-2 text-sm text-gray-600">Your account type</p>
          </div>
        </div>

        <div className="mt-8 bg-white rounded-lg shadow p-6">
          <h2 className="text-2xl font-bold text-gray-800 mb-4">Quick Start</h2>
          <ul className="space-y-3 text-gray-700">
            <li className="flex items-center">
              <span className="text-blue-600 mr-3">✓</span>
              <a href="/profile" className="hover:text-blue-600 cursor-pointer">
                Complete your profile
              </a>
            </li>
            <li className="flex items-center">
              <span className="text-blue-600 mr-3">✓</span>
              <a href="/guidance-requests/create" className="hover:text-blue-600 cursor-pointer">
                Create your first guidance request
              </a>
            </li>
            <li className="flex items-center">
              <span className="text-blue-600 mr-3">✓</span>
              <a href="/guidance-requests" className="hover:text-blue-600 cursor-pointer">
                Browse available requests
              </a>
            </li>
          </ul>
        </div>
      </div>
    </ProtectedRoute>
  );
}
