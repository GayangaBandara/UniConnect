'use client';

import { httpClient } from '@/lib/http-client';
import { API_ENDPOINTS } from '@/constants/api';
import { GuidanceRequest, GuidanceRequestCreateDto, GuidanceRequestUpdateDto } from '@/types';

export class GuidanceRequestService {
  static async getRequests(): Promise<GuidanceRequest[]> {
    return httpClient.get<GuidanceRequest[]>(API_ENDPOINTS.GET_REQUESTS);
  }

  static async getRequest(id: string): Promise<GuidanceRequest> {
    return httpClient.get<GuidanceRequest>(API_ENDPOINTS.GET_REQUEST(id));
  }

  static async createRequest(data: GuidanceRequestCreateDto): Promise<GuidanceRequest> {
    return httpClient.post<GuidanceRequest>(API_ENDPOINTS.CREATE_REQUEST, data);
  }

  static async updateRequest(
    id: string,
    data: GuidanceRequestUpdateDto
  ): Promise<GuidanceRequest> {
    return httpClient.put<GuidanceRequest>(API_ENDPOINTS.UPDATE_REQUEST(id), data);
  }

  static async deleteRequest(id: string): Promise<void> {
    await httpClient.delete(API_ENDPOINTS.DELETE_REQUEST(id));
  }
}
