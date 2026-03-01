'use client';

import { httpClient } from '@/lib/http-client';
import { API_ENDPOINTS } from '@/constants/api';
import { LoginRequest, LoginResponse, RegisterRequest } from '@/types';

export class AuthService {
  static async login(credentials: LoginRequest): Promise<LoginResponse> {
    const response = await httpClient.post<LoginResponse>(
      API_ENDPOINTS.LOGIN,
      credentials
    );

    if (response.accessToken) {
      localStorage.setItem('accessToken', response.accessToken);
      localStorage.setItem('refreshToken', response.refreshToken);
    }

    return response;
  }

  static async register(data: RegisterRequest): Promise<LoginResponse> {
    const response = await httpClient.post<LoginResponse>(
      API_ENDPOINTS.REGISTER,
      data
    );

    if (response.accessToken) {
      localStorage.setItem('accessToken', response.accessToken);
      localStorage.setItem('refreshToken', response.refreshToken);
    }

    return response;
  }

  static async logout(): Promise<void> {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  static getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  static getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  static isAuthenticated(): boolean {
    return !!localStorage.getItem('accessToken');
  }
}
