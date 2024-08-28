export class Order {
    Id: number;
    Item: string;
    Quantity: number;
    Area: string;
  
    constructor(Id: number, Item: string, Quantity: number, Area: string) {
      this.Id = Id;
      this.Item = Item;
      this.Quantity = Quantity;
      this.Area = Area;
    }
  }
  