import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  sideNavOpened:boolean = false;
  searchString:string = "";
  constructor(private changeDetectorRef: ChangeDetectorRef, public productService:ProductService) {
  }
  ngOnInit(): void {
    
  }
  toggle(): void {
    this.sideNavOpened = !(this.sideNavOpened);
    this.changeDetectorRef.detectChanges();
  }
}
