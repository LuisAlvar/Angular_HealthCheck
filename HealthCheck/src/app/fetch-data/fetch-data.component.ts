import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss']
})
export class FetchDataComponent implements OnInit {
  public forecasts?: WeatherForecast[];

  constructor(http: HttpClient) {
    http.get<WeatherForecast[]>('/api/weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {

  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
