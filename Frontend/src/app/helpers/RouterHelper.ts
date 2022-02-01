import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class RouterHelper
{
  constructor(private router: Router)
  {

  }

  public navegateTo() : void
  {

  }

  public reloadComponent(route : string) : void
  {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([route]);
  }

}
