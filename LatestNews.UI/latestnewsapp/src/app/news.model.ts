export interface NewsItem {
    title: string;
    description: string;
    url: string;
    source: string;
    publishedAt: string;
    urlToImage: string;
}

export interface PaginatedNews {
    page: number;
    pageSize: number;
    totalCount: number;
    hasNextPage: boolean;
    hasPerviousPage: boolean;
    items: NewsItem[];
}