import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient) { }
  cart : IProduct[] = [];
  getProducts() : Observable<IProduct[]>{
    return this.http.get<IProduct[]>('https://localhost:5001/api/products');
  }
  AddProductToCart(product: IProduct) : void {
    if(!this.IsInCart(product)) {
      this.cart.push(product);
    }
  }
  RemoveFromCart(product: IProduct) : void {
    this.cart = this.cart.filter((p:IProduct) => p.id != product.id);
  }
  IsInCart(product: IProduct): boolean{
    if(this.cart.find((p:IProduct) => p.id == product.id)) {
      return true;
    }
    return false;
  }
}
