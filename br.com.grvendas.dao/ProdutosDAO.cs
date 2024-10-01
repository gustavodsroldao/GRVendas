﻿using GRVendas.br.com.grvendas.model;
using GRVendas.br.com.vendas.conexao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRVendas.br.com.grvendas.dao
{
    public class ProdutosDAO
    {
        private MySqlConnection conexao;
        public ProdutosDAO()
        {
            this.conexao = new ConnectionFactory().getconnection(); // Instancia a conexão
        }

        #region Metodo CadastarProduto

        public void CadastrarProduto(Produto obj)
        {
            try
            {
                // 1 - Criar o comando SQL
                string sql = "insert into tb_produtos (descricao, preco, qtd_estoque, for_id) values (@descricao, @preco, @qtd_estoque, @for_id)";

                // 2 - Organizar o comando SQL
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@descricao", obj.Descricao);
                executacmd.Parameters.AddWithValue("@preco", obj.Preco);
                executacmd.Parameters.AddWithValue("@qtd_estoque", obj.QtdEstoque);
                executacmd.Parameters.AddWithValue("@for_id", obj.for_id);

                // 3 - Executa o comando SQL
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar produto: " + erro);
                throw;
            }
        }
        #endregion

        #region Método ListarProdutos
        public DataTable ListarProdutos()
        {
            try
            {
                // 1- Criar o datatable e o comando SQL
                DataTable tabelaProdutos = new DataTable();
                string sql = @"select p.id as 'Código',
                        p.descricao as 'Descrição',
                        p.preco as 'Preço',
	                    p.qtd_estoque as 'Qtd Estoque',
                        f.nome as 'Fornecedor' from tb_produtos as p 
                        JOIN tb_fornecedores as f ON (p.for_id = f.id);";

                // 2 - Organizar o comando SQL e executar
                MySqlCommand executaCmd = new MySqlCommand(sql, conexao);

                conexao.Open();
                executaCmd.ExecuteNonQuery();

                // 3 - Criar o MySqlDataAdapter ( adaptador ) e preencher os dados no datatable
                // passando como parâmetro o comando SQL executaCmd
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(executaCmd);
                dataadapter.Fill(tabelaProdutos); // Preencher o datatable com os dados do banco de dados 

                // 4 - Fechar a conexão
                conexao.Close();

                return tabelaProdutos;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro: " + erro);
                return null;
            }
        }
        #endregion

        #region Método ExcluirProduto
        public void ExcluirProduto(Produto obj)
        {
            try
            {
                // 1 - Criar o comando SQL
                string sql = "delete from tb_produtos where id=@id";

                // 2 - Organizar o comando SQL
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@id", obj.Id);

                // 3 - Executa o comando SQL
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir produto: " + erro);
                throw;
            }
        }
        #endregion

        #region Método AlterarProduto
        public void AlterarProduto(Produto obj)
        {
            try
            {
                // 1 - Criar o comando SQL
                string sql = "update tb_produtos set descricao = @descricao, preco = @preco, qtd_estoque = @qtd_estoque, for_id = @for_id where id = @id";

                // 2 - Organizar o comando SQL
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@descricao", obj.Descricao);
                executacmd.Parameters.AddWithValue("@preco", obj.Preco);
                executacmd.Parameters.AddWithValue("@qtd_estoque", obj.QtdEstoque);
                executacmd.Parameters.AddWithValue("@id", obj.Id);
                executacmd.Parameters.AddWithValue("@for_id", obj.for_id);

                // 3 - Executa o comando SQL
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar produto: " + erro);
                throw;
            }
        }
        #endregion

        #region
    }

}
