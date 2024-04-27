import {Injectable} from '@angular/core';
import axios from "axios";

@Injectable({
  providedIn: 'root'
})
export class PatientHttpService {

  constructor() {
  }

  async createMeasurement(measurement: any, ssn: any) {
    return await axios.post("http://localhost:5004/Measurement/AddMeasurement/" + ssn, measurement)
  }
}
