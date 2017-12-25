
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
            function CrudpedidoCtrl($rootScope, api, CrudpedidoService, lista, $q, $scope, SweetAlert) {
                var _this = this;
                _super.call(this);

                this.SweetAlert = SweetAlert;
                                
                this.IniciarPedido = function () {
                    this.Pedido = {};
                    this.Pedido.id = -1;
                    this.Pedido.Produtos = [];
                    this.Pedido.Mesa = 0;
                    this.Pedido.Total = 0;
                    this.Pedido.CodUsr = $rootScope.currentUser.CODIGOSISUSUARIO;
                    this.Pedido.OBS = "";
                }

                this.IniciarPedido();

                this.$rootScope = $rootScope;

                this.api = api;
                this.crudSvc = CrudpedidoService;
                this.lista = lista;
                this.VisualizarProdutos = false;
                this.VisualizarGrupo = false;
                
                this.ConfirmarPedido = function () {
                    _this.crudSvc.ConfirmarPedido(_this.Pedido).then(function (dados) {
                        debugger;
                        _this.Pedido.id = dados.id;
                    });
                }

                this.GetTotal = function () {
                    _this.Pedido.Total = 0;
                    for (var i = 0; i < _this.Pedido.Produtos.length; i++) {
                        _this.Pedido.Total = _this.Pedido.Total + (_this.Pedido.Produtos[i].QTD * _this.Pedido.Produtos[i].PRODN3VLRVENDA);
                    }
                }

                this.SetMesa = function (index) {
                    
                    _this.SweetAlert.swal({
                        title: "Buscar Ultimo Pedido da Mesa?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Sim",
                        cancelButtonText: "Nao"
                    }, function (isConfirm) {
                        _this.VisualizarProdutos = false;
                        _this.VisualizarGrupo = true;

                        if (isConfirm) {
                            _this.crudSvc.PedidoMesa(index).then(function (dados) {
                                
                                if (dados) {                           
                                    _this.Pedido.id = dados.id;
                                    _this.Pedido.CodUsr = dados.CodUsr;
                                    _this.Pedido.Total = dados.Total;
                                    _this.Pedido.OBS = dados.OBS;
                                    _this.VerResumo = true;
                                                                                                            
                                    for (var i = 0; i < dados.Produtos.length; i++)
                                    {
                                        for (var iG = 0; iG < _this.$rootScope.currentUser.Grupos.length; iG++)
                                        {
                                            if (_this.$rootScope.currentUser.Grupos[iG].id == dados.Produtos[i].GRUPICOD) {
                                                _this.SetGrupo(_this.$rootScope.currentUser.Grupos[iG]);

                                                for (var iP = 0; iP < _this.Produtos.length; iP++)
                                                {
                                                    if (_this.Produtos[iP].id == dados.Produtos[i].PRODICOD) {
                                                        _this.Produtos[iP].QTD = dados.Produtos[i].PVITN3QTD - 1;
                                                        _this.AddProduto(_this.Produtos[iP]);
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        }

                                    }
                                }
                            });
                        }                        
                                                
                        _this.Pedido.Mesa = index;
                    });
                                        
                }

                this.SetGrupo = function (grupo) {
                    debugger;
                    _this.GrupoSelecionado = grupo.GRUPA60DESCR;
                    _this.Produtos = grupo.Produtos;
                    _this.VisualizarProdutos = true;
                    _this.VisualizarGrupo = false;

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
                    this.IniciarPedido();

                    if (_this.Produtos)
                    for (var i = 0; i < _this.Produtos.length; i++) {                        
                        _this.Produtos[i].QTD = 0;                                                  
                    }

                    
                }

                this.Resumo = function () {
                    _this.GetTotal();
                    _this.VerResumo = !_this.VerResumo;
                }

                this.myFilter = function (item) {
                    return item.QTD > 0;
                };

                this.AbrirGrupo = function () {
                    _this.VisualizarProdutos = false;
                    _this.VisualizarGrupo = true;
                }
                                
            }

            CrudpedidoCtrl.prototype.crud = function () {
                return "pedidoes";
            };

            return CrudpedidoCtrl;
        })(Controllers.CrudBaseEditCtrl);
        Controllers.CrudpedidoCtrl = CrudpedidoCtrl;

        App.modules.Controllers.controller('CrudpedidoCtrl', CrudpedidoCtrl);


    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
//# sourceMappingURL=ctrl.js.map