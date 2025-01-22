
export interface PaginationInfo {
    currentPage: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
}

interface OrdersPaginationProps {
    initialOrdersData: {
        orders: any[];
        pagination: {
            currentPage: number;
            totalPages: number;
        };
    };
}

interface VinylPaginationProps {
    initialVinylsData: {
        vinyls: any[];
        pagination: {
            currentPage: number;
            totalPages: number;
        };
    };
}
