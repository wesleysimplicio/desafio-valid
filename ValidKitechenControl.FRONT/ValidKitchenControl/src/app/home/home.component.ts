import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Order } from '../models/order.model';
import { ApiService } from '../services/api.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms'; // Importa ReactiveFormsModule


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule, ReactiveFormsModule],
  providers: [BrowserAnimationsModule,],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  orders: Order[] = [];
  orderForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
  ) {
    this.orderForm = this.fb.group({
      item: ['', Validators.required],
      quantity: ['', [Validators.required, Validators.min(1)]],
      area: ['', Validators.required]
    });

  }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.apiService.getOrders().subscribe({
      next: (data: Order[]) => {
        console.log(data);
        this.orders = data;
      },
      error: (err) => {
        // this.toastr.error('Falha ao carregar pedidos', 'Erro'); // Mostra mensagem de erro
        console.error('Failed to load orders:', err);
      }
    });
  }

  addOrder(): void {
    const newOrder = this.orderForm.value;
    this.apiService.addOrder(newOrder).subscribe({
      next: (order) => {
        console.log('Order added:', order);
        this.orders.push(order); // Adiciona o novo pedido à lista
        // this.toastr.success('Pedido adicionado com sucesso!', 'Sucesso'); // Mostra mensagem de sucesso
      },
      error: (err) => {
        // this.toastr.error('Falha ao carregar pedidos', 'Erro'); // Mostra mensagem de erro
        console.error('Failed to load orders:', err);
      }
    });
  }

}
