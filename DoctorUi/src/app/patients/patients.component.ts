import { Component, OnInit } from '@angular/core';
import {DoctorhttpService} from "../doctorhttp.service";

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {
  name: any = "";
  ssn: any = "";
  mail: any = "";

  constructor(private service: DoctorhttpService) { }

  patients : any = [];
  async ngOnInit() {
    const patients = await this.service.getPatients()
    this.patients = patients
    console.log(patients)
  }

  checkMeasurement(id:any){
    this.service.getPatientsMeasurement(id);
  }
  addPatient() {
    let dto = {
      ssn: this.ssn,
      mail: this.mail,
      name: this.name,
      Measurement: [] = []
    }
    this.service.addPatient(dto)
  }
  deletePatient(patient: any){
    let dto = {
      ssn: patient.ssn,
      mail: patient.mail,
      name: patient.name,
      Measurement: [] = []
    }
    this.service.deletePatient(dto)
  }
}
