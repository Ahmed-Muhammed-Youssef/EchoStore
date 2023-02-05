import { Component, OnInit } from '@angular/core';
import { IProduct } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private productService: ProductService) { }
  products: IProduct[] = [];
  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (response:IProduct[]) => {
            if(response)
              this.products = (response);
      },
      e => console.log(e)
    );
  }

}
