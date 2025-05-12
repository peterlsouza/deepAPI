using Dapper;
using PetData.Interfaces;
using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetData.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public ServicoRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Servico?> GetById(int id)
        {
            using var connection = _dbConnection.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Servico>(
                "SELECT * FROM Servicos WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Servico>> GetAll()
        {
            using var connection = _dbConnection.GetConnection();
            return await connection.QueryAsync<Servico>("SELECT * FROM Servicos");
        }

        public async Task<int> Create(Servico servico)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            INSERT INTO Servicos (Nome, Descricao, Preco, DuracaoMinutos, DataCadastro)
            VALUES (@Nome, @Descricao, @Preco, @DuracaoMinutos, @DataCadastro);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, servico);
        }

        public async Task Update(Servico servico)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            UPDATE Servicos
            SET Nome = @Nome, 
                Descricao = @Descricao,
                Preco = @Preco,
                DuracaoMinutos = @DuracaoMinutos
            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, servico);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection.GetConnection();
            await connection.ExecuteAsync(
                "DELETE FROM Servicos WHERE Id = @Id",
                new { Id = id });
        }
    }
}
