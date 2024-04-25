import { Component, OnInit } from '@angular/core';
import {DoctorhttpService} from "../doctorhttp.service";
import {MeasurementService} from "../measurement.service";

@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.scss']
})
export class MeasurementsComponent implements OnInit {

  constructor(public service: DoctorhttpService, private measurementService: MeasurementService) { }

  ngOnInit(): void {
  }

  updateSeen(measurement: { id: string; diastolic: string; systolic: string; date: string; seen:boolean }){
    let dto = {
      id: measurement.id,
      systolic: measurement.systolic,
      diastolic: measurement.diastolic,
      date: measurement.date,
      seen: !measurement.seen,
    }
    this.measurementService.updateSeen(dto)
  }
}
