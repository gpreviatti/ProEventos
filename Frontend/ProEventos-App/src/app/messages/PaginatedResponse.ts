
export interface PaginatedResponse<T> {
  valid: boolean;
  data: T;
  pageNumber: number;
  pageSize: number;
  recordsTotal: number;
  recordsFiltered: number;
  totalPages: number;
}
