'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { AuthService } from '@/services/auth.service';

export default function Sidebar() {
  const pathname = usePathname();
  const isAuthenticated = AuthService.isAuthenticated();

  if (!isAuthenticated) return null;

  const isActive = (path: string) => pathname === path;

  const menuItems = [
    { href: '/dashboard', label: 'Dashboard', icon: '📊' },
    { href: '/guidance-requests', label: 'Requests', icon: '📋' },
    { href: '/guidance-requests/create', label: 'Create Request', icon: '➕' },
    { href: '/users', label: 'Users', icon: '👥' },
    { href: '/profile', label: 'Profile', icon: '👤' },
  ];

  return (
    <aside className="hidden lg:block w-64 bg-white border-r border-gray-200 h-screen sticky top-0">
      <div className="p-6">
        <h2 className="text-xl font-bold text-blue-600 mb-8">Menu</h2>
        <nav className="space-y-2">
          {menuItems.map((item) => (
            <Link
              key={item.href}
              href={item.href}
              className={`flex items-center space-x-3 px-4 py-3 rounded-lg transition ${
                isActive(item.href)
                  ? 'bg-blue-100 text-blue-600 font-semibold'
                  : 'text-gray-700 hover:bg-gray-100'
              }`}
            >
              <span className="text-xl">{item.icon}</span>
              <span>{item.label}</span>
            </Link>
          ))}
        </nav>
      </div>
    </aside>
  );
}
