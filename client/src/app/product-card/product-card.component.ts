import { Component, Input, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IProduct } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: IProduct | null = null;
  isInCart:boolean = false;
  constructor(private productService:ProductService, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if(this.product)
      this.isInCart = this.productService.IsInCart(this.product);
  }
  AddToCart(){
    if(this.product)
      this.productService.AddProductToCart(this.product);
    this.isInCart = true;
    this.OpenSnackBar("The item is added to your cart successfully.");
  }
  RemoveFromCart(){
    if(this.product)
      this.productService.RemoveFromCart(this.product);
    this.isInCart = false;
    this.OpenSnackBar("The item is removed from your cart successfully.");
  }
  OpenSnackBar(message: string) {
    this._snackBar.open(message, "Ok", {
      duration: 2000,
    });
  }
}
