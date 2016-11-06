import {Router, RouterConfiguration} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';

@autoinject
export class App {
  router: Router;

  constructor() {

  }

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "Silverscreen";

    config.map([
      { route: ['wishlist', ''], name: 'wishlist', moduleId: 'features/wishlist/wishlist', nav: true, title: 'Wishlist' },
      { route: 'library', name: 'library', moduleId: 'features/library/library', nav: true, title: 'Library' },
      { route: 'settings', name: 'settings', moduleId: 'features/settings/settings', nav: true, title: 'Settings' },
      { route: 'system', name: 'system', moduleId: 'features/system/system', nav: true, title: 'System' }
    ]);
    
    this.router = router;
  }


}
