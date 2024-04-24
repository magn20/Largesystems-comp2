import { Injectable } from '@angular/core';
import axios from "axios";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  constructor() { }

  updateSeen(dto: { id: string; diastolic: string; systolic: string; date: string; seen:boolean }){
    console.log(dto)
   const httpResult = axios.put("http://localhost:5004/measurement/UpdateMeasurement", dto)
  }
}
