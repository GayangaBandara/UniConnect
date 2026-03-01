'use client';

import { httpClient } from '@/lib/http-client';
import { API_ENDPOINTS } from '@/constants/api';
import { User } from '@/types';

export class UserService {
  static async getUsers(): Promise<User[]> {
    return httpClient.get<User[]>(API_ENDPOINTS.GET_USERS);
  }

  static async getUser(id: string): Promise<User> {
    return httpClient.get<User>(API_ENDPOINTS.GET_USER(id));
  }

  static async updateUser(id: string, data: Partial<User>): Promise<User> {
    return httpClient.put<User>(API_ENDPOINTS.UPDATE_USER(id), data);
  }

  static async deleteUser(id: string): Promise<void> {
    await httpClient.delete(API_ENDPOINTS.DELETE_USER(id));
  }
}
