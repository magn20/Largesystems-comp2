import {Injectable} from '@angular/core';
import axios from 'axios'

@Injectable({
  providedIn: 'root'
})
export class IpHttpService {

  constructor() {
  }


  async getIp(): Promise<string> {
    const res = await axios.get("http://api.ipify.org/?format=json")
    return res.data.ip;
  }

  async getIpInfo(): Promise<any> {
    const ip = await this.getIp();
    return await axios.get("http://ip-api.com/json/" + ip)
  }
}
