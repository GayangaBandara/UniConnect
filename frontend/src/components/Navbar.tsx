'use client';

import Link from 'next/link';
import { AuthService } from '@/services/auth.service';
import { useState } from 'react';

export default function Navbar() {
  const isAuthenticated = AuthService.isAuthenticated();
  const [isOpen, setIsOpen] = useState(false);

  if (!isAuthenticated) return null;

  return (
    <nav className="bg-gray-100 border-b border-gray-200">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-12">
          <ul className="hidden md:flex space-x-6">
            <li>
              <Link href="/dashboard" className="text-sm text-gray-700 hover:text-blue-600 transition">
                Dashboard
              </Link>
            </li>
            <li>
              <Link href="/guidance-requests" className="text-sm text-gray-700 hover:text-blue-600 transition">
                View Requests
              </Link>
            </li>
            <li>
              <Link href="/guidance-requests/create" className="text-sm text-gray-700 hover:text-blue-600 transition">
                Create Request
              </Link>
            </li>
            <li>
              <Link href="/users" className="text-sm text-gray-700 hover:text-blue-600 transition">
                Users
              </Link>
            </li>
          </ul>

          <button
            className="md:hidden"
            onClick={() => setIsOpen(!isOpen)}
          >
            ☰
          </button>
        </div>

        {isOpen && (
          <ul className="md:hidden pb-4 space-y-2">
            <li>
              <Link href="/dashboard" className="block text-sm text-gray-700 hover:text-blue-600">
                Dashboard
              </Link>
            </li>
            <li>
              <Link href="/guidance-requests" className="block text-sm text-gray-700 hover:text-blue-600">
                View Requests
              </Link>
            </li>
            <li>
              <Link href="/guidance-requests/create" className="block text-sm text-gray-700 hover:text-blue-600">
                Create Request
              </Link>
            </li>
            <li>
              <Link href="/users" className="block text-sm text-gray-700 hover:text-blue-600">
                Users
              </Link>
            </li>
          </ul>
        )}
      </div>
    </nav>
  );
}
