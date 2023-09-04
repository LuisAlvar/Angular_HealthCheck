import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HealthCheckService, Result } from './health-check.service';
import { environment } from './../../environments/environment';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.scss']
})

export class HealthCheckComponent implements OnInit {
  public result?: Observable<Result | null>

  constructor(private serivce: HealthCheckService) {
    this.result = this.serivce.result;
  }

  ngOnInit() {
    this.serivce.startConnection();
    this.serivce.addDataListeners();
  }

  onRefresh() {
    this.serivce.sendClientUpdate();
  }

}
