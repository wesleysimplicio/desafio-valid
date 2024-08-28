import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Order } from '../models/order.model';
import { ApiService } from '../services/api.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  orders: Order[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getOrders().subscribe((data: any[]) => {
      console.log(data);
      this.orders = data;
    });
  }
}
