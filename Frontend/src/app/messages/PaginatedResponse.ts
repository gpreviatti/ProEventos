
export interface PaginatedResponse<T> {
  valid: boolean;
  data: T;
  currentPage: number;
  pageSize: number;
  recordsTotal: number;
  recordsFiltered: number;
  totalPages: number;
}
