import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedNews } from './news.model';

@Injectable({
    providedIn: 'root',
})
export class NewsService {
    private apiUrl = 'http://localhost:5005/api/news'; // Replace with your actual API endpoint

    constructor(private http: HttpClient) { }

    getNews(page: number = 1, pageSize: number = 10): Observable<PaginatedNews> {
        return this.http.get<PaginatedNews>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
    }

    getNewsBySource(source: string, page: number = 1, pageSize: number = 10): Observable<any> {
        const url = `${this.apiUrl}/by-source?source=${source}&page=${page}&pageSize=${pageSize}`;
        return this.http.get<any>(url);
    }
}