export class Order {
    id: number;
    item: string;
    quantity: number;
    area: string;
  
    constructor(id: number, item: string, quantity: number, area: string) {
      this.id = id;
      this.item = item;
      this.quantity = quantity;
      this.area = area;
    }
  }
  