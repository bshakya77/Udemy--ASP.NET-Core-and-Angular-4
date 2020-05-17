import * as _ from 'underscore';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import { VehicleModel, SaveVehicle } from '../models/vehicle';
import { ToastyService } from 'ng2-toasty';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  manufacturers: any[];
  features: any[];
  vehicles: any[];
  vehicle: SaveVehicle = {
    id: 0,
    manufacturerId: 0,
    vehicleId: 0,
    isRegistered: false,
    features:[],
    contact: {
      name: '',
      phone: '',
      email: '',
    }
  };
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastyService: ToastyService) {

    route.params.subscribe(p => {
      this.vehicle.id = +p['id'] || 0;
    });
  }

  ngOnInit() {

    var sources =  [
      this.vehicleService.getManufacturers(),
      this.vehicleService.getFeatures()
    ];

    //this.vehicleService.getManufacturers().subscribe(makes => {
    //  this.manufacturers = makes;
    //  console.log(this.manufacturers);
    // });
     
    //this.vehicleService.getFeatures().subscribe(features => {
    //  this.features = features;
    //  console.log(this.features);
    //});


    if (this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    Observable.forkJoin(sources).subscribe(data => {
      this.manufacturers = data[0];
      this.features = data[1];

      if (this.vehicle.id) {
        this.setVehicle(data[2]);
        this.populateModels();
      }
    }, err => {
      if (err.status == 404)
        this.router.navigate(['/vehicles']);
    });
  }

  private setVehicle(v: VehicleModel) {
    this.vehicle.id = v.id;
    this.vehicle.manufacturerId = v.manufacturer.id;
    this.vehicle.vehicleId = v.vehicle.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, "id");
  }

  onManufacturerChange() {

    this.populateModels();
    delete this.vehicle.vehicleId;
  }

  private populateModels() {
    var selectedManufacturer = this.manufacturers.find(m => m.id == this.vehicle.manufacturerId);
    this.vehicles = selectedManufacturer ? selectedManufacturer.vehicles : [];
  }


  onFeatureToggle(featureId, $event) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);   //splice adds or removes items from array and returns the updated array.
    }
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  submit() {
    debugger;
    //result$ is Observable
    var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
    result$.subscribe(vehicle => {
      this.toastyService.success({
        title: 'Success',
        msg: 'Data was successfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      });
      this.router.navigate(['/vehicles/', vehicle.id])
    });
      
    }
}
