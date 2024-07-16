import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'AuthMobile.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'AuthMobile.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
