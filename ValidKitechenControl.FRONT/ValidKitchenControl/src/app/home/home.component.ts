import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Order } from '../models/order.model';
import { ApiService } from '../services/api.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms'; // Importa ReactiveFormsModule

interface OrderItem {
  name: string;
  quantity: number;
  icon: string;
  area: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule, ReactiveFormsModule],
  providers: [BrowserAnimationsModule,],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  items = [
    { name: 'Batata Frita', quantity: 1, icon: '🍟', area: 'fritos' },
    { name: 'Hambúrguer', quantity: 1, icon: '🍔', area: 'grelhados' },
    { name: 'Salada', quantity: 1, icon: '🥗', area: 'saladas' },
    { name: 'Refrigerante', quantity: 1, icon: '🥤', area: 'bebidas' },
    { name: 'Sorvete', quantity: 1, icon: '🍦', area: 'sobremesa' }
  ];

  selectedItems: Order[] = [];
  orders: Order[] = [];
  //orderForm: FormGroup;
  order: OrderItem[] = [];

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
  ) {
  }

  ngOnInit(): void {
    this.loadOrders();
  }

  //seleciona item
  selectItem(item: OrderItem) {
    const existingItem = this.order.find(i => i.name === item.name);
    if (existingItem) {
      existingItem.quantity++;
    } else {
      this.order.push({ ...item, quantity: 1 });
    }
  }

  //aumenta quantidade
  increaseQuantity(item: OrderItem) {
    const existingItem = this.order.find(i => i.name === item.name);
    item.quantity++;
    (existingItem) ? existingItem.quantity = item.quantity++ : '';
  }

  //diminiu quantidade
  decreaseQuantity(item: OrderItem) {
    if (item.quantity > 1) {
      const existingItem = this.order.find(i => i.name === item.name);
      item.quantity--;
      (existingItem) ? existingItem.quantity = item.quantity-- : '';

    }
  }

  loadOrders(): void {
    this.apiService.getOrders().subscribe({
      next: (data: Order[]) => {
        this.orders = data;
      },
      error: (err) => {
        // this.toastr.error('Falha ao carregar pedidos', 'Erro'); // Mostra mensagem de erro
        console.error('Failed to load orders:', err);
      }
    });
  }

  finalizeOrder() {
    
    const orderToSend: Order[] = [];
    this.order.forEach(a=> {
      orderToSend.push({Id: 0, Item: a.name, Quantity: a.quantity, Area: a.area })
    });

    this.apiService.addOrder(orderToSend).subscribe({
      next: (order) => {
        this.order = []; // Limpa o pedido após finalização
        this.loadOrders();
        // this.toastr.success('Pedido adicionado com sucesso!', 'Sucesso'); // Mostra mensagem de sucesso
      },
      error: (err) => {
        // this.toastr.error('Falha ao carregar pedidos', 'Erro'); // Mostra mensagem de erro
        console.error('Failed to load orders:', err);
      }
    });
  }

  getIconByName(name: string): string {
    switch (name) {
      case 'Batata Frita':
        return '🍟';
      case 'Hambúrguer':
        return '🍔';
      case 'Salada':
        return '🥗';
      case 'Refrigerante':
        return '🥤';
      case 'Sorvete':
        return '🍦';
      default:
        return '❓';
    }
  }

}
