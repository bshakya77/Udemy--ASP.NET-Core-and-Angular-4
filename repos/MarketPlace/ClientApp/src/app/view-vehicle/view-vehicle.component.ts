import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastyService } from 'ng2-toasty';
import { VehicleService } from '../services/vehicle.service';
import { PhotoService} from '../services/photo-service.service';
//import { ProgressbarService, BrowserXhrWithProgress } from '../services/progressbar.service';
import { NgZone } from '@angular/core';
import { BrowserXhr } from '@angular/common/http/src/xhr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html'
  //providers: [
  //  { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
  //  ProgressbarService
  //]
})
export class ViewVehicleComponent implements OnInit {

  @ViewChild("fileInput") fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos: any[];
  progress: any[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toasty: ToastyService,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    //private progressbarService: ProgressbarService,
    private zone: NgZone,
    private auth: AuthService
  ) {

    this.route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0)  {
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {
    console.log(this.vehicleId);

    this.photoService.getPhotos(this.vehicleId)
      .subscribe(photos => this.photos = photos);

    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status === 404) {
            this.router.navigate(['/vehicles']);
            return;
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  //uploadPhoto() {

  //  this.progressbarService.startTracking()
  //    .subscribe(progress => {
  //      console.log(progress);
  //      this.zone.run(() => { this.progress = progress; })
  //    }, null, () => { this.progress = null });

  //  var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
  //  var file = nativeElement.files[0];
  //  nativeElement.value = '';

  //  this.photoService.upload(this.vehicleId, file)
  //    .subscribe(photo => {
  //      this.photos.push(photo),
  //        err => {
  //          this.toasty.error({
  //            title: "Error",
  //            msg: err.text(),
  //            theme: "bootstrap",
  //            showClose: true,
  //            timeout: 5000
  //          });
  //        }
  //    })
  //}

}