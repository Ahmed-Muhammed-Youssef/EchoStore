import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Echo';
  products: IProduct[] = [];
  constructor(private http: HttpClient){}
  ngOnInit(): void {
    // this.http.get<IProduct[]>('https://localhost:5001/api/products').subscribe(
    //   (response:IProduct[]) => {
    //     if(response)
    //       this.products = response;
    //     console.log(this.products);
    //   }, 
    //   (e:any) =>
    //   {
    //     console.error(e);
    // });
  }
}
