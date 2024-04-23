import { Injectable } from '@angular/core';
import axios from "axios";
@Injectable({
  providedIn: 'root'
})
export class DoctorhttpService {


  measurements: any = []
  constructor() {

  }

  async deletePatient(dto: { ssn: string; mail: string; name: string; Measurement: [] }) {
    console.log(dto)
    const httpResult =  axios.delete("http://localhost:5005/patient/deletepatient", {
      headers: {
        'Content-Type': 'application/json'
      },
      data: JSON.stringify(dto) // Convert object to JSON string
    });
  }
  addPatient(dto : {ssn: string; mail: string; name:string;Measurement:[];}){
    const httpResult = axios.post("http://localhost:5005/patient/addpatient", dto)
  }
  async getPatients(){
    const httpResult = await axios.get("http://localhost:5005/patient/GetAllPatient");
    return httpResult.data
  }

  async  getPatientsMeasurement(id: any){
    const httpResult = await axios.get("http://localhost:5005/patient/GetPatient/" + id);
    this.measurements = httpResult.data.measurement
    console.log(httpResult.data)
    return httpResult.data
  }

}
