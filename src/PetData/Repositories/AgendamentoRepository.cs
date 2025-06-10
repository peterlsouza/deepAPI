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
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public AgendamentoRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Agendamento?> GetById(int id)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            SELECT a.*, p.*, s.*
            FROM Agendamentos a
            INNER JOIN Profissionais p ON a.ProfissionalId = p.Id
            INNER JOIN Servicos s ON a.ServicoId = s.Id
            WHERE a.Id = @Id";

            return (await connection.QueryAsync<Agendamento, Profissional, Servico, Agendamento>(
                sql,
                (agendamento, profissional, servico) =>
                {
                    agendamento.Profissional = profissional;
                    agendamento.Servico = servico;
                    return agendamento;
                },
                new { Id = id },
                splitOn: "Id,Id")).FirstOrDefault();
        }
        public async Task<int> Create(Agendamento agendamento)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
            INSERT INTO Agendamentos 
                (ProfissionalId, ServicoId, DataHora, Observacoes, Status, DataCriacao)
            VALUES 
                (@ProfissionalId, @ServicoId, @DataHora, @Observacoes, @Status, @DataCriacao);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, agendamento);
        }

        public async Task<IEnumerable<Agendamento>> GetByProfissional(int profissionalId, DateTime data)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
        SELECT * FROM Agendamentos 
        WHERE ProfissionalId = @ProfissionalId 
        AND CONVERT(DATE, DataHora) = CONVERT(DATE, @Data)
        AND Status != 'Cancelado'";

            return await connection.QueryAsync<Agendamento>(sql, new
            {
                ProfissionalId = profissionalId,
                Data = data
            });
        }

        public async Task Update(Agendamento agendamento)
        {
            using var connection = _dbConnection.GetConnection();
            var sql = @"
        UPDATE Agendamentos 
        SET ProfissionalId = @ProfissionalId,
            ServicoId = @ServicoId,
            DataHora = @DataHora,
            Observacoes = @Observacoes,
            Status = @Status
        WHERE Id = @Id";

            await connection.ExecuteAsync(sql, agendamento);
        }

        public async Task Cancel(int id)
        {
            using var connection = _dbConnection.GetConnection();
            await connection.ExecuteAsync(
                "UPDATE Agendamentos SET Status = 'Cancelado' WHERE Id = @Id",
                new { Id = id });
        }

        public Task<IEnumerable<Agendamento>> GetAll()
        {
            throw new NotImplementedException();
        }

        // Implementar outros métodos conforme interface
    }
}