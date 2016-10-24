var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define('app',["require", "exports", 'aurelia-framework'], function (require, exports, aurelia_framework_1) {
    "use strict";
    var App = (function () {
        function App() {
        }
        App.prototype.configureRouter = function (config, router) {
            config.title = "Silverscreen";
            config.map([
                { route: '', name: 'root', moduleId: 'features/library/library', nav: true, title: 'Library' },
                { route: 'wishlist', name: 'wishlist', moduleId: 'features/wishlist/wishlist', nav: true, title: 'Wishlist' },
                { route: 'settings', name: 'settings', moduleId: 'features/settings/settings', nav: true, title: 'Settings' },
                { route: 'system', name: 'system', moduleId: 'features/system/system', nav: true, title: 'System' }
            ]);
            this.router = router;
        };
        App = __decorate([
            aurelia_framework_1.autoinject, 
            __metadata('design:paramtypes', [])
        ], App);
        return App;
    }());
    exports.App = App;
});

define('environment',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        debug: true,
        testing: true
    };
});

define('main',["require", "exports", './environment'], function (require, exports, environment_1) {
    "use strict";
    Promise.config({
        warnings: {
            wForgottenReturn: false
        }
    });
    function configure(aurelia) {
        aurelia.use
            .standardConfiguration()
            .feature('resources')
            .plugin('aurelia-api', function (config) {
            config.registerEndpoint('api', '/api');
            config.registerEndpoint('library', '/api/library');
        });
        if (environment_1.default.debug) {
            aurelia.use.developmentLogging();
        }
        if (environment_1.default.testing) {
            aurelia.use.plugin('aurelia-testing');
        }
        aurelia.start().then(function () { return aurelia.setRoot(); });
    }
    exports.configure = configure;
});

define('infrastructure/IMovie',["require", "exports"], function (require, exports) {
    "use strict";
});

define('resources/index',["require", "exports"], function (require, exports) {
    "use strict";
    function configure(config) {
    }
    exports.configure = configure;
});

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define('services/libraryService',["require", "exports", 'aurelia-framework', 'aurelia-api'], function (require, exports, aurelia_framework_1, aurelia_api_1) {
    "use strict";
    var LibraryService = (function () {
        function LibraryService(endpoint) {
            this.endpoint = endpoint;
        }
        LibraryService.prototype.getAllMovies = function () {
            return this.endpoint.find('movies');
        };
        LibraryService.prototype.getMovie = function (id) {
            return this.endpoint.findOne('movies', id);
        };
        LibraryService.prototype.deleteMovie = function (id) {
            return this.endpoint.destroyOne('movies', id);
        };
        LibraryService = __decorate([
            aurelia_framework_1.inject(aurelia_api_1.Endpoint.of('library')), 
            __metadata('design:paramtypes', [aurelia_api_1.Rest])
        ], LibraryService);
        return LibraryService;
    }());
    exports.LibraryService = LibraryService;
});

define('services/metadataProviderService',["require", "exports"], function (require, exports) {
    "use strict";
    var MetadataProviderService = (function () {
        function MetadataProviderService(serviceName, serviceURL) {
            this.serviceName = serviceName;
            this.serviceURL = serviceURL;
        }
        return MetadataProviderService;
    }());
    exports.MetadataProviderService = MetadataProviderService;
});

var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
define('services/omdbProviderService',["require", "exports", './metadataProviderService'], function (require, exports, metadataProviderService_1) {
    "use strict";
    var OMDBProviderService = (function (_super) {
        __extends(OMDBProviderService, _super);
        function OMDBProviderService() {
            _super.call(this, "OMDB", "http://www.omdbapi.com");
        }
        OMDBProviderService.prototype.findMovie = function (name, year) {
            return null;
        };
        return OMDBProviderService;
    }(metadataProviderService_1.MetadataProviderService));
    exports.OMDBProviderService = OMDBProviderService;
});

define('features/library/library',["require", "exports"], function (require, exports) {
    "use strict";
    var Library = (function () {
        function Library(libraryService) {
            this.libraryService = libraryService;
            this.movies = [];
        }
        Library.prototype.attached = function () {
        };
        return Library;
    }());
    exports.Library = Library;
});

define('features/wishlist/wishlist',["require", "exports"], function (require, exports) {
    "use strict";
    var Wishlist = (function () {
        function Wishlist() {
        }
        return Wishlist;
    }());
    exports.Wishlist = Wishlist;
});

define('features/settings/settings',["require", "exports"], function (require, exports) {
    "use strict";
    var Settings = (function () {
        function Settings() {
        }
        return Settings;
    }());
    exports.Settings = Settings;
});

define('features/system/system',["require", "exports"], function (require, exports) {
    "use strict";
    var System = (function () {
        function System() {
        }
        return System;
    }());
    exports.System = System;
});

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define('resources/elements/nav/navigation',["require", "exports", 'aurelia-framework'], function (require, exports, aurelia_framework_1) {
    "use strict";
    var Navigation = (function () {
        function Navigation() {
        }
        __decorate([
            aurelia_framework_1.bindable, 
            __metadata('design:type', Object)
        ], Navigation.prototype, "router", void 0);
        Navigation = __decorate([
            aurelia_framework_1.autoinject, 
            __metadata('design:paramtypes', [])
        ], Navigation);
        return Navigation;
    }());
    exports.Navigation = Navigation;
});

define('text!app.html', ['module'], function(module) { module.exports = "<template>\n  <require from=\"resources/elements/nav/navigation\"></require>\n  <require from=\"styles/app.css\"></require>\n\n  <div class=\"sidebar\">\n    <div class=\"logo\"></div>\n    <navigation router.bind=\"router\"></navigation>\n  </div>\n  \n  <div class=\"page-host\">\n    <router-view></router-view>\n  </div>\n</template>\n"; });
define('text!features/library/library.html', ['module'], function(module) { module.exports = "<template>\r\n    Library\r\n    <ul>\r\n        <li repeat.for=\"movie of movies\">\r\n            ${movie.title}\r\n        </li>\r\n    </ul>\r\n</template>"; });
define('text!styles/app.css', ['module'], function(module) { module.exports = "html, body {\n  height: 100%;\n  width: 100%;\n  margin: 0; }\n\n.sidebar {\n  float: left;\n  width: 10%;\n  min-width: 85px;\n  height: 100%;\n  background-color: #323232; }\n\n.logo {\n  background-image: url(http://placehold.it/50);\n  height: 50px;\n  width: 50px;\n  margin: 10px auto; }\n\n.page-host {\n  float: left; }\n"; });
define('text!styles/colors.css', ['module'], function(module) { module.exports = ""; });
define('text!features/settings/settings.html', ['module'], function(module) { module.exports = "<template>\r\n    Settings\r\n</template>"; });
define('text!features/wishlist/wishlist.html', ['module'], function(module) { module.exports = "<template>\r\n    Wishlist\r\n</template>"; });
define('text!resources/elements/nav/navigation.css', ['module'], function(module) { module.exports = ".nav {\n  list-style-type: none;\n  margin: 0;\n  padding: 0;\n  width: 100%;\n  height: calc(100% - 70px); }\n\n.nav__item:hover {\n  background-color: #9b9590;\n  color: #bc5a58; }\n\n.nav__item--active {\n  background-color: #9b9590;\n  color: #bc5a58; }\n\n.nav__icon {\n  background-image: url(http://placehold.it/50);\n  height: 50px;\n  width: 50px;\n  margin: 0 auto 5px auto; }\n\n.nav__link {\n  display: block;\n  color: #ffffff;\n  padding: 16px 16px;\n  text-decoration: none;\n  text-align: center; }\n"; });
define('text!features/system/system.html', ['module'], function(module) { module.exports = "<template>\r\n    System\r\n</template>"; });
define('text!resources/elements/nav/navigation.html', ['module'], function(module) { module.exports = "<template bindable=\"router\">\r\n    <require from=\"./navigation.css\"></require>\r\n    <ul class=\"nav\">\r\n        <li repeat.for=\"row of router.navigation\" class=\"nav__item ${row.isActive ? 'nav__item--active' : ''}\">\r\n            \r\n            <a class=\"nav__link\" href.bind=\"row.href\">\r\n                <div class=\"nav__icon\"></div>\r\n                ${row.title}\r\n            </a>\r\n        </li>\r\n    </ul>\r\n</template>"; });
//# sourceMappingURL=app-bundle.js.map