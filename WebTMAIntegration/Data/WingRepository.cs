using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Data
{
    public class WingRepository : IWingRepository
    {
        private readonly SqlClientHelper _db;

        public WingRepository(SqlClientHelper db)
        {
            _db = db;
        }
        public async Task<int> SaveWingsAsync(List<WingEntity> wings)
        {
            if (wings == null || !wings.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var wing in wings)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@WingName", SqlDbType.NVarChar, 250)
                        {
                            Value = string.IsNullOrWhiteSpace(wing.WingName)
                                ? "Test Wing"
                                : wing.WingName
                        },

                        new SqlParameter("@CreatedBy", SqlDbType.Int)
                        {
                            Value = wing.CreatedBy ?? 1
                        },

                        new SqlParameter("@floorId", SqlDbType.Int)
                        {
                            Value = wing.FloorId ?? 1
                        },

                        new SqlParameter("@WingId", SqlDbType.Int)
                        {
                            Value = wing.WingId == 0
                                ? (object)DBNull.Value
                                : wing.WingId
                        },

                        new SqlParameter("@WingType", SqlDbType.Int)
                        {
                            Value = wing.WingType ?? 1
                        },

                        new SqlParameter("@Code", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(wing.Code)
                                ? "TEST-CODE"
                                : wing.Code
                        },

                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = wing.IsActive ?? false
                        },

                        new SqlParameter("@IsSyncRequired", SqlDbType.Bit)
                        {
                            Value = wing.IsSyncRequired ?? false
                        },

                        new SqlParameter("@newId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "Insert_Wing",
                        commandType: CommandType.StoredProcedure,
                        parameters: parameters,
                        connection: conn,
                        transaction: transaction
                    );

                    int insertedId = Convert.ToInt32(
                        parameters.First(p => p.ParameterName == "@newId").Value
                    );

                    totalRowsAffected += rows;
                }

                await transaction.CommitAsync();

                return totalRowsAffected;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
