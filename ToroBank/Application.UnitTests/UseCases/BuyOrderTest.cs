using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;

namespace Application.UnitTests.UseCases
{
    public class BuyOrderTest
    {
        private List<Asset> assets;
        private List<NegotiatedAsset> mostNegotiatedAssets;

        [SetUp]
        public void Setup()
        {
            assets = new List<Asset>()
            {
                new Asset("PETR4",28.44M),
                new Asset("MGLU3",28.44M),
                new Asset("VVAR3",25.91M),
                new Asset("SANB11",25.91M),
                new Asset("TORO4",115.98M),
            };

            List<NegotiatedAsset> negotiatedAssets = new List<NegotiatedAsset>();
            Random rng = new Random();
            
            for (int i = 0; i < 100; i++)
            {
                Random random = new Random(0);
                int index = rng.Next(assets.Count);
                int idx = rng.Next(100);
                negotiatedAssets.Add(new NegotiatedAsset()
                {
                    Asset = assets[index],
                    Total = idx
                });
            }

            var teste = negotiatedAssets.ToList();
            var group = teste.GroupBy(a => a.Asset.Name);
            
            mostNegotiatedAssets = new List<NegotiatedAsset>();
            foreach (var item in group)
            {
                mostNegotiatedAssets.Add(new NegotiatedAsset() { Asset = new Asset(item.Key, 0), Total = item.Count() });
            }
        }

        [Test]
        public void Should_have_a_list_of_5_assets()
        {
            Assert.IsTrue(mostNegotiatedAssets.Count() == 5);
        }

        public void OrdemDeCompra()
        {
            /*No exemplo acima o usuário deseja comprar 3 ações SANB11. Neste caso, a API deve chegar o valor de SANB11 naquele momento (no exemplo, R$40.77), verificar se o usuário tem 
             * pelo menos R$122.31 disponível em conta corrente e, em caso afirmativo, realizar a compra (debitar o saldo e registrar as novas quantidades de ativos SANB11 ao cliente). 
             * Caso não tenha saldo suficiente, ou o ativo informado seja invalido, a API deve retornar uma codido e uma mensagem de erro indicando "saldo insuficiente" ou "ativo invalido". 
             * Esta operação deve impactar o saldo e a lista de ativos do usuário.*/

            var user = new User(123456, "João", "123123456452", 123.24M);
            var userAssets = new List<UserAsset>();

            //1. Carregar uma lista com 5 itens mais negociados.
            var mostNegotiated = mostNegotiatedAssets.Take(5);
            //2. Selecionar um item: SANB11
            var sanb = mostNegotiated.Where(a => a.Asset.Name == "SANB11").FirstOrDefault();
            //3. Calcular o preço da ação vezes a quantidade e guardar o preço
            int quantity = 3;
            var price = sanb.Asset.Value;
            var subtotal = quantity * price;
            //4. Verificar saldo disponível na conta do usuario
            var balance = user.Balance;
            //5. Se o saldo for menor que R$122.31 - NÃO REALIZAR COMPRA
            if(balance < subtotal)
            {
                //nao realiza compra
            }
            else
            {
                balance -= subtotal;
                user.Balance = balance;

                userAssets.Add(new UserAsset(4, 0));


            }
            //5.1. retornar mensagem 'saldo insuficiente' para o usuario
            //6. Se o saldo for maior que R$122.31 - REALIZAR COMPRA
            //6.1. Debitar o valor do saldo do cliente.
            //6.2. Registrar quantidades de ativos SANB11 ao cliente.
            //6.2.2. Se nao houver ativos, criar e adicionar valor.
        }

        public void Should_have_an_valid_user()
        {
            Assert.Fail();
        }

        public void Should_pass_if_asset_value_is_not_eq_0()
        {
            Assert.Fail();
        }

        public void Should_pass_if_users_balance_is_gt_order_value()
        {
            Assert.Fail();
        }

        public void Should_not_pass_if_users_balance_is_insuficient()
        {
            Assert.Fail();
        }

        public void Should_have_error_if_balance_of_client_is_lt_0()
        {
            Assert.Fail();
        }



        public class NegotiatedAsset
        {
            private int v;

            public NegotiatedAsset()
            {
            }

            public NegotiatedAsset(Asset asset, int v)
            {
                Asset = asset;
                this.v = v;
            }

            public Asset Asset { get; set; }
            public int Total { get; set; }
        }


    }
}
