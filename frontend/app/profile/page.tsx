'use client';

import ProtectedRoute from '@/components/ProtectedRoute';
import { useAuth } from '@/contexts/AuthContext';

export default function ProfilePage() {
  const { user } = useAuth();

  return (
    <ProtectedRoute>
      <div className="container mx-auto px-4 py-8">
        <div className="max-w-2xl">
          <h1 className="text-3xl font-bold text-gray-800 mb-8">My Profile</h1>

          <div className="bg-white rounded-lg shadow p-8">
            <div className="mb-6">
              <h2 className="text-xl font-semibold text-gray-800 mb-4">Personal Information</h2>
              <div className="space-y-4">
                <div>
                  <label className="text-sm font-medium text-gray-600">First Name</label>
                  <p className="text-gray-900 text-lg">{user?.firstName}</p>
                </div>
                <div>
                  <label className="text-sm font-medium text-gray-600">Last Name</label>
                  <p className="text-gray-900 text-lg">{user?.lastName}</p>
                </div>
                <div>
                  <label className="text-sm font-medium text-gray-600">Email</label>
                  <p className="text-gray-900 text-lg">{user?.email}</p>
                </div>
                <div>
                  <label className="text-sm font-medium text-gray-600">Role</label>
                  <p className="text-gray-900 text-lg">{user?.role}</p>
                </div>
                <div>
                  <label className="text-sm font-medium text-gray-600">Member Since</label>
                  <p className="text-gray-900 text-lg">
                    {user?.createdAt ? new Date(user.createdAt).toLocaleDateString() : 'N/A'}
                  </p>
                </div>
              </div>
            </div>

            <div className="border-t pt-6">
              <p className="text-gray-600 text-sm">
                More profile editing features coming soon...
              </p>
            </div>
          </div>
        </div>
      </div>
    </ProtectedRoute>
  );
}
