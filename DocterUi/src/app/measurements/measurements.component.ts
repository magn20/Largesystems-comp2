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

  updateSeen(measurement: any){
    this.measurementService.updateSeen(measurement)
  }
}
