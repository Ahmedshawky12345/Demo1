export interface PagedResponse<T> {
    data: {
      data: T[];
      pageSize: number;
      pageNumber: number;
      totalCounts: number;
      totalPages: number;
    };
    success: boolean;
    message: string;
    errors: string[] | null;
  }