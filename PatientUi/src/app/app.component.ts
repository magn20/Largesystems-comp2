import {Component, OnInit} from '@angular/core';
import {IpHttpService} from "./backend communication/ip-http.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'PatientUi';
  isDanish: boolean = true;

  constructor(private ipService: IpHttpService) {

  }

  ngOnInit(): void {
    this.checkCountry()
  }

  private async checkCountry() {
    await this.ipService.getIpInfo().then(res => {
      const code = res.data.countryCode
      this.isDanish = code == "DK"
    })
  }

}
