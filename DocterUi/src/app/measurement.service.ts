import { Injectable } from '@angular/core';
import axios from "axios";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  constructor() { }

  updateSeen(dto: { id: string; Diastolic: string; Systolic: string; date: string; seen:boolean }){
   const httpResult = axios.put("http://localhost:5004/measurement/UpdateMeasurement", dto)
  }
}
