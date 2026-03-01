'use client';

import ProtectedRoute from '@/components/ProtectedRoute';
import InputField from '@/components/InputField';
import Button from '@/components/Button';
import { useState } from 'react';

export default function CreateRequestPage() {
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    subject: '',
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    // TODO: Implement API call
    setTimeout(() => {
      setIsLoading(false);
      alert('Request created! (Feature coming soon)');
    }, 1000);
  };

  return (
    <ProtectedRoute>
      <div className="container mx-auto px-4 py-8">
        <div className="max-w-2xl">
          <h1 className="text-3xl font-bold text-gray-800 mb-8">Create Guidance Request</h1>

          <div className="bg-white rounded-lg shadow p-8">
            <form onSubmit={handleSubmit} className="space-y-6">
              <InputField
                label="Title"
                type="text"
                placeholder="What do you need help with?"
                name="title"
                value={formData.title}
                onChange={handleChange}
                required
              />

              <InputField
                label="Subject"
                type="text"
                placeholder="e.g., Mathematics, Physics"
                name="subject"
                value={formData.subject}
                onChange={handleChange}
                required
              />

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Description
                </label>
                <textarea
                  name="description"
                  placeholder="Provide more details about your request..."
                  value={formData.description}
                  onChange={handleChange}
                  rows={6}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                  required
                />
              </div>

              <div className="flex gap-4">
                <Button type="submit" isLoading={isLoading}>
                  Create Request
                </Button>
                <Button type="button" variant="secondary" onClick={() => window.history.back()}>
                  Cancel
                </Button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </ProtectedRoute>
  );
}
