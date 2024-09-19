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
    public class ClienteDAO
    {   

        private MySqlConnection conexao; // Atributo de conexão
        public ClienteDAO()
        {
            this.conexao = new ConnectionFactory().getconnection(); // Instanciando a conexão com o banco de dados
        }

        #region cadastrarCliente
        public void cadastrarCliente(Cliente obj)
        {
            try
            {
                // 1 - Definir o comando SQL - insert into
                string sql = @"insert into tb_clientes (nome, rg, cpf, email, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) 
                values (@nome, @rg, @cpf, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado)";

                // 2 - Organizar o comando SQL

                MySqlCommand executaCmd = new MySqlCommand(sql, conexao);
                executaCmd.Parameters.AddWithValue("@nome", obj.Nome);
                executaCmd.Parameters.AddWithValue("@rg", obj.Rg);
                executaCmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executaCmd.Parameters.AddWithValue("@email", obj.Email);
                executaCmd.Parameters.AddWithValue("@telefone", obj.Telefone);
                executaCmd.Parameters.AddWithValue("@celular", obj.Celular);
                executaCmd.Parameters.AddWithValue("@cep", obj.Cep);
                executaCmd.Parameters.AddWithValue("@endereco", obj.Endereco);
                executaCmd.Parameters.AddWithValue("@numero", obj.Numero);
                executaCmd.Parameters.AddWithValue("@complemento", obj.Complemento);
                executaCmd.Parameters.AddWithValue("@bairro", obj.Bairro);
                executaCmd.Parameters.AddWithValue("@cidade", obj.Cidade);
                executaCmd.Parameters.AddWithValue("@estado", obj.Estado);


                // 3 - Abrir a conexão com o banco de dados e executar o comando SQL
                conexao.Open();
                executaCmd.ExecuteNonQuery();

                MessageBox.Show("Cliente cadastrado com sucesso!");

            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro:" + erro);
                throw;
            }
        }
        #endregion

        #region listarClientes
        // Método listarClientes
        public DataTable listarClientes()
        {
            try
            {
                // 1- Criar o datatable e o comando SQL

                DataTable tabelacliente = new DataTable();
                string sql = @"select * from tb_clientes";

                // 2 - Organizar o comando SQL e executar
                MySqlCommand executaCmd = new MySqlCommand(sql, conexao);

                conexao.Open();
                executaCmd.ExecuteNonQuery();

                // 3 - Criar o MySqlDataAdapter ( adaptador ) e preencher os dados no datatable
                // passando como parâmetro o comando SQL executaCmd

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(executaCmd);
                dataadapter.Fill(tabelacliente); // Preencher o datatable com os dados do banco de dados 

                return tabelacliente;
                
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro:" + erro);
                return null;
            }
        }
        #endregion

        #region alterarCliente
        // Método alterarCliente
        public void alterarCliente(Cliente obj)
        {
            try
            {
                // 1 - Definir o comando SQL - insert into
                string sql = @"update tb_clientes set (nome=@nome, rg=@rg, cpf=@cpf, email=@email, telefone=@telefone, celular=@celular, cep=@cep, endereco=@endereco, numero=@numero, complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado) 
                where id=@id";

                // 2 - Organizar o comando SQL

                MySqlCommand executaCmd = new MySqlCommand(sql, conexao);
                executaCmd.Parameters.AddWithValue("@nome", obj.Nome);
                executaCmd.Parameters.AddWithValue("@rg", obj.Rg);
                executaCmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executaCmd.Parameters.AddWithValue("@email", obj.Email);
                executaCmd.Parameters.AddWithValue("@telefone", obj.Telefone);
                executaCmd.Parameters.AddWithValue("@celular", obj.Celular);
                executaCmd.Parameters.AddWithValue("@cep", obj.Cep);
                executaCmd.Parameters.AddWithValue("@endereco", obj.Endereco);
                executaCmd.Parameters.AddWithValue("@numero", obj.Numero);
                executaCmd.Parameters.AddWithValue("@complemento", obj.Complemento);
                executaCmd.Parameters.AddWithValue("@bairro", obj.Bairro);
                executaCmd.Parameters.AddWithValue("@cidade", obj.Cidade);
                executaCmd.Parameters.AddWithValue("@estado", obj.Estado);
                executaCmd.Parameters.AddWithValue("@id", obj.Codigo);


                // 3 - Abrir a conexão com o banco de dados e executar o comando SQL
                conexao.Open();
                executaCmd.ExecuteNonQuery();

                MessageBox.Show("Cliente alterado com sucesso!");

            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro:" + erro);
                throw;
            }
        }

        #endregion

        #region excluirCliente
        // Método excluirCliente
        public void excluirCliente(Cliente obj)
        {
            try
            {
                // 1 - Definir o comando SQL - insert into
                string sql = @"delete from tb_clientes where id=@id";

                // 2 - Organizar o comando SQL

                MySqlCommand executaCmd = new MySqlCommand(sql, conexao);
                executaCmd.Parameters.AddWithValue("@id", obj.Codigo);


                // 3 - Abrir a conexão com o banco de dados e executar o comando SQL
                conexao.Open();
                executaCmd.ExecuteNonQuery();

                MessageBox.Show("Cliente excluido com sucesso!");

            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro:" + erro);
                throw;
            }
        }

        #endregion

        // Método buscarClientePorCpf
    }
}
