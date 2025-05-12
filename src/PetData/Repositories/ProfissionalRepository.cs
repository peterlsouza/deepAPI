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
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public ProfissionalRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<Profissional?> GetById(int id)
        {
            using var connection = _dbConnection.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Profissional>(
                "SELECT * FROM Profissionais WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            using var connection = _dbConnection.GetConnection();
            return await connection.QueryAsync<Profissional>("SELECT * FROM Profissionais");
        }

        public async Task<int> Create(Profissional profissional)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            INSERT INTO Profissionais (Nome, Especialidade, Telefone, Email, DataCadastro)
            VALUES (@Nome, @Especialidade, @Telefone, @Email, @DataCadastro);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, profissional);
        }

        public async Task Update(Profissional profissional)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            UPDATE Profissionais
            SET Nome = @Nome, Especialidade = @Especialidade,
                Telefone = @Telefone, Email = @Email
            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, profissional);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection.GetConnection();
            await connection.ExecuteAsync("DELETE FROM Profissionais WHERE Id = @Id", new { Id = id });
        }
    }
}
