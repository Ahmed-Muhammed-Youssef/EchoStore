import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  sideNavOpened:boolean = false;
  numberOfItems:number = 5;
  searchString:string = "";
  constructor(private changeDetectorRef: ChangeDetectorRef) {
  }
  ngOnInit(): void {
    
  }
  toggle(): void {
    this.sideNavOpened = !(this.sideNavOpened);
    this.changeDetectorRef.detectChanges();
  }
}
