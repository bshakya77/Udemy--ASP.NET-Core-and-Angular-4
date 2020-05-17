import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressbarService {

  constructor() { }

  private uploadProgress: Subject<any>;

  startTracking() {
    this.uploadProgress = new Subject();
    return this.uploadProgress;
  }

  notify(progress) {
    if (this.uploadProgress)
      this.uploadProgress.next(progress);
  }

  endTracking() {
    if (this.uploadProgress)
      this.uploadProgress.complete();
  }
}

@Injectable()

export class BrowserXhrWithProgress extends BrowserXhr {

  constructor(private service: ProgressbarService) {
    super(); //base class constructor
  }

  build(): XMLHttpRequest {
    var xhr: XMLHttpRequest = super.build();

    xhr.upload.onprogress = (event) => {
      this.service.notify(this.createProgress(event))
    };

    xhr.upload.onloadend = () => {
      this.service.endTracking();
    }

    return xhr;
  }
  private createProgress(event) {
    return {
      total: event.total,
      percentage: Math.round(event.loaded / event.total * 100)
    }
  }
}
