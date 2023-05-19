﻿using controle_financeiro_loja.Empregados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using controle_financeiro_loja.SistemaInterno;
using System.Runtime.CompilerServices;

namespace controle_financeiro_loja.Produtos
{
    public class Estoque
    {               
        public List<Produto> itens;

        public Estoque(List<Produto> itens)
        {
            this.itens = itens;
        }       
               
        public Estoque(Empregado empregado)
        {
            if (!(empregado is Gerente) && !(empregado is Dono))
            {
                throw new ArgumentException("Apenas gerentes e donos podem criar estoques");
            }
            itens = new List<Produto>();                        
        }
        public string ObterPropriedadesProduto(int codigoProduto)
        {
            Produto produto = itens.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado no estoque");
            }

            return $"Nome: {produto.Nome}, Preço: R${produto.Preco}, Quantidade: {produto.Quantidade}, Data De Validade: {produto.DataValidade}";
        }
        public void AdicionarItem(Produto produto, Empregado empregado)
        {
            if (!(empregado is Gerente) && !(empregado is Dono))
            {
                throw new ArgumentException("Apenas gerentes e donos podem adicionar itens no estoque");
            }
            if (itens.Contains(produto))
            {
                throw new ArgumentException("Item já se encontra no estoque.");
            }
            itens.Add(produto);            
            Console.WriteLine("Novo item adicionado ao estoque " + produto.Nome);
        }
        public void RegistrarVenda(Empregado empregado, Produto produto, int quantidade) 
        {
            if (!(empregado is Gerente) && !(empregado is Vendedor) && !(empregado is Dono))
            {
                throw new ArgumentException("Apenas gerentes e vendedores podem registrar vendas");
            }

            if (!itens.Contains(produto))
            {
                throw new ArgumentException("Produto não encontrado no estoque");
            }

            produto.Vender(quantidade);
            Console.WriteLine("Vendido " + quantidade + " quantidades do produto " + produto.Nome);
        }
        public void RemoverItem(Produto produto, Empregado empregado)
        {
            if (!(empregado is Gerente) && !(empregado is Dono))
            {
                throw new ArgumentException("Apenas gerentes podem remover os itens do estoque");
            }
            itens.Remove(produto);
            Produto.TotalProdutosDiferentes--;
            
        }
        public void AdicionarQuantidade(Produto produto, int quantidade, Empregado empregado)
        {
            if (!(empregado is Gerente) && !(empregado is Repositor) && !(empregado is Dono))
            {
                throw new ArgumentException("Apenas gerentes e repositores podem alterar a quantidade");
            }
            if (!itens.Contains(produto))
            {
                throw new ArgumentException("Item não encontrado no estoque.");
            }

            produto.Quantidade += quantidade;
            Console.WriteLine("A Quantidade " + quantidade + " do item " + produto.Nome + ", foi adicionada ao estoque");
        }                       
    }
}
