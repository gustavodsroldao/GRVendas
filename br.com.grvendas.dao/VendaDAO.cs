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
    public class VendaDAO
    {
        private MySqlConnection conexao;

        public VendaDAO()
        {
            this.conexao = new ConnectionFactory().getconnection();
        }

        #region Método CadastrarVenda

        public void CadastrarVenda(Venda obj)
        {
            try
            {
                // 1 - Criar o comando SQL
                string sql = @"insert into tb_vendas (cliente_id, data_venda, total_venda, observacoes) 
                                values (@cliente_id, @data_venda, @total_venda, @obs)";

                // 2 - Organizar e executar CMD
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@cliente_id", obj.Cliente_id);
                executacmd.Parameters.AddWithValue("@data_venda", obj.Data_venda);
                executacmd.Parameters.AddWithValue("@total_venda", obj.Total_venda);
                executacmd.Parameters.AddWithValue("@obs", obj.Obs);

                // 3 - Abrir conexão e executar comando
                conexao.Open();
                executacmd.ExecuteNonQuery();

                //MessageBox.Show("Venda cadastrada com sucesso!");
                conexao.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro: " + erro);
                throw;
            }
        }

        #endregion

        #region Método RetornaIdUltimaVenda

        public int RetornaIdUltimaVenda()
        {
            try
            {
                int idvenda = 0;

                // 1 - Criar o comando SQL
                // -> Função chamada MAX() que retorna o maior valor ID existente no banco de dados
                // -> Ou seja será sempre a última venda cadastrada
                string sql = @"select max(id) id from tb_vendas"; 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);

                // 2 - Abrir conexão e executar comando
                conexao.Open();

                MySqlDataReader rs = executacmd.ExecuteReader();

                if(rs.Read())
                {
                    idvenda = rs.GetInt32("id"); // Pega o valor do campo ID


                }
                conexao.Close();
                return idvenda;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro: " + erro);
                conexao.Close();
                return 0;
            }
        }
        #endregion

        #region Método ListarVendasPorPeriodo
        public DataTable ListarVendasPorPeriodo(DateTime datainicio, DateTime datafim)
        {
            try
            {
                // 1 - Passo Criar o DataTable e o comando SQL
                DataTable tabelaHistorico = new DataTable();

                string sql = @"select v.id  as  'Código',
                             v.data_venda   as  'Data Venda',
                             c.nome         as  'Cliente',
                             v.total_venda  as  'Total', 
                             v.observacoes  as  'Observações'
                             from tb_vendas as v join tb_clientes as c on (v.cliente_id = c.id)
                             where v.data_venda between @datainicio and @datafim";

                // 2 - Passo organizar e executar o comando sql
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@datainicio", datainicio);
                executacmd.Parameters.AddWithValue("@datafim", datafim);

                // 3 - Abrir a conexao
                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 4 - Criar o MySqlDataAdapter para preenchimento dos dados no data grid view
                MySqlDataAdapter da = new MySqlDataAdapter(executacmd);
                da.Fill(tabelaHistorico);

                return tabelaHistorico;



            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao listar vendas: " + erro);
                return null;
            }
        }
        #endregion

        #region Método ListarVendas
        public DataTable ListarVendas()
        {
            try
            {
                // 1 Passo - Criar o comando sql e o datatable
                DataTable tabelaHistorico = new DataTable();
                string sql = @"select v.id  as  'Código',
                             v.data_venda   as  'Data Venda',
                             c.nome         as  'Cliente',
                             v.total_venda  as  'Total', 
                             v.observacoes  as  'Observações'
                             from tb_vendas as v join tb_clientes as c on (v.cliente_id = c.id)";

                // 2 Passo - Organizar e abrir o comando sql
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);

                // 3 Passo - Abrir conexão
                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 4 Passo - Criar o MySqlDataAdapter para preenchimento dos dados no data grid view
                MySqlDataAdapter da = new MySqlDataAdapter(executacmd);
                da.Fill(tabelaHistorico);

                // 5 Passo - Fechar Conexão
                conexao.Close();

                return tabelaHistorico;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro ao listar as vendas:" + erro);
                throw;
            }
        }
        #endregion

    }
}
