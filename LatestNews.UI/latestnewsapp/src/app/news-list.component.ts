import { Component, OnInit } from '@angular/core';
import { NewsService } from './news.service';
import { NewsItem, PaginatedNews } from './news.model';
import { DatePipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-news-list',
    templateUrl: './news-list.component.html',
    styleUrls: ['./news-list.component.css'],
    imports: [CommonModule, HttpClientModule, DatePipe, FormsModule],
})
export class NewsListComponent implements OnInit {
    newsItems: NewsItem[] = [];
    page: number = 1;
    pageSize: number = 10;
    hasNextPage: boolean = false;
    hasPerviousPage: boolean = false;
    searchSource: string = '';

    constructor(private newsService: NewsService) { }

    ngOnInit(): void {
        this.fetchNews();
    }

    fetchNews() {
        this.newsService.getNews(this.page, this.pageSize).subscribe((data: PaginatedNews) => {
            this.newsItems = data.items;
            this.hasNextPage = data.hasNextPage;
            this.hasPerviousPage = data.hasPerviousPage;
        });
    }

    nextPage() {
        if (this.hasNextPage) {
            this.page++;
            this.fetchNews();
        }
    }

    previousPage() {
        if (this.hasPerviousPage) {
            this.page--;
            this.fetchNews();
        }
    }

    getNewsBySource(): void {
        if (!this.searchSource.trim()) {
            this.fetchNews();
            return;
        }

        this.newsService.getNewsBySource(this.searchSource, this.page, this.pageSize).subscribe((data) => {
            this.newsItems = data.items || [];
        });
    }
}