
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Controllers;
    (function (Controllers) {
        'use strict';
        var CrudpedidoCtrl = (function (_super) {

            __extends(CrudpedidoCtrl, _super);
            function CrudpedidoCtrl($rootScope, api, CrudpedidoService, lista, $q, $scope) {
                var _this = this;
                _super.call(this);

                this.Pedido = {};
                this.Pedido.id = 1;
                this.Pedido.Produtos = [];
                this.Total = 0;

                this.$rootScope = $rootScope;

                this.api = api;
                this.crudSvc = CrudpedidoService;
                this.lista = lista;
                this.VisualizarProdutos = false;

                this.ConfirmarPedido = function () {
                    _this.crudSvc.ConfirmarPedido(_this.Pedido).then(function (dados) {
                        debugger;
                        _this.Pedido = dados;
                    });
                }

                this.GetTotal = function () {
                    _this.Total = 0;
                    for (var i = 0; i < _this.Pedido.Produtos.length; i++) {
                        _this.Total = _this.Total + (_this.Pedido.Produtos[i].QTD * _this.Pedido.Produtos[i].PRODN3VLRVENDA);
                    }
                }

                this.SetMesa = function (index) {                    
                    _this.VisualizarProdutos = true;
                    _this.Mesa = index;
                }

                this.SetGrupo = function (grupo) {
                    debugger;
                    _this.GrupoSelecionado = grupo.GRUPA60DESCR;
                    _this.Produtos = grupo.Produtos;                    
                }

                this.AddProduto = function (Produto) {
                    debugger;
                    if (!Produto.QTD)
                      Produto.QTD = 0;

                    Produto.QTD++;

                    for (var i = 0; i < _this.Pedido.Produtos.length; i++) {
                        if (_this.Pedido.Produtos[i].id === Produto.id) {
                            _this.Pedido.Produtos[i] = Produto;
                            _this.GetTotal();
                            return;
                        }
                    }

                    _this.Pedido.Produtos.push(Produto)
                    _this.GetTotal();
                }

                this.DelProduto = function (Produto) {

                    for (var i = 0; i < _this.Pedido.Produtos.length; i++) {
                        if (_this.Pedido.Produtos[i].id === Produto.id) {
                            if (_this.Pedido.Produtos[i].QTD > 0)
                                _this.Pedido.Produtos[i].QTD--;

                            if (_this.Pedido.Produtos[i].QTD == 0)
                                _this.Pedido.Produtos.splice(i, 1);

                            _this.GetTotal();
                            return;
                        }
                    }

                }

                this.Cancelar = function () {
                    _this.Pedido = {};
                    _this.Pedido.Produtos = [];

                    if (_this.Produtos)
                    for (var i = 0; i < _this.Produtos.length; i++) {                        
                        _this.Produtos[i].QTD = 0;                                                  
                    }

                    _this.Total = 0;
                }

                this.Resumo = function () {
                    _this.GetTotal();
                    _this.VerResumo = true;
                }

                this.myFilter = function (item) {
                    return item.QTD > 0;
                };

                this.FecharResumo = function () {
                    _this.VerResumo = false;
                }

            }

            CrudpedidoCtrl.prototype.crud = function () {
                return "pedido";
            };

            return CrudpedidoCtrl;
        })(Controllers.CrudBaseEditCtrl);
        Controllers.CrudpedidoCtrl = CrudpedidoCtrl;

        App.modules.Controllers.controller('CrudpedidoCtrl', CrudpedidoCtrl);


    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
//# sourceMappingURL=ctrl.js.map