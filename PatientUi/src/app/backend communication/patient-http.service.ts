import {Injectable} from '@angular/core';
import axios from "axios";

@Injectable({
  providedIn: 'root'
})
export class PatientHttpService {

  constructor() {
  }

  async createMeasurement(measurement: any) {
    const patientId = 1; //we dont have have real patients with ids
    return await axios.post("http://localhost:5005/AddMeasurement/"+patientId, measurement)
  }
}
