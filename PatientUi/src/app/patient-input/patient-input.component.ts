import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PatientHttpService} from "../backend communication/patient-http.service";

@Component({
  selector: 'app-patient-input',
  templateUrl: './patient-input.component.html',
  styleUrls: ['./patient-input.component.scss']
})
export class PatientInputComponent implements OnInit {
  form: FormGroup = this.formBuilder.group({
    systolic: ["", Validators.required],
    diastolic: ["", Validators.required],
    patientSsn: ["", Validators.required]
  });


  constructor(private formBuilder: FormBuilder,
              private patientService: PatientHttpService) {

  }

  ngOnInit(): void {
  }

  createMeasurement() {
    if (this.form.valid) {
      const measurement = {
        Systolic: this.form.controls["systolic"].value,
        Diastolic: this.form.controls["diastolic"].value,
        Date: new Date()
      }
      const ssn = this.form.controls["patientSsn"].value;
      this.patientService.createMeasurement(measurement, ssn)
    } else {
      console.log("form not valid")
      console.log(this.form.errors)
    }
  }
}
