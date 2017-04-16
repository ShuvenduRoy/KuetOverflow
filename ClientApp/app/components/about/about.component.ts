import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'about',
    templateUrl: './about.component.html'
})
export class AboutComponent {
    public aboutData: any;

    constructor(http: Http) {
        http.get('/api/home/about').subscribe(result => {
            this.aboutData = result.json();
            console.log(this.aboutData);
        });
    }
}
