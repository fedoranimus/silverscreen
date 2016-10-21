import {bindable, autoinject} from 'aurelia-framework';

@autoinject
export class Navigation {
    @bindable router;

    constructor() {

    }
}