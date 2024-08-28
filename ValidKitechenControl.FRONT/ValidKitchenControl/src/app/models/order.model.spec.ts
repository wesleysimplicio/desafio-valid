import { Order } from './order.model';

describe('Order', () => {
  it('should create an instance', () => {
    expect(new Order(1,'Alface',1,'salada')).toBeTruthy();
  });
});
