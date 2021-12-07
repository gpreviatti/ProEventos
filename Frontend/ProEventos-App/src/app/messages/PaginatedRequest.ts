
export interface PaginatedRequest {
  currentPage: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  searchValue?: any;
}
