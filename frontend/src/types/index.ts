// User Types
export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: 'Student' | 'Mentor' | 'Admin';
  profileImage?: string;
  createdAt: string;
  updatedAt: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  user: User;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface RefreshTokenRequest {
  refreshToken: string;
}

// Guidance Request Types
export interface GuidanceRequest {
  id: string;
  studentId: string;
  mentorId?: string;
  title: string;
  description: string;
  status: 'Open' | 'Assigned' | 'Completed' | 'Closed';
  createdAt: string;
  updatedAt: string;
}

export interface GuidanceRequestCreateDto {
  title: string;
  description: string;
}

export interface GuidanceRequestUpdateDto {
  title?: string;
  description?: string;
  status?: 'Open' | 'Assigned' | 'Completed' | 'Closed';
  mentorId?: string;
}

// API Response Types
export interface ApiResponse<T> {
  status: number;
  message: string;
  data?: T;
  errors?: Record<string, string[]>;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageSize: number;
  currentPage: number;
}
