'use client';

import { ButtonHTMLAttributes } from 'react';

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  isLoading?: boolean;
  variant?: 'primary' | 'secondary' | 'danger';
}

export default function Button({
  isLoading,
  variant = 'primary',
  children,
  disabled,
  ...props
}: ButtonProps) {
  const baseStyles = 'w-full py-2 px-4 rounded-lg font-medium transition duration-200';

  const variantStyles = {
    primary: 'bg-blue-600 text-white hover:bg-blue-700 disabled:bg-blue-400',
    secondary: 'bg-gray-300 text-gray-800 hover:bg-gray-400 disabled:bg-gray-200',
    danger: 'bg-red-600 text-white hover:bg-red-700 disabled:bg-red-400',
  };

  return (
    <button
      {...props}
      disabled={disabled || isLoading}
      className={`${baseStyles} ${variantStyles[variant]} ${
        disabled || isLoading ? 'cursor-not-allowed opacity-50' : ''
      }`}
    >
      {isLoading ? 'Loading...' : children}
    </button>
  );
}
