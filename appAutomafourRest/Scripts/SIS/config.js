var App;
(function (App) {
    'use strict';

    App.modules.App.constant('luarApp',
    {        
        BASEURL: document.baseURI,
        WEBAPI: document.baseURI,
        ITENS_POR_PAGINA: '15',
        CaminhoPDF: 'http://localhost/PDFNfse/',
        apiDelphi: 'http://localhost:1234/api/'
        //ISDEBUG: true
    });

})(App || (App = {}));
//# sourceMappingURL=app.js.map