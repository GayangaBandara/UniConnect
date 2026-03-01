export const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

export const API_ENDPOINTS = {
  // Auth
  LOGIN: '/auth/login',
  REGISTER: '/auth/register',
  REFRESH_TOKEN: '/auth/refresh-token',
  LOGOUT: '/auth/logout',

  // Users
  GET_USERS: '/users',
  GET_USER: (id: string) => `/users/${id}`,
  CREATE_USER: '/users',
  UPDATE_USER: (id: string) => `/users/${id}`,
  DELETE_USER: (id: string) => `/users/${id}`,

  // Guidance Requests
  GET_REQUESTS: '/guidancerequests',
  GET_REQUEST: (id: string) => `/guidancerequests/${id}`,
  CREATE_REQUEST: '/guidancerequests',
  UPDATE_REQUEST: (id: string) => `/guidancerequests/${id}`,
  DELETE_REQUEST: (id: string) => `/guidancerequests/${id}`,
} as const;
