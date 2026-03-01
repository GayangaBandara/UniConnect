'use client';

import Link from 'next/link';
import { AuthService } from '@/services/auth.service';

export default function Home() {
  const isAuthenticated = AuthService.isAuthenticated();

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100">
      {/* Hero Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20 text-center">
        <h1 className="text-5xl md:text-6xl font-bold text-gray-900 mb-6">
          Welcome to <span className="text-blue-600">UniConnect</span>
        </h1>
        <p className="text-xl md:text-2xl text-gray-700 mb-8 max-w-2xl mx-auto">
          Connect with mentors and get academic guidance from experienced professionals
        </p>

        <div className="flex gap-4 justify-center flex-col sm:flex-row mb-20">
          {isAuthenticated ? (
            <>
              <Link
                href="/dashboard"
                className="bg-blue-600 text-white px-8 py-3 rounded-lg font-semibold hover:bg-blue-700 transition text-lg"
              >
                Go to Dashboard
              </Link>
              <Link
                href="/guidance-requests"
                className="bg-white text-blue-600 px-8 py-3 rounded-lg font-semibold border-2 border-blue-600 hover:bg-blue-50 transition text-lg"
              >
                Browse Requests
              </Link>
            </>
          ) : (
            <>
              <Link
                href="/login"
                className="bg-blue-600 text-white px-8 py-3 rounded-lg font-semibold hover:bg-blue-700 transition text-lg"
              >
                Login
              </Link>
              <Link
                href="/register"
                className="bg-white text-blue-600 px-8 py-3 rounded-lg font-semibold border-2 border-blue-600 hover:bg-blue-50 transition text-lg"
              >
                Create Account
              </Link>
            </>
          )}
        </div>

        {/* Features Section */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mt-20">
          <div className="bg-white rounded-lg shadow-lg p-8">
            <div className="text-4xl mb-4">🎓</div>
            <h3 className="text-2xl font-bold text-gray-900 mb-3">Learn from Experts</h3>
            <p className="text-gray-600">
              Get guidance from experienced mentors in your field of study
            </p>
          </div>

          <div className="bg-white rounded-lg shadow-lg p-8">
            <div className="text-4xl mb-4">🤝</div>
            <h3 className="text-2xl font-bold text-gray-900 mb-3">Connect & Collaborate</h3>
            <p className="text-gray-600">
              Build meaningful relationships with peers and mentors in your academic journey
            </p>
          </div>

          <div className="bg-white rounded-lg shadow-lg p-8">
            <div className="text-4xl mb-4">📈</div>
            <h3 className="text-2xl font-bold text-gray-900 mb-3">Grow & Improve</h3>
            <p className="text-gray-600">
              Enhance your skills and knowledge through personalized guidance and feedback
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
