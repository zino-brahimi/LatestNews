import { Component, signal, OnInit } from '@angular/core';
import { NewsListComponent } from './news-list.component';

@Component({
  selector: 'app-root',
  imports: [NewsListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'Latest news';
}
