using MySql.Data.MySqlClient;
using projetointegrador.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace projetointegrador
{
    public static class Database
    {
        public static bool EmailExiste(string email)
        {
            const string cs = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";
            using (var cn = new MySqlConnection(cs))
            using (var cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText =
                  "SELECT COUNT(*) FROM cadastro WHERE email = @email";
                cmd.Parameters.AddWithValue("@email", email);
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public static bool SalvarUsuario(UsuarioModel usuario)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            MySqlConnection conexao = new MySqlConnection(stringDeConexao);
            conexao.Open();

            string query = "insert into cadastro (email, nome, senha, pergunta_rec_1, pergunta_rec_2, resposta_rec_1, resposta_rec_2)" +
                           " values(@email, @nome, @senha, @pergunta_rec_1, @pergunta_rec_2, @resposta_rec_1, @resposta_rec_2)";

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@email", usuario.Email);
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@pergunta_rec_1", usuario.Pergunta_Rec_1);
            cmd.Parameters.AddWithValue("@pergunta_rec_2", usuario.Pergunta_Rec_2);
            cmd.Parameters.AddWithValue("@resposta_rec_1", usuario.Resposta_Rec_1);
            cmd.Parameters.AddWithValue("@resposta_rec_2", usuario.Resposta_Rec_2);

            int quantidade = cmd.ExecuteNonQuery();
            conexao.Close();
            return quantidade > 0;
        }

        public static bool SalvarConta(ContaModel conta)
        {
            if (conta.Id > 0)
            {
                return AtualizarConta(conta); // Já existe → faz UPDATE
            }
            else
            {
                return InserirConta(conta); // Nova conta → faz INSERT
            }
        }

        private static bool InserirConta(ContaModel conta)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();

                string query = "INSERT INTO conta (nome_cliente, data_vencimento, nome_conta, valor, tipo, categoria, descricao, situacao, id_cadastro) " +
                               "VALUES (@nome_cliente, @data_vencimento, @nome_conta, @valor, @tipo, @categoria, @descricao, @situacao, @id_cadastro)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome_cliente", conta.NomeCliente);
                    cmd.Parameters.AddWithValue("@data_vencimento", conta.DataVencimento);
                    cmd.Parameters.AddWithValue("@nome_conta", conta.NomeConta);
                    cmd.Parameters.AddWithValue("@valor", conta.ValorConta);
                    cmd.Parameters.AddWithValue("@tipo", (object)conta.TipoConta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@categoria", (object)conta.Categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@descricao", (object)conta.Descricao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@situacao", conta.Situacao);
                    cmd.Parameters.AddWithValue("@id_cadastro", conta.IdCadastro);

                    int quantidade = cmd.ExecuteNonQuery();
                    return quantidade > 0;
                }
            }
        }

        public static bool AtualizarConta(ContaModel conta)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                string query = @"UPDATE conta SET 
              nome_cliente     = @nome_cliente,
              data_vencimento  = @data_vencimento,
              nome_conta       = @nome_conta,
              valor            = @valor,
              tipo             = @tipo,
              categoria        = @categoria,
              descricao        = @descricao,
              situacao         = @situacao
            WHERE id_conta = @id_conta";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome_cliente", conta.NomeCliente);
                    cmd.Parameters.AddWithValue("@data_vencimento", conta.DataVencimento);
                    cmd.Parameters.AddWithValue("@nome_conta", conta.NomeConta);
                    cmd.Parameters.AddWithValue("@valor", conta.ValorConta);
                    cmd.Parameters.AddWithValue("@tipo", (object)conta.TipoConta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@categoria", (object)conta.Categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@descricao", (object)conta.Descricao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@situacao", conta.Situacao);
                    cmd.Parameters.AddWithValue("@id_conta", conta.Id);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }

        public static bool UsuarioExiste(string email, string senha)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();

                string query = "SELECT * FROM cadastro WHERE email = @email AND senha = @senha";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public static int BuscarIdDoUsuario(string email, string senha)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                string query = "SELECT id FROM cadastro WHERE email = @email AND senha = @senha";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32("id");
                    }
                }
            }
            return -1; // retorna -1 se não encontrou
        }

        public static List<ContaModel> BuscarContasNaoPagas(int idCadastro)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            List<ContaModel> contas = new List<ContaModel>();

            using (MySqlConnection conexao = new MySqlConnection(conexaoStr))
            {
                conexao.Open();
                string query = "SELECT * FROM conta WHERE situacao != 'Pago' AND id_cadastro = @id_cadastro";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@id_cadastro", idCadastro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContaModel conta = new ContaModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_conta")),
                                NomeCliente = reader.GetString(reader.GetOrdinal("nome_cliente")),
                                DataVencimento = reader.GetDateTime(reader.GetOrdinal("data_vencimento")),
                                NomeConta = reader.GetString(reader.GetOrdinal("nome_conta")),
                                ValorConta = reader.GetDecimal(reader.GetOrdinal("valor")),
                                TipoConta = reader.GetString(reader.GetOrdinal("tipo")),
                                Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? "" : reader.GetString(reader.GetOrdinal("categoria")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString(reader.GetOrdinal("descricao")),
                                Situacao = reader.GetString(reader.GetOrdinal("situacao")),
                                IdCadastro = reader.GetInt32(reader.GetOrdinal("id_cadastro"))
                            };

                            contas.Add(conta);
                        }
                    }
                }
            }
            return contas;
        }

        public static List<ContaModel> BuscarContasNaoPagasComFiltro(int usuarioId, string filtro)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            List<ContaModel> contas = new List<ContaModel>();
            string query = @"SELECT * FROM conta 
                 WHERE id_cadastro = @usuarioId 
                 AND situacao != 'Pago'
                 AND (id_conta LIKE @filtro
                     OR nome_cliente LIKE @filtro 
                     OR DATE_FORMAT(data_vencimento, '%d/%m/%Y') LIKE @filtro
                     OR nome_conta LIKE @filtro 
                     OR REPLACE(CAST(valor AS CHAR), '.', ',') LIKE @filtro
                     OR tipo LIKE @filtro
                     OR categoria LIKE @filtro 
                     OR descricao LIKE @filtro
                     OR situacao LIKE @filtro
                 )";

            using (MySqlConnection conn = new MySqlConnection(conexaoStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContaModel conta = new ContaModel
                        {
                            Id = reader.GetInt32("id_conta"),
                            NomeCliente = reader.GetString("nome_cliente"),
                            DataVencimento = reader.GetDateTime("data_vencimento"),
                            NomeConta = reader.GetString("nome_conta"),
                            ValorConta = reader.GetDecimal("valor"),
                            TipoConta = reader.GetString("tipo"),
                            Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? "" : reader.GetString("categoria"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString("descricao"),
                            Situacao = reader.GetString("situacao"),
                            IdCadastro = reader.GetInt32("id_cadastro")
                        };
                        contas.Add(conta);
                    }
                }
            }

            return contas;
        }


        public static void AtualizarStatusConta(int idConta, string novoStatus)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(conexaoStr))
            {
                conexao.Open();
                string query = "UPDATE conta SET situacao = @situacao WHERE id_conta = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@situacao", novoStatus);
                    cmd.Parameters.AddWithValue("@id", idConta);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ContaModel> BuscarContasPagas(int idCadastro)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            List<ContaModel> contas = new List<ContaModel>();

            using (MySqlConnection conexao = new MySqlConnection(conexaoStr))
            {
                conexao.Open();
                string query = "SELECT * FROM conta WHERE situacao = 'Pago' AND id_cadastro = @id_cadastro";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@id_cadastro", idCadastro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContaModel conta = new ContaModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_conta")),
                                NomeCliente = reader.GetString(reader.GetOrdinal("nome_cliente")),
                                DataVencimento = reader.GetDateTime(reader.GetOrdinal("data_vencimento")),
                                NomeConta = reader.GetString(reader.GetOrdinal("nome_conta")),
                                ValorConta = reader.GetDecimal(reader.GetOrdinal("valor")),
                                TipoConta = reader.GetString(reader.GetOrdinal("tipo")),
                                Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? "" : reader.GetString(reader.GetOrdinal("categoria")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString(reader.GetOrdinal("descricao")),
                                Situacao = reader.GetString(reader.GetOrdinal("situacao")),
                                IdCadastro = reader.GetInt32(reader.GetOrdinal("id_cadastro"))
                            };

                            contas.Add(conta);
                        }
                    }
                }
            }
            return contas;
        }

        public static List<ContaModel> BuscarHistoricoComFiltro(int usuarioId, string filtro)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            List<ContaModel> historico = new List<ContaModel>();

            string query = @"
        SELECT * FROM conta 
        WHERE id_cadastro = @usuarioId 
        AND situacao = 'Pago'
        AND (
            id_conta LIKE @filtro
            OR nome_cliente LIKE @filtro
            OR DATE_FORMAT(data_vencimento, '%d/%m/%Y') LIKE @filtro
            OR nome_conta LIKE @filtro
            OR REPLACE(CAST(valor AS CHAR), '.', ',') LIKE @filtro
            OR tipo LIKE @filtro
            OR categoria LIKE @filtro
            OR descricao LIKE @filtro
            OR situacao LIKE @filtro
        )";

            using (MySqlConnection conn = new MySqlConnection(conexaoStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContaModel conta = new ContaModel
                        {
                            Id = reader.GetInt32("id_conta"),
                            NomeCliente = reader.GetString("nome_cliente"),
                            DataVencimento = reader.GetDateTime("data_vencimento"),
                            NomeConta = reader.GetString("nome_conta"),
                            ValorConta = reader.GetDecimal("valor"),
                            TipoConta = reader.GetString("tipo"),
                            Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? "" : reader.GetString("categoria"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString("descricao"),
                            Situacao = reader.GetString("situacao"),
                            IdCadastro = reader.GetInt32("id_cadastro")
                        };
                        historico.Add(conta);
                    }
                }
            }

            return historico;
        }

        // Aqui agora buscamos também o ID, já que usamos no modelo completo
        public static UsuarioModel BuscarDadosUsuario(int idUsuario)
        {
            string conexaoStr = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(conexaoStr))
            {
                conexao.Open();
                string query = "SELECT id, nome, email, senha FROM cadastro WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UsuarioModel
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            Senha = reader.GetString("senha")
                        };
                    }
                }
            }
            return null;
        }

        public static bool AtualizarUsuarioDinamico(UsuarioModel usuario)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();

                // Monta a query dinamicamente
                string query = "UPDATE cadastro SET ";
                var parametros = new List<MySqlParameter>();
                bool camposAlterados = false;

                if (!string.IsNullOrEmpty(usuario.Nome))
                {
                    query += "nome = @nome, ";
                    parametros.Add(new MySqlParameter("@nome", usuario.Nome));
                    camposAlterados = true;
                }

                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    query += "email = @email, ";
                    parametros.Add(new MySqlParameter("@email", usuario.Email));
                    camposAlterados = true;
                }

                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    query += "senha = @senha, ";
                    parametros.Add(new MySqlParameter("@senha", usuario.Senha));
                    camposAlterados = true;
                }

                // Se houver campos a atualizar, finalize a query com o filtro de ID
                if (camposAlterados)
                {
                    query = query.TrimEnd(',', ' ') + " WHERE id = @id";
                    // Aqui usamos o ID que está no modelo do usuário armazenado na sessão
                    parametros.Add(new MySqlParameter("@id", Sessao.UsuarioLogado.Id));

                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        foreach (var param in parametros)
                        {
                            cmd.Parameters.Add(param);
                        }
                        int quantidade = cmd.ExecuteNonQuery();
                        return quantidade > 0;
                    }
                }
                else
                {
                    // Nenhum campo foi alterado
                    return false;
                }
            }
        }

        // Método para obter um usuário com base no email informado
        public static UsuarioModel ObterUsuarioPorEmail(string email)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root; database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                string query = "SELECT id, nome, email, senha FROM cadastro WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UsuarioModel
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Email = reader.GetString("email"),
                                Senha = reader.GetString("senha")
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Método para verificar os dados de recuperação de senha sem impor ordem fixa para as perguntas
        public static bool VerificarDadosRecuperacao(string email,
                                                     string perguntaInput1, string respostaInput1,
                                                     string perguntaInput2, string respostaInput2)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                // Seleciona os dados cadastrados: os pares de perguntas e respostas
                string query = @"SELECT pergunta_rec_1, pergunta_rec_2, resposta_rec_1, resposta_rec_2 
                         FROM cadastro 
                         WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtem os pares cadastrados no banco
                            string dbPerguntaRec1 = reader.GetString("pergunta_rec_1");
                            string dbPerguntaRec2 = reader.GetString("pergunta_rec_2");
                            string dbRespostaRec1 = reader.GetString("resposta_rec_1");
                            string dbRespostaRec2 = reader.GetString("resposta_rec_2");

                            // Verifica se um dos pares informados confere com o par cadastrado 1
                            bool par1Ok =
                                (dbPerguntaRec1.Equals(perguntaInput1, StringComparison.OrdinalIgnoreCase) &&
                                 dbRespostaRec1.Equals(respostaInput1, StringComparison.OrdinalIgnoreCase)) ||
                                (dbPerguntaRec1.Equals(perguntaInput2, StringComparison.OrdinalIgnoreCase) &&
                                 dbRespostaRec1.Equals(respostaInput2, StringComparison.OrdinalIgnoreCase));

                            // Verifica se um dos pares informados confere com o par cadastrado 2
                            bool par2Ok =
                                (dbPerguntaRec2.Equals(perguntaInput1, StringComparison.OrdinalIgnoreCase) &&
                                 dbRespostaRec2.Equals(respostaInput1, StringComparison.OrdinalIgnoreCase)) ||
                                (dbPerguntaRec2.Equals(perguntaInput2, StringComparison.OrdinalIgnoreCase) &&
                                 dbRespostaRec2.Equals(respostaInput2, StringComparison.OrdinalIgnoreCase));

                            // Se ambos os pares cadastrados forem encontrados dentre os informados, os dados são válidos
                            return par1Ok && par2Ok;
                        }
                    }
                }
            }
            return false; // ou seja, email não encontrado ou os dados não conferem
        }

        public static bool AtualizarSenha(string email, string novaSenha)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                string query = "UPDATE cadastro SET senha = @senha WHERE email = @email";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@senha", novaSenha);
                    cmd.Parameters.AddWithValue("@email", email);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }

        public static bool AtualizarSituacaoConta(string idConta, string novaSituacao)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";
            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                conexao.Open();
                string query = "UPDATE conta SET situacao = @situacao WHERE id_conta = @id_conta";

                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@situacao", novaSituacao);
                    cmd.Parameters.AddWithValue("@id_conta", idConta);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }

        public static bool ApagarConta(int idConta)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            using (MySqlConnection conexao = new MySqlConnection(stringDeConexao))
            {
                try
                {
                    conexao.Open();
                    string query = "DELETE FROM conta WHERE id_conta = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idConta);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar a conta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static List<string> ObterAnosDisponiveis()
        {
            var anos = new List<string>();
            string query = "SELECT DISTINCT YEAR(data_vencimento) AS ano FROM conta ORDER BY ano DESC";
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";
            try
            {
                using (var conn = new MySqlConnection(stringDeConexao))
                using (var cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            anos.Add(reader.GetInt32("ano").ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter anos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return anos;
        }

        public static Dictionary<string, decimal> ObterTotaisPorCategoriaMensal(int ano, int mes, string tipo, int idUsuario, string nomeCliente)
        {
            Dictionary<string, decimal> resultados = new Dictionary<string, decimal>();

            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            // Se o nomeCliente for "Todos os clientes", não filtra por cliente.
            string filtroCliente = nomeCliente == "Todos os clientes" ? "" : "AND nome_cliente = @nomeCliente";

            string query = $@"
    SELECT 
      COALESCE(categoria, 'Nenhum') AS categoria, 
      SUM(valor) AS total 
    FROM conta 
    WHERE YEAR(data_vencimento) = @ano 
      AND MONTH(data_vencimento) = @mes 
      AND tipo = @tipo 
      AND id_cadastro = @idUsuario 
      {filtroCliente}
    GROUP BY COALESCE(categoria, 'Nenhum')
    ORDER BY total DESC;
";

            using (MySqlConnection conn = new MySqlConnection(stringDeConexao))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ano", ano);
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                // Se o filtro de cliente não for para "Todos", adiciona o nome do cliente na consulta.
                if (nomeCliente != "Todos os clientes")
                {
                    cmd.Parameters.AddWithValue("@nomeCliente", nomeCliente);
                }

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string categoria = reader.GetString("categoria");
                        decimal total = reader.GetDecimal("total");
                        resultados[categoria] = total;
                    }
                }
            }

            return resultados;
        }

        public static Dictionary<string, decimal> ObterTotaisPorCategoriaAnual(int ano, string tipo, int idUsuario, string nomeCliente)
        {
            Dictionary<string, decimal> resultados = new Dictionary<string, decimal>();

            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            // Se o nomeCliente for "Todos os clientes", não filtra por cliente.
            string filtroCliente = nomeCliente == "Todos os clientes" ? "" : "AND nome_cliente = @nomeCliente";

            string query = @"SELECT 
                     COALESCE(categoria, 'Nenhum') AS categoria, 
                     SUM(valor) AS total 
                 FROM conta 
                 WHERE YEAR(data_vencimento) = @ano 
                   AND tipo = @tipo 
                   AND id_cadastro = @idUsuario "   
               + filtroCliente
               + @" GROUP BY COALESCE(categoria, 'Nenhum') 
                   ORDER BY total DESC;";

            using (MySqlConnection conn = new MySqlConnection(stringDeConexao))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ano", ano);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                // Se o filtro de cliente não for para "Todos", adiciona o nome do cliente na consulta.
                if (nomeCliente != "Todos os clientes")
                {
                    cmd.Parameters.AddWithValue("@nomeCliente", nomeCliente);
                }

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string categoria = reader.GetString("categoria");
                        decimal total = reader.GetDecimal("total");
                        resultados[categoria] = total;
                    }
                }
            }

            return resultados;
        }


        public static Dictionary<int, decimal> ObterSomaPorPeriodo(int ano, int mes, string tipo, bool isAnual, int idUsuario, string nomeCliente = null) // Adicionando o parâmetro opcional nomeCliente
        {
            Dictionary<int, decimal> resultados = new Dictionary<int, decimal>();

            string agrupamento = isAnual ? "MONTH" : "DAY";
            string filtroMes = isAnual ? "" : "AND MONTH(data_vencimento) = @mes";
            string filtroCliente = string.IsNullOrEmpty(nomeCliente) || nomeCliente == "Todos os clientes"
                                   ? ""
                                   : "AND nome_cliente = @cliente"; // Filtro para o nome_cliente, se fornecido

            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            // Modificando a query para incluir o filtro de nome_cliente, se fornecido
            string query = $@"SELECT {agrupamento}(data_vencimento) AS periodo, SUM(valor) AS total FROM conta WHERE YEAR(data_vencimento) = @ano
          {filtroMes} AND tipo = @tipo AND id_cadastro = @idUsuario {filtroCliente} GROUP BY periodo ORDER BY periodo;";

            using (MySqlConnection conn = new MySqlConnection(stringDeConexao))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ano", ano);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                if (!isAnual)
                    cmd.Parameters.AddWithValue("@mes", mes);

                // Só adiciona o parâmetro do cliente se ele não for "Todos os clientes"
                if (!string.IsNullOrEmpty(nomeCliente) && nomeCliente != "Todos os clientes")
                    cmd.Parameters.AddWithValue("@cliente", nomeCliente);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int periodo = reader.GetInt32("periodo");
                        decimal total = reader.GetDecimal("total");
                        resultados[periodo] = total;
                    }
                }
            }

            return resultados;
        }

        public static List<string> ObterClientesDoUsuario(int idUsuario)
        {
            List<string> clientes = new List<string>();

            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            string query = @"SELECT DISTINCT nome_cliente FROM conta WHERE id_cadastro = @idUsuario ORDER BY nome_cliente;";

            using (MySqlConnection conn = new MySqlConnection(stringDeConexao))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nomeCliente = reader.GetString("nome_cliente");
                        clientes.Add(nomeCliente);
                    }
                }
            }

            return clientes;
        }

        // Retorna lista de anos disponíveis na tabela conta
        public static List<int> GetAvailableYears()
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            const string sql = @"
                SELECT DISTINCT YEAR(data_vencimento) AS ano
                  FROM conta
                 ORDER BY ano DESC";

            var anos = new List<int>();
            using (var conn = new MySqlConnection(stringDeConexao))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        anos.Add(reader.GetInt32("ano"));
                }
            }
            return anos;
        }

        public static Dictionary<int, (decimal receita, decimal despesa)> GetMonthlyTotals(int ano, int idCadastro)
        {
            string stringDeConexao = "Server=localhost;Port=3306;User Id=root;database=sistemasomare;";

            const string sql = @"
        SELECT MONTH(data_vencimento) AS mes, tipo, SUM(valor) AS total_valor
          FROM conta
         WHERE YEAR(data_vencimento) = @ano
           AND id_cadastro = @idCadastro
      GROUP BY MONTH(data_vencimento), tipo
      ORDER BY MONTH(data_vencimento)";

            var resultados = new Dictionary<int, (decimal, decimal)>();
            for (int m = 1; m <= 12; m++)
                resultados[m] = (0m, 0m);

            using (var conn = new MySqlConnection(stringDeConexao))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ano", ano);
                cmd.Parameters.AddWithValue("@idCadastro", idCadastro);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int mes = reader.GetInt32("mes");
                        string tipo = reader.GetString("tipo").ToLower();
                        decimal valor = reader.GetDecimal("total_valor");

                        if (tipo == "receita")
                            resultados[mes] = (resultados[mes].Item1 + valor, resultados[mes].Item2);
                        else
                            resultados[mes] = (resultados[mes].Item1, resultados[mes].Item2 + valor);
                    }
                }
            }
            return resultados;
        }
    }
}
