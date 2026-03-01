'use client';

import ProtectedRoute from '@/components/ProtectedRoute';
import Link from 'next/link';

export default function GuidanceRequestsPage() {
  return (
    <ProtectedRoute>
      <div className="container mx-auto px-4 py-8">
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-3xl font-bold text-gray-800">Guidance Requests</h1>
          <Link
            href="/guidance-requests/create"
            className="bg-blue-600 text-white px-6 py-2 rounded-lg hover:bg-blue-700 transition"
          >
            Create Request
          </Link>
        </div>

        <div className="bg-white rounded-lg shadow p-8 text-center">
          <p className="text-gray-600 text-lg">No requests yet.</p>
          <p className="text-gray-500 mt-2">
            Create your first guidance request to get started.
          </p>
        </div>
      </div>
    </ProtectedRoute>
  );
}
